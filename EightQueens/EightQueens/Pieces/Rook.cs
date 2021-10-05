using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.ChessBoards;

namespace EightQueens.Pieces
{
    public class Rook : IPiece
    {
        public Position Position { get; }

        public Rook(int x, int y)
        {
            Position = new Position(x, y);
        }

        public Rook(Position position)
        {
            Position = position;
        }

        public bool Beats(Position position) => Position.X == position.X ||
                                                Position.Y == position.Y;

        public IEnumerable<Position> GetPossibleMoves(ChessBoard board)
        {
            List<Position> positions = new();

            for (int i = Position.X + 1; i < board.Size; i++)
            {
                Position position = new Position(i, Position.Y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);
            }

            for (int i = Position.X - 1; i >= 0; i--)
            {
                Position position = new Position(i, Position.Y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);
            }

            for (int j = Position.Y + 1; j < board.Size; j++)
            {
                Position position = new Position(Position.X, j);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);
            }

            for (int j = Position.Y - 1; j >= 0; j--)
            {
                Position position = new Position(Position.X, j);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);
            }

            return positions;
        }

        public override bool Equals(object obj) => obj is Rook other && Position.Equals(other.Position);

        public override int GetHashCode() => HashCode.Combine(ToString(), Position);

        public IPiece Move(Position position) => new Rook(position);

        public override string ToString() => "R";
    }
}
