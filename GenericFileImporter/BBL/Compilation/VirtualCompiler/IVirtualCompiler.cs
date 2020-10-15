using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GenericFileImporter.BBL.VirtualCompiler
{
    public interface IVirtualCompiler
    {
        Assembly[] Compile(string[] cs_paths, string mainFolder, Encoding encoding, MetadataReference[] metadataReferences = null);

        Assembly Compile(string path, string mainFolder, Encoding encoding, MetadataReference[] metadataReferences = null);
       
    }
}
