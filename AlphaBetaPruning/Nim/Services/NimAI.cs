using AlphaBetaPruning;

namespace Nim.Services
{
    public class NimAI
    {
        private readonly GameTree<Board> _currentTree;

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

        public Board TakeAIMove(Difficulty difficulty = Difficulty.Normal)
        {
            Board bestMove = _currentTree.FindBestMove((int)difficulty);
            _currentTree.CurrentState = bestMove;
            return bestMove;
        }
    }
}