module Q71_80

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic
open System.Linq

// Q51: Find the smallest prime which, by changing the same part of the number, 
// can form eight different primes.
let q71 x = 
  // Start: 56003
  // Need to look for primes with repeats
  let cache = new Dictionary<string, (int64 * int)>()
    
  let rec q71Aux i last =
    if i % 100000 = 0 then printfn "PRIME: %d" last
    if i > 100000000 then failwithf "Got to iteration: %d last prime: %d" i last
    else 
      let p = Utils.getNextPrime last
      let added = new Dictionary<char, bool>()
      let str = p.ToString()
      let chars = str.ToCharArray()
      
      let mutable found = 0L
      for c in chars do
        if not (added.ContainsKey(c)) then
          added.Add(c, true)
          let cCount = (chars |> Array.filter(fun c2 -> c2 = c)).Length
          if cCount > 1 then
            let str = str.Replace(c, '*')
            if (cache.ContainsKey(str)) then 
              let existing = cache.[str]
              let newcount = (snd existing) + 1
              if newcount > 6 then printfn "str: %s count: %d" str newcount
              if newcount = 8 then found <- (fst existing)
              else cache.[str] <- (fst existing, newcount)
            else cache.Add(str, (p, 1))
      if found > 0L then found
      else q71Aux (i + 1) p
  
  q71Aux 0 56002L


// Q
let q72 x = 
  10

// Q
let q73 x = 
  10

// Q
let q74 x = 
  10

// Q
let q75 x = 
  10

// Q
let q76 x = 
  10

// Q
let q77 x = 
  10

// Q
let q78 x = 
  10

// Q
let q79 x = 
  10

// Q
let q80 x = 
  10