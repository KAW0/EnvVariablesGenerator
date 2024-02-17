using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace EnvVariablesGenerator;

[Generator]
public class EnvClassGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var envFile = context.AdditionalFiles.FirstOrDefault(f => f.Path.EndsWith(".env"));
        if(envFile==null)
            return;
        var text = envFile.GetText(context.CancellationToken);
        if (text == null)
            return;
        var classBuilder = new StringBuilder();
        classBuilder.AppendLine("using System;");
        classBuilder.AppendLine("using System.IO;");
        classBuilder.AppendLine("public static class EnvVariables");
        classBuilder.AppendLine("{");
        var lines = text.Lines;
        foreach (var line in lines)
        {
            var lineText = line.ToString();
            if (lineText.Contains('='))
            {
                var parts = lineText.Split('=');
                var propertyName = parts[0].Trim();
                var propertyNamePascalCase = ToPascalCase(parts[0].Trim());

                classBuilder.AppendLine($"    public static string {propertyNamePascalCase} => Environment.GetEnvironmentVariable(\"{propertyName}\");");
            }
        }
        classBuilder.Append($$"""
                              
                                  public static void Load(string filePath)
                                  {
                                      if (!System.IO.File.Exists(filePath))
                                           return;
                                      foreach (var line in System.IO.File.ReadAllLines(filePath))
                                      {
                                          var parts = line.Split(
                                              '=',
                                              StringSplitOptions.RemoveEmptyEntries);
                              
                                          if (parts.Length != 2)
                                              continue;
                                          if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(parts[0])))
                                              Environment.SetEnvironmentVariable(parts[0], parts[1]);
                                      }
                                  }

                              """);
        classBuilder.AppendLine("\tstatic EnvVariables()");
        classBuilder.AppendLine("\t{");
        classBuilder.Append("\t\tLoad(");
        classBuilder.Append("\""+Path.GetFileName(envFile.Path)+"\"");
        classBuilder.AppendLine(@");");
        classBuilder.AppendLine("\t}");
        classBuilder.AppendLine("}");
        var sourceCode = classBuilder.ToString();
        context.AddSource("EnvVariablesGenerated.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
    }
    private static string ToPascalCase(string text)
    {
        return string.Concat(text.Split('_').Where(part => part.Length > 0).Select(part => char.ToUpper(part[0]) + part.Substring(1).ToLower()));
    }
}