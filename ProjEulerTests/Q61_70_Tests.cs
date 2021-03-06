﻿using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  [TestFixture, Timeout(10000)] public class Q61_70_Tests
  {
    [Test] public void Q61() { Assert.AreEqual(153, Q61_70.q61(String.Empty)); }  
    [Test] public void Q62() { Assert.AreEqual(26241, Q61_70.q62(String.Empty)); }  
    [Test] public void Q63() { Assert.AreEqual(510510.0, Q61_70.q63(String.Empty)); }  
    [Test] public void Q64() { Assert.AreEqual(709, Q61_70.q64(String.Empty)); }  
    [Test] public void Q65() { Assert.AreEqual(427337, Q61_70.q65(String.Empty)); }  
    [Test] public void Q66() { Assert.AreEqual(376, Q61_70.q66(String.Empty)); }      
    [Test] public void Q67() { Assert.AreEqual(428570, Q61_70.q67(String.Empty)); }  
    [Test] public void Q68() { Assert.AreEqual(190569291, Q61_70.q68(String.Empty)); }  
    [Test] public void Q69() { Assert.AreEqual(127035954683, Q61_70.q69(String.Empty)); }  
    [Test] public void Q70() { Assert.AreEqual(7295372, Q61_70.q70(String.Empty)); }       
  }
}