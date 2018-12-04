using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day4A : IProblem
    {
        private readonly ParserType _parser;

        public Day4A(ParserType parser) { _parser = parser; }

        public Day4A() : this(new ParserType("day04.in")) { }

        public virtual string Solve()
        {
            var eventStrings = _parser.GetData();
            var guardEvents = ParseGuardEvents(eventStrings);
            var guardShifts = GetGuardShifts(guardEvents);
            var timeAsleep = GetTotalTimeAsleep(guardShifts);
            var sleepiestGuardId = GetLongestSleepingGuard(timeAsleep);
            var sleepiestGuardShifts = guardShifts.Where(gs => gs.GuardId == sleepiestGuardId).ToList();

            var optimalMinute = GetMostAsleepMinuteAcrossShifts(sleepiestGuardShifts);


            return (int.Parse(sleepiestGuardId) * optimalMinute).ToString();
        }

        protected static int GetMostAsleepMinuteAcrossShifts(List<GuardShift> sleepiestGuardShifts)
        {
            var byMinuteSleepCount = new int[60];
            foreach (var shift in sleepiestGuardShifts)
                foreach (var minute in shift.GetMinutesAsleep())
                    byMinuteSleepCount[minute]++;

            var maxCount = 0;
            var optimalMinute = -1;
            for (var minute = 0; minute < 60; minute++)
            {
                if (byMinuteSleepCount[minute] > maxCount)
                {
                    maxCount = byMinuteSleepCount[minute];
                    optimalMinute = minute;
                }
            }

            return optimalMinute;
        }

        protected static Dictionary<string, TimeSpan> GetTotalTimeAsleep(IList<GuardShift> guardShifts)
        {
            var timeAsleep = new Dictionary<string, TimeSpan>();
            foreach (var shift in guardShifts)
            {
                if (!timeAsleep.ContainsKey(shift.GuardId))
                    timeAsleep[shift.GuardId] = TimeSpan.Zero;
                timeAsleep[shift.GuardId] += shift.GetTimeAsleep();
            }

            return timeAsleep;
        }

        protected IList<GuardShift> GetGuardShifts(List<GuardEvent> guardEvents)
        {
            var shifts = new List<GuardShift>();
            GuardShift currentShift = null;
            var lastSleep = DateTime.MinValue;
            foreach (var e in guardEvents)
            {
                if (e.EventType == GuardEventType.Start)
                {
                    if (currentShift != null)
                        shifts.Add(currentShift);
                    currentShift = new GuardShift(e.GuardId);
                }
                else if (e.EventType == GuardEventType.Sleep)
                {
                    lastSleep = e.Time;
                }
                else if (e.EventType == GuardEventType.Wake)
                {
                    currentShift?.AddSleepInterval(new SleepInterval(lastSleep, e.Time));
                }
            }

            if (currentShift != null)
            {
                shifts.Add(currentShift);
            }

            return shifts;
        }

        protected static string GetLongestSleepingGuard(Dictionary<string, TimeSpan> sleepTotals)
        {
            var maxTime = TimeSpan.Zero;
            var sleepiestGuardId = string.Empty;
            foreach (var kvp in sleepTotals)
            {
                if (kvp.Value > maxTime)
                {
                    maxTime = kvp.Value;
                    sleepiestGuardId = kvp.Key;
                }
            }

            return sleepiestGuardId;
        }

        protected static List<GuardEvent> ParseGuardEvents(IEnumerable<string> eventStrings)
        {
            var guardEvents = new List<GuardEvent>();
            foreach (var eventString in eventStrings)
            {
                var dateString = eventString.Substring(1, 16);
                var date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture);
                var guardEventType = GetGuardEventType(eventString);

                var guardEvent = new GuardEvent(date, guardEventType);
                if (guardEventType == GuardEventType.Start)
                {
                    var idStart = eventString.IndexOf('#') + 1;
                    var idEnd = idStart + 1;
                    while (eventString[idEnd] != ' ') idEnd++;

                    var id = eventString.Substring(idStart, idEnd - idStart);
                    guardEvent.GuardId = id;
                }

                guardEvents.Add(guardEvent);
            }

            guardEvents = guardEvents.OrderBy(e => e.Time).ToList();

            return guardEvents;
        }

        protected static GuardEventType GetGuardEventType(string eventString)
        {
            GuardEventType guardEventType;
            switch (eventString[19])
            {
                case 'w':
                    guardEventType = GuardEventType.Wake;
                    break;
                case 'f':
                    guardEventType = GuardEventType.Sleep;
                    break;
                case 'G':
                    guardEventType = GuardEventType.Start;
                    break;
                default:
                    throw new Exception("Unrecognized guard event");
            }

            return guardEventType;
        }
    }

    public class GuardShift
    {
        public string GuardId { get; }

        private readonly List<SleepInterval> _intervals = new List<SleepInterval>();

        public GuardShift(string guardId)
        {
            GuardId = guardId;
        }

        public void AddSleepInterval(SleepInterval i)
        {
            _intervals.Add(i);
        }

        public TimeSpan GetTimeAsleep()
        {
            var total = TimeSpan.Zero;
            foreach (var i in _intervals)
                total += i.GetTotal();
            return total;
        }

        public IEnumerable<int> GetMinutesAsleep()
        {
            foreach (var i in _intervals)
                foreach (var min in i.GetMinutes())
                    yield return min;
        }
    }

    public class SleepInterval
    {
        private readonly DateTime _sleep;
        private readonly DateTime _wake;

        public SleepInterval(DateTime sleep, DateTime wake)
        {
            _sleep = sleep;
            _wake = wake;
        }

        public TimeSpan GetTotal()
        {
            return _wake - _sleep;
        }

        public IList<int> GetMinutes()
        {
            var minutes = new List<int>(_wake.Minute - _sleep.Minute);
            for (var i = _sleep.Minute; i < _wake.Minute; i++)
                minutes.Add(i);
            return minutes;
        }
    }

    public class GuardEvent
    {
        public DateTime Time { get; }
        public GuardEventType EventType { get; }
        public string GuardId { get; set; }
        public GuardEvent(DateTime time, GuardEventType eventType)
        {
            Time = time;
            EventType = eventType;
        }
    }

    public enum GuardEventType
    {
        Start,
        Sleep,
        Wake
    }
}
