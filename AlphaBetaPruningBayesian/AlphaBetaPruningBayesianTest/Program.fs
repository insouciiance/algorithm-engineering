open System
open AlphaBetaPruningBayesian
open Night
open Night.Services
open Night.Services.BoardGenerator

[<EntryPoint>]
let main argv =
    let board = BoardGenerator.Generate()
    printfn "%s" (board.ToString())
    let eval = (board :> IGame).StaticEvaluation()
    printf "%d" eval
    
    let nightAI = NightAI(board)
    while not (nightAI.IsGameFinished()) do
        printf "x="
        let x = Int32.Parse(Console.ReadLine())
        printf "y="
        let y = Int32.Parse(Console.ReadLine())
        printf "card index="
        let ci = Int32.Parse(Console.ReadLine())
        let moveInput = MoveInput(x, y, ci)
        nightAI.TakePlayerMove moveInput
        printfn "%s" (nightAI.CurrentBoard.ToString())
        printfn "%s" (nightAI.TakeAIMove().ToString())
    0