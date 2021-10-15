using System;
using IndexedFile;

namespace IndexedFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IndexedFileRepository repo = new ("students");
            for (int i = 100; i < 101; i++)
            {
                repo.Add("student" + i);
            }
        }
    }
}
