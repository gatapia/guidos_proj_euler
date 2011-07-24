module Q31_40

open System
open System.IO
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
  let isAbundantCached x = allAbundants |> Seq.exists (fun a -> a = x)
  let cannotBeSummedBy2Abundants x =    
    allAbundants |> Seq.exists(fun a -> x > a && isAbundantCached (x - a))

  [1..upper_limit] |> List.filter cannotBeSummedBy2Abundants |> List.sum