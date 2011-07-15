using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  public class Q1_10_Tests
  {
    [Test] public void Q1() { Assert.AreEqual(233168, Q1_10.q1(String.Empty)); }
    [Test] public void Q2() { Assert.AreEqual(4613732, Q1_10.q2(String.Empty)); }
    [Test] public void Q3() { Assert.AreEqual(25164150, Q1_10.q3(String.Empty)); }
    [Test] public void Q4() { Assert.AreEqual(232792560, Q1_10.q4(String.Empty)); }
    [Test] public void Q5() { Assert.AreEqual(6857, Q1_10.q5(String.Empty)); }
    [Test] public void Q6() { Assert.AreEqual(906609, Q1_10.q6(String.Empty)); }
    [Test] public void Q7() { Assert.AreEqual(104743, Q1_10.q7(String.Empty)); }
    [Test] public void Q8() { Assert.AreEqual(40824, Q1_10.q8(String.Empty)); }
    [Test] public void Q9() { Assert.AreEqual(31875000, Q1_10.q9(String.Empty)); }
    [Test] public void Q10() { Assert.AreEqual(142913828922, Q1_10.q10(String.Empty)); }
  }
}