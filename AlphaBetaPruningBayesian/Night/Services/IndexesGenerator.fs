namespace Night.Services

module internal IndexesGenerator = 
    let getAdjacentIndexes x y rows cols =
        let mutable adjacentIndexes = List.empty
        if x > 0 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x - 1, y)]
        if y > 0 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x, y - 1)]
        if x > 0 && y > 0 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x - 1, y - 1)]
        if x < rows - 1 && y < cols - 1 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x + 1, y + 1)]
        if x < rows - 1 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x + 1, y)]
        if y < cols - 1 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x, y + 1)]
        if x > 0 && y < cols - 1 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x - 1, y + 1)]
        if x < rows - 1 && y > 0 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x + 1, y - 1)]
        Array.ofList adjacentIndexes