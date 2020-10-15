using GenericFileImporter.BBL.ExcelReader;
using GenericFileImporter.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.BBL
{
    public class Importation
    {
        AssemblyFactory.IMethodInvoker _methodInvoker = null;
        IDataBaseDAO<IJotazonObject> _dataBaseDAO = null;
        string _layoutsDirectory = null;

        public Importation(AssemblyFactory.IMethodInvoker methodInvoker, IDataBaseDAO<IJotazonObject> dataBaseDAO, string laytousDirectory)
        {
            _methodInvoker = methodInvoker;
            _dataBaseDAO = dataBaseDAO;
            _layoutsDirectory = laytousDirectory;
        }

        public void Import(string path, string layout_name, Encoding encoding)
        {
            string json_text = System.IO.File.ReadAllText(System.IO.Path.Combine(_layoutsDirectory, layout_name),encoding);

            ImportLayout importLayout = new ImportLayout(json_text);

            IFileReader fileReader = new FileReader(encoding);
            fileReader.Read(path, row => {
                try
                {
                    List<IJotazonObject> entities = buildEntities(row, importLayout);
                    insertInDataBase(entities);
                }
                catch (Exception)
                {
                    
                }           
            });
        }

        List<IJotazonObject> buildEntities(List<object> row, ImportLayout importLayout)
        {
            IJotazonObject jzonLayout = importLayout.Layout.MakeACopy();

            jzonLayout.ForEach(jzonProperty => {
                jzonProperty.Value = row[0];
                row.RemoveAt(0);
                if (jzonProperty.Required && jzonProperty is null) throw new Exception("");                              
            });

            formattingDataOfEntities(jzonLayout);

            List<IJotazonObject> entities = new List<IJotazonObject>(importLayout.Tables);
            for (int i = 0; i < entities.Count; i++)
            {
                IJotazonObject entity = entities[i].MakeACopy();
                entity.ForEach(property =>
                {
                    property.Value = jzonLayout[property.Name];
                });
            }

            if (entities.Count.Equals(0)) return new List<IJotazonObject>() { jzonLayout };
            return entities;
        }

        void formattingDataOfEntities(IJotazonObject entity)
        {
            _methodInvoker.InvokeStatic("Formatting", "FormatData", entity);
        }

        void insertInDataBase(List<IJotazonObject> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                _dataBaseDAO.Insert(entities[i]);
            }

        }
    }
}
