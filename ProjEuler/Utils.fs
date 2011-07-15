[<AutoOpen>]
module Utils

let sqr x = x * x

let sumOfSquares lst = lst |> Seq.map sqr |> Seq.sum

let squareOfSums lst = sqr (Seq.sum lst)

let rec fib n = if n < 2 then 1 else fib (n-2) + fib(n-1)

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
    if from <= 2L then raise (System.Exception("Error"))
    let x = if from % 2L = 0L then from - 1L else from - 2L
    if isPrime x then x
    else getPrevPrime x

let getHighestPrimeFactor (num:int64) =
  let rec getHighestPrimeFactorAux (num:int64) (prime:int64) =
    if num <= 1L || num < prime then 
      prime
    else 
      let newPrime = getNextPrime prime
      if num % newPrime = 0L then
        let newNum = (num / newPrime)
        getHighestPrimeFactorAux newNum newPrime
      else getHighestPrimeFactorAux num newPrime
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


let rec getFactorial num =
  let rec getFactorialAux (acc:System.Numerics.BigInteger) num =
    if num = 0 then acc
    else    
      getFactorialAux (System.Numerics.BigInteger.Multiply(acc, new System.Numerics.BigInteger(num))) (num - 1)
  getFactorialAux (new System.Numerics.BigInteger(1)) num