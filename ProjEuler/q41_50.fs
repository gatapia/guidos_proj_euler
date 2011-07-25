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

// QXX: 
let q42 x = 10

// QXX: 
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