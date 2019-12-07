using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day15A : IProblem
    {
        protected readonly ParserType Parser;

        public Day15A(ParserType parser) { Parser = parser; }

        public Day15A() : this(new ParserType("day15.in")) { }

        public virtual string Solve()
        {
            var map = new BattleMap();
            var x = 0;
            var y = 0;
            var init = 0;
            foreach(var line in Parser.GetData())
            {
                x = 0;
                foreach(var c in line)
                {
                    if (c == '#')
                        map.AddWall(new Point(x, y));
                    else if (c == 'E' || c =='G')
                        map.AddCreatures(new Creature(init++, c), new Point(x, y));
                    x++;
                }
                y++;
            }

            map.SetBounds(x, y);
           
            return map.SimulateBattle().ToString();
        }
    }

    public class BattleMap
    {
        private HashSet<Point> _walls = new HashSet<Point>();
        private Dictionary<Point, Creature> _locationToCreature = new Dictionary<Point, Creature>();
        private Dictionary<Creature, Point> _creatureToLocation = new Dictionary<Creature, Point>();
        private List<Creature> _initiative = new List<Creature>();
        private int MaxX = int.MaxValue;
        private int MaxY = int.MinValue;

        public void AddWall(Point p)
        {
            _walls.Add(p);
        }

        public void AddCreatures(Creature c, Point p)
        {
            _locationToCreature.Add(p, c);
            _creatureToLocation.Add(c, p);
            _initiative.Add(c);
        }

        public void SetBounds(int x, int y)
        {
            MaxX = x;
            MaxY = y;
        }

        public int SimulateBattle()
        {
            var rounds = 0;
            while(_initiative.Select(i => i.Race).Distinct().Count() > 1)
            {
                rounds++;

                Console.WriteLine($"-------------Round {rounds}-------------");
                Print();

                foreach (var creature in _initiative)
                {
                    var enemy = GetPreferredEnemy(creature);
                    if(enemy == null)
                    {
                        Move(creature);
                    }

                    if (enemy != null)
                    {
                        enemy.HP -= creature.AttackPower;
                        if (enemy.HP <= 0)
                        {
                            RemoveFromCombat(enemy);
                        }
                    }
                }

                CleanupInitiative();
            }

            return rounds * _initiative.Select(i => i.HP).Sum();
        }

        private void Move(Creature c)
        {
            // Need to re-work this
            // * Targets
            // * In Range
            // * Reachable
            // * Nearest
            // * Chosen


            var distance = int.MaxValue;
            var reachable = new List<Point>();
            foreach(var e in GetEnemies(c))
            {
                foreach (var t in GetThreatenableLocations(e))
                {
                    var tDistance = GetDistance(c, t);
                    if (distance > tDistance)
                    {
                        reachable = new List<Point> { t };
                        distance = tDistance;
                    }
                    else if(distance == tDistance)
                    {
                        reachable.Add(t);
                    }
                }
            }

            if (reachable.Count > 0)
            {
                reachable.Sort((t1, t2) => t1.Y == t2.Y ? t2.X - t1.X : t1.Y - t2.Y);
                var target = reachable.First();

                // Get step
                var location = GetCreatureLocation(c);
                if (target.Y > location.Y)
                {
                    target = new Point(location.X, location.Y + 1);
                }
                else if (target.X < location.X)
                {
                    target = new Point(location.X - 1, location.Y);
                }
                else if (target.X > location.X)
                {
                    target = new Point(location.X + 1, location.Y);
                }
                else if (target.Y > location.Y)
                {
                    target = new Point(location.X, location.Y + 1);
                }

                _locationToCreature.Remove(_creatureToLocation[c]);
                _locationToCreature[target] = c;
                _creatureToLocation[c] = target;
            }
        }

        private IEnumerable<Point> GetThreatenableLocations(Creature c)
        {
            var location = GetCreatureLocation(c);

            var pt = new Point(location.X, location.Y + 1);
            if (!_locationToCreature.ContainsKey(pt) &&
                !_walls.Contains(pt))
            {
                yield return pt;
            }

            // left
            pt = new Point(location.X - 1, location.Y);
            if (!_locationToCreature.ContainsKey(pt) &&
                !_walls.Contains(pt))
            {
                yield return pt;
            }

            // right 
            pt = new Point(location.X + 1, location.Y);
            if (!_locationToCreature.ContainsKey(pt) &&
                !_walls.Contains(pt))
            {
                yield return pt;
            }

            // bottom 
            pt = new Point(location.X, location.Y - 1);
            if (!_locationToCreature.ContainsKey(pt) &&
                !_walls.Contains(pt))
            {
                yield return pt;
            }
        }

        private int GetDistance(Creature c1, Creature c2)
        {
            // need to convert to BFS
            var l1 = GetCreatureLocation(c1);
            var l2 = GetCreatureLocation(c2);
            return Math.Abs(l1.X - l2.X) + Math.Abs(l1.Y - l2.Y);
        }

        private int GetDistance(Creature c1, Point l2)
        {
            // need to convert to BFS
            var l1 = GetCreatureLocation(c1);
            return Math.Abs(l1.X - l2.X) + Math.Abs(l1.Y - l2.Y);
        }

        private void CleanupInitiative()
        {
            _initiative = _initiative.Where(c => c.HP > 0).ToList();
        }

        private Creature GetPreferredEnemy(Creature c)
        {
            Creature preferredEnemy = null;
            foreach(var enemy in GetAttackableEnemies(c))
            {
                if (preferredEnemy == null || preferredEnemy.HP > enemy.HP)
                    preferredEnemy = enemy;
            }
            return preferredEnemy;
        }

        private IEnumerable<Creature> GetEnemies(Creature c)
        {
            return _creatureToLocation.Keys.Where(e => e.Race != c.Race);
        }

        private IEnumerable<Creature> GetAttackableEnemies(Creature c)
        {
            var location = GetCreatureLocation(c);

            var top = new Point(location.X, location.Y + 1);
            if (_locationToCreature.ContainsKey(top) && _locationToCreature[top].Race != c.Race)
                yield return _locationToCreature[top];

            var left = new Point(location.X - 1, location.Y);
            if (_locationToCreature.ContainsKey(left) && _locationToCreature[left].Race != c.Race)
                yield return _locationToCreature[left];

            var right = new Point(location.X + 1, location.Y);
            if (_locationToCreature.ContainsKey(right) && _locationToCreature[right].Race != c.Race)
                yield return _locationToCreature[right];

            var bottom = new Point(location.X, location.Y - 1);
            if (_locationToCreature.ContainsKey(bottom) && _locationToCreature[bottom].Race != c.Race)
                yield return _locationToCreature[bottom];
        }

        private Point GetCreatureLocation(Creature c)
        {
            return _creatureToLocation.ContainsKey(c) ? _creatureToLocation[c] : Point.OutOfBounds;
        }

        private void RemoveFromCombat(Creature c)
        {
            Point key = GetCreatureLocation(c);
            if (!key.Equals(Point.OutOfBounds))
                _locationToCreature.Remove(key);
            _creatureToLocation.Remove(c);
        }

        public void Print()
        {
            var sb = new StringBuilder(MaxX * MaxY);
            for(var y = 0; y < MaxY; y++)
            {
                for(var x = 0; x < MaxX; x++)
                {
                    Point p = new Point(x, y);
                    if (_locationToCreature.ContainsKey(p))
                        sb.Append(_locationToCreature[p].Race);
                    else if (_walls.Contains(p))
                        sb.Append('#');
                    else
                        sb.Append('.');
                }
                sb.Append(Environment.NewLine);
            }

            Console.Write(sb);
        }
    }

    public class Creature
    {
        public int Initiative { get; }
        public char Race { get; }
        public int HP { get; set; } = 200;
        public int AttackPower { get; } = 3;

        public Creature(int initiative, char race)
        {
            Initiative = initiative;
            Race = race;
        }
    }

    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly static Point OutOfBounds = new Point(int.MinValue, int.MinValue);

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;
            return Equals((Point)obj);
        }
        public bool Equals(Point p)
        {
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
