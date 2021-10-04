using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens.Models.Pieces
{
    public interface IPiece
    {
        Position Position { get; }

        bool Beats(Position position);
        IPiece Move(Position position);
        IEnumerable<Position> GetPossibleMoves(ChessBoard board);
    }
}
