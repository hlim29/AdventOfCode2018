using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayOne
    {
        public void Calibrate()
        {
            var fileContents = ReadFile();
            var parsedContents = ParseFile(fileContents);
            Console.WriteLine($"The sum is: {parsedContents.Sum()}");
        }

        public void FirstFrequency()
        {
            var fileContents = ReadFile();
            var parsedContents = ParseFile(fileContents);

            var frequencies = new List<int>();

            var currentFrequency = 0;

            while (true)
            {
                foreach (var offset in parsedContents)
                {
                    if (frequencies.Contains(currentFrequency))
                    {
                        Console.WriteLine($"The first duplicate is: {currentFrequency}");
                        return;
                    }

                    frequencies.Add(currentFrequency);
                    currentFrequency += offset;
                }
            }
        }

        public string ReadFile()
        {
            return File.ReadAllText(@"Inputs/dayOne.txt");
        }

        public List<int> ParseFile(string fileContents)
        {
            var delimitedFile = fileContents.Split("\n");
            var result = new List<int>();

            foreach(var line in delimitedFile)
            {
                if (int.TryParse(line, out var parsedLine))
                {
                    result.Add(parsedLine);
                }
            }
            return result;
        }
    }
}
