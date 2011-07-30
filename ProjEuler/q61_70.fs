module Q61_70

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections
open System.Collections.Generic
open System.Linq

// Q57: In the first one-thousand expansions, how many fractions 
// contain a numerator with more digits than denominator?
let q61 x = 
  let sqrt2 num den =
    let newNum = num + den * 2I
    let newDen = den * 2I + (num - den)
    (newNum, newDen)
  
  let rec sqrt2Aux n d lst =
    let lst = (n,d)::lst
    if (lst.Length = 1000) then lst
    else
      let nd = sqrt2 n d
      sqrt2Aux (fst nd) (snd nd) lst
  
  let expansions = sqrt2Aux 3I 2I [] |> List.rev
  (expansions |> List.filter(fun nd -> (fst nd).ToString().Length > (snd nd).ToString().Length)).Length

// Q58: Investigate the number of primes that lie on the diagonals of 
// the spiral grid.
// SLOW!!!
let q62 x =
  // Copied from q 28
  let rec q62Aux sidelength accnp accp (num:int) direction x y =    
    let isp = (x = y || x = -y) && Utils.isPrime (int64 num)    
    let diagaccnp, diagaccp = if isp then accnp, (accp + 1.0) else (accnp + 1.0), accp        

    if x <= 0 && x = y then q62Aux sidelength diagaccnp diagaccp (num + 1) 6 (x + 1) (y) 
    elif x < 0 && -x = y then q62Aux sidelength diagaccnp diagaccp (num + 1) 8 (x) (y - 1)
    elif y < 0 && x = -y then // Spiral completed
      let perc = (accp * 100.0 / (diagaccnp + diagaccp))
      // printfn "perc:%f num:%d sidelength:%d" perc num sidelength 
      if (perc < 10.0) then sidelength 
      else q62Aux (sidelength + 2) diagaccnp diagaccp (num + 1) 2 (x + 1) (y)    
    elif x = y then q62Aux sidelength diagaccnp diagaccp (num + 1) 4 (x - 1) (y)
    else 
      let newx = if num <> 2 && direction = 6 then x + 1 elif direction = 4 then x - 1 else x
      let newy = if num = 2 || direction = 2 then y + 1 elif direction = 8 then y - 1 else y
      q62Aux sidelength accnp accp (num + 1) direction newx newy

  q62Aux 3 0.0 0.0 1 6 0 0

// Q69: Find the value of n ≤ 1,000,000 for which n/φ(n) is a maximum.
let q63 x = 
  let lst = [|1.0..1000000.0|]
  let primes = [1..10] |> List.map(fun i -> float(Utils.findXthPrime(i)))
  let getPrimeDivisors x = primes |> List.filter(fun p -> x % p = 0.0)
  let divs = lst |> Array.map(fun i -> getPrimeDivisors i)  
  let doTermsShareDivisor a b =    
    let aDivs = divs.[Array.IndexOf(lst, a)]
    let bDivs = divs.[Array.IndexOf(lst, b)]        
    aDivs |> List.exists(fun a -> bDivs |> List.exists(fun b -> b = a))
  
  let phi (x:float) = 1.0 + float(([2.0..(x - 1.0)] |> List.filter(fun i -> not(doTermsShareDivisor x i))).Length)
  printfn "Starting..."
  let nscore = [2.0..1000000.0] |> List.map(fun n -> (n, (n / phi n)))
  printfn "Getting Max..."
  fst (nscore |> List.maxBy(fun ns -> snd ns))

// QX:
let q64 x = 1

// QX:
let q65 x = 1

// QX:
let q66 x = 1

// QX:
let q67 x = 1

// QX:
let q68 x = 1

// QX:
let q69 x = 1

// QX:
let q70 x = 1