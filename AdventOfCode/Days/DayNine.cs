using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class DayNine
    {
        private readonly int _playerCount;
        private long[] _players;
        private int _lastMarbleValue;
        private LinkedList<int> _circle;
        private LinkedListNode<int> _currentNode;

        public DayNine()
        {
            var input = ReadFile();
            _playerCount = int.Parse(input[0]);
            _lastMarbleValue = int.Parse(input[6]);
            _circle = new LinkedList<int>();
            _circle.AddFirst(0);
            _players = new long[_playerCount];
        }

        public void GetHiscore()
        {
            Play();
            Console.WriteLine($"The hiscore is {_players.Max()}");
        }

        public void GetHiscoreWithHighMarble()
        {
            _lastMarbleValue *= 100;
            GetHiscore();
        }

        private void Play()
        {
            _currentNode = _circle.First;
            for (int i = 1; i <= _lastMarbleValue; i++)
            {
                var currentPlayerIndex = (i % _playerCount);
                var playerIndex = i % _playerCount;
                if (i % 23 == 0)
                {
                    _currentNode = GetPreviousDistance(_currentNode, 6);
                    _players[currentPlayerIndex] += _currentNode.Value;
                    var temp = _currentNode.Next;
                    _circle.Remove(_currentNode);
                    _players[currentPlayerIndex] += i;
                    _currentNode = GetPrevious(temp);
                }
                else
                {
                    _currentNode = GetNextInsertion(_currentNode);
                    _circle.AddAfter(_currentNode, i);
                }
            }
        }

        private LinkedListNode<int> GetNextInsertion(LinkedListNode<int> node)
        {
            for (var i = 0; i < 2; i++)
            {
                node = GetNext(node);
            }
            return node;
        }

        private LinkedListNode<int> GetPreviousDistance(LinkedListNode<int> node, int length)
        {
            for (var i = 0; i < length; i++)
            {
                node = GetPrevious(node);
            }
            return node;
        }

        private LinkedListNode<int> GetPrevious(LinkedListNode<int> node) => node.Previous ?? _circle.Last;

        private LinkedListNode<int> GetNext(LinkedListNode<int> node) => node.Next ?? _circle.First;

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/daynine.txt").Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
