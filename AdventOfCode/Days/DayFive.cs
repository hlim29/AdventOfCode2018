using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayFive
    {
        private readonly List<char> _characters;
        private readonly string _input;

        public DayFive()
        {
            _input = ReadFile().Trim();
            _characters = _input.ToLower().ToCharArray().Distinct().ToList();
        }
        public void FindShorestPolymer()
        {
            var best = GetBestPolymer(ReadFile().Trim());
            Console.WriteLine($"Removing {best.Item1} and reacting the polymer will yield a length of {best.Item2}.");
        }

        public void React()
        {
            var originalInput = _input;
            var input = originalInput;
            var prevInputLength = 0;
            while (input.Length != prevInputLength)
            {
                prevInputLength = input.Length;
                input = RemoveOppositePolarities(input);
            }
            Console.WriteLine($"The polymer length after all reactions is {input.Length}.");
        }

        private Tuple<char, int> GetBestPolymer(string originalInput)
        {
            var best = new Tuple<char, int>('0', int.MaxValue);
            foreach (var character in _characters)
            {
                var cleanedPolymer = originalInput.Replace(character.ToString(), "").Replace(char.ToUpper(character).ToString(), "");
                var prevInputLength = 0;
                while (cleanedPolymer.Length != prevInputLength)
                {
                    prevInputLength = cleanedPolymer.Length;
                    cleanedPolymer = RemoveOppositePolarities(cleanedPolymer);
                }
                if (cleanedPolymer.Length < best.Item2)
                {
                    best = new Tuple<char, int>(character, cleanedPolymer.Length);
                }
            }
            return best;
        }

        private string ReadFile()
        {
            return File.ReadAllText(@"Inputs/dayfive.txt");
        }

        private string RemoveOppositePolarities(string input)
        {
            foreach(var lowerChar in _characters)
            {
                var pattern = new string[] { lowerChar.ToString() + char.ToUpper(lowerChar),  char.ToUpper(lowerChar).ToString() + lowerChar };
                input = input.Replace(pattern[0], "").Replace(pattern[1], "");
            }
            return input;
        }
    }
}
