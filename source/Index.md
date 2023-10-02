<style>
red { color: Red }
green { color: Green }
blue { color: Blue }
yellow { color: Yellow }
gray { color: Gray }
</style>

# May Roslyn be with you
https://github.com/Flash0ver/F0-Talks-Roslyn

## Stefan Pölz
* Clean C# Coder
* Test-driven .NET developer
* [FlashOWare.net](http://flashoware.net)
* Senior Software Developer @ [Trayport](https://www.trayport.com)

#### Community
<a href="https://www.credly.com/users/flashover"><img src="https://images.credly.com/size/340x340/images/5c687ffb-7ab6-4fd5-bf8c-14f0178acd21/image.png" alt="Microsoft MVP (Developer Technologies)" width="128" height="128" style="padding: 0px 64px 0px 0px;"></a>
<a href="https://www.jetbrains.com/lp/jetbrains-community-contributor/"><img src="https://www.jetbrains.com/lp/jetbrains-community-contributor/static/preview_img-81301bdd3f94fe3515542956b09a385e.svg" alt="JetBrains Community Contributor (.NET)" width="128" height="128" style="padding: 0px 64px 0px 0px;"></a>
<a href="https://dotnetdevs.at/"><img src="https://pbs.twimg.com/profile_images/1096389781305602048/6IpwI2Mh_400x400.png" alt="Co-organizer of DotNetDevs.at" width="128" height="128" style="padding: 0px 64px 0px 0px;"></a>

#### Channels
* Twitch: [FlashOWare](https://www.twitch.tv/flashoware)
* YouTube: [FlashOWare](https://www.youtube.com/@FlashOWare)

#### Social
* X (Twitter): [@0x_F0](https://twitter.com/0x_F0)
* Mastodon: [@0x_F0@dotnet.social](https://dotnet.social/@0x_F0)
* LinkedIn: [Stefan Pölz](https://www.linkedin.com/in/flashover/)
* GitHub: [Flash0ver](https://github.com/Flash0ver)

<br />
<br />
<hr />
<br />
<br />

### Table of contents

1. [Roslyn](#roslyn)
1. [Source Code Analysis](#source-code-analysis)
1. [Diagnostic Analyzer](#diagnostic-analyzer)
1. [Code Fixer](#code-fixer)
1. [Code Refactoring](#code-refactoring)
1. [Diagnostic Suppressor](#diagnostic-suppressor)
1. [(Incremental) Source Generators](#incremental-source-generators)
1. [Standalone Code Analysis Tool](#standalone-code-analysis-tool)
1. [Versioning](#versioning)
1. [Testing](#testing)
1. [Tooling](#tooling)

<br />
<br />
<hr />
<br />
<br />

### Roslyn

The .NET Compiler Platform (_C#_ and _Visual Basic_)

NuGet
- https://www.nuget.org/packages/Microsoft.CodeAnalysis
- https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp
- https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Workspaces
- https://www.nuget.org/packages/Microsoft.CodeAnalysis.VisualBasic
- https://www.nuget.org/packages/Microsoft.CodeAnalysis.VisualBasic.Workspaces
- https://www.nuget.org/packages/Microsoft.CodeAnalysis.Analyzers

GitHub
- https://github.com/dotnet/roslyn
- https://github.com/dotnet/roslyn-sdk
- https://github.com/dotnet/roslyn-analyzers

#### The .NET Compiler Platform SDK model

https://learn.microsoft.com/dotnet/csharp/roslyn-sdk/compiler-api-model

- Workspace
  - Solution
    - Project
    - Project
      - Document
      - Document
        - Text
        - Syntax Tree

#### Syntax Tree
- Document
  - <blue>·</blue> Compilation Unit
    - <blue>·</blue> Syntax Node
    - <blue>·</blue> Syntax Node
      - <yellow>·</yellow> Syntax Token
      - <blue>·</blue> Syntax Node
        - <yellow>·</yellow> Syntax Token
        - <yellow>·</yellow> Syntax Token
          - <gray>·</gray> Syntax Trivia (Leading)
          - <gray>·</gray> Syntax Trivia (Trailing)

#### Semantic Model
- Compilation
  - SemanticModel (Document)
    - Symbols

#### Example
```CSharp
using System;
using static System.Console;

namespace My.Namespace;

// Single-line comment
internal static class Program
{
    /*
        Multi-line comment
    */
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        string configuration = GetConfiguration();
        WriteLine(configuration);
    }

    /// <summary>
    /// Get the build configuration.
    /// </summary>
    /// <returns>A <see cref="global::System.String"/> representing the name of the build configuration.</returns>
    private static String GetConfiguration()
    {
#if DEBUG
        return "Debug";
#else
        return "Release";
#endif
    }

    public static string ReadOnlyProperty
    {
        get { return "Text"; }
    }

    public static string ExpressionBodiedProperty => "Text";
}
```

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Source Code Analysis

- https://learn.microsoft.com/visualstudio/code-quality/roslyn-analyzers-overview
- https://learn.microsoft.com/dotnet/fundamentals/code-analysis/overview
- https://www.nuget.org/packages/StyleCop.Analyzers/
- https://www.nuget.org/packages/SonarAnalyzer.CSharp/
- https://www.nuget.org/packages/Roslynator.Analyzers/
- https://www.nuget.org/packages/xunit.analyzers/

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Diagnostic Analyzer
[DiagnosticAnalyzer](https://learn.microsoft.com/dotnet/api/microsoft.codeanalysis.diagnostics.diagnosticanalyzer)

#### Example
https://github.com/Flash0ver/F0.Analyzers

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Code Fixer
[CodeFixProvider](https://learn.microsoft.com/dotnet/api/microsoft.codeanalysis.codefixes.codefixprovider)

#### Example
https://github.com/Flash0ver/F0.Analyzers

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Code Refactoring
[CodeRefactoringProvider](https://learn.microsoft.com/dotnet/api/microsoft.codeanalysis.coderefactorings.coderefactoringprovider)

#### Example
https://github.com/Flash0ver/F0.Analyzers

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Diagnostic Suppressor
[DiagnosticSuppressor](https://learn.microsoft.com/dotnet/api/microsoft.codeanalysis.diagnostics.diagnosticsuppressor)

#### Example
https://github.com/Flash0ver/F0.Analyzers

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### (Incremental) Source Generators
- [ISourceGenerator](https://learn.microsoft.com/dotnet/api/microsoft.codeanalysis.isourcegenerator)
- [IIncrementalGenerator](https://learn.microsoft.com/dotnet/api/microsoft.codeanalysis.iincrementalgenerator)

- https://learn.microsoft.com/dotnet/csharp/roslyn-sdk/source-generators-overview
- https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md
- https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.md
- https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md

#### Example
https://github.com/Flash0ver/F0-Talks-SourceGenerators

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Standalone Code Analysis Tool
- [MSBuildLocator](https://learn.microsoft.com/dotnet/api/microsoft.build.locator.msbuildlocator)
  - https://www.nuget.org/packages/Microsoft.Build.Locator
- [MSBuildWorkspace](https://learn.microsoft.com/dotnet/api/microsoft.codeanalysis.workspace)
  - https://www.nuget.org/packages/Microsoft.CodeAnalysis.Workspaces.MSBuild

#### Example
https://github.com/FlashOWare/FlashOWare.Tool

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Versioning

https://github.com/dotnet/roslyn/blob/main/docs/wiki/NuGet-packages.md

https://learn.microsoft.com/dotnet/core/tools/global-json

|               |           |           |           |           |
| ------------- | --------- | --------- | --------- | --------- |
| .NET SDK      | `6.0.100` | `7.0.100` | `7.0.400` | `8.0.100` |
| Roslyn        | `4.0.1`   | `4.4.0`   | `4.7.0`   | `4.8.0`   |
| Visual Studio | `17.0.0`  | `17.4.0`  | `17.7.0`  | `17.8.0`  |

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Testing

[Microsoft.CodeAnalysis.Testing](https://github.com/dotnet/roslyn-sdk/tree/main/src/Microsoft.CodeAnalysis.Testing)

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

### Tooling

- Solution Explorer
- [SharpLab (Syntax Tree)](https://sharplab.io/)
- [Visual Studio: Syntax Visualizer](https://learn.microsoft.com/dotnet/csharp/roslyn-sdk/syntax-visualizer)
  - [.NET Compiler Platform SDK](https://learn.microsoft.com/dotnet/csharp/roslyn-sdk/)
- [RoslynQuoter](https://roslynquoter.azurewebsites.net/)
- [Source Browser](https://sourceroslyn.io/)
  - [//grep.app](https://grep.app/search?filter[repo][0]=dotnet/roslyn)
- [Decompilation Differ](https://wengier.com/DecompilationDiffer/)
- [MSBuild Binary and Structured Log Viewer](https://msbuildlog.com/)
  - [Structured Log Viewer](https://live.msbuildlog.com/)
- [NuGet Package Explorer](https://nuget.info/)

###### [TOC](#table-of-contents)

<br />
<br />
<hr />
<br />
<br />

###### [TOP](#may-roslyn-be-with-you)
