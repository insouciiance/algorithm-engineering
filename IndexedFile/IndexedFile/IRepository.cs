using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedFile
{
    interface IRepository<T>
    {
        void Add(T item);
        void Remove(int id);
    }
}
