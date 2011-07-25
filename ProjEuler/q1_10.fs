module Q1_10
open System

// All questions take an argument that is not being used.  This is only
// to ensure that we do not run the function as a static property (on load)
// but rather run it as a funciton (on demand).

// Q1: Add all the natural numbers below one thousand that are multiples 
// of 3 or 5.
let q1 x = [3..3..999] @ [5..5..999] |> Seq.distinct |> Seq.sum

// Q2: By considering the terms in the Fibonacci sequence whose values do not 
// exceed four million, find the sum of the even-valued terms.
let q2 x = 
  let rec q2Aux n acc = 
    let fb = Utils.fib n
    if fb > 4000000 then acc 
    elif fb % 2 = 0 then q2Aux (n+1) acc + fb
    else q2Aux (n+1) acc  
  
  q2Aux 1 0

// Q6: Find the difference between the sum of the squares of the first one 
// hundred natural numbers and the square of the sum.
let q3 x = Utils.squareOfSums [1..100] - Utils.sumOfSquares [1..100]

// Q5 = What is the smallest positive number that is evenly divisible by all 
// of the numbers from 1 to 20?
let q4 x = 
  let rec findSmallestCD num test =
    if num < test || num % test <> 0 then findSmallestCD (num + 1) 20
    elif test = 1 then num
    else findSmallestCD num (test - 1)

  findSmallestCD 1 20

// Q3 = What is the largest prime factor of the number 600851475143 ?
let q5 x = Utils.getHighestPrimeFactor 600851475143L  

// Q4: Find the largest palindrome made from the product of two 
// 3-digit numbers.
let q6 x = 
  let s1, s2 = [100..999], [100..999]
  let prods = s1 |> Seq.collect (fun (x) -> s2 |> Seq.map(fun y -> x * y))
  let palindromes = [for p in prods do if Utils.isPalindrome p then yield p]
  Seq.max palindromes

// Q7: What is the 10001st prime number?
let q7 x = Utils.findXthPrime 10001

// Q8: Find the greatest product of five consecutive digits in the 1000-digit number.
let thousandDigitNum = 
  "73167176531330624919225119674426574742355349194934" +
  "96983520312774506326239578318016984801869478851843" +
  "85861560789112949495459501737958331952853208805511" +
  "12540698747158523863050715693290963295227443043557" +
  "66896648950445244523161731856403098711121722383113" +
  "62229893423380308135336276614282806444486645238749" +
  "30358907296290491560440772390713810515859307960866" +
  "70172427121883998797908792274921901699720888093776" +
  "65727333001053367881220235421809751254540594752243" +
  "52584907711670556013604839586446706324415722155397" +
  "53697817977846174064955149290862569321978468622482" +
  "83972241375657056057490261407972968652414535100474" +
  "82166370484403199890008895243450658541227588666881" +
  "16427171479924442928230863465674813919123162824586" +
  "17866458359124566529476545682848912883142607690042" +
  "24219022671055626321111109370544217506941658960408" +
  "07198403850962455444362981230987879927244284909188" +
  "84580156166097919133875499200524063689912560717606" +
  "05886116467109405077541002256983155200055935729725" +
  "71636269561882670428252483600823257530420752963450"

let getProdOf5ConseqDigits idx =
  if idx >= thousandDigitNum.Length - 5 then raise (Exception("Err"))
  let sub = thousandDigitNum.Substring(idx, 5)
  sub.ToCharArray() |> Seq.map (fun c -> int (Char.GetNumericValue(c))) |> Seq.reduce (fun acc char -> (char * acc))

let q8 x = [for i in 0..994 -> getProdOf5ConseqDigits i] |> Seq.max

// Q9: There exists exactly one Pythagorean triplet for which a + b + c = 1000.
// Find the product abc.
//
//  2*m*m + 2*m*n = 1000
//  m(m + n) = 500
// m = 25, n = 20
let rec find2FactorsOfX accList lastFac num =  
  let fac = lastFac + 1
  if fac > num then accList
  elif num % fac <> 0 then find2FactorsOfX accList fac num  
  else
    let other = num / fac
    let accList = accList @ [(fac, other)]    
    find2FactorsOfX accList fac num

let find2FactorsOfSumOfX x =   
  let factors = find2FactorsOfX [] 0 (x / 2)  
  let mns = factors |> Seq.map(fun(x,y) -> (min x y, abs (x-y)))  
  let abcs = mns |> Seq.map(fun(x,y) -> ((x*x - y*y), (2*x*y), (x*x+y*y)))
  abcs |> Seq.find (fun(a, b, c) -> a > 0 && b > 0 && c > 0 && a + b + c = 1000)

let q9 x = 
  // abc = 200 x 375 x 425 = 31,875,000
  let x = find2FactorsOfSumOfX 1000
  x |> fun (a,b,c) -> a*b*c

// Q10: Find the sum of all the primes below two million.
let rec getPrimesBelow prime list below =
  let next = Utils.getNextPrime prime
  if next >= below then list
  else 
    let list = next::list
    getPrimesBelow next list below

let q10 x =
  let primes = getPrimesBelow 0L [] 2000000L
  Seq.sum primes
