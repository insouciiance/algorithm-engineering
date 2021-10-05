using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Pieces;

namespace EightQueens.ChessBoards
{
    public class ChessBoard
    {
        public int Size { get; }
        public List<IPiece> Pieces { get; }

        public ChessBoard(int size, List<IPiece> pieces = null)
        {
            Size = size;
            Pieces = pieces ?? new List<IPiece>();
        }

        public bool IsPositionOccupied(Position position) => Pieces.Any(piece => position.Equals(piece.Position));
        public bool IsPositionOccupied(Predicate<Position> predicate) => Pieces.Any(piece => predicate(piece.Position));

        public bool IsBoardThreatened()
        {
            foreach (IPiece first in Pieces)
            {
                foreach (IPiece second in Pieces)
                {
                    if (first.Beats(second.Position) && !first.Position.Equals(second.Position))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ChessBoard other)
            {
                return false;
            }

            foreach (IPiece piece in Pieces)
            {
                if (!other.Pieces.Contains(piece))
                {
                    return false;
                }
            }

            foreach (IPiece piece in other.Pieces)
            {
                if (!this.Pieces.Contains(piece))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode() => this.ToString().GetHashCode();

        public override string ToString()
        {
            StringBuilder builder = new();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Position currentPosition = new(i, j);
                    IPiece piece;

                    builder.Append((piece = Pieces.FirstOrDefault(p => currentPosition.Equals(p.Position))) != null
                        ? $"{piece} "
                        : "- ");
                }

                builder.Append('\n');
            }

            return builder.ToString();
        }
    }
}
