[<AutoOpen>]
module Utils
open System

let sqr x = x * x

let sumOfSquares lst = lst |> Seq.map sqr |> Seq.sum

let squareOfSums lst = sqr (Seq.sum lst)

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
  elif n % 2L = 0L then false
  else not (isFactorable n)    

let rec getNextPrime (from:int64) =  
  match from with
  | 0L | 1L -> 2L
  | 2L      -> 3L
  | _ ->    
    let x = if from % 2L = 0L then from + 1L else from + 2L
    if isPrime x then x
    else getNextPrime x

let rec getPrevPrime (from:int64) =  
  match from with
  | 3L      -> 2L  
  | _ ->    
    if from <= 2L then raise (Exception("Error"))
    let x = if from % 2L = 0L then from - 1L else from - 2L
    if isPrime x then x
    else getPrevPrime x

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