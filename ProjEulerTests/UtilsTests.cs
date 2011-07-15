using System;
using System.Numerics;
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

    [Test, Sequential] public void Test_fib_sequance(
        [Values(0, 1, 2, 10, 20)] int input, 
        [Values(1, 1, 2, 89, 10946)]int expectedFib) {
      Assert.AreEqual(expectedFib, Utils.fib(input));
        //Utils.getHighestPrimeFactor()
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
      
      Console.WriteLine("NUM: " + num + " X: " + nonPrime + " MULT: " + multiplier);
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
  }
}