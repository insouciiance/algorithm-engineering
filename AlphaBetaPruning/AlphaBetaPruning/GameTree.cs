using System;
using System.Text;

namespace AlphaBetaPruning
{
    public class GameTree<T> where T : IGame
    {
        public T CurrentState { get; set; }

        public Func<T, T[]> ChildStatesGenerator { get; }

        public GameTree(T currentState, Func<T, T[]> childStatesGenerator)
        {
            CurrentState = currentState;
            ChildStatesGenerator = childStatesGenerator;
        }

        public bool HasChildStates() => ChildStatesGenerator.Invoke(CurrentState).Length > 0;
        
        public T FindBestMove(int depth)
        {
            T[] childStates = ChildStatesGenerator.Invoke(CurrentState);

            if (childStates.Length == 0)
            {
                return default;
            }

            T bestMove = childStates[0];

            foreach(T childState in childStates)
            {
                int miniMaxValue = MiniMax(childState, depth, false);

                if (miniMaxValue > bestMove.StaticEvaluation(false))
                {
                    bestMove = childState;
                }
            }

            return bestMove;
        }

        public int Run(int depth, bool maximizingPlayer)
        {
            return MiniMax(CurrentState, depth, maximizingPlayer);
        }

        private int MiniMax(T state, int depth, bool maximizingPlayer, int alpha = int.MinValue, int beta = int.MaxValue)
        {
            T[] childStates = ChildStatesGenerator.Invoke(state);

            if (depth <= 0 || childStates.Length == 0)
            {
                return state.StaticEvaluation(maximizingPlayer);
            }

            int miniMaxValue = maximizingPlayer ? int.MinValue : int.MaxValue;
            foreach(T childState in childStates)
            {
                int childMiniMax = MiniMax(childState, depth - 1, !maximizingPlayer, alpha, beta);
                if (maximizingPlayer)
                {
                    miniMaxValue = Math.Max(miniMaxValue, childMiniMax);

                    alpha = Math.Max(alpha, childMiniMax);
                }

                if (!maximizingPlayer)
                {
                    miniMaxValue = Math.Min(miniMaxValue, childMiniMax);

                    beta = Math.Min(beta, childMiniMax);
                }

                if (beta <= alpha)
                {
                    break;
                }
            }

            return miniMaxValue;
        }

        public void Print()
        {
            StringBuilder sb = new StringBuilder();

            System.Console.WriteLine("Game Tree");
            
            int indentationSize = 0;

            AppendString(CurrentState, true);

            void AppendString(T currentState, bool maximizingPlayer)
            {
                Console.ForegroundColor = (ConsoleColor)indentationSize;
                Console.WriteLine(new string(' ', indentationSize * 2) + $"{currentState}(ev. {MiniMax(currentState, 1000, maximizingPlayer)}[{maximizingPlayer}])");
                Console.ForegroundColor = ConsoleColor.Gray;
                indentationSize++;

                foreach(T childState in ChildStatesGenerator.Invoke(currentState))
                {
                    AppendString(childState, !maximizingPlayer);
                }

                indentationSize--;
            }
        }
    }
}
