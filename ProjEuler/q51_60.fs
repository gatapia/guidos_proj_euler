module Q51_60

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic
open System.Linq

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
  let all = Utils.getAllCombinations("0123456789")

  let isSubStringDivisble str =    
    let primes = [|2;3;5;7;11;13;17|]

    let rec isSubStringDivisbleAux (str:string) idx =      
      if idx = 8 then true
      else
        let n = Int32.Parse(str.Substring(idx, 3))
        if n % primes.[idx - 1] = 0 then isSubStringDivisbleAux str (idx + 1)
        else false
    isSubStringDivisbleAux str 1   

  all.ToArray() |> Array.filter (fun str -> isSubStringDivisble str) |> Array.sumBy(fun str -> Int64.Parse (str))

// Q49: What 12-digit number do you form by concatenating the three 
// terms in this sequence?
let q56 x = 
  let cache = new Dictionary<int64, bool>()  
  let rec fillCache n =
    let prime = Utils.getNextPrimeCached n cache
    if prime >= 9999L then ()
    else fillCache prime
  fillCache 999L
  let allprimes = cache |> Seq.filter(fun kvp -> kvp.Value && kvp.Key > 999L && kvp.Key <= 9999L) |> Seq.map(fun kvp -> kvp.Key) |> Seq.sort |> Seq.toArray

  let rec get3PermutationTerms num idx =        
    if idx = allprimes.Length then []
    else
      let num2 = allprimes.[idx]                  
      if (num <> num2 && Utils.areNumbersPermutations num num2) then 
        let diff = num2 - num
        let num3 = num2 + diff
        if (Utils.areNumbersPermutations num num3) && (Utils.isPrimeCached num3 cache) then [num;num2;num3]
        else get3PermutationTerms num (idx + 1)
      else
        get3PermutationTerms num (idx + 1)
  
  let rec findFirstSetOfPerms i =    
    let prime = allprimes.[i]
    // Ignore primes with repeats digits
    if prime = 1487L then findFirstSetOfPerms (i + 1)
    else
      let perms = get3PermutationTerms prime i
      if perms.Length = 3 && perms.[0] - perms.[1] = perms.[1] - perms.[2] then perms 
      else findFirstSetOfPerms (i + 1)
  
  let perm = findFirstSetOfPerms 0 
  perm.[0].ToString() + perm.[1].ToString() + perm.[2].ToString()
  

// Q44: Find the pair of pentagonal numbers, Pj and Pk, for which their sum 
// and difference is pentagonal and D = |Pk - Pj| is minimised; 
// what is the value of D?
let q57 x = 
  let pents = [for n in 1..10000 -> Utils.getPentagonal n]
  let combinations = seq {
    for p1 in pents do
      for p2 in pents do        
        if p1 < p2 && Utils.isPentagonal (float(p1 + p2)) && Utils.isPentagonal (float(Math.Abs(p2 - p1))) then 
          yield (p1, p2)
  }
  let Ds = combinations |> Seq.map(fun p1p2 -> Math.Abs(snd p1p2 - fst p1p2))
  Ds |> Seq.min

// Q63: How many n-digit positive integers exist which are also an nth power?
let q58 x = 
  let findNDigitNumsThatArePowerOfN n acc =
    let rec findNDigitNumsThatArePowerOfNAux (i:bigint) acc =
      let str = (pown i n).ToString()
      if str.Length > n then acc
      elif str.Length = n then
        findNDigitNumsThatArePowerOfNAux (i + 1I) (acc + 1)
      else findNDigitNumsThatArePowerOfNAux (i + 1I) acc
    findNDigitNumsThatArePowerOfNAux 1I acc
  

  [1..100] |> Seq.map(fun n -> findNDigitNumsThatArePowerOfN n 0) |> Seq.sum

// Q59: Using a brute force attack, can you decrypt the cipher 
// using XOR encryption?
let q59 x : int = 
  let bytes = File.ReadAllText("q59_cipher.txt").Split(',') |> Array.map (fun s -> Int32.Parse(s))
  let decrypt (key:array<int>) = bytes |> Array.mapi(fun i x -> x ^^^ key.[i % key.Length])
  let getEnglishyScore(decrypted:array<int>) = (decrypted |> Array.filter(fun b -> b = 32 || (b >= 65 && b <= 122) || (b >= 48 && b <= 57))).Length
  
  let possibleKeys = new List<array<int>>()
  for c1 in 'a'..'z' do
    for c2 in 'a'..'z' do
      for c3 in 'a'..'z' do
        if c1 <> c2 && c2 <> c3 && c1 <> c3 then          
          possibleKeys.Add([|int(c1);int(c2);int(c3)|])
  
  let englishy = possibleKeys.ToArray() |> Array.map(fun k -> (k, getEnglishyScore (decrypt k))) |> Array.sortBy(fun ks -> 0 - (snd ks))  
  let unencrypted = decrypt (fst englishy.[0])
  // printfn "unencrypted1: %s" (new String((decrypt (fst englishy.[0])) |> Array.map(fun i -> char(if i = 0 then 32 else i))))
  Array.sum unencrypted

// Q92: Investigating a square digits number chain with a surprising property.
let q60 x = 
  let cache = new Dictionary<int, bool>()
  let acc = [|0|]
  let charCache = new Dictionary<char, int>()
  ['0'..'9'] |> List.iter(fun c -> charCache.Add(c, pown (int(c) - int('0')) 2))  
  
  let sum_terms x = x.ToString().ToCharArray() |> Array.sumBy(fun c -> charCache.[c]) 
  
  let rec endsIn89 n (lst:List<int>) =        
    if cache.ContainsKey(n) then 
      let v = cache.[n]
      if v then acc.[0] <- acc.[0] + lst.Count
      lst.ForEach(fun i -> cache.Add(i, v))      
    else
      lst.Add(n)
      if n = 89 then 
        acc.[0] <- acc.[0] + lst.Count
        lst.ForEach(fun i -> cache.Add(i, true))
      elif n = 1 then lst.ForEach(fun i -> cache.Add(i, false))        
      else endsIn89 (sum_terms n) lst   
         
  // Fill Cache
  [2..567] |> List.iter(fun n -> endsIn89 n (new List<int>()))
  
  // Count matching sums
  let filtered = [258..9999999] |> List.filter(fun n -> 
    let sos = sum_terms n
    cache.ContainsKey(sos) && cache.[sos]
  ) 
  (filtered |> List.length) + acc.[0]

