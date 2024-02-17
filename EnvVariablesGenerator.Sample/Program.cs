using System;

namespace EnvVariablesGenerator.Sample;

public class Program
{
    public static void Main(string[] arg)
    {
        Console.WriteLine(EnvVariables.Host);
        Console.WriteLine(EnvVariables.Port);
        Console.WriteLine(EnvVariables.DbHost);
        Console.WriteLine(EnvVariables.DbPort);
        Console.WriteLine(EnvVariables.DbUser);
        Console.WriteLine(EnvVariables.DbPass);
        Console.WriteLine(EnvVariables.DbName);
        Console.WriteLine(EnvVariables.ApiKey);
        Console.WriteLine(EnvVariables.ApiSecret);
        Console.WriteLine(EnvVariables.NodeEnv);
        Console.WriteLine(EnvVariables.LogLevel);
        Console.WriteLine(EnvVariables.EmailSender);
    }
 }