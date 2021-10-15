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
                string line = Console.ReadLine();
                string option = line.Split(' ')[0];
                if (option == "a") 
                {
                    repo.Add(line.Replace("a ", ""));
                }
                else 
                {
                    int removeId = int.Parse(line.Split(' ')[1]);
                    repo.Remove(removeId);
                }
            }
        }
    }
}
