using GenericFileImporter.Lib;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace GenericFileImporter.BBL.VirtualCompiler
{
    public class VirtualCompiler : IVirtualCompiler
    {        

        public VirtualCompiler()
        {
        }     

        public Assembly[] Compile(string[] cs_files, string mainFolder, Encoding encoding, MetadataReference[] metadataReferences = null)
        {
            List<Assembly> assemblies = new List<Assembly>();
            for (int i = 0; i < cs_files.Length; i++)
            {
                try
                {
                    Assembly assembly = Compile(cs_files[i], mainFolder, encoding, metadataReferences);
                    assemblies.Add(assembly);
                }catch(Exception ex)
                {
                    continue;
                }          

            }
            return assemblies.ToArray();
        }

        public Assembly Compile(string csFilePath, string mainFolder, Encoding encoding, MetadataReference[] metadataReferences = null)
        {
            string code = File.ReadAllText(csFilePath, encoding);
            var tree = SyntaxFactory.ParseSyntaxTree(code);

            string cs_fileName = csFilePath.Remove(0, csFilePath.LastIndexOf( csFilePath.Contains(@"\") ? @"\" : @"/" ) +1).Split(".")[0];

            string fileName = $"{cs_fileName}.dll";

            var compilation = CSharpCompilation.Create(fileName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(metadataReferences)
                .AddSyntaxTrees(tree);

            string path = Path.Combine(Directory.GetCurrentDirectory(),mainFolder, "MyLibs", fileName);
            EmitResult compilationResult = compilation.Emit(path);
            if (compilationResult.Success)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                return assembly;
            }
            else
            {
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    string issue = $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()},Location: { codeIssue.Location.GetLineSpan()},Severity: { codeIssue.Severity}";
                    Console.WriteLine(issue);
                }
                throw new Exception($"It's no possible to complie {csFilePath} class");
            }
        }
    }
}
