using System;
using System.IO;
using System.Linq;

namespace ProjEulerCSharp
{
  public class Q81_90
  {
    // Q87: Investigating numbers that can be expressed as the sum of a 
    // prime square, cube, and fourth power?
    public static int q81() { 
      const int max = 50000000;
      var primeSquares = Utils.PRIME_CACHE.Select(p => p * p).Where(s => s < max).ToArray();
      var primeCubes = Utils.PRIME_CACHE.Select(p => p * p * p).Where(c => c < max).ToArray();
      var primeFourths = Utils.PRIME_CACHE.Select(p => p * p * p * p).Where(f => f < max).ToArray();

      
      return primeSquares.
        SelectMany(s => primeCubes.Select(c => c + s)).
        SelectMany(sc => primeFourths.Select(f => f + sc)).
        Distinct().
        Count(n => n < max);
    }

    // Q82: Find the minimal path sum from the left column to the right column.
    public static int q82() {
      var matrix = File.ReadAllLines(@"files\q82_matrix.txt").
        Select(l => l.Split(',').Select(Int32.Parse).ToArray()).
        ToArray();

      return 1;
    }
  }
}