using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DaySeven
    {
        private List<(string start, string end)> _orders;
        private string _start;
        private string _end;
        private List<char> result;
        private Stack<string> called;

        public DaySeven()
        {
            _orders = new List<(string start, string end)>();
            called = new Stack<string>();
            result = new List<char>();
            var input = ReadFile();
            Populate(input);
        }

        public void PartOne(string start)
        {
            var group = _orders.GroupBy(x => x.start);
            var allLetters = _orders.Select(x => x.start).Concat(_orders.Select(y => y.end)).Distinct().OrderBy(x => x).ToList();
            var result = "";

            while( allLetters.Count > 0)
            {
                var lowestDependency = allLetters.Where(s => !_orders.Any(d => d.end == s)).First();

                result+= lowestDependency;

                allLetters.Remove(lowestDependency);
                _orders.RemoveAll(x => x.start == lowestDependency);
            }

            Console.WriteLine(result);
        }

        private void Populate(List<string> input)
        {
            var starts = new HashSet<char>();
            var ends = new HashSet<char>();

            input.ForEach(x =>
            {
                _orders.Add((x[5].ToString(), x[36].ToString()));
            });
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/dayseven.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
