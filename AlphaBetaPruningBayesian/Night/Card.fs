namespace Night

open System

[<AllowNullLiteral>]
type public Card(nominal : CardNominal, suit : CardSuit) = 
    member val public Nominal = nominal with get
    member val public Suit = suit with get
    member val public Open = false with get, set

    interface IEquatable<Card> with
        member this.Equals(other) =
            this.Nominal = other.Nominal && this.Suit = other.Suit

    override this.ToString() =
        this.Nominal.ToString() + " of " + this.Suit.ToString()