module Q31_40

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic

// Q31: Find the sum of all numbers which are equal to the sum 
// of the factorial of their digits
let q31 x =  
  let cached_factorials = new Dictionary<char, int>()
  [0I..9I] |> List.iter(fun n -> cached_factorials.Add (n.ToString().[0], int(Utils.getFactorial n)))
  let lst = [3..9999999] |> List.filter(fun x -> x = (x.ToString().ToCharArray() |> Array.sumBy (fun c -> cached_factorials.[c])))
  lst |> List.sum

// Q32: Find the sum of all the positive integers which cannot be written as the sum 
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

// Q33: How many circular primes are there below one million?
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

// Q34: If dn represents the nth digit of the fractional part, find the value 
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

// Q35: Using words.txt (right click and 'Save Link/Target As...'), a 16K text 
// file containing nearly two-thousand common English words, how many are triangle words?
let q35 x =
  let tri n = ((n + 1) * n) / 2
  let tris = [for i in [1..1000] -> tri i]
  let isNumTri n = tris |> List.exists (fun t -> t = n)
  let isWordTri (w:string) =    
    let score = w.Replace("\"", "").ToCharArray() |> Array.sumBy(fun c -> (1 + int(c) - int('A')))
    isNumTri score
  ((File.ReadAllText "q35_words.txt").Split(',') |> Array.filter (isWordTri)).Length

// Q36: Find the product of the coefficients, a and b, for the quadratic 
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

// Q37: Find the next triangle number that is also pentagonal and hexagonal
let q37 x2 =
  let start = 40755.0
  let isNatural n = n - float(int(n)) = 0.0
  let getTriangluarN x = ((sqrt((8.0 * x) + 1.0) - 1.0) / 2.0)
  let getTriangle n = ((n + 1.0) * n) / 2.0  
  let isPentagonal n = isNatural ((sqrt((24.0 * n) + 1.0) + 1.0) / 6.0)
  let isHexagonal n = isNatural ((sqrt((8.0 * n) + 1.0) + 1.0) / 4.0)
  
  let startn = getTriangluarN start
  let rec testNext nextn =
    let tri = getTriangle nextn
    if isPentagonal (tri) && isHexagonal (tri) then tri
    else testNext (nextn + 1.0)

  testNext (startn + 1.0)



// Q38: Find the value of d  1000 for which 1/d contains the longest recurring 
// cycle in its decimal fraction part
let q38 x = 
  let countUnitRecurringDecs denom =
    let v = (1000000000000000000000000000000000000000000000000000000000000000000000000000000I / denom)
    printfn "denom %s decimals: %s" (denom.ToString()) (v.ToString())
    let str = v.ToString().Split('.').[0]    

    // TODO: Implement using long division, will not work without BigDecimal
    let rec countUnitRecurringDecsAux start len = 
      if start >= str.Length || (len+start) >= str.Length then (len - 1)      
      else 
        let sub = str.Substring(start, len)
        // printfn "str: %s sub: %s" str sub
        if (sub.ToCharArray() |> Seq.distinct |> Seq.length = 1) || Regex.Matches(str, sub).Count <= 1 then (len - 1)
        else countUnitRecurringDecsAux start (len + 1)

    // HACK: Test ignoring zeroeth to 5th first digit in decimal portion, 
    // Start with 7 as we know 1/7 has 6
    [0..5] |> List.map (fun i -> countUnitRecurringDecsAux i 7) |> List.max     

  let rec findDWithLongestCycle d maxd max =
    if d = 1000I then maxd
    else
      let c = countUnitRecurringDecs d
      if c <> 6 && c > max then printfn "1/%s has a %d long recurring cycle" (d.ToString()) c
      let maxd = if c > max then d else maxd
      let max = if c > max then c else max
      findDWithLongestCycle (d + 1I) maxd max
  
  findDWithLongestCycle 2I 2I 0