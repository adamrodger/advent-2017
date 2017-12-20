using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 20
    /// </summary>
    public class Day20
    {
        /// <summary>
        /// Get the ID of the pixel that will remain closest to (0,0,0) over time from the real input file
        /// </summary>
        /// <returns>Closest pixel ID</returns>
        public int Part1()
        {
            string[] lines = File.ReadAllLines("inputs/day20.txt");
            return this.Part1(lines);
        }

        /// <summary>
        /// Get the ID of the pixel that will remain closest to (0,0,0) over time from the given input
        /// </summary>
        /// <param name="lines">Input lines</param>
        /// <returns>Closest pixel ID</returns>
        public int Part1(string[] lines)
        {
            List<Pixel> pixels = lines.Select(Pixel.Parse).ToList();

            // hack: perform enough simulations that everything is now probably travelling away from 0 then pick closest
            for (int i = 0; i < 1000; i++)
            {
                pixels.ForEach(p => p.Tick());
            }

            var closest = pixels.Min(p => p.TotalDistance);
            return pixels.First(p => p.TotalDistance == closest).Id;
        }

        /// <summary>
        /// Get the total number of pixels remaining after all collisions have been performed
        /// </summary>
        /// <returns>Number of pixels after collisions are removed</returns>
        public int Part2()
        {
            string[] lines = File.ReadAllLines("inputs/day20.txt");
            return this.Part2(lines);
        }

        /// <summary>
        /// Get the total number of pixels remaining after all collisions have been performed
        /// </summary>
        /// <param name="lines">Input lines</param>
        /// <returns>Number of pixels after collisions are removed</returns>
        public int Part2(string[] lines)
        {
            List<Pixel> pixels = lines.Select(Pixel.Parse).ToList();

            // hack: perform enough simulations that all collisions have probably happened
            for (int i = 0; i < 1000; i++)
            {
                pixels.ForEach(p => p.Tick());

                // remove collisions
                List<Pixel> collisions = pixels.GroupBy(p => p.Position)
                                               .Where(g => g.Count() > 1)
                                               .SelectMany(g => g).ToList();
                collisions.ForEach(c => pixels.Remove(c));
            }

            return pixels.Count;
        }

        /// <summary>
        /// Pixel
        /// </summary>
        private class Pixel
        {
            private int x, y, z;
            private int vx, vy, vz;
            private int ax, ay, az;

            /// <summary>
            /// Pixel ID
            /// </summary>
            public int Id { get; private set; }
            
            /// <summary>
            /// Current position (x,y,z)
            /// </summary>
            public (int x, int y, int z) Position => (this.x, this.y, this.z);

            /// <summary>
            /// Total distance from (0,0,0)
            /// </summary>
            public int TotalDistance => Math.Abs(this.x) + Math.Abs(this.y) + Math.Abs(this.z);

            /// <summary>
            /// Accelerate and then move the pixel
            /// </summary>
            public void Tick()
            {
                this.vx += this.ax;
                this.vy += this.ay;
                this.vz += this.az;

                this.x += this.vx;
                this.y += this.vy;
                this.z += this.vz;
            }

            /// <summary>
            /// Parse a pixel from the given line with the given ID
            /// </summary>
            /// <param name="line">Line to parse</param>
            /// <param name="id">Pixel ID</param>
            /// <returns>Parsed pixel</returns>
            public static Pixel Parse(string line, int id)
            {
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

                return new Pixel
                {
                    Id = id,
                    x = position[0],
                    y = position[1],
                    z = position[2],
                    vx = velocity[0],
                    vy = velocity[1],
                    vz = velocity[2],
                    ax = acceleration[0],
                    ay = acceleration[1],
                    az = acceleration[2]
                };
            }
        }
    }
}
