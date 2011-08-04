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
  let cache = new Dictionary<string, (int64 * List<int64>)>()
  let found = [|0L|]

  let testKey p key =
    if (cache.ContainsKey(key)) then 
      let cp, lst = cache.[key]
      lst.Add(p)
      if lst.Count > 6 then printfn "key: %s primes: %A" key lst
      if lst.Count = 8 then failwithf "FOUND YOU CNT!!! [%d] PRIMES: %A" cp lst
    else 
      let lst = new List<int64>()
      lst.Add(p)
      cache.Add(key, (p, lst))
  
  let rec testCharSeq p (str:string) sidx =
    let idx = str.IndexOf('*', sidx)              
    let testCharSeqImpl nidx =
      if nidx < 0 || nidx = str.Length then ()
      else 
        let keyChars2 = str.ToCharArray()                
        keyChars2.[idx] <- keyChars2.[nidx]
        testKey p (new String(keyChars2))

    testCharSeqImpl (idx - 1)
    testCharSeqImpl (idx + 1)
    testCharSeqImpl (idx)
    
    testCharSeq p str idx

  let rec doCharIdx p (added:Dictionary<char, bool>) (str:string) (chars:array<char>) idx =
    if idx = chars.Length then ()
    else
      let c = chars.[idx]
      if added.ContainsKey(c) then doCharIdx p added str chars (idx + 1)
      else
        added.Add(c, true)
        let cCount = (chars |> Array.filter(fun c2 -> c2 = c)).Length
        if cCount > 1 then
          let key = str.Replace(c, '*')
          testCharSeq p key 0

  let rec q71Aux i last =    
    if i > 100000000 then failwithf "Got to iteration: %d last prime: %d" i last

    let p = Utils.getNextPrime last      

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