namespace AlphaBetaPruningBayesian

[<AllowNullLiteral>]
type public IGame =
    abstract member StaticEvaluation : bool -> int
    abstract member IsFinished : unit -> bool