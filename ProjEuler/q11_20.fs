module Q11_20

// Q11: What is the sum of the digits of the number 2^1000?
let q11 x = 
  let num = System.Numerics.BigInteger 2
  let bigNum = System.Numerics.BigInteger.Pow(num, 1000)
  bigNum.ToString().ToCharArray() |> Seq.sumBy (fun c -> System.Char.GetNumericValue c)

// Q12: Find the sum of digits in 100!
let q12 x = 
  let bigNum = getFactorial 100
  bigNum.ToString().ToCharArray() |> Seq.sumBy (fun c -> System.Char.GetNumericValue c)

// Q13: What is the greatest product of four adjacent numbers on the same 
// straight line in the 20 by 20 grid?
