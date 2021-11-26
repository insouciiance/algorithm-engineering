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
            Board board = new(1, 2, 3, 4);
            GameTree<Board> tree = new(board, BoardGenerator.GenerateChildBoards);

            System.Console.WriteLine(board);

            NimAI nimAi = new(board);

            while(!nimAi.IsGameFinished())
            {
                System.Console.WriteLine("Enter heap's ID:");
                int heapId = int.Parse(Console.ReadLine());
                System.Console.WriteLine("Enter a number of objects to remove:");
                int objectsToTake = int.Parse(Console.ReadLine());

                MoveInput input = new(heapId, objectsToTake);

                Board playersBoardResult = nimAi.TakePlayersMove(input);

                System.Console.WriteLine("Player's move");
                System.Console.WriteLine(playersBoardResult);

                Board aiBoardResult = nimAi.TakeAIMove();

                if (aiBoardResult is null)
                {
                    System.Console.WriteLine("Player won!");
                    return;
                }

                System.Console.WriteLine("AI's move");
                System.Console.WriteLine(aiBoardResult);
            }

            System.Console.WriteLine("AI won!");
        }
    }
}
