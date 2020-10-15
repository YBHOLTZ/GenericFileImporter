using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.BBL.ExcelReader
{
    public interface IFileReader
    {
        void ReadExcel(string path, Action<List<object>> toDoEveryRow);

        void ReadCSV(string path, Action<List<object>> toDoEveryRow, Encoding encoding, char separator = '|');

        void Read(string path, Action<List<object>> toDoEveryRow);

    }
}
