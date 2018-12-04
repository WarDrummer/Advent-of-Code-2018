using System.Collections.Generic;
using AdventOfCode.Solutions.Parsers;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day4B : Day4A
    {
        private readonly ParserType _parser;

        public Day4B(ParserType parser) { _parser = parser; }

        public Day4B() : this(new ParserType("day04.in")) { }

        public override string Solve()
        {
            var eventStrings = _parser.GetData();
            var guardEvents = ParseGuardEvents(eventStrings);
            var guardShifts = GetGuardShifts(guardEvents);

            var aggregateShifts = MapShiftsToGuardId(guardShifts);

            var guardId = string.Empty;
            var maxAsleepMinute = 0;
            foreach (var guard in aggregateShifts)
            {
                var optimalMinute = GetMostAsleepMinuteAcrossShifts(guard.Value);
                if (optimalMinute > maxAsleepMinute)
                {
                    maxAsleepMinute = optimalMinute;
                    guardId = guard.Key;
                }
            }

            return (int.Parse(guardId) * maxAsleepMinute).ToString();
        }

        private static Dictionary<string, List<GuardShift>> MapShiftsToGuardId(IList<GuardShift> guardShifts)
        {
            var aggregateShifts = new Dictionary<string, List<GuardShift>>();
            foreach (var guardShift in guardShifts)
            {
                if (!aggregateShifts.ContainsKey(guardShift.GuardId))
                    aggregateShifts[guardShift.GuardId] = new List<GuardShift>();

                aggregateShifts[guardShift.GuardId].Add(guardShift);
            }

            return aggregateShifts;
        }
    }
}