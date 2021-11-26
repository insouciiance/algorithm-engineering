using AlphaBetaPruning;

namespace Nim.Services
{
    public class NimAI
    {
        private GameTree<Board> _currentTree;

        public NimAI(Board initialBoard)
        {
            _currentTree = new GameTree<Board>(initialBoard, BoardGenerator.GenerateChildBoards);
        }

        public bool IsGameFinished() => !_currentTree.HasChildStates();

        public Board TakePlayersMove(MoveInput playersMove)
        {
            Board currentBoard = _currentTree.CurrentState;
            currentBoard.Heaps[playersMove.HeapId].ObjectsLeft -= playersMove.ObjectsToTake;
            return currentBoard;
        }

        public Board TakeAIMove(bool maximizingPlayer = false)
        {
            Board bestMove = _currentTree.FindBestMove(100, maximizingPlayer);
            _currentTree.CurrentState = bestMove;
            return bestMove;
        }
    }
}