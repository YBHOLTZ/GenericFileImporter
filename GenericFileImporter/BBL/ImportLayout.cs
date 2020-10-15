using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenericFileImporter.BBL
{
    public class ImportLayout
    {
        public IJotazonObject Layout { get; private set; } = null;
        public List<IJotazonObject> Tables { get; private set; } = null;

        public ImportLayout(string json_text)
        {                        
            JObject jObject = JObject.Parse(json_text);

            IEnumerator<JProperty> jProperties = jObject.Properties().GetEnumerator();

            List<IJotazonObject> jotazonObjects = new List<IJotazonObject>();

            while (jProperties.MoveNext())
            {
                JProperty jproperty = jProperties.Current;
                string jotazonObjectName = jproperty.Name;
                List<JotazonProperty> jotazonProperties = JotazonUtil.ExtractJotazonProperties(JObject.Parse(jproperty.Value.ToString()));
                IJotazonObject jotazonObject = new JotazonObject(jotazonObjectName, jotazonProperties);
                jotazonObjects.Add(jotazonObject);
            }

            Layout = jotazonObjects[0];

            if(jotazonObjects.Count > 1)
                Tables = new List<IJotazonObject>(jotazonObjects.GetRange(1, jotazonObjects.Count - 1));

        }
    }
}
