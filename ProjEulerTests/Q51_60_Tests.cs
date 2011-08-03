using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  [TestFixture, Timeout(10000)] public class Q51_60_Tests
  {
    [Test] public void Q51() { Assert.AreEqual(73162890, Q51_60.q51(String.Empty)); }  
    [Test] public void Q52() { Assert.AreEqual(997651, Q51_60.q52(String.Empty)); }  
    [Test] public void Q53() { Assert.AreEqual(134043, Q51_60.q53(String.Empty)); }  
    [Test] public void Q54() { Assert.AreEqual(5777, Q51_60.q54(String.Empty)); }  
    [Test] public void Q55() { Assert.AreEqual(16695334890, Q51_60.q55(String.Empty)); }  
    [Test] public void Q56() { Assert.AreEqual("296962999629", Q51_60.q56(String.Empty)); }  
    [Test] public void Q57() { Assert.AreEqual(5482660, Q51_60.q57(String.Empty)); }  
    [Test] public void Q58() { Assert.AreEqual(49, Q51_60.q58(String.Empty)); }  
    [Test] public void Q59() { Assert.AreEqual(107359, Q51_60.q59(String.Empty)); }  
    [Test, Timeout(100000)] public void Q60() { Assert.AreEqual(8581146, Q51_60.q60(String.Empty)); }  
  }
}