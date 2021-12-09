namespace Night

open AlphaBetaPruningBayesian
open AdjacentIndexesGenerator

type public Board(boardMatrix : Card[,], playerHand : Card[], opponentHand: Card[]) =
    let mutable Board = boardMatrix
    let mutable PlayerHand = playerHand
    let mutable OpponentHand = opponentHand

    let rows = boardMatrix.GetLength 0
    let cols = boardMatrix.GetLength 1
                        
    let getCardIndexes (card : Card) =
        let y = int card.Nominal
        let indexes = 
            [|0..rows - 1|]
            |> Array.map(fun i -> (i, boardMatrix.[i..i, *]))
            |> Array.filter(fun row -> snd(row)
                                      |> Seq.cast<Card>
                                      |> Seq.where(fun el -> el.Open && el.Suit <> card.Suit)
                                      |> Seq.length = 0)
            |> Array.map(fun row -> (fst(row), y))
        indexes

    new() = Board(Array2D.create<Card> 4 7 null, Array.create 3 null, Array.create 3 null)

    interface IGame with
        member this.StaticEvaluation maximizingPlayer =
            let mutable evaluation = 0
            
            for i = 0 to rows - 1 do
                for j = 0 to cols - 1 do
                    if boardMatrix.[i, j].Open then
                        let adjacentIndexes = getAdjacentIndexes i j rows cols
                        for adjacentIndex in adjacentIndexes do
                            for card in (if maximizingPlayer then PlayerHand else OpponentHand) do
                                if getCardIndexes(card) |> Array.contains adjacentIndex then
                                    evaluation <- evaluation + (if maximizingPlayer then 1 else -1)
            evaluation