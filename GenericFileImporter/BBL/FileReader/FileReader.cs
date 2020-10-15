using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ExcelDataReader;

namespace GenericFileImporter.BBL.ExcelReader
{
    public class FileReader : IFileReader
    {
        public Encoding Encoding { get; private set; }
        public char Separator { get; private set; }

        public FileReader(Encoding encoding, char separator = '|')
        {
            this.Encoding = encoding;
            this.Separator = separator;
        }

        public void ReadExcel(string path, Action<List<object>> toDoEveryRow)
        {
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(File.OpenRead(path)))
            {
                ReadAllData(reader,toDoEveryRow);
            }
        }

        public void ReadCSV(string path, Action<List<object>> toDoEveryRow, Encoding encoding, char separator = '|')
        {
            ExcelReaderConfiguration excelReaderConfiguration = new ExcelReaderConfiguration()
            {
                AutodetectSeparators = new char[] { separator,';',',' },
                FallbackEncoding = encoding,
            };
            using (IExcelDataReader reader = ExcelReaderFactory.CreateCsvReader(File.OpenRead(path), excelReaderConfiguration))
            {
                ReadAllData(reader, toDoEveryRow);
            }
        }

        public void ReadAllData(IExcelDataReader reader, Action<List<object>> toDoEveryRow)
        {
            while (reader.Read())
            {                
                List<object> row = new List<object>();
                int fieldCount = reader.FieldCount;
                for (int i = 0; i < fieldCount; i++)
                    row.Add(reader.GetValue(i));

                toDoEveryRow.Invoke(row);
            }
        }

        public void Read(string path, Action<List<object>> toDoEveryRow)
        {
            string format = path.Remove(0, path.LastIndexOf('.') + 1);
            if (format.ToLower().StartsWith("xls"))
                ReadExcel(path, toDoEveryRow);
            else
                ReadCSV(path, toDoEveryRow, this.Encoding, this.Separator);
        }
    }
}
