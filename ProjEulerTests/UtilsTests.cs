using System;
using System.Numerics;
using Microsoft.FSharp.Core;
using NUnit.Framework;

namespace ProjEulerTests
{
  [TestFixture] public class UtilsTests
  {
     [Test, Sequential] public void Test_sqr_gives_square_of_input(
           [Values(1, 2, 10, 99)] int input, 
           [Values(1, 4, 100, 9801)]int expectedSquare) {
        Assert.AreEqual(expectedSquare, Utils.sqr(input));
     }

    [Test] public void Test_sumOfSquares_is_squares_all_values_in_list_then_sums() {
      Assert.AreEqual(30, Utils.sumOfSquares(new[] {1, 2, 3, 4}));
      Assert.AreEqual(300, Utils.sumOfSquares(new[] {10, 10, 10}));
    }

    [Test] public void Test_squareOfSums_is_the_sum_of_all_vals_then_squared() {
      Assert.AreEqual(100, Utils.squareOfSums(new[] {1, 2, 3, 4}));
      Assert.AreEqual(900, Utils.squareOfSums(new[] {10, 10, 10}));
    }

    [Test] public void Test_getFibSeq() {
      CollectionAssert.AreEqual(new BigInteger[] {0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144}, Utils.getFibSeq(12));
    }

    [Test] public void Test_getFibSeqItemAndIndex()
    {
      Tuple<BigInteger, int> result = Utils.getFibSeqItemAndIndex(FSharpFunc<BigInteger, bool>.FromConverter(fib => fib == 144), 10);
      Assert.AreEqual((BigInteger) 144, result.Item1);
      Assert.AreEqual(12, result.Item2);
    }

    [Test, Sequential] public void Test_fib_sequance(
        [Values(0, 1, 2, 10, 20)] int input, 
        [Values(1, 1, 2, 89, 10946)]int expectedFib) {
      Assert.AreEqual(expectedFib, Utils.fib(input));
    }

    [Test, Sequential] public void Test_isFactorable_returns_true_for_numbers_that_have_factors(
        [Random(2, 1000, 100)] int nonPrime, 
        [Random(2, 7, 100)] int multiplier) {
      Assert.IsTrue(Utils.isFactorable(nonPrime * multiplier));
    }

    [Test, Sequential] public void Test_isFactorable_returns_false_for_numbers_that_do_not_have_factors(
        [Values(2, 3, 5, 7, 11, 13, 17, 3433, 3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511)] int prime) {
      Assert.IsFalse(Utils.isFactorable(prime));
    }

    [Test, Sequential] public void Test_isPrime_returns_true_only_when_input_is_prime(
        [Values(2, 3,5 , 7, 11, 13, 17, 3433, 3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511)] int prime) {
      Assert.IsTrue(Utils.isPrime(prime));
    }

    [Test, Sequential] public void Test_isPrime_returns_false_when_input_is_not_prime(
        [Random(2, 1000, 100)] int nonPrime, 
        [Random(2, 7, 100)] int multiplier) {      
      int num = nonPrime * multiplier;      
      Assert.IsFalse(Utils.isPrime(num));
    }

    [Test, Sequential] public void Test_isPaliondrome_returns_true_when_number_is_same_in_both_ways(
        [Values(101, 1001, 9889, 1230321, 2222, 22, 4)] int palindrome) {
      Assert.IsTrue(Utils.isPalindrome(palindrome));
    }

    [Test, Sequential] public void Test_isPaliondrome_returns_false_when_number_is_not_same_in_both_ways(
        [Values(1031, 105601, 94889, 120321, 23222, 2452, 46)] int palindrome) {
      Assert.IsFalse(Utils.isPalindrome(palindrome));
    }    

    [Test, Sequential] public void Test_getNextPrime_returns_the_next_prime_number_from_input(
        [Values(4, 6, 10, 11, 13, 3430, 3440, 3456, 3460, 3461, 3466, 3468, 3471, 3491, 3500)] int fromValue, 
        [Values(5, 7, 11, 13, 17, 3433, 3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511)] int expectedPrime) {      
      Assert.AreEqual(expectedPrime, Utils.getNextPrime(fromValue));
    }

    [Test, Sequential] public void Test_getPrevPrime_returns_the_prev_prime_number_from_input(
        [Values(6, 9, 12, 17, 19, 3435)] int fromValue, 
        [Values(5, 7, 11, 13, 17, 3433)] int expectedPrime) {      
      Assert.AreEqual(expectedPrime, Utils.getPrevPrime(fromValue));
    }

    [Test, Sequential] public void Test_findXthPrime_returns_the_xth_numbered_prime(
        [Values(3, 4, 5, 6, 7, 481, 482, 483, 484, 485, 486, 487, 488, 489, 490)] int x, 
        [Values(5, 7, 11, 13, 17, 3433, 3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511)] int expectedPrime) {
      Assert.AreEqual(expectedPrime, Utils.findXthPrime(x));
    }

    [Test, Sequential] public void Test_getHighestPrimeFactor_gets_the_highest_possible_prime_number(
        [Values(7, 11, 13, 17, 3433, 3449, 3457)] int prime, 
        [Random(3, 7, 7)] int multiplier) {
      int num = prime * multiplier;
      Assert.AreEqual(prime, Utils.getHighestPrimeFactor(num));
    }    
     
    [Test, Sequential] public void Test_getFactorial_is_correct(
        [Values(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20)] int num,
        [Values(1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 2432902008176640000)] long expected
      ) {
      Assert.AreEqual(new BigInteger(expected), Utils.getFactorial(num));
    }

    [Test, Sequential] public void Test_countTimeDivisbleBy_is_correct(
        [Values(2, 3, 1)] int exp,
        [Values(100, 27, 7)] int x,
        [Values(2, 3, 7)] int divisor
      ) {
      Assert.AreEqual(exp, Utils.countTimeDivisbleBy(x, divisor).Item2);
    }

    
    [Test, Sequential] public void Test_countDivisors_is_correct([Values(60, 24, 126000)] long x, [Values(12, 8, 120)] int exp) {
      Assert.AreEqual(exp, Utils.countDivisors(x));
    }

    [Test] public void Test_getAllDivisors()
    {
      Assert.AreEqual(new[] {1, 2, 4, 5, 10, 20, 25, 50}, Utils.getAllDivisors(100));
      Assert.AreEqual(new[] {1, 2, 11}, Utils.getAllDivisors(22));
      Assert.AreEqual(new[] {1, 2, 4, 5, 10, 11, 20, 22, 44, 55, 110}, Utils.getAllDivisors(220));
      Assert.AreEqual(new[] {1, 2, 4, 71, 142}, Utils.getAllDivisors(284));
    }

    [Test] public void Test_numToWord()
    {      
      Assert.AreEqual("Zero".Replace("-", " ").ToLower(), Utils.numToWord(0));
      Assert.AreEqual("One".Replace("-", " ").ToLower(), Utils.numToWord(1));
      Assert.AreEqual("Two".Replace("-", " ").ToLower(), Utils.numToWord(2));
      Assert.AreEqual("Three".Replace("-", " ").ToLower(), Utils.numToWord(3));
      Assert.AreEqual("Four".Replace("-", " ").ToLower(), Utils.numToWord(4));
      Assert.AreEqual("Five".Replace("-", " ").ToLower(), Utils.numToWord(5));
      Assert.AreEqual("Six".Replace("-", " ").ToLower(), Utils.numToWord(6));
      Assert.AreEqual("Seven".Replace("-", " ").ToLower(), Utils.numToWord(7));
      Assert.AreEqual("Eight".Replace("-", " ").ToLower(), Utils.numToWord(8));
      Assert.AreEqual("Nine".Replace("-", " ").ToLower(), Utils.numToWord(9));
      Assert.AreEqual("Ten".Replace("-", " ").ToLower(), Utils.numToWord(10));
      Assert.AreEqual("Eleven".Replace("-", " ").ToLower(), Utils.numToWord(11));
      Assert.AreEqual("Twelve".Replace("-", " ").ToLower(), Utils.numToWord(12));
      Assert.AreEqual("Thirteen".Replace("-", " ").ToLower(), Utils.numToWord(13));
      Assert.AreEqual("Fourteen".Replace("-", " ").ToLower(), Utils.numToWord(14));
      Assert.AreEqual("Fifteen".Replace("-", " ").ToLower(), Utils.numToWord(15));
      Assert.AreEqual("Sixteen".Replace("-", " ").ToLower(), Utils.numToWord(16));
      Assert.AreEqual("Seventeen".Replace("-", " ").ToLower(), Utils.numToWord(17));
      Assert.AreEqual("Eighteen".Replace("-", " ").ToLower(), Utils.numToWord(18));
      Assert.AreEqual("Nineteen".Replace("-", " ").ToLower(), Utils.numToWord(19));
      Assert.AreEqual("Twenty".Replace("-", " ").ToLower(), Utils.numToWord(20));
      Assert.AreEqual("Twenty-one".Replace("-", " ").ToLower(), Utils.numToWord(21));
      Assert.AreEqual("Twenty-two".Replace("-", " ").ToLower(), Utils.numToWord(22));
      Assert.AreEqual("Twenty-three".Replace("-", " ").ToLower(), Utils.numToWord(23));
      Assert.AreEqual("Twenty-four".Replace("-", " ").ToLower(), Utils.numToWord(24));
      Assert.AreEqual("Twenty-five".Replace("-", " ").ToLower(), Utils.numToWord(25));
      Assert.AreEqual("Twenty-six".Replace("-", " ").ToLower(), Utils.numToWord(26));
      Assert.AreEqual("Twenty-seven".Replace("-", " ").ToLower(), Utils.numToWord(27));
      Assert.AreEqual("Twenty-eight".Replace("-", " ").ToLower(), Utils.numToWord(28));
      Assert.AreEqual("Twenty-nine".Replace("-", " ").ToLower(), Utils.numToWord(29));
      Assert.AreEqual("Thirty".Replace("-", " ").ToLower(), Utils.numToWord(30));
      Assert.AreEqual("Thirty-one".Replace("-", " ").ToLower(), Utils.numToWord(31));
      Assert.AreEqual("Forty".Replace("-", " ").ToLower(), Utils.numToWord(40));
      Assert.AreEqual("Fifty".Replace("-", " ").ToLower(), Utils.numToWord(50));
      Assert.AreEqual("Sixty".Replace("-", " ").ToLower(), Utils.numToWord(60));
      Assert.AreEqual("Seventy".Replace("-", " ").ToLower(), Utils.numToWord(70));
      Assert.AreEqual("Eighty".Replace("-", " ").ToLower(), Utils.numToWord(80));
      Assert.AreEqual("Eighty-seven".Replace("-", " ").ToLower(), Utils.numToWord(87));
      Assert.AreEqual("Ninety".Replace("-", " ").ToLower(), Utils.numToWord(90));
      Assert.AreEqual("One hundred".Replace("-", " ").ToLower(), Utils.numToWord(100));
      Assert.AreEqual("One hundred and one".Replace("-", " ").ToLower(), Utils.numToWord(101));
      Assert.AreEqual("One hundred and ten".Replace("-", " ").ToLower(), Utils.numToWord(110));
      Assert.AreEqual("One hundred and eleven".Replace("-", " ").ToLower(), Utils.numToWord(111));
      Assert.AreEqual("One hundred and twenty".Replace("-", " ").ToLower(), Utils.numToWord(120));
      Assert.AreEqual("One hundred and twenty-one".Replace("-", " ").ToLower(), Utils.numToWord(121));
      Assert.AreEqual("One hundred and forty-four".Replace("-", " ").ToLower(), Utils.numToWord(144));
      Assert.AreEqual("One hundred and sixty-nine".Replace("-", " ").ToLower(), Utils.numToWord(169));
      Assert.AreEqual("Two hundred".Replace("-", " ").ToLower(), Utils.numToWord(200));
      Assert.AreEqual("Three hundred".Replace("-", " ").ToLower(), Utils.numToWord(300));
      Assert.AreEqual("Six Hundred and sixty-six".Replace("-", " ").ToLower(), Utils.numToWord(666));
      Assert.AreEqual("One thousand".Replace("-", " ").ToLower(), Utils.numToWord(1000));

    }
  }
}