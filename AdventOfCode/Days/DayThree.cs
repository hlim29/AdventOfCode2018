using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayThree
    {
        private Dictionary<Coord, List<string>> CoordClaims;
        private Dictionary<string, int> ClaimCount;

        public DayThree()
        {
            CoordClaims = new Dictionary<Coord, List<string>>();
            ClaimCount = new Dictionary<string, int>();
            var input = ReadFile();
            foreach (var item in input)
            {
                var parsedClaim = ParseClaim(item);
                ClaimCount.Add(parsedClaim[0].ToString(), parsedClaim[3] * parsedClaim[4]);
                Fill(parsedClaim[0].ToString(), parsedClaim[1], parsedClaim[2], parsedClaim[3], parsedClaim[4]);
            }
        }

        public void CountOverlaps()
        {
            Console.WriteLine($"Overlap count: {GetOverlapCount()}");
        }

        public void ShowNonOverlap()
        {
            Console.WriteLine($"Non-overlapping claim: {GetNonOverlappingClaim()}");
        }
        
        private void Fill(string claim, int x, int y, int length, int height)
        {
            for (int j = y; j < (y+height); j++)
            {
                for (int i = x; i < (x + length); i++)
                {
                    if (CoordClaims.ContainsKey(new Coord(i, j)))
                    {
                        CoordClaims[new Coord(i, j)].Add(claim);
                    }
                    else
                    {
                        CoordClaims[new Coord(i, j)] = new List<string> { claim };
                    }
                }
            }
        }

        private int GetOverlapCount()
        {
            return CoordClaims.Values.Where(x => x.Count > 1).Count();
        }

        private string GetNonOverlappingClaim()
        {
            var group = CoordClaims.Values.Where(x => x.Count == 1).Select(x => x.First()).GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var intersect = group.Intersect(ClaimCount);
            return intersect.First().Key;
        }

        private List<int> ParseClaim(string claimLine)
        {
            var delimitedLine = claimLine.Split(new char[] {'#', '@', ',', ':', 'x'});
            return delimitedLine.Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/daythree.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }

    public class Coord : Tuple<int, int>
    {
        public Coord(int item1, int item2) : base(item1, item2)
        {
        }
    }
}
