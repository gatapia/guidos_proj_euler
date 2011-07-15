using System;
using System.Numerics;
using NUnit.Framework;

namespace ProjEulerTests
{
  public class Q11_20_Tests
  {
    [Test] public void Q11() { Assert.AreEqual(1366, Q11_20.q11(String.Empty)); }  
    [Test] public void Q12() { Assert.AreEqual(648, Q11_20.q12(String.Empty)); }  
    [Test] public void Q13() { Assert.AreEqual(70600674, Q11_20.q13(String.Empty)); }  
    [Test] public void Q14() { Assert.AreEqual("5537376230", Q11_20.q14(String.Empty)); }  
    [Test] public void Q15() { Assert.AreEqual(837799, Q11_20.q15(String.Empty)); }  
    [Test] public void Q16() { Assert.AreEqual(76576500, Q11_20.q16(String.Empty)); }  
    [Test] public void Q17() { Assert.AreEqual(137846528820, Q11_20.q17(String.Empty)); }  
  }
}