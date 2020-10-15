using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GenericFileImporter.BBL.VirtualCompiler;
using GenericFileImporter.BBL;

namespace GenericFileImporter.BBL.AssemblyFactory
{
    public static class AssemblyFactory
    {
        private static Dictionary<string, Assembly> _assemblies { get; set; } = new Dictionary<string, Assembly>();

        public static void Add(Assembly assembly)
        {
            string name = assembly.ManifestModule.Name.Split('.')[0];
            _assemblies.Add(name, assembly);
        }

        public static void Add(Assembly[] assemblies)
        {
            for (int i = 0; i < assemblies.Length; i++)
            {
                Add(assemblies[i]);
            }
        }

        public static Assembly Get(string name)
        {
            return _assemblies[name];
        }

        public static void Init(IVirtualCompiler virtualCompiler)
        {

        }
    }
}
