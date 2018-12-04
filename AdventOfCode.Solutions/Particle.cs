using System;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Particle
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }

        public Particle(Vector3 position, Vector3 velocity, Vector3 acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        public void Advance(int numberOfTicks)
        {
            for (var i = 0; i < numberOfTicks; i++)
                Tick();
        }

        public void Tick()
        {
            Velocity.X += Acceleration.X;
            Velocity.Y += Acceleration.Y;
            Velocity.Z += Acceleration.Z;
            //Console.WriteLine("Start: " + ToString() + $" + Velocity: {Velocity.X},{Velocity.Y},{Velocity.Z}");
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
            //Console.WriteLine("End: " + ToString());
        }

        public static Particle FromString(string particle)
        {
            var parts = particle.Split(new [] {", "}, StringSplitOptions.RemoveEmptyEntries);

            var pos = parts[0];
            var position = new Vector3(
                pos.Substring(pos.IndexOf('<') + 1, pos.IndexOf('>') - pos.IndexOf('<') - 1)
                    .Split(',')
                    .Select(v => v.Trim())
                    .Select(int.Parse)
                    .ToArray());

            var vel = parts[1];
            var velocity = new Vector3(
                vel.Substring(vel.IndexOf('<') + 1, vel.IndexOf('>') - vel.IndexOf('<') - 1)
                    .Split(',')
                    .Select(v => v.Trim())
                    .Select(int.Parse)
                    .ToArray());

            var acc = parts[2];
            var acceleration = new Vector3(
                acc.Substring(acc.IndexOf('<') + 1, acc.IndexOf('>') - acc.IndexOf('<') - 1)
                    .Split(',')
                    .Select(v => v.Trim())
                    .Select(int.Parse)
                    .ToArray());


            return new Particle(position, velocity, acceleration);
        }

        public override string ToString()
        {
            return $"{Position.X},{Position.Y},{Position.Z}";
        }
    }
}