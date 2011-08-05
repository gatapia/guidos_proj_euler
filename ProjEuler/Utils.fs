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

let isStringPandigital (str:string) =
  let chars = str.ToCharArray() |> Seq.distinct |> Seq.sort |> Seq.toArray
  if chars.Length <> str.Length || (chars.[0] <> '0' && chars.[0] <> '1') then false
  else 
    let max = if chars.[0] = '0' then str.Length - 1 else str.Length
    chars.[chars.Length - 1] = max.ToString().Chars(0)

let isPandigital n =
  isStringPandigital (n.ToString())

let isNatural n = n - float(int(n)) = 0.0

let getTriangluarN x = ((sqrt((8.0 * x) + 1.0) - 1.0) / 2.0)

let getTriangle n = ((n + 1.0) * n) / 2.0  

let isPentagonal n = isNatural ((sqrt((24.0 * n) + 1.0) + 1.0) / 6.0)

let getPentagonal n = (n * ((3 * n) - 1)) / 2

let isHexagonal n = isNatural ((sqrt((8.0 * n) + 1.0) + 1.0) / 4.0)

let forall3str f (l1:string) (l2:string) (l3:string) =
  let rec forall3Aux i =   
    if i = l1.Length then true
    else
      let a, b, c = l1.Chars(i), l2.Chars(i), l3.Chars(i)
      if f a b c then forall3Aux (i + 1)
      else false
  if l1.Length <> l2.Length || l1.Length <> l3.Length then false
  else forall3Aux 0



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

let PRIME_CACHE = [2L; 3L; 5L; 7L; 11L; 13L; 17L; 19L; 23L; 29L; 31L; 37L; 41L; 43L; 47L; 53L; 59L; 61L; 67L; 71L; 73L; 79L; 83L; 89L; 97L; 101L; 103L; 107L; 109L; 113L; 127L; 131L; 137L; 139L; 149L; 151L; 157L; 163L; 167L; 173L; 179L; 181L; 191L; 193L; 197L; 199L; 211L; 223L; 227L; 229L; 233L; 239L; 241L; 251L; 257L; 263L; 269L; 271L; 277L; 281L; 283L; 293L; 307L; 311L; 313L; 317L; 331L; 337L; 347L; 349L; 353L; 359L; 367L; 373L; 379L; 383L; 389L; 397L; 401L; 409L; 419L; 421L; 431L; 433L; 439L; 443L; 449L; 457L; 461L; 463L; 467L; 479L; 487L; 491L; 499L; 503L; 509L; 521L; 523L; 541L; 547L; 557L; 563L; 569L; 571L; 577L; 587L; 593L; 599L; 601L; 607L; 613L; 617L; 619L; 631L; 641L; 643L; 647L; 653L; 659L; 661L; 673L; 677L; 683L; 691L; 701L; 709L; 719L; 727L; 733L; 739L; 743L; 751L; 757L; 761L; 769L; 773L; 787L; 797L; 809L; 811L; 821L; 823L; 827L; 829L; 839L; 853L; 857L; 859L; 863L; 877L; 881L; 883L; 887L; 907L; 911L; 919L; 929L; 937L; 941L; 947L; 953L; 967L; 971L; 977L; 983L; 991L; 997L; 1009L; 1013L; 1019L; 1021L; 1031L; 1033L; 1039L; 1049L; 1051L; 1061L; 1063L; 1069L; 1087L; 1091L; 1093L; 1097L; 1103L; 1109L; 1117L; 1123L; 1129L; 1151L; 1153L; 1163L; 1171L; 1181L; 1187L; 1193L; 1201L; 1213L; 1217L; 1223L; 1229L; 1231L; 1237L; 1249L; 1259L; 1277L; 1279L; 1283L; 1289L; 1291L; 1297L; 1301L; 1303L; 1307L; 1319L; 1321L; 1327L; 1361L; 1367L; 1373L; 1381L; 1399L; 1409L; 1423L; 1427L; 1429L; 1433L; 1439L; 1447L; 1451L; 1453L; 1459L; 1471L; 1481L; 1483L; 1487L; 1489L; 1493L; 1499L; 1511L; 1523L; 1531L; 1543L; 1549L; 1553L; 1559L; 1567L; 1571L; 1579L; 1583L; 1597L; 1601L; 1607L; 1609L; 1613L; 1619L; 1621L; 1627L; 1637L; 1657L; 1663L; 1667L; 1669L; 1693L; 1697L; 1699L; 1709L; 1721L; 1723L; 1733L; 1741L; 1747L; 1753L; 1759L; 1777L; 1783L; 1787L; 1789L; 1801L; 1811L; 1823L; 1831L; 1847L; 1861L; 1867L; 1871L; 1873L; 1877L; 1879L; 1889L; 1901L; 1907L; 1913L; 1931L; 1933L; 1949L; 1951L; 1973L; 1979L; 1987L; 1993L; 1997L; 1999L; 2003L; 2011L; 2017L; 2027L; 2029L; 2039L; 2053L; 2063L; 2069L; 2081L; 2083L; 2087L; 2089L; 2099L; 2111L; 2113L; 2129L; 2131L; 2137L; 2141L; 2143L; 2153L; 2161L; 2179L; 2203L; 2207L; 2213L; 2221L; 2237L; 2239L; 2243L; 2251L; 2267L; 2269L; 2273L; 2281L; 2287L; 2293L; 2297L; 2309L; 2311L; 2333L; 2339L; 2341L; 2347L; 2351L; 2357L; 2371L; 2377L; 2381L; 2383L; 2389L; 2393L; 2399L; 2411L; 2417L; 2423L; 2437L; 2441L; 2447L; 2459L; 2467L; 2473L; 2477L; 2503L; 2521L; 2531L; 2539L; 2543L; 2549L; 2551L; 2557L; 2579L; 2591L; 2593L; 2609L; 2617L; 2621L; 2633L; 2647L; 2657L; 2659L; 2663L; 2671L; 2677L; 2683L; 2687L; 2689L; 2693L; 2699L; 2707L; 2711L; 2713L; 2719L; 2729L; 2731L; 2741L; 2749L; 2753L; 2767L; 2777L; 2789L; 2791L; 2797L; 2801L; 2803L; 2819L; 2833L; 2837L; 2843L; 2851L; 2857L; 2861L; 2879L; 2887L; 2897L; 2903L; 2909L; 2917L; 2927L; 2939L; 2953L; 2957L; 2963L; 2969L; 2971L; 2999L; 3001L; 3011L; 3019L; 3023L; 3037L; 3041L; 3049L; 3061L; 3067L; 3079L; 3083L; 3089L; 3109L; 3119L; 3121L; 3137L; 3163L; 3167L; 3169L; 3181L; 3187L; 3191L; 3203L; 3209L; 3217L; 3221L; 3229L; 3251L; 3253L; 3257L; 3259L; 3271L; 3299L; 3301L; 3307L; 3313L; 3319L; 3323L; 3329L; 3331L; 3343L; 3347L; 3359L; 3361L; 3371L; 3373L; 3389L; 3391L; 3407L; 3413L; 3433L; 3449L; 3457L; 3461L; 3463L; 3467L; 3469L; 3491L; 3499L; 3511L; 3517L; 3527L; 3529L; 3533L; 3539L; 3541L; 3547L; 3557L; 3559L; 3571L]
let MAX_PRIME = 3571L;
// HOW CAN THIS BE SLOWER!!!!
let isPrime2 n = 
  if n = 2L || n = 3L then true
  elif n <= 1L || n % 2L = 0L then false
  elif n <= MAX_PRIME then PRIME_CACHE |> List.exists(fun p -> n = p)
  else
    let max = int64(float n |> sqrt) + 1L
    if max > MAX_PRIME then failwith "Number being tested is too large for cached prime test"
    PRIME_CACHE |> List.forall (fun p -> p > max || n % p <> 0L)

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

// Algorithm from: http://en.wikipedia.org/wiki/Binary_GCD_algorithm 
// Approx x2.5 faster
let rec binGcd (u:int64) (v:int64) =
  if u = v || u = 0L || v = 0L then u ||| v
  elif u % 2L = 0L then
    if v % 2L = 0L then 2L * binGcd (u/2L) (v/2L)
    else binGcd (u/2L) v
  elif v % 2L = 0L then binGcd u (v/2L)
  elif u > v then binGcd ((u-v)/2L) v
  else binGcd ((v-u)/2L) u  

let sumDivisors x = getAllDivisors x false |> List.sum

let areAmicable x y = (sumDivisors x) = y && (sumDivisors y) = x