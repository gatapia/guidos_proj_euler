module Q51_60

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic

// Q79: By analysing a user's login attempts, can you determine the secret 
// numeric passcode?
let q51 x = 
  let keylogs = "319,680,180,690,129,620,762,689,762,318,368,710,720,710,629,168,160,689,716,731,736,729,316,729,729,710,769,290,719,680,318,389,162,289,162,718,729,319,790,680,890,362,319,760,316,729,380,319,728,716".Split(',') |> Array.map(fun str -> str.ToCharArray())
  // We now know there is no repeated numbers
  // let hasRepeatNums = keylogs |> Array.filter (fun l -> l.ToCharArray() |> Seq.distinct |> Seq.length <> 3)
  let lst = new List<char>(keylogs |> Array.collect(fun arr -> arr) |> Seq.distinct)
 
  let analyse (combination:array<char>) =          
    let swapitems i1 i2 =
      let tmp = lst.Item(i1)
      lst.Item(i1) <- lst.[i2]
      lst.Item(i2) <- tmp
      true

    let idx1 = lst.IndexOf(combination.[0])
    let idx2 = lst.IndexOf(combination.[1])   
    let idx3 = lst.IndexOf(combination.[2])
    
    if idx2 > idx3 then swapitems idx2 idx3
    elif idx1 > idx2 then swapitems idx1 idx2
    else false
  
  while (keylogs |> Array.exists(fun comb -> analyse comb)) do ()  

  int64(new String(lst.ToArray()))

// Q50: Which prime, below one-million, can be written as the sum of the most 
// consecutive primes?

let q52 x = 
  let cache = new Dictionary<int64, bool>()  

  let isKnownPrime n = cache.ContainsKey n && cache.[n]

  let rec fillCache n =
    let prime = Utils.getPrevPrimeCached n cache
    if prime = 2L then () else fillCache prime
  fillCache 1000000L
  cache.Add(2L, true)
  
  let cache = cache |> Seq.filter(fun kvp -> kvp.Value) |> Seq.map (fun kvp -> kvp.Key)
  let primes = cache |> Seq.sort |> Seq.toList

  let primesAndTerms = new List<int64 * int>()
  let rec sumNextIsPrime acc n =    
    if n >= primes.Length then ()

    let prime = primes.Item(n - 1)  
    let acc = acc + prime    
    // printfn "prime: %d acc: %d" prime acc

    if acc > 1000000L then ()
    else 
      if n > 20 && isKnownPrime acc then 
        // printfn "terms: %d prime: %d" n acc
        primesAndTerms.Add(acc, n)    
      sumNextIsPrime acc (n + 1)
  
  // Starting at the 1st to 10th prime
  ignore ([for i in [1..10] -> sumNextIsPrime 0L i])   
  fst (primesAndTerms |> Seq.maxBy(fun pt -> snd pt))

// Q47: Find the first four consecutive integers to have four distinct 
// primes factors. What is the first of these numbers?
let q53 x = 
  let cache = new Dictionary<int64, bool>()  
  let has4distinctPrimes n = 
    let factors = Utils.getAllDivisors n false |> List.filter (fun f -> Utils.isPrimeCached (int64 f) cache)
    factors.Length = 4
  
  let rec q53Aux nxt (lst:list<int>) =        
    if has4distinctPrimes nxt then
     if lst.Length > 2 then printfn "has4distinctPrimes nxt: %d list: %d" nxt lst.Length
     let lst = nxt :: lst
     if lst.Length = 4 then lst
     else q53Aux (nxt + 1) lst
    else q53Aux (nxt + 1) []

  let lst = q53Aux 134000 []  
  lst.Item(lst.Length - 1)

// Q46: What is the smallest odd composite that cannot be written as the 
// sum of a prime and twice a square?
let q54 x = 
  let cache = new Dictionary<int64, bool>()  
  let isGoldbach x =
    let rec isGoldbachAux prime =
      let rem = (x - prime) / 2L      
            
      let sq = sqrt(float(rem)) 
      if sq = float(int(sq)) then true
      else 
        if prime = 2L then false
        else isGoldbachAux (Utils.getPrevPrimeCached prime cache)
    
    Utils.isPrimeCached x cache || isGoldbachAux (Utils.getPrevPrimeCached x cache)
  
  let rec findFirstOddNotGoldbach odd =
    if isGoldbach odd then findFirstOddNotGoldbach (odd + 2L)
    else odd
  
  findFirstOddNotGoldbach 33L

// Q43: Find the sum of all pandigital numbers with an unusual sub-string 
// divisibility property.
let q55 x = 
  10

// QX: xxx
let q56 x = 
  10

// QX: xxx
let q57 x = 
  10

// QX: xxx
let q58 x = 
  10

// QX: xxx
let q59 x = 
  10

// QX: xxx
let q60 x = 
  10
