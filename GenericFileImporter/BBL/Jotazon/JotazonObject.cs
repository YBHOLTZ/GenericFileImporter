using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.BBL
{
    public class JotazonObject : IJotazonObject
    {
        public string Name { get; }

        List<JotazonProperty> jotazonProperties { get; set; }
        public object this[string key] {
            get { return jotazonProperties.Find(property => property.Name.Equals(key)); }
            set { jotazonProperties.Find(property => property.Name.Equals(key)).Value = value; }
        }       
        

        public JotazonObject(string name, List<JotazonProperty> properties)
        {
            this.Name = name;
            jotazonProperties = properties;
        }

        public void ForEach(Action<JotazonProperty> action)
        {
            for (int i = 0; i < jotazonProperties.Count; i++)
            {
                action.Invoke(jotazonProperties[i]);
            }
        }

        public IJotazonObject MakeACopy()
        {
            IJotazonObject copy = new JotazonObject(this.Name,this.jotazonProperties);
            return copy;
        }
    }
}
