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

let swapArrayItems (arr:array<'a>) idx1 idx2 =
  let tmp = arr.[idx1]
  arr.[idx1] <- arr.[idx2]
  arr.[idx2] <- tmp

let areNumbersPermutations num1 num2 =
  let num1arr = num1.ToString().ToCharArray()
  let num2arr = num2.ToString().ToCharArray()
  if num1arr.Length <> num2arr.Length then false
  else Array.forall2 (fun a b -> a = b) (Array.sort(num1arr)) (Array.sort(num2arr))

let rec heapPermute n (lst:List<string>) (arr:array<char>) = 
  if n = 1 then lst.Add(new String(arr));
  else 
    for i in 0..(n - 1) do
      heapPermute (n-1) lst arr
      if n % 2 = 1 then swapArrayItems arr 0 (n-1)
      else swapArrayItems arr i (n-1)

let getAllCombinations (str:string) =
  let lst = new List<string>()
  let strc = str.ToCharArray()
  heapPermute strc.Length lst strc
  lst

// This algorithm from: 
// http://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
let getAllCombinations2 (str:string) =
  let rec go (acc:List<string>) (char_list:array<char>) k m =
    if k = m then 
      acc.Add(new String(char_list))
    else
      for i in [k..m] do
        swapArrayItems char_list k i
        go acc char_list (k + 1) m
        swapArrayItems char_list k i

  let setper (char_list:array<char>) =
    let acc = new List<string>()
    go acc char_list 0 (char_list.Length - 1)
    acc

  setper(str.ToCharArray())

let isPandigital n =
  let str = n.ToString()
  let chars = str.ToCharArray() |> Seq.distinct |> Seq.sort |> Seq.toArray
  if chars.Length <> str.Length || (chars.[0] <> '0' && chars.[0] <> '1') then false
  else 
    let max = if chars.[0] = '0' then str.Length - 1 else str.Length
    chars.[chars.Length - 1] = max.ToString().Chars(0)

let isNatural n = n - float(int(n)) = 0.0

let getTriangluarN x = ((sqrt((8.0 * x) + 1.0) - 1.0) / 2.0)

let getTriangle n = ((n + 1.0) * n) / 2.0  

let isPentagonal n = isNatural ((sqrt((24.0 * n) + 1.0) + 1.0) / 6.0)

let getPentagonal n = (n * ((3 * n) - 1)) / 2

let isHexagonal n = isNatural ((sqrt((8.0 * n) + 1.0) + 1.0) / 4.0)

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
    if from <= 2L then failwith "Cannot call getPrevRime with a value <= 2"
    let x = if from % 2L = 0L then from - 1L else from - 2L
    if isPrime x then x
    else getPrevPrime x

let rec getPrevPrimeCached (from:int64) (cache:Dictionary<int64, bool>) =  
  match from with
  | 3L      -> 2L  
  | _ ->    
    if from <= 2L then failwith "Cannot call getPrevRime with a value <= 2"
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

let getAllPrimeDivisors x inclusive (cache:Dictionary<int64, bool>) =
  let rec getAllPrimeDivisorsAux prime lst =
    let lst = if x % prime = 0L then (prime :: lst) else lst
    
    if prime = 2L then lst
    else getAllPrimeDivisorsAux (getPrevPrimeCached prime cache) lst
  
  let start = if inclusive then x + 1L else x
  getAllPrimeDivisorsAux (getPrevPrimeCached start cache) []

let rec euclidianHCF (a:int64) (b:int64) =
  let l, s = (if a > b then a else b), (if a > b then b else a)
  if s = LanguagePrimitives.GenericZero then l
  else euclidianHCF (l - s) s  

let sumDivisors x = getAllDivisors x false |> List.sum

let areAmicable x y = (sumDivisors x) = y && (sumDivisors y) = x