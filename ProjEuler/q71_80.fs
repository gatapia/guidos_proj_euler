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
  let cache = new Dictionary<string, (int64 * List<int64>)>()
  let cacheKeys = List<string>()

  let compareKeys (primeNum:string) (key:string) (cacheKey:string) =    
    key = cacheKey || Utils.forall3str(fun key cached prime -> key = cached || prime = cached) key cacheKey primeNum

  let getPrimesTuppleForKey p pstr key =
    let matches = cacheKeys.Where(fun k -> compareKeys pstr key k).Select(fun m -> cache.[m])
    if matches.Any() then matches.ToArray()
    else 
      let lst = new List<int64>()
      let tp = (p, lst)
      cacheKeys.Add(key)
      cache.Add(key, tp)
      [|tp|]    
        
  let testKey p pstr key : int64 =
    let tupples = getPrimesTuppleForKey p pstr key
    let rec testKeyAux idx =      
      if idx = tupples.Length then 0L
      else
        let (op:int64), (lst:List<int64>) = tupples.[idx]
        lst.Add(p)
        if lst.Count = 8 then op
        else testKeyAux (idx + 1)

    testKeyAux 0

  let rec doCharIdx p (added:Dictionary<char, bool>) (str:string) (chars:array<char>) idx : int64 =
    if idx = chars.Length then 0L
    else
      let c = chars.[idx]
      if added.ContainsKey(c) then doCharIdx p added str chars (idx + 1)
      else
        added.Add(c, true)
        let cCount = (chars |> Array.filter(fun c2 -> c2 = c)).Length
        if cCount = 3 then 
          let found = testKey p str (str.Replace(c, '*'))
          if found > 0L then found
          else doCharIdx p added str chars (idx + 1)
        else doCharIdx p added str chars (idx + 1)

  let rec q71Aux last =    
    let p = Utils.getNextPrime last      

    let added = new Dictionary<char, bool>()
    let str = p.ToString()
    let chars = str.ToCharArray()
      
    let found = doCharIdx p added str chars 0      
    if found > 0L then found
    else q71Aux p
  
  q71Aux 56002L

// Q112: Find the least number for which the proportion of bouncy numbers is 
// exactly 99%.
let q72 x =     
  let isBouncy i =
    let chars = i.ToString().ToCharArray()
    let pairs = chars |> Seq.pairwise
    pairs |> Seq.exists(fun (a, b) -> a > b) && 
        pairs |> Seq.exists(fun (a, b) -> a < b)

  let rec q72Aux i b =
    let ni = i + 1.0
    let nb = if isBouncy ni then b + 1.0 else b
    if (nb / ni) = 0.99 then ni
    else q72Aux ni nb
  q72Aux 21780.0 (21780.0 * 0.9)

// Q206: Find the unique positive integer whose square has the 
// form 1_2_3_4_5_6_7_8_9_0, where each “_” is a single digit.
let q73 x = 
  let tmpl = "1_2_3_4_5_6_7_8_9_0"

  let testNum n =
    let nstr = (n * n).ToString()
    nstr.Chars(0) = '1' && 
      nstr.Chars(2) = '2' &&
      nstr.Chars(4) = '3' &&
      nstr.Chars(6) = '4' &&
      nstr.Chars(8) = '5' &&
      nstr.Chars(10) = '6' &&
      nstr.Chars(12) = '7' &&
      nstr.Chars(14) = '8' &&
      nstr.Chars(16) = '9' &&
      nstr.Chars(18) = '0'

  let rec q73Aux n =
    if n > 1389026623L then failwith "Err"
    elif testNum n then n
    else q73Aux (n + 1L)    
  q73Aux 1010101010L

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

printfn "q71: %s" ((q71 "").ToString())