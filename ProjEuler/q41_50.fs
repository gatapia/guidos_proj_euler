module Q41_50

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic

// Q37: Find the sum of the only eleven primes that are both truncatable 
// from left to right and right to left.
let q41 x = 
  let primeCache = new Dictionary<int64, bool>()

  let rec isLRTruncatable x =
    let xstr = x.ToString();    

    if xstr.Length = 1 then true
    else
      [1..xstr.Length - 1] |> List.forall(
        fun len -> 
          let left = Int64.Parse(xstr.Substring(0, len))
          let right = Int64.Parse(xstr.Substring(xstr.Length - len))
          Utils.isPrimeCached left primeCache && Utils.isPrimeCached right primeCache
        )
  
  let rec getNextLRTruncable x =    
    let x = x + 2L
    // printfn "getNextLRTruncable:%d is prime:%b" x (Utils.isPrimeCached x)
    if (Utils.isPrimeCached x primeCache) && isLRTruncatable x then x
    else getNextLRTruncable x

  let rec getAllLRTruncatablePrimes rem lst prime =
    if rem = 0 then lst
    else 
      let prime = getNextLRTruncable prime
      // printfn "getNextLRTruncable: %d" prime
      let lst = prime::lst
      getAllLRTruncatablePrimes (rem - 1) lst prime
  
  getAllLRTruncatablePrimes 11 [] 7L |> List.sum

// Q39: For which value of p  1000, is the number of solutions maximised?
let q42 x = 
  let getNumberOfSolutionsForPerimeter perimeter =
    let rec getNumberOfSolutionsForPerimeterAux acc hyp adj =
      if hyp = perimeter - 2 then acc
      else 
        let opp = perimeter - (hyp + adj)
        if (opp > adj) then getNumberOfSolutionsForPerimeterAux acc hyp (adj + 1)
        else
          // printfn "perimeter: %d hyp: %d adj: %d opp: %d" perimeter hyp adj opp
          let acc = if (pown hyp 2) = (pown opp 2) + (pown adj 2) then acc + 1 else acc        
          let finishedIter = (adj >= hyp - 1)
          let adj = if finishedIter then 1 else adj + 1
          let hyp = if finishedIter then hyp + 1 else hyp
          getNumberOfSolutionsForPerimeterAux acc hyp adj
        
    getNumberOfSolutionsForPerimeterAux 0 2 1
    
  let perimeters = [4..1000]
  let pss = perimeters |> List.map(fun p -> p, getNumberOfSolutionsForPerimeter p)
  let max = pss |> List.maxBy(fun ps -> snd ps)
  fst max

// Q33: If the product of these four fractions is given in its lowest common 
// terms, find the value of the denominator
let q43 x = 
  let min = 10
  let max = 99

  let isCurious numerator denominator =    
    let numstr = numerator.ToString();
    let denstr = denominator.ToString();
    if denstr.Chars(1) = '0' then false
    elif (numstr.Chars(0) = denstr.Chars(1)) then
      // printfn "numstr: %s denstr: %s" numstr denstr
      let exp = decimal(Char.GetNumericValue (numstr.Chars(1))) / decimal(Char.GetNumericValue (denstr.Chars(0)))
      exp = (decimal(numerator) / decimal(denominator))
    else if (numstr.Chars(1) = denstr.Chars(0)) then
      let exp = decimal(Char.GetNumericValue (numstr.Chars(0))) / decimal(Char.GetNumericValue (denstr.Chars(1)))
      exp = (decimal(numerator) / decimal(denominator))
    else false
  
  let rec findCuriosNums lst num den =
    let lst = if isCurious num den then (num,den) :: lst else lst

    if num = max - 1 && den = max then lst
    else          
      if den = max then findCuriosNums lst (num + 1) (num + 2)
      else findCuriosNums lst num (den + 1)
  
  let curouss = findCuriosNums [] min (min + 1)
  
  let numerator = curouss |> List.fold(fun acc tup -> acc * fst tup) 1
  let denominator = curouss |> List.fold(fun acc tup -> acc * snd tup) 1
  let numDivisors = Utils.getAllDivisors numerator true
  let denDivisors = Utils.getAllDivisors denominator true
  let commons = (Set.ofList numDivisors) |> Set.intersect (Set.ofList denDivisors)
  // printfn "commons: %A" (Set.toArray commons)
  // printfn "original %d / %d" numerator denominator
  // printfn "reduced %d / %d" (numerator / commons.MaximumElement) (denominator / commons.MaximumElement)
  denominator / commons.MaximumElement

// Q53: How many, not necessarily distinct, values of  nCr, for 1  n  100, 
// are greater than one-million?
let q44 x = 
  let ns = [23I..100I]  
  let nCr n r = Utils.getFactorial n / (Utils.getFactorial r * (Utils.getFactorial (n - r)))
  
  let howManyGreaterThan1M n =
    let rs = [2I..(n-1I)]
    let scoresGT1M = rs |> List.map (fun r -> nCr n r) |> List.filter (fun s -> s > 1000000I)
    scoresGT1M.Length

  let scoresGT1MForAllNs = ns |> List.map(fun n -> howManyGreaterThan1M n)
  scoresGT1MForAllNs |> List.sum

// Q41: What is the largest n-digit pandigital prime that exists?
let q45 x = 
  
  // n = 7 as sum of digist of 8 and 9 dig pandigital is divisible by nine  
  let max = 7654321L
  let all = Utils.getAllCombinations("1234567")
  all.Sort()
  all.Reverse()  
   
  let rec getHighestPrime idx =    
    let n = Int64.Parse(all.Item(idx))
    if Utils.isPrime n then n
    else getHighestPrime(idx + 1)

  getHighestPrime 0   

// Q:32 Find the sum of all products whose multiplicand/multiplier/product 
// identity can be written as a 1 through 9 pandigital.
let q46 x = 
  failwith "Not Implemented"
  10

// Q56: Considering natural numbers of the form, a^b, where a, b < 100, 
// what is the maximum digital sum?
let q47 x = 
  let limit = 99
  
  let sumDigits n = 
    n.ToString().ToCharArray() |> Array.sumBy(fun c -> int (c) - int ('0'))

  let rec getMaxSum max (a:int) b =
    let pwned = pown (bigint (a)) b
    let sum = sumDigits pwned    

    let max = if sum > max then sum else max
    
    if a = limit && b = limit then max
    else
      let a = if b = limit then (a + 1) else a
      let b = if b = limit then 1 else (b + 1)
      getMaxSum max a b

  getMaxSum 0 1 1

// Q97: Find the last ten digits of the non-Mersenne prime: 28433 × (2^7830457) + 1.: 
let q48 x =   
  let pow = bigint.Pow(2I,7830457)
  let num = (28433I * pow) + 1I  
  num % 10000000000I

// Q55: How many Lychrel numbers are there below ten-thousand?
let q49 x = 
  let rec isLychrel n attempts =
    if attempts = 50 then true
    else
      let rev = n.ToString().ToCharArray() |> Array.rev 
      let sum = n + bigint.Parse(new String(rev))
      if Utils.isPalindrome sum then false
      else isLychrel sum (attempts + 1)
  
  let rec countLychrelsBelow acc n =
    if n = 0I then acc
    else
      let acc = if isLychrel n 1 then (acc + 1) else acc
      countLychrelsBelow acc (n - 1I)
  
  countLychrelsBelow 0 9999I

// Q38: What is the largest 1 to 9 pandigital 9-digit number that can be 
// formed as the concatenated product of an integer with (1,2, ... , n) where n  1?
let q50 x = 
  let is9DigitPandigital (str:string) =
    str.IndexOf('0') < 0 && Utils.isPandigital str

  let getConcatenated i n =
    let mults = [1..n] |> List.map(fun m -> i * m)
    let sb = new StringBuilder()
    mults |> List.iter (fun m -> (ignore(sb.Append(m))))
    sb.ToString()

  // Assume has to start with 9
  let ints = List.concat [[9] ; [99..-1..90] ; [999..-1..900] ; [9999..-1..9000]]
  let concats = ints |> List.collect(fun i -> [2..11] |> List.map (fun n -> getConcatenated i n))
  let pandigts = concats |> List.filter (fun str -> is9DigitPandigital str)
  let nums = pandigts |> List.map(fun str -> Int64.Parse (str))
  nums |> List.max

