using System.Text;
using AlphaBetaPruning;

namespace Nim
{
    public class Board : IGame
    {
        public Heap[] Heaps { get; }

        public int HeapsCount => Heaps.Length;

        public Board(params Heap[] heaps)
        {
            Heaps = heaps;
        }

        public int StaticEvaluation(bool maximizingPlayer)
        {
            int eval = 0;

            foreach(Heap heap in Heaps)
            {
                eval ^= heap.ObjectsLeft;
            }

            if (maximizingPlayer)
            {
                return eval <= 0 ? 0 : 1;
            }

            return eval <= 0 ? 1 : 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            foreach(Heap heap in Heaps)
            {
                sb.Append($"[{heap.ObjectsLeft} ({heap.ObjectsCount})] ");
            }

            return sb.ToString();
        }
    }
}