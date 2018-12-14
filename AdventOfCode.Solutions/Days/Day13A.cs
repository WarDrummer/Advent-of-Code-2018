using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Coordinates;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day13A : IProblem
    {
        protected readonly ParserType Parser;

        public Day13A(ParserType parser) { Parser = parser; }

        public Day13A() : this(new ParserType("day13.in")) { }

        public virtual string Solve()
        {
            var trackLookup = GetCartsAndTracks(out var carts);

            while (true)
            {
                var sortedCarts = carts.OrderBy(c => c.Coordinate).ToList();
                foreach (var cart in sortedCarts)
                {
                    cart.Direction = trackLookup[cart.Coordinate].NextDirection(cart);
                    cart.Tick();

                    if (sortedCarts.Count(c => c.Coordinate.X == cart.Coordinate.X && c.Coordinate.Y == cart.Coordinate.Y) > 1)
                        return cart.Coordinate.ToString();
                }
            }
        }

        protected Dictionary<Coordinate, Track> GetCartsAndTracks(out List<Cart> carts)
        {
            var trackLookup = new Dictionary<Coordinate, Track>();
            carts = new List<Cart>();
            var lines = Parser.GetData();
            var col = 0;
            foreach (var line in lines)
            {
                var row = 0;
                foreach (var c in line)
                {
                    var coord = new Coordinate(row, col);

                    if (!char.IsWhiteSpace(c))
                        trackLookup.Add(coord, Track.Create(c, coord));

                    if (c == '<')
                        carts.Add(new Cart(coord, CartDirection.Left));
                    else if (c == '>')
                        carts.Add(new Cart(coord, CartDirection.Right));
                    else if (c == '^')
                        carts.Add(new Cart(coord, CartDirection.Up));
                    else if (c == 'v')
                        carts.Add(new Cart(coord, CartDirection.Down));

                    row++;
                }

                col++;
            }

            return trackLookup;
        }
    }

    public enum CartDirection
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3
    }

    public class Cart
    {
        private int _turnIndex;
        public Coordinate Coordinate { get; set; }
        public CartDirection Direction { get; set; }

        public Cart(Coordinate coordinate, CartDirection direction)
        {
            Coordinate = coordinate;
            Direction = direction;
        }
        
        private static readonly Dictionary<CartDirection, CartDirection> LeftTurn = new Dictionary<CartDirection, CartDirection>
        {
            { CartDirection.Down, CartDirection.Right },
            { CartDirection.Right, CartDirection.Up },
            { CartDirection.Up, CartDirection.Left },
            { CartDirection.Left, CartDirection.Down }
        };

        private static readonly Dictionary<CartDirection, CartDirection> RightTurn = new Dictionary<CartDirection, CartDirection>
        {
            { CartDirection.Down, CartDirection.Left },
            { CartDirection.Right, CartDirection.Down },
            { CartDirection.Up, CartDirection.Right },
            { CartDirection.Left, CartDirection.Up }
        };

        public CartDirection GetNextTurn()
        {
            var direction = Direction;
            
            if (_turnIndex == 0)
                direction = LeftTurn[Direction];
            else if (_turnIndex == 2)
                direction =  RightTurn[Direction];

            _turnIndex = (_turnIndex + 1) % 3;
            return direction;
        }

        public void Tick()
        {
            switch (Direction)
            {
                case CartDirection.Up:
                    Coordinate = new Coordinate(Coordinate.X, Coordinate.Y - 1);
                    break;
                case CartDirection.Right:
                    Coordinate = new Coordinate(Coordinate.X + 1, Coordinate.Y);
                    break;
                case CartDirection.Down:
                    Coordinate = new Coordinate(Coordinate.X, Coordinate.Y + 1);
                    break;
                case CartDirection.Left:
                    Coordinate = new Coordinate(Coordinate.X - 1, Coordinate.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class RightCornerTrack : Track
    {
        public RightCornerTrack(Coordinate coordinate) 
            : base(coordinate) { }

        public override CartDirection NextDirection(Cart cart)
        {
            if (cart.Direction == CartDirection.Left)
                return CartDirection.Down;
            if (cart.Direction == CartDirection.Right)
                return CartDirection.Up;
            if (cart.Direction == CartDirection.Up)
                return CartDirection.Right;
            if (cart.Direction == CartDirection.Down)
                return CartDirection.Left;

            throw new Exception("Incoming direction not appropriate for right corner track");
        }
    }

    public class LeftCornerTrack : Track
    {
        public LeftCornerTrack(Coordinate coordinate) 
            : base(coordinate) { }

        public override CartDirection NextDirection(Cart cart)
        {
            if (cart.Direction == CartDirection.Left)
                return CartDirection.Up;
            if (cart.Direction == CartDirection.Right)
                return CartDirection.Down;
            if (cart.Direction == CartDirection.Up)
                return CartDirection.Left;
            if (cart.Direction == CartDirection.Down)
                return CartDirection.Right;

            throw new Exception("Incoming direction not appropriate for left corner track");
        }
    }

    public class IntersectionTrack : Track
    {
        public IntersectionTrack(Coordinate coordinate) : base(coordinate) { }

        public override CartDirection NextDirection(Cart cart)
        {
            return cart.GetNextTurn();
        }
    }

    public class Track
    {
        public Coordinate Coordinate { get; }

        protected Track(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public virtual CartDirection NextDirection(Cart cart)
        {
            return cart.Direction;
        }

        public static Track Create(char trackType, Coordinate coordinate)
        {
            switch (trackType)
            {
                case '/':
                    return new RightCornerTrack(coordinate);
                case '\\':
                    return new LeftCornerTrack(coordinate);
                case '+':
                    return new IntersectionTrack(coordinate);
                case '-': case '<': case '>': // horizontal
                case '|': case '^': case 'v': // vertical
                    return new Track(coordinate);
                default:
                    throw new Exception("Unknown track type");
            }
        }
    }
}
