namespace AlphaBetaPruningBayesian

open System

[<AllowNullLiteral>]
type public IGame =
    abstract member StaticEvaluation : unit -> int
    abstract member IsFinished : unit -> bool