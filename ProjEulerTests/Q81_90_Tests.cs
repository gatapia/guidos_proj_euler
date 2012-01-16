using System;
using NUnit.Framework;
using ProjEulerCSharp;

namespace ProjEulerTests
{
  [TestFixture, Timeout(1000000)] public class Q81_90_Tests
  {
    [Test] public void Q81() { Assert.AreEqual(1, Q81_90.q81()); }  
  }
}