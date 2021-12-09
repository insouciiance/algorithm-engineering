namespace Night

open System

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

    type public BoardGenerator() = 
        static member Generate() =
            let mutable boardArray = Array2D.create<Card> 4 9 null
            let mutable cardsArray = Array.create<Card> 36 null
            
            for i = 0 to 8 do
                for j = 0 to 3 do
                    cardsArray.[i * 4 + j] <- Card(enum i, enum j)
            
            cardsArray <- shuffle cardsArray

            for i = 0 to 3 do
                for j = 1 to 7 do
                    boardArray.[i, j] <- cardsArray.[i * 7 + j]

            let playerHand = 
                last 4 cardsArray
                |> Seq.toArray

            let aiHand = 
                last 8 cardsArray
                |> Seq.take 4
                |> Seq.toArray

            Board(boardArray, playerHand, aiHand)

        static member GenerateChildren(board : Board) = 
            null


            


