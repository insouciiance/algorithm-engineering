namespace Night.Services

open System
open Night
open AlphaBetaPruningBayesian
open BoardGenerator

type public NightAI(board : Board) =
    member val CurrentBoard = board with get, set

    member public this.IsGameFinished() = (this.CurrentBoard :> IGame).IsFinished()

    member public this.TakePlayerMove(moveInput : MoveInput) =
        let temp = this.CurrentBoard.Board.[moveInput.X, moveInput.Y]
        if moveInput.CardIndex < 0 then
            this.CurrentBoard.PlayerHand <- [| yield! this.CurrentBoard.PlayerHand; temp |]
            this.CurrentBoard.Board.[moveInput.X, moveInput.Y] <- null
            temp.Open <- true
        else
            let card = this.CurrentBoard.PlayerHand.[moveInput.CardIndex]
            this.CurrentBoard.Board.[moveInput.X, moveInput.Y] <- card
            this.CurrentBoard.Board.[moveInput.X, moveInput.Y].Open <- true
            if temp <> null then
                this.CurrentBoard.PlayerHand.[moveInput.CardIndex] <- temp
                temp.Open <- true
            else
                this.CurrentBoard.PlayerHand <- 
                this.CurrentBoard.PlayerHand
                |> Array.filter(fun c -> not ((c :> IEquatable<Card>).Equals card))

    member public this.TakeAIMove() = 
        let gameTree = GameTree(this.CurrentBoard, BoardGenerator.GenerateChildBoards, BoardGenerator.GenerateHandPredictions)
        let bestMove = gameTree.FindBestMove 1
        bestMove.PlayerHand <- this.CurrentBoard.PlayerHand
        this.CurrentBoard <- bestMove
        bestMove