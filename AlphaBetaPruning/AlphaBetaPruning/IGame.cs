namespace AlphaBetaPruning
{
    public interface IGame
    {
        int StaticEvaluation(bool maximizingPlayer);
    }
}