using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.BBL.AssemblyFactory
{
    public interface IMethodInvoker
    {
        bool InvokeStatic(string assemblyName, string methodName, object paramater);
    }
}
