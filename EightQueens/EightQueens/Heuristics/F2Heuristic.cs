using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.ChessBoards;
using EightQueens.Pieces;

namespace EightQueens.Heuristics
{
    public class F2Heuristic : IHeuristic<ChessBoard>
    {
        public int Evaluate(ChessBoard board)
        {
            int heuristicsResult = 0;

            foreach (IPiece first in board.Pieces)
            {
                foreach (IPiece second in board.Pieces)
                {
                    if (first.Beats(second.Position) && !first.Position.Equals(second.Position))
                    { 
                        heuristicsResult++;
                    }
                }
            }

            return heuristicsResult;
        }
    }
}
