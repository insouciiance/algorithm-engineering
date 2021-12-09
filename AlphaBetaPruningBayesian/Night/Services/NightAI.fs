namespace Night.Services

open System
open Night
open AlphaBetaPruningBayesian
open BoardGenerator
open MoveInput

module public NightAI = 
    type NightAI(board : Board) =
        member val CurrentBoard = board with get, set

        member public this.IsGameFinished() = (this.CurrentBoard :> IGame).IsFinished()

        member public this.TakePlayerMove(moveInput : MoveInput) =
            let temp = this.CurrentBoard.Board.[moveInput.X, moveInput.Y]
            let card = this.CurrentBoard.PlayerHand.[moveInput.CardIndex]
            this.CurrentBoard.Board.[moveInput.X, moveInput.Y] <- card
            this.CurrentBoard.Board.[moveInput.X, moveInput.Y].Open <- true
            if temp <> null then
                this.CurrentBoard.PlayerHand.[moveInput.CardIndex] <- temp
            else
                this.CurrentBoard.PlayerHand <- 
                    this.CurrentBoard.PlayerHand
                    |> Array.filter(fun c -> not ((c :> IEquatable<Card>).Equals card))

        member public this.TakeAIMove() = 
            let mutable bestMove = null
            let mutable counter = 0
            let handPredictions = BoardGenerator.GenerateHandPredictions this.CurrentBoard
            for handPrediction in handPredictions do
                let gameTree = GameTree(handPrediction, BoardGenerator.GenerateChildBoards)
                let currentBestMove = gameTree.FindBestMove 1
                bestMove <- 
                    match currentBestMove with
                    | move when bestMove = null || (move :> IGame).StaticEvaluation true < (bestMove :> IGame).StaticEvaluation true -> move
                    | _ -> bestMove
                if counter % 5000 = 0 then
                    printfn "%d" counter
                counter <- counter + 1
            bestMove.PlayerHand <- this.CurrentBoard.PlayerHand
            this.CurrentBoard <- bestMove
            bestMove