using System;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2018");
            Console.WriteLine("Please enter in a day (1-25):");
            var input = ParseInput(Console.ReadLine());
            //TODO: Days go here
        }

        static int ParseInput(string input)
        {
            var isValid = int.TryParse(input, out var number);
            if (!isValid)
                return -1;
            return number;
        }
    }
}
