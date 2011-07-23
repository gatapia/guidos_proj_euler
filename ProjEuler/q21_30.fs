module Q21_30

open System
open System.Collections
open System.Collections.Generic

// Q21: Evaluate the sum of all the amicable numbers under 10000
let q21 x = 
  let lst = [1..9999]
  let sums = [for i in [1..9999] -> (i, Utils.sumDivisors i)]
  let amicables = sums |> List.filter (fun x -> fst x <> snd x && Utils.sumDivisors(snd x) = (fst x))
  printfn "%A" amicables
  amicables |> List.sumBy (fun x -> fst x)

// Q22: If all the numbers from 1 to 1000 (one thousand) inclusive were 
// written out in words, how many letters would be used?  
let q22 x = [1..1000] |> List.map Utils.numToWord |> List.sumBy (fun w -> w.Replace(" ", "").Length)

