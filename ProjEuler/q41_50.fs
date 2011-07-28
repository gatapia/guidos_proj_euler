module Q41_50

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic

// Q37: Find the sum of the only eleven primes that are both truncatable 
// from left to right and right to left.
let q41 x = 
  let primeCache = new Dictionary<int64, bool>()

  let rec isLRTruncatable x =
    let xstr = x.ToString();    

    if xstr.Length = 1 then true
    else
      [1..xstr.Length - 1] |> List.forall(
        fun len -> 
          let left = Int64.Parse(xstr.Substring(0, len))
          let right = Int64.Parse(xstr.Substring(xstr.Length - len))
          Utils.isPrimeCached left primeCache && Utils.isPrimeCached right primeCache
        )
  
  let rec getNextLRTruncable x =    
    let x = x + 2L
    // printfn "getNextLRTruncable:%d is prime:%b" x (Utils.isPrimeCached x)
    if (Utils.isPrimeCached x primeCache) && isLRTruncatable x then x
    else getNextLRTruncable x

  let rec getAllLRTruncatablePrimes rem lst prime =
    if rem = 0 then lst
    else 
      let prime = getNextLRTruncable prime
      // printfn "getNextLRTruncable: %d" prime
      let lst = prime::lst
      getAllLRTruncatablePrimes (rem - 1) lst prime
  
  getAllLRTruncatablePrimes 11 [] 7L |> List.sum

// Q39: For which value of p  1000, is the number of solutions maximised?
let q42 x = 
  let getNumberOfSolutionsForPerimeter perimeter =
    let rec getNumberOfSolutionsForPerimeterAux acc hyp adj =
      if hyp = perimeter - 2 then acc
      else 
        let opp = perimeter - (hyp + adj)
        if (opp > adj) then getNumberOfSolutionsForPerimeterAux acc hyp (adj + 1)
        else
          // printfn "perimeter: %d hyp: %d adj: %d opp: %d" perimeter hyp adj opp
          let acc = if (pown hyp 2) = (pown opp 2) + (pown adj 2) then acc + 1 else acc        
          let finishedIter = (adj >= hyp - 1)
          let adj = if finishedIter then 1 else adj + 1
          let hyp = if finishedIter then hyp + 1 else hyp
          getNumberOfSolutionsForPerimeterAux acc hyp adj
        
    getNumberOfSolutionsForPerimeterAux 0 2 1
    
  let perimeters = [4..1000]
  let pss = perimeters |> List.map(fun p -> p, getNumberOfSolutionsForPerimeter p)
  let max = pss |> List.maxBy(fun ps -> snd ps)
  fst max

// Q33: If the product of these four fractions is given in its lowest common 
// terms, find the value of the denominator
let q43 x = 10

// QXX: 
let q44 x = 10

// QXX: 
let q45 x = 10

// QXX: 
let q46 x = 10

// QXX: 
let q47 x = 10

// Q41: 
let q48 x = 10

// QXX: 
let q49 x = 10

// QXX: 
let q59 x = 10