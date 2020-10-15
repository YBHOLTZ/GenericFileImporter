using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.BBL
{
    public interface IJotazonObject
    {
        string Name { get; }
        object this[string key] { get; set; }       

        void ForEach(Action<JotazonProperty> action);

        IJotazonObject MakeACopy();
    }
}
