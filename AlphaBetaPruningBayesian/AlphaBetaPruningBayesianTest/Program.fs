open System
open Night.BoardGenerator

[<EntryPoint>]
let main argv =
    BoardGenerator.Generate(4, 7)
    0