module Q31_40

open System
open System.IO
open System.Text
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