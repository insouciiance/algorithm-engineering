using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public IndexedFileRepository() : this(null) { }

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

            foreach (string line in allLines)
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
            using StreamWriter indexWriter = new(_indexedFileName);

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

                string[] blockLines = allLines.Skip(BlockSize * blockId).Take(BlockSize).ToArray();

                if (id < currentId && blockLines.Any(line => line.Equals(string.Empty)))
                {
                    indexWriter.WriteLine($"{id},{dataLineIndex}");
                    isIndexAdded = true;

                    for (int j = i; j < BlockSize * (blockId + 1) - 1; j++)
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

            foreach (string line in allData)
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

            using StreamWriter indexWriter = new(_indexedFileName);

            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i];

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
                    removed = true;
                    int removedLineIndex = i;

                    int blockId = id / BlockValuesGap;

                    for (int j = i + 1; j < allLines.Length; j++)
                    {
                        if (j == (blockId + 1) * BlockSize && removedLineIndex < BlocksCount * BlockSize)
                        {
                            indexWriter.WriteLine();
                            indexWriter.WriteLine(allLines[j]);
                        }
                        else
                        {
                            indexWriter.WriteLine(allLines[j]);
                        }
                    }

                    break;
                }

                indexWriter.WriteLine(line);
            }

            return removed;
        }

        public int Find(int id)
        {
            int blockId = id / BlockValuesGap;

            (int, int)[] lines = File
                .ReadAllLines(_indexedFileName)
                .Skip(blockId * BlockSize)
                .Take(BlockSize)
                .Where(l => !l.Equals(string.Empty))
                .Select(l =>
                {
                    int elementIndex = int.Parse(l.Split(',')[0]);
                    int lineIndex = int.Parse(l.Split(',')[1]);

                    return (elementIndex, lineIndex);
                })
                .ToArray();

            (int, int)? line = BinarySearch(lines);

            if (line is not null)
            {
                return blockId * BlockSize + Array.IndexOf(lines, line);
            }

            (int, int)[] overflowLines = File
                .ReadAllLines(_indexedFileName)
                .Skip(BlocksCount * BlockSize)
                .Where(l => !l.Equals(string.Empty))
                .Select(l =>
                {
                    int elementIndex = int.Parse(l.Split(',')[0]);
                    int lineIndex = int.Parse(l.Split(',')[1]);

                    return (elementIndex, lineIndex);
                })
                .ToArray();

            foreach ((int elementId, int dataId) overflowLine in overflowLines)
            {
                if (overflowLine.elementId == id)
                {
                    return BlocksCount * BlockSize + Array.IndexOf(overflowLines, overflowLine);
                }
            }

            return -1;

            (int, int)? BinarySearch((int, int)[] array)
            {
                if (array.Length == 0)
                {
                    return null;
                }

                (int elementId, int lineId) = array[array.Length / 2];

                return elementId switch
                {
                    int i when i > id => BinarySearch(array.Take(array.Length / 2).ToArray()),
                    int i when i < id => BinarySearch(array.Skip(array.Length / 2 + 1).ToArray()),
                    _ => (elementId, lineId)
                };
            }
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
