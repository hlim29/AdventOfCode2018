using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class DayFour
    {
        private SortedList<DateTime, string> Log;
        private Dictionary<string, List<int>> GuardSleepLog;

        public DayFour()
        {
            Log = new SortedList<DateTime, string>();
            GuardSleepLog = new Dictionary<string, List<int>>();
            foreach (var line in ReadFile())
            {
                var timestamp = line.Split(']')[0];
                var action = line.Split(']')[1];
                Log.Add(DateTime.ParseExact(timestamp, "[yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture), action.Trim());
            }
            string currentGuard = "";
            var currentTimestamp = DateTime.Now;
            foreach (var item in Log)
            {
                if (item.Value.Contains("Guard"))
                {
                    currentGuard = StripGuardShiftDescription(item.Value);
                }
                if (item.Value.Contains("falls"))
                {
                    currentTimestamp = item.Key;
                }
                if (item.Value.Contains("wakes"))
                {
                    if (!GuardSleepLog.ContainsKey(currentGuard))
                    {
                        GuardSleepLog[currentGuard] = new List<int>();
                    }
                    for (int i = currentTimestamp.Minute; i < item.Key.Minute; i++)
                    {
                        GuardSleepLog[currentGuard].Add(i);
                    }
                }
            }
        }

        public void GetSleepiestGuard()
        {
            var guard = GetMaxSleeping();
            Console.WriteLine($"Sleepiest guard: {guard.Id} sleeps the most on minute {guard.Minute}. Answer is {int.Parse(guard.Id) * guard.Minute}.");
        }

        public void GetSleepiestGuardByMinute()
        {
            var guard = AsleepOnSameMinute();
            Console.WriteLine($"Guard: {guard.Id} sleeps the most on minute {guard.Minute}. Answer is {int.Parse(guard.Id) * guard.Minute}.");
        }

        private string StripGuardShiftDescription(string line)
        {
            return line.Replace("Guard #", "").Replace(" begins shift", "");
        }

        private Guard GetMaxSleeping()
        {
            var max = new Guard();
            var group = GuardSleepLog.ToList();
            var sleepiestGuard = group.First(x => x.Value.Count == group.Max(y => y.Value.Count));

            return GetMostFrequentSleepMinute(sleepiestGuard.Key, sleepiestGuard.Value);
        }

        private Guard GetMostFrequentSleepMinute(string guardId, List<int> sleepTimes)
        {
            var item = sleepTimes.GroupBy(y => y)
                .Select(y => new GuardActivityCount { SleepMinute = y.Key, SleepCount = y.Count() })
                .OrderBy(y => y.SleepCount);
            var maxMinute = item.Last().SleepMinute;
            var minuteCount = item.Last().SleepCount;
            return new Guard { Id = guardId, Minute = maxMinute, MinuteCount = minuteCount };
        }

        private Guard AsleepOnSameMinute()
        {
            var max = new Guard();
            var group = GuardSleepLog.ToList();
            group.ForEach(x =>
            {
                var currentGuard = GetMostFrequentSleepMinute(x.Key, x.Value);
                if (currentGuard.MinuteCount > max.MinuteCount)
                {
                    max.MinuteCount = currentGuard.MinuteCount;
                    max.Minute = currentGuard.Minute;
                    max.Id = x.Key;
                }
            });
            return max;
        }

        private List<string> ReadFile()
        {
            return File.ReadAllText(@"Inputs/dayfour.txt").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }

    public class GuardActivityCount
    {
        public int SleepMinute { get; set; }
        public int SleepCount { get; set; }
    }

    public class Guard
    {
        public string Id { get; set;}
        public int Minute { get; set; }
        public int MinuteCount { get; set; }
    }
}
