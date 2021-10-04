using System;
using System.Collections.Generic;
using EightQueens.Benchmarks;
using EightQueens.Models;
using EightQueens.Models.Pieces;
using EightQueens.Tree;

namespace EightQueens
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ChessBoard board = ChessBoardGenerator.GenerateQueens(8);

            Console.WriteLine("Initial board:");
            Console.WriteLine(board);

            IBenchmark<ChessBoard> rbfsBenchmark = new RBFSBenchmark<ChessBoard>(board, new F2Heuristic());
            BenchmarkResult<ChessBoard> rbfsResult = rbfsBenchmark.Run(new ChessTreeNode(board));
            Console.WriteLine(rbfsResult);

            Console.WriteLine();

            IBenchmark<ChessBoard> idsBenchmark = new IDSBenchmark<ChessBoard>(board);
            BenchmarkResult<ChessBoard> idsResult = idsBenchmark.Run(new ChessTreeNode(board));
            Console.WriteLine(idsResult);


            Console.ReadKey();
        }
    }
}