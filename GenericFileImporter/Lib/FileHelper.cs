using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GenericFileImporter.Lib
{
    public static class FileHelper
    {
        public static void CheckFolder(string folderName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }


        public static void CheckDirectory(string directory)
        {            
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }

        public static string ExtractFileName(string path)
        {
            string[] parts = path.Split(path.Contains(@"/") ? @"/" : @"\", StringSplitOptions.None);
            string composeFileName = parts[parts.Length - 1];

            string fileName = composeFileName.Split('.')[0];
            return fileName.Replace(@".", "");
        }

        public static string[] GetAllFiles(string folderName, string format = ".cs")
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,folderName);

            string[] files = Directory.GetFiles(directory);
            return files;
        }

    }
}
