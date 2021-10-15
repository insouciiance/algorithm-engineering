using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedFile
{
    public class IndexedFileRepository : IRepository<string>
    {
        private const int BlocksCount = 10;
        private const int BlockValuesGap = 20;
        private const int BlockSize = 10;
        private readonly string _fileName;
        private readonly string _indexedFileName;
        private int _currentIdentityIndex;

        public IndexedFileRepository(string fileName)
        {
            fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));

            _fileName = fileName;
            _indexedFileName = $"{fileName}.index";

            File.Create(_fileName).Dispose();
            File.Create(_indexedFileName).Dispose();

            using StreamWriter indexWriter = new(_indexedFileName);
            for (int i = 0; i < BlockSize * BlocksCount; i++)
            {
                indexWriter.WriteLine();
            }
        }

        public void Add(string item)
        {
            item = item ?? throw new ArgumentNullException(nameof(item));
            int id = _currentIdentityIndex++;

            int blockId = id / BlockValuesGap;
            bool isIndexAdded = false;
            int dataLineIndex = File.ReadLines(_fileName).Count();

            string[] allLines = File.ReadAllLines(_indexedFileName);
            using StreamWriter indexWriter = new(            
                File.Open(_indexedFileName, 
                    FileMode.OpenOrCreate,                 
                    FileAccess.Write,                    
                    FileShare.ReadWrite));

            for (int i = 0; i < BlockSize * blockId; i++)
            {
                indexWriter.WriteLine(allLines[i]);
            }

            for (int i = BlockSize * blockId; i < BlockSize * (blockId + 1); i++)
            {
                if (!int.TryParse(allLines[i].Split(',')[0], out int currentId))
                {
                    indexWriter.WriteLine($"{id},{dataLineIndex}");
                    isIndexAdded = true;

                    for (int j = i; j < BlockSize * (blockId + 1); j++)
                    {
                        indexWriter.WriteLine();
                    }

                    break;
                }

                if (id < currentId)
                {
                    indexWriter.WriteLine($"{id},{dataLineIndex}");
                    isIndexAdded = true;
                }

                indexWriter.WriteLine(allLines[i]);
            }

            for (int i = BlockSize * (blockId + 1); i < allLines.Length; i++)
            {
                indexWriter.WriteLine(allLines[i]);
            }

            if (!isIndexAdded) 
            {
                indexWriter.WriteLine($"{id},{dataLineIndex}");    
            }

            string[] allData = File.ReadAllLines(_fileName);
            using StreamWriter dataWriter = new(_fileName);
            bool isDataAdded = false;

            foreach(string line in allData)
            {
                if (!bool.Parse(line.Split(',')[2]))
                {
                    dataWriter.WriteLine($"{id},{item},true");
                    isDataAdded = true;
                }
                else
                {
                    dataWriter.WriteLine(line);
                }
            }

            if (!isDataAdded)
            {
                dataWriter.WriteLine($"{id},{item},true");
            }
        }

        public void Remove(int id)
        {
            string[] allLines = File.ReadAllLines(_indexedFileName);

            StreamWriter indexWriter = new (_indexedFileName);

            foreach(string line in allLines) 
            {
                if (!int.TryParse(line.Split(',')[0], out int currentId)) 
                {
                    indexWriter.WriteLine(line);
                    continue;
                }

                if (currentId == id) 
                {
                    int dataLineIndex = int.Parse(line.Split(',')[1]);
                    string[] dataLines = File.ReadAllLines(_fileName);
                    
                    dataLines[dataLineIndex] = dataLines[dataLineIndex].Replace("true", "false");

                    StreamWriter dataWriter = new(_fileName);

                    File.WriteAllLines(_fileName, dataLines);

                    continue;
                }

                indexWriter.WriteLine(line);
            }
        }
    }
}
