using System;
using System.Numerics;
using NUnit.Framework;

namespace ProjEulerTests
{
  [TestFixture, Timeout(10000)] public class Q41_50_Tests
  {
    [Test] public void Q41() { Assert.AreEqual(748317, Q41_50.q41(String.Empty)); }  
    [Test] public void Q42() { Assert.AreEqual(840, Q41_50.q42(String.Empty)); }  
    [Test] public void Q43() { Assert.AreEqual(100, Q41_50.q43(String.Empty)); }  
    [Test] public void Q44() { Assert.AreEqual(4075, Q41_50.q44(String.Empty)); }  
    [Test] public void Q45() { Assert.AreEqual(7652413, Q41_50.q45(String.Empty)); }  
    [Test] public void Q46() { Assert.AreEqual(45228, Q41_50.q46(String.Empty)); }  
    [Test] public void Q47() { Assert.AreEqual(972, Q41_50.q47(String.Empty)); }  
    [Test] public void Q48() { Assert.AreEqual(new BigInteger(8739992577), Q41_50.q48(String.Empty)); }  
    [Test] public void Q49() { Assert.AreEqual(249, Q41_50.q49(String.Empty)); }  
    [Test] public void Q50() { Assert.AreEqual(932718654, Q41_50.q50(String.Empty)); }  
  }
}