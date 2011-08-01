﻿module Q61_70

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
  let lines = File.ReadAllLines("q64_base_exp.txt") |> Array.map(fun l -> l.Split(',') |> Array.map(fun p -> Double.Parse(p)))
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
  let values = cards |> Seq.map(fun vc -> fst vc) |> Seq.sort |> Seq.toList    
  let score = values |> List.mapi(fun i v -> v + (pown v i)) |> List.sum
  score

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
let q67 x =     
  let maxD = 1000000L
  let cache = new Dictionary<int64, bool>()  
  let rec addNewFractionAux n d lst =        
    if n = d || d = 0L then 
      if n = maxD then lst
      else 
        printfn "n: %d d: %d lst: %d" (n + 1L) maxD (lst |> List.length)
        addNewFractionAux (n + 1L) maxD lst
    elif d % n = 0L || Utils.doNumbersShareDivisorCached n d cache then addNewFractionAux n (d - 1L) lst
    else addNewFractionAux n (d - 1L) ((n, d, float(n)/float(d))::lst)

  let allFractions = addNewFractionAux 1L maxD [] |> List.sortBy(fun (n,d,v) -> v)
  allFractions.[(allFractions |> List.findIndex(fun (n,d,v) -> n = 3L && d = 7L)) + 1]

// QX:
let q68 x = 1

// QX:
let q69 x = 1

// QX:
let q70 x = 1