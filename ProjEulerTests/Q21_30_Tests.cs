using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  public class Q21_30_Tests
  {
    [Test] public void Q21() { Assert.AreEqual(31626, Q21_30.q21(String.Empty)); }  
    [Test] public void Q22() { Assert.AreEqual(21124, Q21_30.q22(String.Empty)); }  
    [Test] public void Q23() { Assert.AreEqual(871198282, Q21_30.q23(String.Empty)); }  
    [Test] public void Q24() { Assert.AreEqual(171, Q21_30.q24(String.Empty)); }  
    [Test] public void Q25() { Assert.AreEqual(669171001, Q21_30.q25(String.Empty)); }  
    [Test] public void Q26() { Assert.AreEqual(669171001, Q21_30.q26(String.Empty)); }  
    
    [Test] public void Q28() { Assert.AreEqual(7273, Q21_30.q28(String.Empty)); }  
    [Test] public void Q29() { Assert.AreEqual(9183, Q21_30.q29(String.Empty)); }  
    [Test] public void Q30() { Assert.AreEqual(872187, Q21_30.q30(String.Empty)); }  
  }
}