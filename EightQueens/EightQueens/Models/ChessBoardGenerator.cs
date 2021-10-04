using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Models.Pieces;

namespace EightQueens.Models
{
    public static class ChessBoardGenerator
    {
        private static readonly Random Random = new ();

        public static ChessBoard GenerateQueens(int size, int piecesCount = 8)
        {
            if (piecesCount > size)
            {
                throw new ArgumentException($"There can not be more than {size} queens on board.", nameof(piecesCount));
            }

            ChessBoard board = new (size);

            int piecesGenerated = 0;

            while (piecesGenerated < piecesCount)
            {
                int x = Random.Next(size);
                int y = Random.Next(size);

                Position position = new (x, y);

                if (!board.IsPositionOccupied(p => position.X == p.X || position.Y == p.Y))
                {
                    board.Pieces.Add(new Queen(position));
                    piecesGenerated++;
                }
            }

            return board;
        }
    }
}
