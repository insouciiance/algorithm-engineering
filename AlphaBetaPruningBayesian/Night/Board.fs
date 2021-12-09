namespace Night

open System.Text
open AlphaBetaPruningBayesian
open IndexesGenerator

type public Board(boardMatrix : Card[,], playerHand : Card[], opponentHand: Card[]) =
    let mutable Board = boardMatrix
    let mutable PlayerHand = playerHand
    let mutable OpponentHand = opponentHand

    do
        boardMatrix.[2, 5].Open <- true

    let rows = boardMatrix.GetLength 0
    let cols = boardMatrix.GetLength 1
                        
    let getCardIndexes (card : Card) =
        let y = int card.Nominal - 1
        let indexes = 
            [|0..rows - 1|]
            |> Array.map(fun i -> (i, boardMatrix.[i..i, *]))
            |> Array.filter(fun row ->
                snd(row)
                |> Seq.cast<Card>
                |> Seq.where(fun c -> c <> null)
                |> Seq.where(fun el -> el.Open && el.Suit <> card.Suit)
                |> Seq.length = 0)
            |> Array.map(fun row -> (fst(row), y))
        indexes

    new() = Board(Array2D.create<Card> 4 9 null, Array.create 3 null, Array.create 3 null)

    override this.ToString() = 
        let sb = StringBuilder()
        sb.AppendLine "Board:" |> ignore
        for i in [0..rows - 1] do
            for j in [0..cols - 1] do
                let cardString = 
                    match boardMatrix.[i, j] with
                    | null -> "[nil]"
                    | _ -> boardMatrix.[i, j].ToString()
                let isOpenString = 
                    if boardMatrix.[i, j] <> null then
                        if boardMatrix.[i, j].Open then "o " else "c "
                    else ""
                sb.Append $"{cardString + isOpenString, -5}" |> ignore
            sb.Append "\n" |> ignore
        sb.AppendLine "Player's hand" |> ignore
        for card in PlayerHand do
            sb.Append card |> ignore
        sb.AppendLine "\nOpponent's hand" |> ignore
        for card in OpponentHand do
            sb.Append card |> ignore
        sb.ToString()

    interface IGame with
        member this.StaticEvaluation maximizingPlayer =
            let mutable evaluation = 0
            
            for i = 0 to rows - 1 do
                for j = 0 to cols - 1 do
                    let currentCard = boardMatrix.[i, j]
                    if currentCard <> null && currentCard.Open then
                        let adjacentIndexes = getAdjacentIndexes i j rows cols
                        for adjacentIndex in adjacentIndexes do
                            for card in (if maximizingPlayer then PlayerHand else OpponentHand) do
                                if getCardIndexes(card) |> Array.contains adjacentIndex then
                                    evaluation <- evaluation + (if maximizingPlayer then 1 else -1)
            evaluation