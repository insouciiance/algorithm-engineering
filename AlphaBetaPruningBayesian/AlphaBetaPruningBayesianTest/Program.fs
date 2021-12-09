open System
open AlphaBetaPruningBayesian
open Night.BoardGenerator

[<EntryPoint>]
let main argv =
    let board = BoardGenerator.Generate()
    printfn "%s" (board.ToString())
    let eval = (board :> IGame).StaticEvaluation true
    printf "%d" eval
    0