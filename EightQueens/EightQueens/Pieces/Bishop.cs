using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.ChessBoards;

namespace EightQueens.Pieces
{
    public class Bishop : IPiece
    {
        public Position Position { get; }

        public Bishop(int x, int y)
        {
            Position = new Position(x, y);
        }

        public Bishop(Position position)
        {
            Position = position;
        }

        public bool Beats(Position position) => Position.X + Position.Y == position.X + position.Y ||
                                                Position.X - Position.Y == position.X - position.Y;

        public IEnumerable<Position> GetPossibleMoves(ChessBoard board)
        {
            List<Position> positions = new();

            int x = Position.X + 1;
            int y = Position.Y + 1;

            while (x < board.Size && y < board.Size)
            {
                Position position = new Position(x, y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);

                x += 1;
                y += 1;
            }

            x = Position.X - 1;
            y = Position.Y - 1;

            while (x >= 0 && y >= 0)
            {
                Position position = new Position(x, y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);

                x -= 1;
                y -= 1;
            }

            x = Position.X - 1;
            y = Position.Y + 1;

            while (x >= 0 && y < board.Size)
            {
                Position position = new Position(x, y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);

                x -= 1;
                y += 1;
            }

            x = Position.X + 1;
            y = Position.Y - 1;

            while (x < board.Size && y >= 0)
            {
                Position position = new (x, y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                positions.Add(position);

                x += 1;
                y -= 1;
            }

            return positions;
        }

        public override bool Equals(object obj) => obj is Bishop other && Position.Equals(other.Position);

        public override int GetHashCode() => HashCode.Combine(ToString(), Position);

        public IPiece Move(Position position) => new Bishop(position);

        public override string ToString() => "B";
    }
}
