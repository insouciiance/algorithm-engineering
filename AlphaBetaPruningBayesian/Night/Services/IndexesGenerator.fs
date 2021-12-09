module IndexesGenerator

let getAdjacentIndexes x y rows cols =
    let mutable adjacentIndexes = List.empty
    if x > 0 then
        adjacentIndexes <- [
        yield! adjacentIndexes; 
        (x - 1, y)]

        if y > 0 then
            adjacentIndexes <- [
            yield! adjacentIndexes; 
            (x, y - 1); 
            (x - 1, y - 1)]

            if x + 1 < rows then
                adjacentIndexes <- [
                yield! adjacentIndexes; 
                (x + 1, y); 
                (x + 1, y - 1)]

                if y + 1 < cols then
                    adjacentIndexes <- [
                    yield! adjacentIndexes; 
                    (x, y + 1); 
                    (x + 1, y + 1);
                    (x - 1, y + 1)]
    Array.ofList adjacentIndexes