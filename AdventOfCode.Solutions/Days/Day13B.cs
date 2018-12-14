using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day13B : Day13A
    {
        public override string Solve()
        {
            var trackLookup = GetCartsAndTracks(out var carts);

            var sortedCarts = carts.OrderBy(c => c.Coordinate).ToList();
            while (sortedCarts.Count > 1)
            {
                foreach (var cart in sortedCarts)
                {
                    cart.Direction = trackLookup[cart.Coordinate].NextDirection(cart);
                    cart.Tick();

                    if (sortedCarts.Count(c =>
                            c.Coordinate.X == cart.Coordinate.X && c.Coordinate.Y == cart.Coordinate.Y) > 1)
                    {
                        var coord = cart.Coordinate;
                        carts.RemoveAll(c => coord.X == c.Coordinate.X && coord.Y == c.Coordinate.Y);
                    }
                        
                }
                sortedCarts = carts.OrderBy(c => c.Coordinate).ToList();
            }
            return sortedCarts[0].Coordinate.ToString();
        }
    }
}
