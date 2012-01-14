using System;
using NUnit.Framework;
using ProjEulerCSharp;

namespace ProjEulerTests
{
  [TestFixture, Timeout(1000000)] public class Q71_80_Tests
  {
    // F#
    [Test] public void Q71() { Assert.AreEqual(121313, Q71_73.q71(String.Empty)); }  
    [Test] public void Q72() { Assert.AreEqual(1587000, Q71_73.q72(String.Empty)); }  
    [Test] public void Q73() { Assert.AreEqual(1389019170, Q71_73.q73(String.Empty)); }  

    // C#
    [Test] public void Q74() { Assert.AreEqual(272, Q74_80.Q74()); }  
    [Test] public void Q75() { Assert.AreEqual(2772, Q74_80.Q75()); }  
    [Test] public void Q76() { Assert.AreEqual(228, Q74_80.Q76()); }      
    [Test] public void Q77() { Assert.AreEqual(28684, Q74_80.Q77()); }  
    [Test] public void Q78() { Assert.AreEqual(26033, Q74_80.Q78()); }  
    [Test] public void Q79() { Assert.AreEqual(990326167, Q74_80.Q79()); }  
    [Test] public void Q80() { Assert.AreEqual(1, Q74_80.Q80()); }       
  }
}