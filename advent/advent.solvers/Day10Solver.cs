using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent.solvers
{
    public class Day10Solver : Solver
    {
        public string ProblemName => "The Stars Align";

        private readonly DataProvider<Light> dataProvider;

        public Day10Solver(DataProvider<Light> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public Sky GetSky()
        {
            return new Sky(dataProvider.GetData().ToList());
        }

        public string SolveFirstPart()
        {
            var sky = new Sky(dataProvider.GetData().ToList());
            var smallest = GetSmallestSky(sky);
            return Print(smallest);
        }

        private static Sky GetSmallestSky(Sky start)
        {
            var current = (Sky) start.Clone();
            var next = (Sky) current.Clone();
            next.MoveLights();
            while (next.Size.X < current.Size.X)
            {
                current = (Sky) next.Clone();
                next.MoveLights();
            }

            return current;
        }

        private static string Print(Sky sky)
        {
            var builder = new StringBuilder();
            var minX = sky.Lights.Min(l => l.Position.X);
            var minY = sky.Lights.Min(l => l.Position.Y);
            var maxX = sky.Lights.Max(l => l.Position.X);
            var maxY = sky.Lights.Max(l => l.Position.Y);
            for (var j = minY; j <= maxY; ++j)
            {
                for (var i = minX; i <= maxX; ++i)
                    builder.Append(sky.Contains(i, j) ? '#' : ' ');
                builder.AppendLine();
            }

            return builder.ToString();
        }

        public string SolveSecondPart()
        {
            var sky = new Sky(dataProvider.GetData().ToList());
            var smallest = GetSmallestSky(sky);
            return smallest.Time.ToString();
        }

        public class Light
        {
            public (int X, int Y) Position;
            public readonly (int X, int Y) Velocity;

            public Light(int posX, int posY, int velX, int velY)
            {
                Position = (posX, posY);
                Velocity = (velX, velY);
            }

            public void Move()
            {
                Position.X += Velocity.X;
                Position.Y += Velocity.Y;
            }
        }

        public class Sky : ICloneable
        {
            public int Time;
            public readonly IList<Light> Lights;
            public (int X, int Y) Size;

            public Sky(IEnumerable<Light> lights)
            {
                Time = 0;
                Lights = lights.ToList();
                Size.X = Lights.Max(l => l.Position.X) - Lights.Min(l => l.Position.X);
                Size.Y = Lights.Max(l => l.Position.Y) - Lights.Min(l => l.Position.Y);
            }

            public void MoveLights()
            {
                Time++;
                foreach (var light in Lights)
                    light.Move();
                Size.X = Lights.Max(l => l.Position.X) - Lights.Min(l => l.Position.X);
                Size.Y = Lights.Max(l => l.Position.Y) - Lights.Min(l => l.Position.Y);
            }

            public bool Contains(int x, int y)
            {
                return Lights.FirstOrDefault(l => l.Position.X == x && l.Position.Y == y) != null;
            }

            public object Clone()
            {
                return new Sky(Lights.Select(l => new Light(l.Position.X, l.Position.Y, l.Velocity.X, l.Velocity.Y))) { Time = Time };
            }
        }
    }
}