using System;
using Nim;
using Nim.Services;
using AlphaBetaPruning;

namespace NimTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new(2, 1);

            GameTree<Board> tree = new(board, BoardGenerator.GenerateChildBoards);

            System.Console.WriteLine(tree.Run(100, true));
        }
    }
}
