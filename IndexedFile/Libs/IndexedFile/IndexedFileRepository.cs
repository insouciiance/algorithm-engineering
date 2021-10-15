using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedFile
{
    public class IndexedFileRepository : IIndexedRepository
    {
        private const int BlocksCount = 10;
        private const int BlockValuesGap = 20;
        private const int BlockSize = 10;
        private readonly string _fileName;
        private readonly string _indexedFileName;
        private readonly List<int> _existingIndexes = new();

        public IndexedFileRepository() : this(null) {}

        public IndexedFileRepository(string fileName)
        {
            fileName ??= "repository";

            _fileName = fileName;
            _indexedFileName = $"{fileName}.index";

            if (!File.Exists(_fileName))
            {
                File.Create(_fileName).Dispose();
                File.Create(_indexedFileName).Dispose();

                using StreamWriter indexWriter = new(_indexedFileName);
                for (int i = 0; i < BlockSize * BlocksCount; i++)
                {
                    indexWriter.WriteLine();
                }
            }

            string[] allLines = File.ReadAllLines(_indexedFileName);

            foreach(string line in allLines)
            {
                if (line is "") continue;

                int currentIndex = int.Parse(line.Split(',')[0]);
                _existingIndexes.Add(currentIndex);
            }
        }

        public void Add(string item)
        {
            item = item ?? throw new ArgumentNullException(nameof(item));
            int id = 0;
            while (_existingIndexes.Contains(id))
            {
                id++;
            }

            int blockId = id / BlockValuesGap;
            bool isIndexAdded = false;

            string[] dataLines = File.ReadAllLines(_fileName);

            int dataLineIndex = File.ReadLines(_fileName).Count();

            for (int i = 0; i < dataLines.Length; i++)
            {
                if (!bool.Parse(dataLines[i].Split(',')[2])) 
                {
                    dataLineIndex = i;
                    break;
                }
            }

            string[] allLines = File.ReadAllLines(_indexedFileName);
            using StreamWriter indexWriter = new(            
                File.Open(_indexedFileName, 
                    FileMode.OpenOrCreate,                 
                    FileAccess.Write,                    
                    FileShare.ReadWrite));

            // skip before needed block
            for (int i = 0; i < BlockSize * blockId; i++)
            {
                indexWriter.WriteLine(allLines[i]);
            }

            // process needed block
            for (int i = BlockSize * blockId; i < BlockSize * (blockId + 1); i++)
            {
                if (!int.TryParse(allLines[i].Split(',')[0], out int currentId))
                {
                    indexWriter.WriteLine($"{id},{dataLineIndex}");
                    isIndexAdded = true;

                    for (int j = i + 1; j < BlockSize * (blockId + 1); j++)
                    {
                        indexWriter.WriteLine(allLines[j]);
                    }

                    break;
                }

                if (id < currentId)
                {
                    indexWriter.WriteLine($"{id},{dataLineIndex}");
                    isIndexAdded = true;

                    for (int j = i + 1; j < BlockSize * (blockId + 1); j++)
                    {
                        indexWriter.WriteLine(allLines[j]);
                    }

                    break;
                }

                indexWriter.WriteLine(allLines[i]);
            }

            //skip after needed block
            for (int i = BlockSize * (blockId + 1); i < allLines.Length; i++)
            {
                indexWriter.WriteLine(allLines[i]);
            }

            if (!isIndexAdded) 
            {
                indexWriter.WriteLine($"{id},{dataLineIndex}");    
            }

            _existingIndexes.Add(id);

            string[] allData = File.ReadAllLines(_fileName);
            using StreamWriter dataWriter = new(_fileName);
            bool isDataAdded = false;

            foreach(string line in allData)
            {
                if (!bool.Parse(line.Split(',')[2]) && !isDataAdded)
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

        public bool Remove(int id)
        {
            string[] allLines = File.ReadAllLines(_indexedFileName);
            bool removed = false;

            using StreamWriter indexWriter = new (_indexedFileName);

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

                    File.WriteAllLines(_fileName, dataLines);
                    _existingIndexes.Remove(id);

                    continue;
                }

                indexWriter.WriteLine(line);
            }

            if (removed)
            {
                indexWriter.WriteLine();
            }

            return removed;
        }

        public string[] GetAllData()
        {
            return File.ReadAllLines(_fileName);
        }

        public string[] GetAllIndexes()
        {
            return File.ReadAllLines(_indexedFileName);
        }
    }
}
