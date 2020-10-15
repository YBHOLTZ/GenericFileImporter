using System;
using System.Collections.Generic;
using System.Text;

namespace GenericFileImporter.DAO
{
    public interface IDataBaseDAO<T>
    {
        void Insert(T entity);
    }
}
