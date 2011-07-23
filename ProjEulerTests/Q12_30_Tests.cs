using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  public class Q21_30_Tests
  {
    [Test] public void Q21() { Assert.AreEqual(31626, Q21_30.q21(String.Empty)); }  
    [Test] public void Q22() { Assert.AreEqual(21124, Q21_30.q22(String.Empty)); }  
  }
}