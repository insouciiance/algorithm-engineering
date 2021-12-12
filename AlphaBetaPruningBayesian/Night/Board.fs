namespace Night

open System
open System.Text
open AlphaBetaPruningBayesian
open Night.Services.IndexesGenerator

[<AllowNullLiteral>]
type public Board(boardMatrix : Card[,], playerHand : Card[], opponentHand: Card[]) =  
    member val public Board = boardMatrix with get, set
    member val public PlayerHand = playerHand with get, set
    member val public OpponentHand = opponentHand with get, set
        
    member val public Rows = boardMatrix.GetLength 0 with get
    member val public Cols = boardMatrix.GetLength 1 with get
        
    member public this.GetCardIndexes (card : Card) =
        let y = int card.Nominal
        let openSuits = 
            [|0..this.Rows - 1|]
            |> Array.map(fun i -> (i, boardMatrix.[i..i, *]))
            |> Array.map(fun row -> 
                (fst(row), snd(row)
                |> Seq.cast<Card>
                |> Seq.filter(fun c -> c <> null)
                |> Seq.filter(fun c -> c.Open)
                |> Seq.map(fun c -> c.Suit)
            ))
        let x = openSuits |> Seq.tryFindIndex(fun s -> snd(s) |> Seq.exists(fun c -> c = card.Suit))
        if x.IsSome then 
            [|(x.Value, y)|]
        else
        let indexes = 
            [|0..this.Rows - 1|]
            |> Array.map(fun i -> (i, boardMatrix.[i..i, *]))
            |> Array.filter(fun row ->
                snd(row)
                |> Seq.cast<Card>
                |> Seq.filter(fun c -> c <> null)
                |> Seq.filter(fun el -> el.Open && el.Suit <> card.Suit)
                |> Seq.length = 0)
            |> Array.map(fun row -> (fst(row), y))
        indexes

    new() = Board(Array2D.create<Card> 4 9 null, Array.create 3 null, Array.create 3 null)

    override this.ToString() = 
        let sb = StringBuilder()
        sb.AppendLine "Board:" |> ignore
        for i in [0..this.Rows - 1] do
            for j in [0..this.Cols - 1] do
                let cardString = 
                    match boardMatrix.[i, j] with
                    | null -> "[nil]"
                    | _ -> boardMatrix.[i, j].ToString()
                let isOpenString = 
                    if boardMatrix.[i, j] <> null then
                        if boardMatrix.[i, j].Open then "o " else "c "
                    else ""
                sb.Append (cardString + isOpenString) |> ignore
            sb.Append "\n" |> ignore
        sb.AppendLine "Player's hand" |> ignore
        for card in this.PlayerHand do
            sb.Append card |> ignore
        sb.AppendLine "\nOpponent's hand" |> ignore
        for card in this.OpponentHand do
            sb.Append card |> ignore
        sb.ToString()

    interface IGame with
        member this.StaticEvaluation() =
            let mutable evaluation = 0
            
            for i = 0 to this.Rows - 1 do
                for j = 0 to this.Cols - 1 do
                    let currentCard = boardMatrix.[i, j]
                    if currentCard <> null && currentCard.Open then
                        let adjacentIndexes = getAdjacentIndexes i j this.Rows this.Cols
                        for adjacentIndex in adjacentIndexes do
                            for card in this.PlayerHand do
                                if this.GetCardIndexes(card) |> Array.contains adjacentIndex then
                                    evaluation <- evaluation + 1
                            for card in this.OpponentHand do
                                if this.GetCardIndexes(card) |> Array.contains adjacentIndex then
                                    evaluation <- evaluation - 1
            evaluation
        
        member this.IsFinished() =
            playerHand.Length = 0 || opponentHand.Length = 0

    interface ICloneable with
        member this.Clone() = 
            let mutable newBoard = Array2D.create<Card> this.Rows this.Cols null
            for i in [0..this.Rows - 1] do
                for j in [0..this.Cols - 1] do
                    let currentCard = this.Board.[i, j]
                    if currentCard <> null then 
                        newBoard.[i, j] <- Card(currentCard.Nominal, currentCard.Suit)
                        newBoard.[i, j].Open <- currentCard.Open
                    else
                        newBoard.[i, j] <- null
            let mutable playerHand = Array.create<Card> this.PlayerHand.Length null
            for i in [0..this.PlayerHand.Length - 1] do
                let currentCard = this.PlayerHand.[i]
                playerHand.[i] <- Card(currentCard.Nominal, currentCard.Suit)
                playerHand.[i].Open <- currentCard.Open
            let mutable opponendHand = Array.create<Card> this.OpponentHand.Length null
            for i in [0..this.OpponentHand.Length - 1] do
                let currentCard = this.OpponentHand.[i]
                opponendHand.[i] <- Card(currentCard.Nominal, currentCard.Suit)
                opponendHand.[i].Open <- currentCard.Open

            Board(newBoard, playerHand, opponendHand) :> obj