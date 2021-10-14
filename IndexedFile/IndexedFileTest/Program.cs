using System;
using IndexedFile;

namespace IndexedFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IndexedFileRepository repo = new ("students");
            for (int i = 0; i < 30; i++)
            {
                repo.Add("student" + i);
            }
        }
    }
}
