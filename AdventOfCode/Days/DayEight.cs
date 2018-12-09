using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class DayEight
    {
        private List<int> _nodes;
        private List<int> _processedNodes;

        public DayEight()
        {
            //_nodes = new List<int> { 2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2 };
            _nodes = ReadFile();
            _processedNodes = new List<int>();
            Process(0);
            Console.WriteLine($"Sum is: {_processedNodes.Sum()}");
        }

        private int Process(int start)
        {
            var childrenCount = _nodes[start++];
            var manifestCount = _nodes[start++];

            while (childrenCount > 0)
            {
                start = Process(start);
                childrenCount--;
            }

            _processedNodes.AddRange(_nodes.Skip(start).Take(manifestCount).ToList());
            start += manifestCount;

            return start;
        }


        private List<int> ReadFile()
        {
            return File.ReadAllText(@"Inputs/dayeight.txt").Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
        }
    }

    public class Node {
        public List<int> Manifest { get; set; }
        public List<Node> Children { get; set; }

        public Node()
        {
            Manifest = new List<int>();
            Children = new List<Node>();
        }
    }

}
