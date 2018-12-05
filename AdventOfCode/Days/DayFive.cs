using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayFive
    {
        public void FindShorestPolymer()
        {
            var best = GetBestPolymer(ReadFile().Trim());
            Console.WriteLine($"Removing {best.Item1} and reacting the polymer will yield a length of {best.Item2}.");
        }

        public void React()
        {
            var originalInput = ReadFile().Trim();
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
            var best = new Tuple<char, int>('0',int.MaxValue);
            var characters = originalInput.ToLower().ToCharArray().Distinct();
            foreach (var character in characters)
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
            var buffer = new StringBuilder();
            for(int i = 1; i < input.Length; i++)
            {
                if (char.ToLowerInvariant(input[i-1]) == char.ToLowerInvariant(input[i]) && input[i - 1]!= input[i])
                {
                    i++;
                }
                else
                {
                    buffer.Append(input[i-1]);
                }

                if (i == input.Length - 1)
                {
                    buffer.Append(input[i]);
                }
            }
            return buffer.ToString();
        }
    }
}
