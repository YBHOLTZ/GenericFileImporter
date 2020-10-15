using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;

namespace GenericFileImporter.BBL
{
    public static class Compiler
    {

        //TODO: To remove from this.
        static string formattingScriptDefault = @"using System;
using System.IO;
using GenericFileImporter.BBL;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GenericFileImporter
{
	public static class Formatting
	{
		public static void FormatData(BBL.IJotazonObject jotazonObject)
		{

		}
		
		public static object RunTrimIfString(object value)
		{
			if(value is string)
				return value.ToString().Trim();
			return value;
		}

		public static void RunTrimIfString(BBL.IJotazonObject jotazonObject)
		{
			jotazonObject.ForEach(property => {
				property.Value = RunTrimIfString(property.Value);
			});
		}		
		
	}
}";

        public static void Start(string formatting_directory)
        {
            string formattingCsPath = Path.Combine(formatting_directory, "Formatting.cs");

            if (!File.Exists(formattingCsPath))
            {
                File.Create(formattingCsPath).Close();
                File.WriteAllText(formattingCsPath, formattingScriptDefault, Encoding.UTF8);
            }


            List<string> dot_cs_files = new List<string>(Lib.FileHelper.GetAllFiles(formatting_directory));
            dot_cs_files.Remove(formattingCsPath);

            BBL.VirtualCompiler.IVirtualCompiler virtualCompiler = new BBL.VirtualCompiler.VirtualCompiler();
                            
            List<MetadataReference> metadataReferences = new List<MetadataReference>()
            {
                MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IJotazonObject).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(KeyValuePair<string,object>).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Collections").Location),                
                MetadataReference.CreateFromFile(Assembly.Load("System.Reflection").Location)
            };

            List<Assembly> assemblies = new List<Assembly>(virtualCompiler.Compile(dot_cs_files.ToArray(),formatting_directory, Encoding.UTF8, metadataReferences.ToArray()));

            for (int i = 0; i < assemblies.Count; i++)
                metadataReferences.Add(MetadataReference.CreateFromFile(assemblies[i].Location));

            assemblies.Add(virtualCompiler.Compile(formattingCsPath,formatting_directory, Encoding.UTF8, metadataReferences.ToArray()));

            BBL.AssemblyFactory.AssemblyFactory.Add(assemblies.ToArray());
        }
    }
}
