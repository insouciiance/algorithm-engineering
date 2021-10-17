using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedFile
{
    public interface IIndexedRepository
    {
        void Add(string item);
        bool Remove(int id);
        int Find(int id);
        string[] GetAllData();
        string[] GetAllIndexes();
    }
}
