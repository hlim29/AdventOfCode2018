using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class DaySix
    {
        private List<(int X, int Y)> Coords;
        private Dictionary<(int X, int Y), (int X, int Y)> ClosestPoint;
        private Dictionary<(int X, int Y), int> SafePoints;
        private (int X, int Y) TopLeft;
        private (int X, int Y) BottomRight;

        public DaySix()
        {
            ClosestPoint = new Dictionary<(int X, int Y), (int X, int Y)>();
            SafePoints = new Dictionary<(int X, int Y), int>();

            var file = ReadFile();
            Coords = ParseInput(file).ToList();
            GetBoundaries();
        }

        public void GetSafestLocation()
        {
            const int threshold = 10000;
            GetDistances();
            Console.WriteLine($"There are {SafePoints.Where(x => x.Value < 10000).Count()} safe points with a total distance of at most {threshold}.");
        }

        private int TotalDistance(int x, int y)
        {
            return Coords.Select(point => Math.Abs(x - point.X) + Math.Abs(y - point.Y)).Sum();
        }

        private void GetDistances()
        {
            for (var y = TopLeft.Y; y <= BottomRight.Y; y++)
            {
                for (var x = TopLeft.X; x <= BottomRight.X; x++)
                {
                    SafePoints.Add((x, y), TotalDistance(x, y));
                }
            }
        }

        public void GetLargestFinite()
        {
            CalculateClosest();
            var finite = GetLargestFiniteArea(GetFiniteAreas());
            Console.WriteLine($"The largest finite area is {finite}");
        }

        private int GetLargestFiniteArea(List<(int X, int Y)> grid)
        {
            return grid.GroupBy(x => x).Select(x => x.Count()).Max();
        }

        private List<(int X, int Y)> GetFiniteAreas()
        {
            var xBarriers = new int[] { TopLeft.X, BottomRight.X };
            var yBarriers = new int[] { TopLeft.Y, BottomRight.Y };
            var infiniteAreas = ClosestPoint.Where(point => xBarriers.Contains(point.Key.X) || yBarriers.Contains(point.Key.Y)).Select(x => x.Value).Distinct().ToList();
            return ClosestPoint.Where(x => !infiniteAreas.Contains(x.Value)).Select(x => x.Value).ToList();
        }

        private void CalculateClosest()
        {
            for (var y = TopLeft.Y; y <= BottomRight.Y; y++)
            {
                for (var x = TopLeft.X; x <= BottomRight.X; x++)
                {
                    var groups = Coords.GroupBy(e => e);
                    var distances = Coords.Select(point => (Point: point, Distance: Math.Abs(x - point.X) + Math.Abs(y - point.Y)));

                    var minDistance = distances.Min(e => e.Distance);
                    if (distances.Count(distance => distance.Distance == minDistance) > 1)
                    {
                        continue;
                    }

                    ClosestPoint.Add((X: x, Y: y), distances.First(distance => distance.Distance == minDistance).Point);
                }
            }
        }

        private void GetBoundaries()
        {
            TopLeft = (X: Coords.Min(x => x.X), Y: Coords.Min(x => x.Y));
            BottomRight = (X: Coords.Max(x => x.X), Y: Coords.Max(x => x.Y));
        }

        private IEnumerable<(int x, int y)> ParseInput(List<string> input)
        {
            return input.Select(e =>
            {
                var delimited = e.Split(',');
                int.TryParse(delimited[0], out var x);
                int.TryParse(delimited[1], out var y);
                return (x: x, y: y);
            });
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/daysix.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
