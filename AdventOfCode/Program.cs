﻿using AdventOfCode.Days;
using System;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2018");
            var input = -1;
            while (input == -1)
            {
                input = AskForDay();
                switch (input)
                {
                    case 1:
                        var calibrator = new DayOne();
                        calibrator.Calibrate();
                        calibrator.FirstFrequency();
                        break;
                    case 2:
                        var badge = new DayTwo();
                        badge.Process();
                        badge.GetCorrectBox();
                        break;
                    case 3:
                        var fabric = new DayThree();
                        fabric.CountOverlaps();
                        fabric.ShowNonOverlap();
                        break;
                    case -1:
                        Console.WriteLine("Invalid day. Please try again.");
                        break;
                    default:
                        Console.WriteLine("Invalid day. Please try again.");
                        break;
                }
                input = -1;
            }
            
            //TODO: Days go here
        }

        static int AskForDay()
        {
            Console.WriteLine("Please enter in a day (1-25):");
            return ParseInput(Console.ReadLine());
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
