using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class DayNine
    {
        private readonly int _playerCount;
        private List<Player> _players;
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
            _players = new List<Player>(_playerCount);
            for (var i = 0; i < _playerCount; i++)
            {
                _players.Add( new Player { Score = 0 });
            }
        }

        public void GetHiscore()
        {
            Play();
            Console.WriteLine($"The hiscore is {_players.Max(x => x.Score)}");
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
                var currentPlayer = _players[i % _playerCount];
                var playerIndex = i % _playerCount;
                if (i % 23 == 0)
                {
                    _currentNode = GetPreviousDistance(_currentNode, 6);
                    currentPlayer.Score += _currentNode.Value;
                    var temp = _currentNode.Next;
                    _circle.Remove(_currentNode);
                    currentPlayer.Score += i;
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

        private LinkedListNode<int> SeekTo(LinkedListNode<int> node, int index)
        {
            for (int i = 0; i < index; i++)
            {
                if (node.Next == null)
                {
                    node = _circle.First;
                }
                else
                {
                    node = node.Next;
                }
            }
            return node;
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/daynine.txt").Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }

    public class Player
    {
        public long Score { get; set; }
    }
}
