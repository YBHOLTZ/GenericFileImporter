using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.BBL
{
    public static class JotazonUtil
    {
        public static Dictionary<string,object> ExtractProperties(JObject jObject)
        {
            IEnumerator<JProperty> jProperties = jObject.Properties().GetEnumerator();
            Dictionary<string, object> properties = new Dictionary<string, object>();

            while (jProperties.MoveNext())
            {
                JProperty jProperty = jProperties.Current;
                string name = jProperty.Name;
                object value = jProperty.Value;
                properties.Add(name, value);
            }
            return properties;
        }


        public static List<JotazonProperty> ExtractJotazonProperties(JObject jObject)
        {
            IEnumerator<JProperty> jProperties = jObject.Properties().GetEnumerator();
            List<JotazonProperty> properties = new List<JotazonProperty>();

            while (jProperties.MoveNext())
            {
                JProperty jProperty = jProperties.Current;
                string name = jProperty.Name;
                object value = jProperty.Value;
                bool required = value is string ? (value.ToString().ToLower().Equals("x") ? true : false) : false;
                properties.Add(new JotazonProperty(name,value,required));
            }
            return properties;
        }
    }
}
