module Q61_70

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic
open System.Linq

// Q57: In the first one-thousand expansions, how many fractions 
// contain a numerator with more digits than denominator?
let q61 x = 
  let sqrt2 num den =
    let newNum = num + den * 2I
    let newDen = den * 2I + (num - den)
    (newNum, newDen)
  
  let rec sqrt2Aux n d lst =
    let lst = (n,d)::lst
    if (lst.Length = 1000) then lst
    else
      let nd = sqrt2 n d
      sqrt2Aux (fst nd) (snd nd) lst
  
  let expansions = sqrt2Aux 3I 2I [] |> List.rev
  (expansions |> List.filter(fun nd -> (fst nd).ToString().Length > (snd nd).ToString().Length)).Length

// Q58: Investigate the number of primes that lie on the diagonals of 
// the spiral grid.
// SLOW!!!
let q62 x =
  // Copied from q 28
  let rec q62Aux sidelength accnp accp (num:int) direction x y =    
    let isp = (x = y || x = -y) && Utils.isPrime (int64 num)    
    let diagaccnp, diagaccp = if isp then accnp, (accp + 1.0) else (accnp + 1.0), accp        

    if x <= 0 && x = y then q62Aux sidelength diagaccnp diagaccp (num + 1) 6 (x + 1) (y) 
    elif x < 0 && -x = y then q62Aux sidelength diagaccnp diagaccp (num + 1) 8 (x) (y - 1)
    elif y < 0 && x = -y then // Spiral completed
      let perc = (accp * 100.0 / (diagaccnp + diagaccp))
      // printfn "perc:%f num:%d sidelength:%d" perc num sidelength 
      if (perc < 10.0) then sidelength 
      else q62Aux (sidelength + 2) diagaccnp diagaccp (num + 1) 2 (x + 1) (y)    
    elif x = y then q62Aux sidelength diagaccnp diagaccp (num + 1) 4 (x - 1) (y)
    else 
      let newx = if num <> 2 && direction = 6 then x + 1 elif direction = 4 then x - 1 else x
      let newy = if num = 2 || direction = 2 then y + 1 elif direction = 8 then y - 1 else y
      q62Aux sidelength accnp accp (num + 1) direction newx newy

  q62Aux 3 0.0 0.0 1 6 0 0

// Q69: Find the value of n ≤ 1,000,000 for which n/φ(n) is a maximum.
// TOO SLOW!!!!!!!!!!!
let q63 x =   
  let phi x2 =
    // This algorithm copied from http://www.velocityreviews.com/
    // forums/t459467-computing-eulers-totient-function.html
    // Boooooooooooooo
    let mutable x = x2
    let mutable ret = 1
    let mutable i = 2
    let mutable pow = 0

    while x <> 1 do  
      pow <- 1
      while (x % i = 0) do
        x <- x / i
        pow <- pow * i    
      ret <- ret * (pow - (pow/i))    
      i <- i + 1
    ret  
  
  let nscore = [2.0..520000.0] |> List.map(fun n -> (n, (n / float(phi (int n)))))
  fst (nscore |> List.maxBy(fun ns -> snd ns))  

// Q99: Which base/exponent pair in the file has the greatest numerical value?
let q64 x = 
  let spAndParse = Array.map(fun (l:string) -> l.Split(',') |> Array.map(fun p -> Double.Parse(p)))
  let lines = File.ReadAllLines("q64_base_exp.txt") |> spAndParse
  let rec findMax curri maxval maxi =
    if lines.Length = curri then maxi + 1
    else
      let line = lines.[curri]
      let b, e = line.[0], line.[1]
      let lval = e * log (b)
      if lval > maxval then findMax (curri + 1) lval curri
      else findMax (curri + 1) maxval maxi

  findMax 0 0.0 0

// Q81: Find the minimal path sum from the top left to the bottom right by 
// moving right and down.:
let q65 x = 
  let matrix = File.ReadAllLines("q65_matrix.txt") |> Array.map(fun l -> l.Split(',') |> Array.map (fun t -> Int64.Parse t))
  let scores = Array.init matrix.Length (fun y -> Array.init matrix.[0].Length (fun x -> Int64.MaxValue))
  
  let rec populateScores x y =
    if y = scores.Length then ()
    elif x = scores.[y].Length then populateScores 0 (y + 1)
    elif x = 0 && y = 0 then
      scores.[x].[y] <- matrix.[x].[y]
      populateScores (x + 1) y
    else
      let s1 = if x = 0 then Int64.MaxValue else scores.[x - 1].[y]
      let s2 = if y = 0 then Int64.MaxValue else scores.[x].[y - 1]
      scores.[x].[y] <- (matrix.[x].[y] + min s1 s2)
      populateScores (x + 1) y    
  populateScores 0 0
  scores.[79].[79]

////////////////////////////////////////////////////////////////////////////////
// q66 POKER
////////////////////////////////////////////////////////////////////////////////
let getCardValue c =
  match c with
  | 'A' -> 14
  | 'K' -> 13
  | 'Q' -> 12
  | 'J' -> 11
  | 'T' -> 10
  | _ -> int (c) - int('0')
    

let sumLoseCards (cards:seq<int * int>) =
  let getValues = Seq.map(fun vc -> fst vc) >> Seq.sort >> Seq.toList
  let values = cards |> getValues
  let sumScores = List.mapi(fun i v -> v + (pown v i)) >> List.sum
  values |> sumScores

let scoreHand (hand:array<string>) =
  let values = hand |> Array.map(fun c -> c.Chars(0)) |> Array.sort
  let valuesAsNum = values |> Array.map(fun v -> getCardValue v) |> Array.sort
  let valueCounts = valuesAsNum |> Seq.distinct |> Seq.map(fun v -> v, valuesAsNum.Count(fun v2 -> v2 = v))
  let suits = hand |> Array.map(fun c -> c.Chars(1))
  let isSameSuit = (suits |> Seq.distinct) |> Seq.length = 1
  let isStraight = valueCounts.Count() = 5 &&  valuesAsNum.Last() - valuesAsNum.First() = 4
    
  // Royaly
  if isStraight && isSameSuit && valuesAsNum.Last() = 14 then 
    Int32.MaxValue
  // Straight Flush
  elif isStraight && isSameSuit then 
    100000000 + valuesAsNum.Last()
  else
    // Four of a Kind
    let fourOfkind = valueCounts.Where(fun vc -> snd vc = 4) 
    if fourOfkind.Count() = 1 then 
      90000000 + (100 * fst (fourOfkind.First())) + fst (valueCounts.Single(fun vc -> snd vc = 1))
    // Full house
    elif (Seq.length valueCounts) = 2 && valueCounts.Where(fun vc -> snd vc = 3).Count() = 1 then 
      let sorted = valueCounts |> Seq.sortBy(fun vc -> snd vc)
      80000000 + (fst (sorted.First())) + (fst (sorted.ElementAt(1)) * 100)
    // Flush
    elif isSameSuit then 
      70000000 + valuesAsNum.Last()
    // Straight
    elif isStraight then 
      60000000 + valuesAsNum.Last()
    // 3 of a kind
    elif valueCounts.Where(fun vc -> snd vc = 3).Count() = 1 then
      let highest = valueCounts.SingleOrDefault(fun vc -> snd vc = 3)
      50000000 + (fst highest * 100000) + sumLoseCards (valueCounts.Where(fun vc -> snd vc = 1))
    // 2 Pair
    elif valueCounts.Count(fun vc -> snd vc = 2) = 2 then
      let remScores = valueCounts |> Seq.sumBy(fun vc -> snd vc * 100 * fst vc)
      40000000 + remScores
    // 1 Pair
    else      
      let pair = valueCounts.Where(fun vc -> snd vc = 2)
      if pair.Count() = 1 then 
        30000000 + (fst (pair.First()) * 100000) + sumLoseCards (valueCounts.Where(fun vc -> snd vc = 1))
      else 
        sumLoseCards (valueCounts)

// Q54: How many hands did player one win in the game of poker?
let q66 x = 
  let hands = File.ReadAllLines("q66_poker.txt") |> Array.map(fun l -> l.Split(' '))  
  
  let player1winningHands =
    let split = hands |> Array.map(fun both -> (both |> Seq.take(5) |> Seq.toArray, both |> Seq.skip(5) |> Seq.toArray))    
    let winning = split |> Array.map(fun hands -> if scoreHand(fst hands) > scoreHand(snd hands) then 1 else 0)
    winning |> Array.sum
    
  player1winningHands

// Q71: Listing reduced proper fractions in ascending order of size.
// TOO SLOW (12 min) AND WRONG ANSWER (2)
let q67 x =     
  let maxD = 1000000L
  let target = float(3) / float(7)
    
     
  let findClosestLessThanHCFWithDenenom d =        
    // if (d % 10000L = 0L) then printfn "findClosestLessThanHCFWithDenenom d:%d" d
    let getValueOfNDPair n = float(n)/float(d)

    let rec findNextLowestProperFunction n =
      if Utils.euclidianHCF n d <> 1L then 
        if (n = 1L) then (0L, 0.0)
        else findNextLowestProperFunction (n - 1L)
      else (n, getValueOfNDPair n)
    
    // Binary search style search for closes value
    let rec findClosestLessThanHCFWithDenenomAux low hi curr =            
      let mid = low + ((hi - low) / 2L)          

      if (hi < low || mid = low || mid = hi) then curr
      else                   
        let v = getValueOfNDPair mid        
        if v < target then findClosestLessThanHCFWithDenenomAux mid hi (mid, v)
        else findClosestLessThanHCFWithDenenomAux low mid (mid, v)
    let curr = ((d/2L), getValueOfNDPair (d / 2L))
    
    let closest = findClosestLessThanHCFWithDenenomAux 1L d curr
    findNextLowestProperFunction (fst closest)
    // Now we need to find on that is a proper reduced fraction

  let closest = [2L..maxD] |> List.map (fun d -> findClosestLessThanHCFWithDenenom d) |> List.maxBy(fun t -> snd t)
  fst closest

// Q76: How many different ways can one hundred be written as a sum of at 
// least two positive integers?
// Note: This algorithm was blatantly copied from: http://en.wikipedia.org/wiki/Partition_(number_theory)
// with little or no appreciation of internals.
let q68 x = 
  let cache = new Dictionary<int * int, int>()
  let rec pkn k n =    
    if k > n then 0
    elif k = n then 1
    elif cache.ContainsKey((k, n)) then cache.[k, n]
    else 
      let v = (pkn (k+1) n) + (pkn k (n - k))
      cache.Add((k, n), v)
      v
  (pkn 1 100) - 1

// Q62: Find the smallest cube for which exactly five permutations of its 
// digits are cube.
let q69 x = 
  let cube_chars_sorted_cache = new Dictionary<string, int>()
  let cube_list = new List<int64>()    
  let get_key num = new String(num.ToString().ToCharArray() |> Array.sort)

  ignore [for i in 1L..50000L -> 
    let c = i * i * i
    cube_list.Add(c)
    let key = get_key c
    
    if cube_chars_sorted_cache.ContainsKey(key) then 
      cube_chars_sorted_cache.[key] <- cube_chars_sorted_cache.[key] + 1
    else  cube_chars_sorted_cache.Add(key, 1)
  ]
  
  let rec find idx =        
    let cube = cube_list.[idx]
    if cube_chars_sorted_cache.[get_key cube] = 5 then cube
    else find (idx + 1)

  let start_idx = cube_list.FindIndex(fun c -> c > 50000L)
  find start_idx
     

// Q73 :How many fractions lie between 1/3 and 1/2 in a sorted set of 
// reduced proper fractions?
let q70 x = 
  let value n d = n/d
  let min, max =  1.0/3.0, 0.5
  let rec go acc n d =
    if d > 12000L then acc
    else
      let v = float(n) / float(d)
      let valid = v > min && v < max && Utils.euclidianHCF n d = 1L
      let acc = if valid then (acc + 1L) else acc
      // Next
      let n = n + 1L
      if n = d then go acc 1L (d + 1L)
      else go acc n d
    
  go 0L 1L 2L
