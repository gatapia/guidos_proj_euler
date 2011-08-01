using System;
using NUnit.Framework;

namespace ProjEulerTests
{
  [TestFixture, Timeout(1000000)] public class Q61_70_Tests
  {
    [Test] public void Q61() { Assert.AreEqual(153, Q61_70.q61(String.Empty)); }  
    [Test] public void Q62() { Assert.AreEqual(26241, Q61_70.q62(String.Empty)); }  
    [Test] public void Q63() { Assert.AreEqual(510510.0, Q61_70.q63(String.Empty)); }  
    [Test] public void Q64() { Assert.AreEqual(709, Q61_70.q64(String.Empty)); }  
    [Test] public void Q65() { Assert.AreEqual(427337, Q61_70.q65(String.Empty)); }  
    [Test] public void Q66() { Assert.AreEqual(376, Q61_70.q66(String.Empty)); }      
    [Test] public void Q67() { Assert.AreEqual(-1, Q61_70.q67(String.Empty)); }  
    [Test] public void Q68() { Assert.AreEqual(-1, Q61_70.q68(String.Empty)); }  
    [Test] public void Q69() { Assert.AreEqual(-1, Q61_70.q69(String.Empty)); }  
    [Test] public void Q70() { Assert.AreEqual(-1, Q61_70.q70(String.Empty)); }       
  }

  [TestFixture] public class Q66_PokerTests {
    [Test] public void ScoreHandTests() {
      string[] loseCardsTest = new[] {"AC 2D 3D 4D 5D", "KD QD JD TC 8C", "KC 2D 3D 4D 5D", 
          "QD JD TC 9D 7C", "QD 2C 3D 4D 5D", "JD TC 9S 8C 6C", "JD 2D 3D 4D 5C", "TC 9S 8C 7C 5D",
          "TD 2D 3D 4D 5C", "9C 8C 7C 6C 4S", "9C 2C 3D 4D 5D", "8C 7D 6S 5S 3H", "8C 2D 3D 4D 5D", 
          "7D 6D 5H 4H 2S"};
      string[] smallestPairIsHigherThanAnyLoseCardSet = new[] {"2D 2H 3H 4H 5H", "AC KD QH JH 9H"};
      string[] pairLoseCardComparison = new[] {"2D 2H 6H 7H 9H", "2D 2H 6H 7H 8H", "2D 2H 5H 7H 8H"};
      string[] largerPairAlwaysWins = new[] {"3D 3H 2H 4H 5H", "2D 2H KH QH AH"};      
      string[] twoPairBeatsAnyPair = new[] {"2D 2H 3H 3D 4D", "AH AH KC QD JH"};
      string[] twoPairsWithSameCards = new[] {"2D 2H 3H 3D 6D", "3H 3H 2C 2D 5H"};
      string[] twoPairsHighest1stPair = new[] {"2D 2H 4H 4D 6D", "2D 2H 3H 3D 6D"};
      string[] twoPairsHighest2ndPair = new[] {"3D 3H 4H 4D 6D", "2D 2H 4H 4D 6D"};
      string[] threeOfAKindBeatsAnyPair = new[] {"2D 2H 2H 3H 4H", "AH AH KC QD JH"};
      string[] threeOfAKindBeatsAnytWOPair = new[] {"2D 2H 2H 3H 4H", "AH AH KC QD KH"};
      string[] threeOfAKind = new[] {"8D 8H 8H 4H 5H", "7D 7H 7H AH KH"};

      AssertAllHandsAreInDecreasingOrder(loseCardsTest);
      AssertAllHandsAreInDecreasingOrder(smallestPairIsHigherThanAnyLoseCardSet);
      AssertAllHandsAreInDecreasingOrder(pairLoseCardComparison);
      AssertAllHandsAreInDecreasingOrder(largerPairAlwaysWins);
      AssertAllHandsAreInDecreasingOrder(twoPairBeatsAnyPair);
      AssertAllHandsAreInDecreasingOrder(twoPairsWithSameCards);
      AssertAllHandsAreInDecreasingOrder(twoPairsHighest1stPair);
      AssertAllHandsAreInDecreasingOrder(twoPairsHighest2ndPair);
      AssertAllHandsAreInDecreasingOrder(threeOfAKindBeatsAnyPair);      
      AssertAllHandsAreInDecreasingOrder(threeOfAKindBeatsAnytWOPair);      
      AssertAllHandsAreInDecreasingOrder(threeOfAKind);
    }

    private void AssertAllHandsAreInDecreasingOrder(string[] handsInDecreasingOrder) {
      int lastScore = 0;
      string lastHand = String.Empty;
      for (int i = 0; i < handsInDecreasingOrder.Length; i++)
      {
        string hand = handsInDecreasingOrder[i];
        int score = Q61_70.scoreHand(hand.Split(' '));
        if (i > 0) {
          Assert.Greater(lastScore, score, String.Format("H1[{0} ({1})] should be > H2[{2} ({3})]", lastHand, lastScore, hand, score));
        }
        lastHand = hand;
        lastScore = score;
      }
    }
  }
}