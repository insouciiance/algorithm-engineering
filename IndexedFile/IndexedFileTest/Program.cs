using System;
using IndexedFile;

namespace IndexedFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IndexedFileRepository repo = new ("students");
            
            while(true) 
            {
                repo.Add(Console.ReadLine());    
            }
        }
    }
}
