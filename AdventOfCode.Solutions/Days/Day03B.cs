namespace AdventOfCode.Solutions.Days
{
    public class Day3B : Day3A
    {
        public override string Solve()
        {
            var claims = ParseClaims();

            for (var i = 0; i < claims.Count; i++)
            {
                var claim1 = claims[i];
                var overlap = false;
                for (var j = 0; j < claims.Count; j++)
                {
                    if(i == j)
                        continue;

                    var claim2 = claims[j];
                    if (claim2.Overlaps(claim1))
                    {
                        overlap = true;
                        break;
                    }
                }

                if (!overlap)
                    return claim1.Id;
            }

            return "Unsolved";
        }
    }
}
