namespace AlphaBetaPruningBayesian

open System
open System.Linq

type public GameTree<'T when 'T :> IGame>
    (initialState : 'T, 
    childStatesGenerator : 'T -> 'T[], 
    randomStatesGenerator : 'T -> 'T[]) = 
    member val public CurrentState = 
        initialState
        with get, set
       
    member val public ChildStatesGenerator = 
        childStatesGenerator
        with get, set

    member val public RandomStatesGenerator = 
        randomStatesGenerator
        with get, set

    member public this.HasChildStates() = 
        (this.ChildStatesGenerator this.CurrentState).Length > 0

    member public this.FindBestMove(depth) =
        let childStates = this.ChildStatesGenerator this.CurrentState

        if childStates.Length = 0 then
            Unchecked.defaultof<'T>
        else
            let mutable bestEvaluation = Int32.MaxValue;
            let mutable bestMove = childStates.[0]

            for childState in childStates do
                let mutable totalEvaluation = 0
                let randomStates = (this.RandomStatesGenerator  childState).Take 1000

                for randomState in randomStates do
                    let miniMaxValue = this.MiniMax(randomState, depth - 1, true, Int32.MinValue, Int32.MaxValue)
                    totalEvaluation <- totalEvaluation + miniMaxValue

                if totalEvaluation < bestEvaluation then
                    bestEvaluation <- totalEvaluation
                    bestMove <- childState

            bestMove

    member public this.Run(depth, maximizingPlayer) =
        this.MiniMax(this.CurrentState, depth, maximizingPlayer, Int32.MinValue, Int32.MaxValue)

    member private this.MiniMax(state, depth, maximizingPlayer, alpha, beta) = 
        let childStates = this.ChildStatesGenerator state

        if depth <= 0 || childStates.Length = 0 then
            state.StaticEvaluation()
        else 
            let mutable miniMaxValue = if maximizingPlayer then Int32.MinValue else Int32.MaxValue

            let mutable newAlpha = alpha
            let mutable newBeta = beta
        
            let mutable valueFound = false

            for childState in childStates do
                if not valueFound then
                    let childMiniMax = this.MiniMax(childState, depth - 1, not maximizingPlayer, alpha, beta)

                    if maximizingPlayer then
                        miniMaxValue <- max miniMaxValue childMiniMax

                        newAlpha <- max newAlpha childMiniMax
                    else
                        miniMaxValue <- min miniMaxValue childMiniMax

                        newBeta <- min beta childMiniMax

                    if newBeta <= newAlpha then
                        valueFound <- true

            miniMaxValue
                
