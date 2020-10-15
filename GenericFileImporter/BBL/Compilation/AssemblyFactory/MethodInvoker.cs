using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GenericFileImporter.BBL.AssemblyFactory
{
    public class MethodInvoker : IMethodInvoker
    {
        public bool InvokeStatic(string assemblyName, string methodName, object paramater)
        {
            try
            {
                Assembly assembly = AssemblyFactory.Get(assemblyName);
                string mainNameSpace = Assembly.GetExecutingAssembly().ManifestModule.Name.Split('.')[0];
                assembly.GetType($"{mainNameSpace}.{assemblyName}").GetMethod(methodName).Invoke(null, new object[] { paramater });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
