using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nim.Services
{
    public class BoardSerializer
    {
        private readonly string _path;

        public BoardSerializer(string path)
        {
            _path = path;
        }

        public void WriteBoard(Board board)
        {
            using StreamWriter writer = new StreamWriter(_path);

            foreach (Heap heap in board.Heaps)
            {
                writer.WriteLine(heap.ObjectsLeft);
            }
        }

        public Board ReadBoard()
        {
            using StreamReader reader = new StreamReader(_path);

            List<Heap> heaps = new List<Heap>();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                heaps.Add(int.Parse(line));
            }

            return new Board(heaps.ToArray());
        }
    }
}
