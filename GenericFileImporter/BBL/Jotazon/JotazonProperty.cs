using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.BBL
{
    public class JotazonProperty
    {
        public bool Required { get; private set; }
        public string Name { get; set; }
        public object Value { get; set; }

        public JotazonProperty(string name, object value, bool required = false)
        {
            this.Name = name;
            this.Value = value;
            this.Required = required;
        }
    }
}
