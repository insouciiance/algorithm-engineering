module public MoveInput

type MoveInput(x : int, y : int, cardIndex : int) =
    member val public X = x with get, set
    member val public Y = y with get, set
    member val public CardIndex = cardIndex with get, set