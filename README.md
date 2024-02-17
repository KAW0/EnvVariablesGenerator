# EnvVariablesGenerator

`EnvVariablesGenerator is a source generator that generates a special class called EnvVariables based on an *.env file. 
While there are other libraries that provide similar functionality, they often hardcode variables from the *.env file, making them less flexible.

However, in this library, the .env **file is copied to the built project**, allowing for changes to be made to the variables from there.

Additionally, you can opt to exclude this file from the built project and allow your application to load these variables from the actual environment instead.

## Instalation
To use add those lines in your .csproj file:
```xml
<ItemGroup>
   <PackageReference Include=" EnvVariablesGenerator" Version="1.0.0"  PrivateAssets="all" />
</ItemGroup>
<ItemGroup>
    <AdditionalFiles Include="var.env">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </AdditionalFiles>
</ItemGroup>
```
## Example var.env file
```
DB_HOST=localhost
DB_PORT=5432
```
Unfortunately, we cannot use a file named .env because MSBuild goes crazy when trying to add a file with just an extension as an additional file.
Aside from that, you can use whatever name you prefer, but only one file can be utilized.## Example usage

```cs
 Console.WriteLine(EnvVariables.Host);
 Console.WriteLine(EnvVariables.Port);
```
## License
This library is licensed under the MIT License.