using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  public class Q11_20_Tests
  {
    [Test] public void Q11() { Assert.AreEqual(1366, Q11_20.q11(String.Empty)); }  
    [Test] public void Q12() { Assert.AreEqual(648, Q11_20.q12(String.Empty)); }  
  }
}