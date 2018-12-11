using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayEleven
    {
        private Dictionary<(int X, int Y), int> _grid;
        private readonly int _serial;

        public DayEleven()
        {
            _grid = new Dictionary<(int X, int Y), int>();
            _serial = 3214;
            Populate();
        }

        public void GetLargestFixedSquare()
        {
            var largest = GetLargestCluster(3, 3);
            Console.WriteLine($"Largest 3x3 is from {largest.Coord.Item1},{largest.Coord.Item2}, sum is: {largest.Sum}");
        }

        public void GetLargestFlexibleSquare()
        {
            var largest = GetLargestCluster(1, 300);
            Console.WriteLine($"Largest 3x3 is from {largest.Coord.Item1},{largest.Coord.Item2}, sum is: {largest.Sum}");
        }

        private int CalculatePower(int x, int y)
        {
            var rackId = x + 10;
            var power = (((rackId * y + _serial)*rackId)/100%10) - 5;
            return power;
        }
        
        private ((int, int) Coord, int Sum, int SquareSize) GetLargestCluster(int start, int end)
        {
            var max = (Coord: (0, 0), Sum: 0, SquareSize: 0);
            for (int i = start; i <= end; i++)
            {
                for (int y = 1; y <= 300-i; y++)
                {
                    for (int x = 1; x <= 300-i; x++)
                    {
                        var sum = 0;
                        for (int internalY = y; internalY < y + i; internalY++)
                        {
                            for (int internalX = x; internalX < x + i; internalX++)
                            {
                                sum += _grid[(internalX, internalY)];
                            }
                        }

                        if (sum > max.Sum)
                        {
                            max.Coord = (x, y);
                            max.Sum = sum;
                            max.SquareSize = i;
                        }
                    }
                }
            }
            
            return max;
        }

        private void Populate()
        {
            for (int y = 1; y <= 300; y++)
            {
                for (int x = 1; x <= 300; x++)
                {
                    var power = CalculatePower(x, y);
                    _grid.Add((x, y), power);
                }
            }
        }
    }
}
