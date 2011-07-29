using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  [TestFixture, Timeout(10000)] public class Q31_40_Tests
  {
    [Test] public void Q31() { Assert.AreEqual(40730, Q31_40.q31(String.Empty)); }  
    [Test] public void Q32() { Assert.AreEqual(4179871, Q31_40.q32(String.Empty)); }  
    [Test] public void Q33() { Assert.AreEqual(55, Q31_40.q33(String.Empty)); }  
    [Test] public void Q34() { Assert.AreEqual(210, Q31_40.q34(String.Empty)); }  
    [Test] public void Q35() { Assert.AreEqual(162, Q31_40.q35(String.Empty)); }  
    [Test] public void Q36() { Assert.AreEqual(-59231, Q31_40.q36(String.Empty)); }  
    [Test] public void Q37() { Assert.AreEqual(1533776805.0, Q31_40.q37(String.Empty)); }  
    [Test] public void Q38() { Assert.AreEqual(983, Q31_40.q38(String.Empty)); }  
    [Test] public void Q39() { Assert.AreEqual(142857, Q31_40.q39(String.Empty)); }  
    [Test] public void Q40() { Assert.AreEqual(73682, Q31_40.q40(String.Empty)); }  
  }
}