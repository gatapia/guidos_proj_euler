module Q31_40

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic

// Q34: Find the sum of all numbers which are equal to the sum 
// of the factorial of their digits
let q31 x =  
  let cached_factorials = new Dictionary<char, int>()
  [0I..9I] |> List.iter(fun n -> cached_factorials.Add (n.ToString().[0], int(Utils.getFactorial n)))
  let lst = [3..9999999] |> List.filter(fun x -> x = (x.ToString().ToCharArray() |> Array.sumBy (fun c -> cached_factorials.[c])))
  lst |> List.sum

// Q23: Find the sum of all the positive integers which cannot be written as the sum 
// of two abundant numbers.
let q32 x = 
  let isAbundant x = Utils.sumDivisors x > x  
  let upper_limit = 28123
  let allAbundants = [0..upper_limit] |> List.filter isAbundant
  let cache = new Dictionary<int, bool>();
  let isAbundantCached x = 
    if cache.ContainsKey x then cache.[x]
    else 
      let v = allAbundants |> Seq.exists (fun a -> a = x)
      cache.Add(x, v)
      v

  let cannotBeSummedBy2Abundants x =    
    not(allAbundants |> Seq.exists(fun a -> x > a && isAbundantCached(x - a)))

  [1..upper_limit] |> List.filter cannotBeSummedBy2Abundants |> List.sum

// Q35: How many circular primes are there below one million?
let q33 x = 
  let getAllRotations x =
    let orig = x.ToString()
    let rec getAllRotationsAux (lst:list<int64>) =  
      let len = lst.Length
      if len = orig.Length then lst
      else               
        let newstr = orig.Substring(len) + orig.Substring(0, len)
        let newint = Int64.Parse(newstr)
        getAllRotationsAux (newint::lst)
    getAllRotationsAux [x]
  let rots = [2L..999999L] |> List.map getAllRotations
  let allRotsPrime = rots |> List.filter (fun l -> l |> List.forall Utils.isPrime)
  allRotsPrime.Length

// Q40: If dn represents the nth digit of the fractional part, find the value 
// of the following expression. 
// d1 x d10 x d100 x d1000 x d10000 x d100000 x d1000000
let q34 x =  
  let sb = new StringBuilder()
  [0..1000000] |> List.iter(fun i -> ignore(sb.Append (i)))
  Char.GetNumericValue(sb.Chars(1)) *
    Char.GetNumericValue(sb.Chars(10)) *
    Char.GetNumericValue(sb.Chars(100)) *
    Char.GetNumericValue(sb.Chars(1000)) *
    Char.GetNumericValue(sb.Chars(10000)) *
    Char.GetNumericValue(sb.Chars(100000)) *
    Char.GetNumericValue(sb.Chars(1000000))

// Q42: Using words.txt (right click and 'Save Link/Target As...'), a 16K text 
// file containing nearly two-thousand common English words, how many are triangle words?
let q35 x =
  let tri n = ((n + 1) * n) / 2
  let tris = [for i in [1..1000] -> tri i]
  let isNumTri n = tris |> List.exists (fun t -> t = n)
  let isWordTri (w:string) =    
    let score = w.Replace("\"", "").ToCharArray() |> Array.sumBy(fun c -> (1 + int(c) - int('A')))
    isNumTri score
  ((File.ReadAllText "q35_words.txt").Split(',') |> Array.filter (isWordTri)).Length

// Q27: Find the product of the coefficients, a and b, for the quadratic 
// expression that produces the maximum number of primes for consecutive values of n, starting with n = 0.
let q36 x = 
  let min = -999
  let max = 999
  let quad a b n = int64((pown n 2) + (a * n) + b)  
  
  let countPrimesFrom0 a b =    
    let rec countPrimesFrom0Aux a b n acc =                  
      if not (Utils.isPrime (quad a b n)) then acc
      else countPrimesFrom0Aux a b (n+1) (acc+1)        
    countPrimesFrom0Aux a b 0 0
  
  let findMaxCoeffs =
    let rec findMaxCoeffsAux maxAcc mult a b =
      let c = countPrimesFrom0 a b
      // if c > maxAcc then printfn "countPrimesFrom0 a:%d b:%d = %d -> maxAcc:%d" a b c maxAcc
      let newmult = if c > maxAcc then (a * b) else mult
      let maxAcc = if c > maxAcc then c else maxAcc

      if b = max then
        if a = max then mult
        else findMaxCoeffsAux maxAcc newmult (a + 1) min
      else findMaxCoeffsAux maxAcc newmult a (b + 1)

    findMaxCoeffsAux 0 0 min min
  findMaxCoeffs

// Q45: Find the next triangle number that is also pentagonal and hexagonal
let q37 x2 =
  let start = 40755.0  
  
  let startn = Utils.getTriangluarN start
  let rec testNext nextn =
    let tri = Utils.getTriangle nextn
    if Utils.isPentagonal (tri) && Utils.isHexagonal (tri) then tri
    else testNext (nextn + 1.0)

  testNext (startn + 1.0)

// Q26: Find the value of d  1000 for which 1/d contains the longest recurring 
// cycle in its decimal fraction part
let q38 x = 
  let rex = new Regex("^[0-9]*([0-9]{7,}?)\1+[0-9]*?$")
  
  let countUnitRecurringDecs denom =    
    // !! TERRIBLE HACK !!!!!!! use long division or something smarter, this is terrible!!!
    let v = (1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000I / denom)
    let str = v.ToString().Split('.').[0]        
    let m = rex.Match(str)
    if m.Success then m.Groups.[1].Value.Length  else 0

    // Algorithm2 by: http://www.mathblog.dk/2011/project-euler-26-find-the-value-of-d-1000-for-which-1d-contains-the-longest-recurring-cycle/
    // 
    // Let me illustrate with 1/7.  First calculation of the remainder of 1/7 gives us 1.
    // Second calculation to analyse the remainder on the first decimal place we multiply by 10,  and divide by 7. The remainder of 10/7 is 3.
    // In the third calculation we get 30/7 which gives us a remainder of 2.
    // In the fourth calculation we get 20/7 which gives us a remainder of 6.
    // In the fifth calculation we get 60/7 which gives us a remainder of 4.
    // In the sixth calculation we get 40/7 which gives us a remainder of 5.
    // In the seventh calculation we get 50/7 which gives us a remainder of 1.
  let countUnitRecurringDecs2 denom =
    "TODO: Implement"

  let rec findDWithLongestCycle d maxd max =
    if d = 1000I then int(maxd)
    else
      let c = countUnitRecurringDecs d
      // if c <> 6 && c > max then printfn "1/%s has a %d long recurring cycle" (d.ToString()) c

      let maxd = if c > max then d else maxd
      let max = if c > max then c else max
      findDWithLongestCycle (d + 1I) maxd max
  
  findDWithLongestCycle 2I 2I 0

// Q52: Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, 
// contain the same digits
let q39 x = 
  let allEqual = Array.forall2 (fun elem1 elem2 -> elem1 = elem2)
  let rec q39Aux n =
    let nchars = n.ToString().ToCharArray() |> Array.sort

    let rec q39AuxWithMult mult =      
      let multchars = (n * mult).ToString().ToCharArray() |> Array.sort
      let success = multchars.Length = nchars.Length && allEqual nchars multchars
      // printfn "q39AuxWithMult n:%d mult:%d yes:%b" n mult success
      success
    
    if [2..6] |> Seq.forall (fun m -> q39AuxWithMult m) then n
    else q39Aux (n + 1)
  
  q39Aux 1

// Q31: How many different ways can £2 be made using any number of coins?
let q40 x = 
  let target = 200
  let coins = [200;100;50;20;10;5;2;1]      
  let mutable count = 0
  for c200 = 0 to 1 do
    if c200 = 1 then count <- (count + 1)
    else 
      for c100 = 0 to target/100 do
        if target = 100 * c100 then count <- (count + 1)
        else 
          for c50 = 0 to target/50 do
            if target = 100 * c100 + 50 * c50 then count <- (count + 1)
            else 
              for c20 = 0 to target/20 do
                if target = 100 * c100 + 50 * c50 + 20 * c20 then count <- (count + 1)
                else 
                  for c10 = 0 to target/10 do
                    if target = 100 * c100 + 50 * c50 + 20 * c20 + 10 * c10 then count <- (count + 1)
                    else 
                      for c5 = 0 to target/5 do
                        if target = 100 * c100 + 50 * c50 + 20 * c20 + 10 * c10 + 5 * c5 then count <- (count + 1)
                        else 
                          for c2 = 0 to target/2 do
                            if target = 100 * c100 + 50 * c50 + 20 * c20 + 10 * c10 + 5 * c5 + c2 * 2 then count <- (count + 1)
                            else 
                              for c1 = 0 to target do                  
                                if target = c200 * 200 + c100 * 100 + c50 * 50 + c20 * 20 + c10 * 10 + c5 * 5 + c2 * 2 + c1 then count <- (count + 1)
  count   