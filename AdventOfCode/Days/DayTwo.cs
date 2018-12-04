using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayTwo
    {
        private Dictionary<int, int> DuplicateCounts = new Dictionary<int, int>();

        public void Process()
        {
            var ids = ReadFile();
            foreach(var id in ids)
            {
                ProcessId(id);
            }
            Console.WriteLine($"Checksum: {CalculateChecksum()}");
        }

        private void GetCorrectBox()
        {
            FindClosest(ReadFile().ToList());
        }

        private int CalculateChecksum()
        {
            return DuplicateCounts.Values.Aggregate(1, (x, y) => x * y);
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/daytwo.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        private void ProcessId(string id)
        {
            var characters = new List<char>(id.ToCharArray());
            var groupedCharacters = characters.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => new CharacterFrequency(x.Key, x.Count())).ToList();
            var duplicates = groupedCharacters.GroupBy(x => x.Item2).Select(x => x.Key).ToList();
            UpdateChecksum(duplicates);
        }

        private void FindClosest(List<string> duplicates)
        {
            var closest = new Tuple<string, string, int>(null,null,int.MaxValue);
            foreach(var firstWord in duplicates)
            {
                foreach(var secondWord in duplicates)
                {
                    if (firstWord == secondWord)
                    {
                        continue;
                    }
                    var difference = firstWord.Where((x, y) => x != secondWord[y]).Count();
                    if (difference < closest.Item3)
                    {
                        closest = new Tuple<string, string, int> (firstWord, secondWord, difference );
                    }
                }
            }
            Console.WriteLine(closest.Item1);
            Console.WriteLine(closest.Item2);
            Console.WriteLine(string.Concat(closest.Item1.Where((x, y) => x != closest.Item2[y])));
        }

        private void UpdateChecksum(List<int> duplicates)
        {
            duplicates.ForEach(x =>
            {
                if (!DuplicateCounts.ContainsKey(x))
                {
                    DuplicateCounts[x] = 1;
                }
                else
                {
                    DuplicateCounts[x] = ++DuplicateCounts[x];
                }
            });
        }
    }

    public sealed class CharacterFrequency : Tuple<char, int>
    {
        public CharacterFrequency(char item1, int item2) : base(item1, item2)
        {
        }
    }
}
