# How to create class libraries with multiple target frameworks

1. Create a new class library

    Use the dotnet cli command `dotnet new classlib -f netstandard2.0`. Note that we explicitely asked to create a library that targets <i>.NET Standard 2.0</i> but it could have been anything else.

2. Edit the <i>.csproj</i> file

    Find:

    `<TargetFramework>netstandard2.0</TargetFramework>`

    And replace it with:

    `<TargetFrameworks>net7.0;net48</TargetFrameworks>`

    Your library will now be compiled to <i>.NET 7</i> and <i>.NET Framework 4.8</i>. Here again, you could choose to target other frameworks, or more of them. You can get a complete list of framework identifiers to use in your <i>.csproj</i> here: https://learn.microsoft.com/en-us/dotnet/standard/frameworks#supported-target-frameworks

3. Ensure that your library is compatible with the frameworks

    If you need to provide different implementations based on the target framework, you can use compiler directives. For example:

    ```
    public void Foo()
    {
    #if NET7_0
        // Implementation for .NET 7.0
    #elif NET48
        // Implementation for .NET Framework 4.8
    #else
    #error Unsupported target framework
    #endif
    }
    ```
    You can find a list of compiler directives that can be used based on your target frameworks here: https://learn.microsoft.com/en-us/dotnet/core/tutorials/libraries#preprocessor-symbols

    It is possible that you need to reference different NuGet packages (or any other kind of dependency) depending on the target framework. For instance, if you are targeting <i>.NET Standard 2.0</i> and <i>.NET 7.0</i> and need to use types from the `System.Reflection.Emit` namespaces, you would need to import a NuGet package when targeting <i>.NET Standard 2.0</i> because it is not directly supported by <i>.NET Standard 2.0</i>. To do this, use a condition in your <i>.csproj</i>:

    ```
    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <TargetFrameworks>net7.0;netstandard2.0</TargetFrameworks>
      </PropertyGroup>

      <ItemGroup Condition="'$(TargetFramework)' == 'netstandard2.0'">
        <PackageReference Include="System.Reflection.Emit" Version="4.7.0" />
      </ItemGroup>

    </Project>
    ```
