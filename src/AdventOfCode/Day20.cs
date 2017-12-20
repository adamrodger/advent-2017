using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day20
    {
        public int Solve()
        {
            string[] lines = File.ReadAllLines("inputs/day20.txt");
            return this.Solve(lines);
        }

        public int Solve(string[] lines)
        {
            List<Pixel> pixels = new List<Pixel>();

            for (var i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                var parts = line.Split(new[] { ", " }, StringSplitOptions.None);
                var position = parts[0].Substring(0, parts[0].Length - 1)
                                       .Substring(3)
                                       .Split(',')
                                       .Select(int.Parse)
                                       .ToArray();
                var velocity = parts[1].Substring(0, parts[1].Length - 1)
                                       .Substring(3)
                                       .Split(',')
                                       .Select(int.Parse)
                                       .ToArray();
                var acceleration = parts[2].Substring(0, parts[2].Length - 1)
                                           .Substring(3)
                                           .Split(',')
                                           .Select(int.Parse)
                                           .ToArray();

                pixels.Add(new Pixel
                {
                    Id = i,
                    PositionX = position[0],
                    PositionY = position[1],
                    PositionZ = position[2],
                    VelocityX = velocity[0],
                    VelocityY = velocity[1],
                    VelocityZ = velocity[2],
                    AccelerationX = acceleration[0],
                    AccelerationY = acceleration[1],
                    AccelerationZ = acceleration[2]
                });
            }

            var minSpeed = pixels.Min(p => p.TotalAcceleration);
            var slowest = pixels.Where(p => p.TotalAcceleration == minSpeed).ToArray();

            var closest = slowest.MinBy(p => p.TotalDistance);

            string values = string.Join(Environment.NewLine, pixels.OrderBy(p => p.TotalAcceleration)
                                                                   .Select(p => p.ToString()));

            return closest.Id;
        }

        private struct Pixel
        {
            public int Id { get; set; }

            public int PositionX { get; set; }
            public int PositionY { get; set; }
            public int PositionZ { get; set; }
            public int TotalDistance => Math.Abs(this.PositionX) + Math.Abs(this.PositionY) + Math.Abs(this.PositionZ);

            public int VelocityX { get; set; }
            public int VelocityY { get; set; }
            public int VelocityZ { get; set; }
            public int TotalVelocity => Math.Abs(this.VelocityX) + Math.Abs(this.VelocityY) + Math.Abs(this.VelocityZ);

            public int AccelerationX { get; set; }
            public int AccelerationY { get; set; }
            public int AccelerationZ { get; set; }
            public int TotalAcceleration => Math.Abs(this.AccelerationX) + Math.Abs(this.AccelerationY) + Math.Abs(this.AccelerationZ);

            /// <summary>Returns the fully qualified type name of this instance.</summary>
            /// <returns>The fully qualified type name.</returns>
            public override string ToString()
            {
                return $"{Id}, " +
                       $"({PositionX}, {PositionY}, {PositionZ})={TotalDistance}, " +
                       $"({VelocityX}, {VelocityY}, {VelocityZ})={TotalVelocity}, " + 
                       $"({AccelerationX}, {AccelerationY}, {AccelerationZ})={TotalAcceleration}";
            }
        }
    }

    public static class Extensions
    {
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, null);
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            comparer = comparer ?? Comparer<TKey>.Default;

            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }
    }
}
