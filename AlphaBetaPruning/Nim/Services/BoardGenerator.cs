using System;
using System.Collections.Generic;

namespace Nim.Services
{
    public static class BoardGenerator
    {
        public static Board[] GenerateChildBoards(Board currentBoard)
        {
            List<Board> childBoards = new List<Board>();

            for(int i = 0; i < currentBoard.HeapsCount; i++)
            {                
                for(int j = 0; j < currentBoard.Heaps[i].ObjectsLeft; j++)
                {
                    Heap[] newHeaps = new Heap[currentBoard.HeapsCount];
                    Array.Copy(currentBoard.Heaps, newHeaps, currentBoard.HeapsCount);

                    newHeaps[i].ObjectsLeft = j;

                    childBoards.Add(new Board(newHeaps));
                }
            }

            return childBoards.ToArray();
        }
    }
}