using System.Diagnostics.CodeAnalysis;
using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

Console.WriteLine("Hallo, BASTA!");
Console.WriteLine();

SyntaxTree tree = CSharpSyntaxTree.ParseText(Code.Text);
CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

Console.WriteLine($"Using Directives: {root.Usings.Count}");
Console.WriteLine($"Namespace: {root
    .DescendantNodes()
    .OfType<FileScopedNamespaceDeclarationSyntax>()
    .Single()
    .Name}");

Console.WriteLine();

MyCSharpSyntaxWalker walker = new();
walker.Visit(root);

Console.WriteLine();

var compilation = CSharpCompilation.Create("BASTA",
    new[] { tree },
    ReferenceAssemblies.NetStandard20,
    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

SemanticModel model = compilation.GetSemanticModel(tree);

var declaration = root
    .DescendantNodes()
    .OfType<ClassDeclarationSyntax>()
    .Single()
    .Members
    .OfType<MethodDeclarationSyntax>()
    .ElementAt(1);
TypeInfo info = model.GetTypeInfo(declaration.ReturnType);
ITypeSymbol? type = info.Type;
Console.WriteLine($"Syntax: {declaration.ReturnType.GetType()}");
Console.WriteLine($"Symbol: {type} of {type!.Kind} is {type.SpecialType}");

sealed class MyCSharpSyntaxWalker : CSharpSyntaxWalker
{
    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        Console.WriteLine($"- Class: {node.Identifier.ValueText}");
        base.VisitClassDeclaration(node);
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        Console.WriteLine($"  - Method: {node.Identifier.ValueText}");
        base.VisitMethodDeclaration(node);
    }

    public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        Console.WriteLine($"  - Property: {node.Identifier.ValueText}");
        base.VisitPropertyDeclaration(node);
    }
}

static class Code
{
    [StringSyntax(LanguageNames.CSharp)]
    public static string Text => """
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
        """;
}
