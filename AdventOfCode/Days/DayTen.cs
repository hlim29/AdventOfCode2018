using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayTen
    {
        private List<Square> _board;
        private int _iterations;

        public DayTen()
        {
            _board = new List<Square>();
            var input = ReadFile();
            foreach(var line in input)
            {
                ProcessLine(line);
            }
            MoveUntil100Chars();
        }

        public void UserInput()
        {
            Console.WriteLine("Press any key to advance, 'x' to exit");
            while (Console.ReadKey().Key != ConsoleKey.X)
            {
                Console.WriteLine();
                Print();
                Move();
                Console.WriteLine("Press any key to advance, 'x' to exit");
            }
        }

        private void MoveUntil100Chars()
        {
            while ((_board.Max(x => x.Coord.X) - (_board.Min(x => x.Coord.X)) > 100)){
                Move();
            }
        }

        private void Move()
        {
            _iterations++;
            var keys = _board.ToList();

            for (int i = 0; i < _board.Count(); i++)
            {
                var newX = _board[i].Coord.X + _board[i].Velocity.X;
                var newY = _board[i].Coord.Y + _board[i].Velocity.Y;

                _board[i] = new Square
                {
                    Coord = (newX, newY),
                    Velocity = _board[i].Velocity
                };
            }
        }

        private void Print()
        {
            var output = new List<string>();
            var topLeft = (X: _board.Min(x => x.Coord.X), Y: _board.Min(x => x.Coord.Y));
            var bottomRight = (X: _board.Max(x => x.Coord.X), Y: _board.Max(x => x.Coord.Y));

            for (var y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                var line = new StringBuilder();
                for (var x = topLeft.X; x <= bottomRight.X; x++)
                {
                    if (_board.Where(e => e.Coord.X == x && e.Coord.Y == y).Count() > 0)
                    {
                        line.Append('#');
                    }
                    else
                    {
                        line.Append('.');
                    }
                }
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine($"Moves: {_iterations}-----------------------------------------");
        }

        private void ProcessLine(string line)
        {
            var output = line.Split(new char []{ '<',',','>'});
            _board.Add(new Square
            {
                Coord = (int.Parse(output[1]), int.Parse(output[2])),
                Velocity = (int.Parse(output[4]), int.Parse(output[5]))
            });
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/dayten.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }

    public class Square
    {
        public (int X, int Y) Coord { get; set; }
        public (int X, int Y) Velocity { get; set; }
    }
}
