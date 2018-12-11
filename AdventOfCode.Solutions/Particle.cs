namespace AdventOfCode.Solutions
{
    public class Particle
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        //public Vector3 Acceleration { get; set; }

        //public Particle(Vector3 position, Vector3 velocity, Vector3 acceleration)
        //{
        //    Position = position;
        //    Velocity = velocity;
        //    Acceleration = acceleration;
        //}

        public Particle(Vector3 position, Vector3 velocity)
            //  : this(position, velocity, Vector3.Zero)
        {
            Position = position;
            Velocity = velocity;
        }

        public void Advance(int numberOfTicks)
        {
            Position.X += Velocity.X *numberOfTicks;
            Position.Y += Velocity.Y * numberOfTicks;
            Position.Z += Velocity.Z * numberOfTicks;
        }

        public void Tick()
        {
            //Velocity.X += Acceleration.X;
            //Velocity.Y += Acceleration.Y;
            //Velocity.Z += Acceleration.Z;

            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
        }

        public override string ToString()
        {
            return $"{Position.X},{Position.Y},{Position.Z}";
        }
    }
}