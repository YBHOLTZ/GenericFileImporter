using System;
using System.Collections.Generic;
using System.Text;
using GenericFileImporter.BBL;
using GenericFileImporter.BBL.AssemblyFactory;
using GenericFileImporter.DAO;
using System.IO;

namespace GenericFileImporter
{
    public class GenericImporter
    {

        string _mainDirectory = null;
        string _layout_directory = null;
        string _formattingScripts_directory = null;

        public GenericImporter(string folder)
        {
            init(folder);
        }

        void init(string folder = "GenericFileImporterContents")
        {
            folderMapping(folder);
            compilation(folder);
        }

        void folderMapping(string folder)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            _mainDirectory = Path.Combine(baseDirectory, folder);            
            _layout_directory = Path.Combine(_mainDirectory, "Layout");
            _formattingScripts_directory = Path.Combine(_mainDirectory, "Formatting");

            try
            {
                Lib.FileHelper.CheckDirectory(_mainDirectory);
                Lib.FileHelper.CheckDirectory(_layout_directory);
                Lib.FileHelper.CheckDirectory(_formattingScripts_directory);
            }
            catch { throw; }

        }

        void compilation(string folder)
        {
            try
            {
                Compiler.Start(folder);
            }catch(Exception exception)
            {
                //TODO: Details here more information about compiler errors...
                
            }            
        }

        public void StartImporting(string filePath, string layout_name,Encoding encoding)
        {
            IMethodInvoker methodInvoker = new MethodInvoker();
            IDataBaseDAO<IJotazonObject> dataBaseDAO = new DataBaseDAO();
            Importation importation = new Importation(methodInvoker, dataBaseDAO, _layout_directory);
            importation.Import(filePath, layout_name, encoding);
        }


    }
}
