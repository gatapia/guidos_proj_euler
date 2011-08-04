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
  let cache = new Dictionary<string, (int64 * List<int64>)>()

  let compareKeys (pstr:string) (k1:string) (k2:string) =    
    if k1.Length <> k2.Length then false
    else       
      let kchars = k1.ToCharArray()
      let cacheChars = k2.ToCharArray()
      let pchars = pstr.ToCharArray()

      Utils.forall3(fun key cached prime -> key = cached || prime = cached) kchars cacheChars pchars 

  let getPrimesTuppleForKey p pstr key =
    let matches = cache.Keys |> Seq.filter(fun k -> compareKeys pstr key k)
    if matches.Count() > 0 then matches |> Seq.map(fun m -> cache.[m]) |> Seq.toList
    else 
      let lst = new List<int64>()
      let tp = (p, lst)
      cache.Add(key, tp)
      [tp]
    
        
  let testKey p pstr key =
    let rec testKeyAux tupples =      
      match tupples with
      | head :: tail ->
        let op, (lst:List<int64>) = head
        lst.Add(p)
        if lst.Count = 8 then failwithf "FOUND YOU CNT!!! [%d] PRIMES: %A" op lst    
        testKeyAux tail
      | [] -> ()

    testKeyAux (getPrimesTuppleForKey p pstr key)
    

  let rec doCharIdx p (added:Dictionary<char, bool>) (str:string) (chars:array<char>) idx =
    if idx = chars.Length then ()
    else
      let c = chars.[idx]
      if added.ContainsKey(c) then doCharIdx p added str chars (idx + 1)
      else
        added.Add(c, true)
        let cCount = (chars |> Array.filter(fun c2 -> c2 = c)).Length
        if cCount > 2 then testKey p str (str.Replace(c, '*'))
        doCharIdx p added str chars (idx + 1)

  let rec q71Aux i last =    
    if i > 100000000 then failwithf "Got to iteration: %d last prime: %d" i last

    let p = Utils.getNextPrime last      
    if p > 929399L then failwith "Did not match actual list"

    if i % 100000 = 0 then printfn "LAST: %d THIS: %d" last p
    let added = new Dictionary<char, bool>()
    let str = p.ToString()
    let chars = str.ToCharArray()
      
    doCharIdx p added str chars 0      
    q71Aux (i + 1) p
  
  q71Aux 0 56002L
  100


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