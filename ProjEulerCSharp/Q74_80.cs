using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;

namespace ProjEulerCSharp
{
  public class Q74_80
  {
    // Q65: Find the sum of digits in the numerator of the 100th 
    // convergent of the continued fraction for e.
    public static int Q74() { 
      // e = [2; 1,2,1, 1,4,1, 1,6,1 , ... , 1,2k,1, ...].
      var quotients = Enumerable.Range(1, 33).
        SelectMany(i => new [] {1, i * 2, 1}).        
        ToList();  
      quotients.Insert(0, 2);
        
      var numerators = new BigInteger[100];
      Func<int, int, BigInteger> getNumerator = (idx, q) => {
        if (idx == 0) return 2;
        if (idx == 1) return 3;
        return (q*numerators[idx - 1]) + numerators[idx - 2];
      };
      for (int i = 0; i < numerators.Length; i++) {
        numerators[i] = getNumerator(i, quotients[i]);
      }
      return numerators.Last().ToString().Sum(c => Int32.Parse(c.ToString()));
    }

    // Q85: Investigating the number of rectangles in a rectangular grid.
    public static int Q75() { 
      // The number of inner rects is given by (m x n):      
      // m(m+1)(n)(n+1)/4
      Func<int, int, int> getRectCount = (m, n) => (m * (m+1) * n * (n + 1)) / 4;
      var diff = Int32.MaxValue;
      var best = 0;
      for (int m = 1; m < Int32.MaxValue; m++) {
        for (int n = m; n < Int32.MaxValue; n++) {
          var c = getRectCount(m, n);          
          var thisdiff = Math.Abs(c - 2000000);
          if (thisdiff < diff) {
            diff = thisdiff;
            best = m * n;
          }
          if (c <= 2000010) { continue; }

          if (n == m) return best;
          break;
        }
      }
      return 10; 
    }

    // Q102: For how many triangles in the text file does the 
    // interior contain the origin?
    public static int Q76() {
      Func<PointF[], bool> originInTriangle = tri => {
        int i, j = 2;
        bool oddNodes = false;

        for (i = 0; i < 3; i++) {
          if ((tri[i].Y < 0 && tri[j].Y >= 0 || tri[j].Y < 0 && tri[i].Y >= 0) && (tri[i].X <= 0 || tri[j].X <= 0)) {
            oddNodes ^= (tri[i].X + (0 - tri[i].Y)/(tri[j].Y - tri[i].Y)*(tri[j].X - tri[i].X) < 0);
          }
          j = i;
        }
        return oddNodes;
      };
      Func<string, PointF[]> parseTriangle = line => line.
        Split(',').
        Select(Int32.Parse).
        Select((n, i) => new { Group = i / 2, Value = n }).
        GroupBy(g => g.Group, g => g.Value).
        Select(g => new PointF(g.ElementAt(0), g.ElementAt(1))).ToArray();
      
      return File.ReadAllLines(@"files\q102_triangles.txt").
        Select(parseTriangle).
        Count(originInTriangle);
    }

    // Q61: Find the sum of the only set of six 4-digit 
    // figurate numbers with a cyclic property.
    public static int Q77() { 
      Func<int, int> triangle = n => n * (n + 1) / 2;
      Func<int, int> square = n => n * n;
      Func<int, int> pentagonal = n => n * (3 * n - 1) / 2;
      Func<int, int> hexagonal = n => n * (2 * n - 1);
      Func<int, int> heptagonal = n => n * (5 * n - 3) / 2;
      Func<int, int> octagonal = n => n * (3 * n - 2);
      
      Func<Func<int, int>, string[]> get4digs = func => {
        int i = 0;
        IList<string> fourdigs = new List<string>();
        while (true) {
          var res = func(++i);
          if (res > 9999) return fourdigs.ToArray();
          if (res > 999 && Int32.Parse(res.ToString().Substring(2)) > 9) fourdigs.Add(res.ToString());          
        }
      };

      var triangle4digits = get4digs(triangle);
      var square4digits = get4digs(square);
      var pentagonal4digits = get4digs(pentagonal);
      var hexagonal4digits = get4digs(hexagonal);
      var heptagonal4digits = get4digs(heptagonal);
      var octagonal4digits = get4digs(octagonal);
      
      Func<List<string>, bool> isCyclicUniqueSet = set => {
        if (set.Distinct().Count() != set.Count) return false;

        return set.All(n => set.Any(m => m != n && n.Substring(2) == m.Substring(0, 2)));
      };
      var all = new [] {
        triangle4digits, 
        square4digits, 
        pentagonal4digits, 
        hexagonal4digits, 
        heptagonal4digits, 
        octagonal4digits}.
          OrderBy(s => s.Length).
          ToArray();

      Func<string, string, bool> isCyclic = (m, n) => m != n && 
        (m.Substring(2) == n.Substring(0, 2) || 
          n.Substring(2) == m.Substring(0, 2));

      foreach (var str1 in all.ElementAt(0)) {
        foreach (var str2 in all.ElementAt(1)) {
          if (!isCyclic(str1, str2)) continue;
          foreach (var str3 in all.ElementAt(2)) {
            if (!isCyclic(str3, str2) && !isCyclic(str3, str1)) continue;
            foreach (var str4 in all.ElementAt(3)) {
              if (!isCyclic(str4, str3) && !isCyclic(str4, str2) && !isCyclic(str4, str1)) continue;
              foreach (var str5 in all.ElementAt(4)) {
                if (!isCyclic(str5, str4) && !isCyclic(str5, str3) && !isCyclic(str5, str2) && !isCyclic(str5, str1)) continue;
                foreach (var str6 in all.ElementAt(5)) {                  
                  var set = new List<string> {str1, str2, str3, str4, str5, str6};
                  if (isCyclicUniqueSet(set)) return set.Select(Int32.Parse).Sum();
                }
              }
            }
          }
        }
      }
      return Int32.MinValue; 
    }

    // Q60: Find a set of five primes for which any two 
    // primes concatenate to produce another prime.
    public static long Q78()
    {
      const int MAX = 10000;
      var allPrimes = Utils.PRIME_CACHE.Select(p => p.ToString()).ToArray();
      var isPrimeLookup = new Dictionary<string, bool>();
      Array.ForEach(allPrimes, p => isPrimeLookup.Add(p, true));
      var largest = Utils.PRIME_CACHE.Last();
      for (long i = largest - 1; i > 2; i--)
      {
        var str = i.ToString();
        if (!isPrimeLookup.ContainsKey(str)) isPrimeLookup.Add(str, false);
      }
      var primes = Utils.PRIME_CACHE.Where(p => p < MAX).Select(p => p.ToString()).ToArray();      
      var possibles = new List<long>();
      
      foreach (var p1 in primes) {
        foreach (var p2 in primes.SkipWhile(p => p != p1)) {
          if (!AreConcatPrimes(isPrimeLookup, p2, p1)) { continue; }
          foreach (var p3 in primes.SkipWhile(p => p != p2)) {
            if (!AreConcatPrimes(isPrimeLookup, p3, p2, p1)) { continue; }            
            foreach (var p4 in primes.SkipWhile(p => p != p3)) {              
              if (!AreConcatPrimes(isPrimeLookup, p4, p3, p2, p1)) { continue; }                            
              foreach (var p5 in primes.SkipWhile(p => p != p4)) {
                if (AreConcatPrimes(isPrimeLookup, p5, p4, p3, p2, p1)) {                   
                  var sum = new [] {p5, p4, p3, p2, p1}.Sum(str => Int64.Parse(str));
                  possibles.Add(sum);
                }
              }
            }
          }
        }
      }
      return possibles.Min();
    }

    private static bool AreConcatPrimes(IDictionary<string, bool> allPrimes, string newp, params string[] nums) {
      Func<string, bool> isPrime = str => {
        if (allPrimes.ContainsKey(str)) return allPrimes[str];
        var num  = Int64.Parse(str);
        var isp = Utils.isPrime(num);
        return allPrimes[str] = isp;
      };
      return nums.All(p1 => isPrime(p1 + newp) && isPrime(newp + p1));
    }  

    public static int Q79() { return 10; }

    public static int Q80() { return 10; }
  }
}