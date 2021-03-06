﻿module Q21_30

open System
open System.IO
open System.Collections
open System.Collections.Generic

// Q21: Evaluate the sum of all the amicable numbers under 10000
let q21 x = 
  let lst = [1..9999]
  let sums = [for i in [1..9999] -> (i, Utils.sumDivisors i)]
  let amicables = sums |> List.filter (fun x -> fst x <> snd x && Utils.sumDivisors(snd x) = (fst x))
  printfn "%A" amicables
  amicables |> List.sumBy (fun x -> fst x)

// Q17: If all the numbers from 1 to 1000 (one thousand) inclusive were 
// written out in words, how many letters would be used?  
let q22 x = [1..1000] |> List.map Utils.numToWord |> List.sumBy (fun w -> w.Replace(" ", "").Length)

// Q22: What is the total of all the name scores in the file?
let q23 x : int64 = 
  let getAlphaScore (name:string) =
    let getCharAlphaScore (x:char) = 
      let charScore = Math.Max (0, (int (x)) - (int ('A')) + 1)
      Math.Max (0, charScore)
    name.ToCharArray() |> Array.sumBy (fun c -> getCharAlphaScore c)
  let names = (File.ReadAllText "q23_names.txt").Split(',')
  let namesAndScores = names |> Array.map (fun n -> (n, getAlphaScore n))
  let namesAndScoresSorted = namesAndScores |> Array.sortBy (fun ns -> fst ns)  
  let scores = namesAndScoresSorted |> Array.mapi (fun i ns -> int64(i + 1) * int64(snd ns))
  // printfn "namesAndScoresSorted:%A scores: %A" namesAndScoresSorted scores  
  scores |> Array.sum

// Q19:  How many Sundays fell on the first of the month during the 
// twentieth century (1 Jan 1901 to 31 Dec 2000)?
let q24 x =
  let rec countSundaysAux acc (dt:DateTime) enddt =
    if dt > enddt then acc
    else
      let newdt = dt.AddMonths 1
      if dt.DayOfWeek = DayOfWeek.Sunday then countSundaysAux (acc + 1) newdt enddt
      else countSundaysAux acc newdt enddt
  let startdt = new DateTime(1901, 1, 1)
  let enddt = new DateTime(2000, 12, 31)
  countSundaysAux 0 startdt enddt

// Q28: What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral 
// formed in the same way?
let q25 x =
  let len = 500
  let rec q25Aux acc num direction x y =
    // printfn "q25Aux acc:%d num:%d direction:%d x:%d y:%d" acc num direction x y
    if (x > len || x < -len || y > len || y < -len) then acc   
    elif x <= 0 && x = y then q25Aux (acc + num) (num + 1) 6 (x + 1) (y) 
    elif x < 0 && -x = y then q25Aux (acc + num) (num + 1) 8 (x) (y - 1)
    elif y < 0 && x = -y then q25Aux (acc + num) (num + 1) 2 (x + 1) (y)    
    elif x = y then q25Aux (acc + num) (num + 1) 4 (x - 1) (y)
    else 
      let newx = if num <> 2 && direction = 6 then x + 1 elif direction = 4 then x - 1 else x
      let newy = if num = 2 || direction = 2 then y + 1 elif direction = 8 then y - 1 else y
      q25Aux acc (num + 1) direction newx newy

  q25Aux 0 1 6 0 0



// Q30: Find the sum of all the numbers that can be written as the sum of 
// fifth powers of their digits.
// TODO: This is too slow, should not be a pass!!
let q26 x =  
  let cache = new Dictionary<char, int64>()
  [0L..9L] |> List.iter (fun(i) -> cache.Add(i.ToString().ToCharArray().[0], pown i 5))

  let rec q26Aux num acc =        
    if num > 355000L then acc
    else
      let sum = num.ToString().ToCharArray() |> Array.sumBy(fun c -> cache.[c])      
      if sum = num then  q26Aux (num + 1L) (acc + sum)
      else q26Aux (num + 1L) acc
    
  q26Aux 1000L 0L

// Q24: What is the millionth lexicographic permutation of the digits 
// 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
// TODO: This would dramatically cut down time if we used an ordered 
// permutation algorithm
let q27 x = 
  let lst = Utils.getAllCombinations "0123456789"
  lst.Sort()
  lst.Item(999999)
  
  
// Q67: Find the maximum total from top to bottom in triangle.txt 
let q28 x =   
  let triangle = 
    File.ReadAllText("q28_triangle.txt").Split('\n') |> Array.filter (fun (str:string) -> str.Length > 0) 
      |> Array.map (fun (ln:string) -> ln.Split(' ') |> Array.filter (fun (str:string) -> str.Length > 0) |> Array.map (fun str -> Int64.Parse(str))) 
  let scores = Utils.scoreTree triangle 
  List.nth scores (scores.Length - 1) |> List.max

// Q29: How many distinct terms are in the sequence generated by a^b for 
// 2 <= a  100 and 2 <= b <= 100?
let q29 x =
  let alist = [2.0..100.0]
  let blist = [2.0..100.0]
  let all = alist |> Seq.collect (fun a -> blist |> Seq.map(fun b -> Math.Pow(a, b)))
  let all = Seq.distinct(all)
  Seq.toList(all).Length

// Q36: Find the sum of all numbers, less than one million, which are 
// palindromic in base 10 and base 2
let q30 x =
  let lst = [1..999999] |> List.filter (fun n -> 
    Utils.isPalindrome n && Utils.isPalindrome (Convert.ToString(n, 2)))
  printfn "lst: %A" lst
  lst |> List.sum

  