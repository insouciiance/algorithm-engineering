namespace Night.Services

open System
open Night
open IndexesGenerator

module public BoardGenerator = 
    let rng = new Random()
    
    let shuffle (org:_[]) = 
        let arr = Array.copy org
        let max = (arr.Length - 1)
        let randomSwap (arr:_[]) i =
            let pos = rng.Next(max)
            let tmp = arr.[pos]
            arr.[pos] <- arr.[i]
            arr.[i] <- tmp
            arr
        [|0..max|] |> Array.fold randomSwap arr

    let last n xs = 
        xs 
        |> Seq.skip (Seq.length xs - n)

    let getAllCards() = 
        let mutable cardsArray = Array.create<Card> 36 null
            
        for i = 0 to 8 do
            for j = 0 to 3 do
                cardsArray.[i * 4 + j] <- Card(enum i, enum j)
        cardsArray

    let rec comb n l = 
        match n, l with
        | 0, _ -> [[]]
        | _, [] -> []
        | k, (x::xs) -> List.map ((@) [x]) (comb (k - 1) xs) @ comb k xs

    type public BoardGenerator() = 
        static member Generate() =
            let mutable boardArray = Array2D.create<Card> 4 9 null

            let cardsArray = shuffle (getAllCards())

            for i = 0 to 3 do
                for j = 1 to 7 do
                    boardArray.[i, j] <- cardsArray.[i * 7 + j - 1]

            let playerHand = 
                last 4 cardsArray
                |> Seq.toArray
            
            for card in playerHand do
                card.Open <- true

            let aiHand = 
                last 8 cardsArray
                |> Seq.take 4
                |> Seq.toArray

            Board(boardArray, playerHand, aiHand)

        static member GenerateHandPredictions(board : Board) = 
            let possiblePlayerCards = 
                board.Board
                |> Seq.cast<Card>
                |> Seq.filter(fun c -> c <> null)
                |> Seq.filter(fun c -> not c.Open)
                |> Seq.append board.PlayerHand
                |> List.ofSeq
            let possiblePlayerHands = 
                comb 4 possiblePlayerCards
                |> Array.ofList
                |> Array.map (fun hand -> Array.ofList hand)
                |> Array.map (fun hand -> Board(board.Board, hand, board.OpponentHand))
            possiblePlayerHands

        static member GenerateChildBoards(board: Board) = 
            let mutable childBoards = []
            let mutable possibleIndexes = []
            for i in [0..board.Rows - 1] do
                for j in [0..board.Cols - 1] do
                    if board.Board.[i, j] <> null && board.Board.[i, j].Open then
                        let adjacentIndexes = getAdjacentIndexes i j board.Rows board.Cols
                        possibleIndexes <- List.distinct (possibleIndexes @ (adjacentIndexes |> List.ofArray))
                    elif board.Board.[i, j] = null && j > 0 && j < board.Cols - 1 then
                         possibleIndexes <- List.distinct (possibleIndexes @ ([(i, j)]))

            if possibleIndexes.Length = 0 then
                for i in [0..board.Rows - 1] do
                    for j in [0..board.Cols - 1] do
                        possibleIndexes <- possibleIndexes @ [(i, j)]
                            
            for card in board.OpponentHand do
                for cardIndex in board.GetCardIndexes card do
                    for possibleIndex in possibleIndexes do
                        if cardIndex = possibleIndex then
                            let boardCard = board.Board.[fst(possibleIndex), snd(possibleIndex)]
                            if boardCard = null || not boardCard.Open then
                                let childBoard = (board :> ICloneable).Clone() :?> Board
                                let temp = childBoard.Board.[fst(possibleIndex), snd(possibleIndex)]
                                let handCardIndex = 
                                    childBoard.OpponentHand
                                    |> Array.findIndex(fun c -> (c :> IEquatable<Card>).Equals card)
                                childBoard.Board.[fst(possibleIndex), snd(possibleIndex)] <- childBoard.OpponentHand.[handCardIndex]
                                childBoard.Board.[fst(possibleIndex), snd(possibleIndex)].Open <- true
                                if temp <> null then
                                    childBoard.OpponentHand.[handCardIndex] <- temp
                                else 
                                    childBoard.OpponentHand <- 
                                        childBoard.OpponentHand
                                        |> Array.filter(fun c -> not ((c :> IEquatable<Card>).Equals card))
                                childBoards <- [yield! childBoards; childBoard]
            if childBoards.Length > 0 then
                Array.ofList childBoards
            else
                for i in [0..board.Rows - 1] do
                    for j in [1..board.Cols - 2] do
                        let childBoard = (board :> ICloneable).Clone() :?> Board
                        let cardToTake = childBoard.Board.[i, j]
                        if cardToTake <> null && not cardToTake.Open then
                            childBoard.Board.[i, j] <- null
                            childBoard.OpponentHand <- 
                                childBoard.OpponentHand
                                |> Array.append [| cardToTake |]
                            childBoards <- [yield! childBoards; childBoard]
                Array.ofList childBoards