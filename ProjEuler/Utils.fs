module Utils

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic


////////////////////////////////////////////////////////////////////////////////
// MISC
////////////////////////////////////////////////////////////////////////////////
let sqr x = x * x

let sumOfSquares lst = lst |> Seq.map sqr |> Seq.sum

let squareOfSums lst = sqr (Seq.sum lst)

let rec numToWord x = 
  let w10 = ["zero";"one";"two";"three";"four";"five";"six";"seven";"eight";"nine";"ten"]
  let wteens = ["eleven";"twelve";"thirteen";"fourteen";"fifteen";"sixteen";"seventeen";"eighteen";"nineteen"]
  let wdecs = ["twenty";"thirty";"forty";"fifty";"sixty";"seventy";"eighty";"ninety";]
  
  if x = 1000 then "one thousand"
  elif x < 11 then w10.[x]
  elif x < 20 then wteens.[x - 11]
  elif x < 100 then 
    let maj = x / 10
    let remainder = x - (maj * 10)
    if remainder = 0 then wdecs.[maj - 2] else wdecs.[maj - 2] + " " + (numToWord remainder)
  else    
    let hund = x / 100
    let remainder = x - (hund * 100)
    let hundStr = (numToWord hund) + " " + "hundred"
    if remainder = 0 then hundStr
    else hundStr + " and " + (numToWord remainder)

let scoreTree (tree:array<array<int64>>) = 
  [for row in 0..(tree.Length - 1) ->
    [for col in 0..(tree.[row].Length - 1) ->
      if row = 0 then int64 tree.[row].[col]
      else         
        let upl = if col = 0 then 0L else int64 tree.[row - 1].[col - 1]
        let upr = if col = (tree.[row].Length - 1) then 0L else int64 tree.[row - 1].[col]
        let score = int64  tree.[row].[col] + Math.Max(upl, upr)
        tree.[row].[col] <- score
        score
    ]
  ]

////////////////////////////////////////////////////////////////////////////////
// FIBONACCI
////////////////////////////////////////////////////////////////////////////////
let rec fib n = if n < 2 then 1 else fib (n-2) + fib(n-1)

let getFibSeq n =
  let rec getFibSeqAux lst idx =
    if idx < 2 then getFibSeqAux [0I; 1I; 1I] 3
    elif idx > n then lst
    else
      let lst = lst @ [(lst.[idx - 1] + lst.[idx - 2])]
      getFibSeqAux lst (idx + 1)

  getFibSeqAux [] 0

let getFibSeqItemAndIndex ifMatch startCheckingFrom =
  let rec getFibSeqItemAndIndexAux lst idx =
    if idx < 2 then getFibSeqItemAndIndexAux [0I; 1I; 1I] 3
    else
      let nth = (lst.[idx - 1] + lst.[idx - 2])
      if idx > startCheckingFrom && ifMatch nth then (nth, idx)
      else getFibSeqItemAndIndexAux (lst @ [nth]) (idx + 1)

  getFibSeqItemAndIndexAux [] 0

////////////////////////////////////////////////////////////////////////////////
// PRIME
////////////////////////////////////////////////////////////////////////////////

let isFactorable (num:int64) =
  let rec isFactorableAux (num:int64) (max:int64) (factor:int64) =
    if num <> 2L && num % 2L = 0L then true
    elif factor = 1L || factor % 2L = 0L then isFactorableAux num max (factor + 1L)
    elif factor >= max then false
    elif factor <> num && num % factor = 0L then true    
    else isFactorableAux num max (factor + 2L)
  
  //if num <> 2L && num % 2L = 0L then true
  //else
  let max = int64(float num |> sqrt) + 1L
  isFactorableAux num max 2L

let isPrime (n:int64) = 
  if n = 2L || n = 3L then true
  elif n <= 1L || n % 2L = 0L then false
  else not (isFactorable n)    

let isPrimeCached (n:int64) (cache:Dictionary<int64, bool>) =
  if cache.ContainsKey(n) then cache.[n]
  else 
    let is = isPrime n
    cache.Add(n, is)
    is

let rec getNextPrime (from:int64) =  
  match from with
  | 0L | 1L -> 2L
  | 2L      -> 3L
  | _ ->    
    let x = if from % 2L = 0L then from + 1L else from + 2L
    if isPrime x then x
    else getNextPrime x

let rec getNextPrimeCached (from:int64) (cache:Dictionary<int64, bool>) =  
  match from with
  | 0L | 1L -> 2L
  | 2L      -> 3L
  | _ ->    
    let x = if from % 2L = 0L then from + 1L else from + 2L
    if isPrimeCached x cache then x
    else getNextPrimeCached x cache

let rec getPrevPrime (from:int64) =  
  match from with
  | 3L      -> 2L  
  | _ ->    
    if from <= 2L then raise (Exception("Error"))
    let x = if from % 2L = 0L then from - 1L else from - 2L
    if isPrime x then x
    else getPrevPrime x

let rec getPrevPrimeCached (from:int64) (cache:Dictionary<int64, bool>) =  
  match from with
  | 3L      -> 2L  
  | _ ->    
    if from <= 2L then raise (Exception("Error"))
    let x = if from % 2L = 0L then from - 1L else from - 2L
    if isPrimeCached x cache then x
    else getPrevPrimeCached x cache

let getHighestPrimeFactor (num:int64) =
  let rec getHighestPrimeFactorAux (num:int64) (prime:int64) =
    if num <= 1L || num < prime then 
      prime
    else 
      let prime = getNextPrime prime
      if num % prime = 0L then
        let num = (num / prime)
        getHighestPrimeFactorAux num prime
      else getHighestPrimeFactorAux num prime
  getHighestPrimeFactorAux num 3L

let findXthPrime x = 
  let rec findXthPrimeAux countDown (from:int64) =  
    if countDown = 0 then from
    else findXthPrimeAux (countDown - 1) (getNextPrime from)

  match x with 
  | 0 -> 0L
  | 1 -> 2L
  | 2 -> 3L
  | _ -> findXthPrimeAux (x - 2) 3L

let isPalindrome n =   
  let str = n.ToString()
  let rev = new string (str.ToCharArray() |> Array.rev)
  rev.Equals str

////////////////////////////////////////////////////////////////////////////////
// Division
////////////////////////////////////////////////////////////////////////////////

let rec getFactorial num:bigint =
  let rec getFactorialAux (acc:bigint) (num:bigint) =
    if num = 0I then acc
    else    
      getFactorialAux (acc * num) (num - 1I)
  getFactorialAux 1I num

let countTimeDivisbleBy x div =
  let rec countTimeDivisbleByAux x div acc =
    if x % div <> 0L then (x, acc)
    else countTimeDivisbleByAux (x / div) div (acc + 1)
  countTimeDivisbleByAux x div 0

let countDivisors x =
  let rec countDivisorsAux x (prime:int64) (acc:int) =
    let numAndAcc = countTimeDivisbleBy x prime
    if snd numAndAcc <> 0 then 
      let acc = acc * ((snd numAndAcc) + 1)
      countDivisorsAux (fst numAndAcc) (getNextPrime prime) acc
    else acc
  countDivisorsAux x 2L 1

let getAllDivisors x inclusive =
  let rec getAllDivisorsAux x div lst =
    if div < 1 then lst
    elif (x % div) = 0 then getAllDivisorsAux x (div - 1) (div :: lst)
    else getAllDivisorsAux x (div - 1) lst
   
  let initialDiv = if inclusive then x else (x - 1)
  getAllDivisorsAux x initialDiv []

let sumDivisors x = getAllDivisors x false |> List.sum

let areAmicable x y = (sumDivisors x) = y && (sumDivisors y) = x