using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayTwelve
    {
        private List<Pot> _plants;
        private Dictionary<string, string> _patterns;
        private readonly char[] _barrier;
        private long _iterations;
        private long _total;
        private long _firstIndex;
        public DayTwelve()
        {
            _barrier = new char[] { '.', '.','.','.' };
            var input = ReadFile();
            var plants = input[0].Split(' ')[2].ToCharArray();
            _plants = new List<Pot>();
            for (int i = 0; i < input[0].Split(' ')[2].Length; i++){
                _plants.Add(new Pot { Index = i, Value = plants[i] });
            }
            _patterns = new Dictionary<string, string>();

            for (int i = 1; i < input.Count(); i++)
            {
                var line = input[i].Split(" => ");
                _patterns.Add(line[0], line[1]);
            }
            _firstIndex =  -5;
        }

        public void Billions()
        {
            while (_iterations < 100)
            {
                Age();
            }
            _total += 49999999900L * 81;

            Console.WriteLine($"Age: 50,000,000,000; # pot sum: {_total.ToString("N")}");
        }

        public void UserInput()
        {
            Console.WriteLine("Press any key to age, 'x' to exit");
            while (Console.ReadKey().Key != ConsoleKey.X)
            {
                Console.WriteLine();
                Print();
                Age();
                Console.WriteLine("Press any key to age, 'x' to exit");
            }
            Console.WriteLine();
        }

        public void Print()
        {
            var output = new string(_plants.Select(x=>x.Value).ToArray());
            Console.WriteLine($"Age: {_iterations}, # pot sum: {_total}, {output}");
        }

        private void Age()
        {
            _firstIndex = _plants.First().Index - 5;
            var barrier = new List<Pot>
            {
                new Pot { Index = 0, Value = '.'},
                new Pot { Index = 0, Value = '.'},
                new Pot { Index = 0, Value = '.'},
                new Pot { Index = 0, Value = '.'},
                new Pot { Index = 0, Value = '.'}
            };

            var tempList = barrier.Concat(_plants).Concat(barrier).ToArray();
            var result = new List<Pot>();
            for (int i = 2; i < tempList.Count()-2; i++)
            {
                var currentSample = new string(tempList.Skip(i - 2).Take(5).Select(x => x.Value).ToArray());
                if (_patterns.ContainsKey(currentSample)){
                    result.Add(new Pot { Index = i+ _firstIndex, Value = _patterns[currentSample][0] });
                } else
                {
                    result.Add(new Pot { Index = (i + _firstIndex), Value = '.' });
                }
            }

            var first = result.IndexOf(result.Where(x => x.Value == '#').First());
            var last = result.IndexOf(result.Where(x => x.Value == '#').Last());
            var newArray = result.Skip(first).Take(last);

            _total = newArray.Where(x => x.Value.Equals('#')).Select(x => x.Index).Sum();
            _plants = newArray.ToList();
            _iterations++;
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/daytwelve.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }

    public class Pot
    {
        public long Index { get; set; }
        public char Value { get; set; }
    }
}
