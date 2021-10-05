using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.ChessBoards;
using EightQueens.Pieces;

namespace EightQueens.Tree
{
    public class ChessTreeNode : ITreeNode<ChessBoard>
    {
        public ChessBoard State { get; }
        public ITreeNode<ChessBoard> ParentNode { get; }
        public int PathCost { get; }
        public int Depth { get; }

        public ChessTreeNode(ChessBoard state, ITreeNode<ChessBoard> parentNode = null)
        {
            State = state;
            ParentNode = parentNode;
            PathCost = parentNode?.PathCost + 1 ?? 0;
            Depth = parentNode?.Depth + 1 ?? 0;
        }

        public bool IsNodeCompliant()
        {
            return !State.IsBoardThreatened();
        }

        public IEnumerable<ITreeNode<ChessBoard>> GetChildren()
        {
            foreach (IPiece piece in State.Pieces)
            {
                IEnumerable<Position> possibleMoves = piece.GetPossibleMoves(State);

                foreach (Position newPosition in possibleMoves)
                {
                    List<IPiece> newPieces = new(State.Pieces.Where(p => !piece.Position.Equals(p.Position)))
                    {
                        piece.Move(newPosition)
                    };

                    ChessBoard newBoard = new(State.Size, newPieces);

                    yield return new ChessTreeNode(newBoard, this);
                }
            }
        }
    }
}
