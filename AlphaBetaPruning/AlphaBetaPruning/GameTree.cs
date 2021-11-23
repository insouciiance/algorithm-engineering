using System;
using System.Text;

namespace AlphaBetaPruning
{
    public class GameTree<T> where T : IGame
    {
        public T InitialState { get; }

        public Func<T, T[]> ChildStatesGenerator { get; }

        public GameTree(T initialState, Func<T, T[]> childStatesGenerator)
        {
            InitialState = initialState;
            ChildStatesGenerator = childStatesGenerator;
        }
        
        public int Run(int depth, bool maximizingPlayer)
        {
            return MiniMax(InitialState, depth, int.MinValue, int.MaxValue, maximizingPlayer);
        }

        private int MiniMax(T state, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            T[] childStates = ChildStatesGenerator.Invoke(state);

            if (depth <= 0 || childStates.Length == 0)
            {
                return state.StaticEvaluation(maximizingPlayer);
            }

            int miniMaxValue = maximizingPlayer ? int.MinValue : int.MaxValue;
            foreach(T childState in childStates)
            {
                int childMiniMax = MiniMax(childState, depth - 1, alpha, beta, !maximizingPlayer);
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
            StringBuilder sb = new();

            System.Console.WriteLine("Game Tree");
            
            int indentationSize = 0;

            AppendString(InitialState, true);

            void AppendString(T currentState, bool maximizingPlayer)
            {
                Console.ForegroundColor = (ConsoleColor)indentationSize;
                Console.WriteLine(new string(' ', indentationSize * 2) + $"{currentState}(ev. {MiniMax(currentState, 1000, int.MinValue, int.MaxValue, maximizingPlayer)}[{maximizingPlayer}])");
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
