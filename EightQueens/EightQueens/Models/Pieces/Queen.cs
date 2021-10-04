using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens.Models.Pieces
{
    public class Queen : IPiece
    {
        public Position Position { get; }

        public Queen(int x, int y)
        {
            Position = new Position(x, y);
        }

        public Queen(Position position)
        {
            Position = position;
        }

        public bool Beats(Position position) => Position.X == position.X ||
                                                Position.Y == position.Y ||
                                                Position.X + Position.Y == position.X + position.Y ||
                                                Position.X - Position.Y == position.X - position.Y;


        public IEnumerable<Position> GetPossibleMoves(ChessBoard board)
        {
            for (int i = Position.X + 1; i < board.Size; i++)
            {
                Position position = new Position(i, Position.Y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                yield return position;
            }

            for (int i = Position.X - 1; i >= 0; i--)
            {
                Position position = new Position(i, Position.Y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                yield return position;
            }

            for (int j = Position.Y + 1; j < board.Size; j++)
            {
                Position position = new Position(Position.X, j);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                yield return position;
            }

            for (int j = Position.Y - 1; j >= 0; j--)
            {
                Position position = new Position(Position.X, j);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                yield return position;
            }

            int x = Position.X + 1;
            int y = Position.Y + 1;

            while (x < board.Size && y < board.Size)
            {
                Position position = new Position(x, y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                yield return position;

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

                yield return position;

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

                yield return position;

                x -= 1;
                y += 1;
            }

            x = Position.X + 1;
            y = Position.Y - 1;

            while (x < board.Size && y >= 0)
            {
                Position position = new Position(x, y);

                if (board.IsPositionOccupied(position))
                {
                    break;
                }

                yield return position;

                x += 1;
                y -= 1;
            }
        }

        public IPiece Move(Position position) => new Queen(position);

        public override bool Equals(object obj) => obj is Queen other && Position.Equals(other.Position);

        public override int GetHashCode() => HashCode.Combine(ToString(), Position);

        public override string ToString() => "Q";
    }
}
