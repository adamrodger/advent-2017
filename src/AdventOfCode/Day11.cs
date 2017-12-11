using System;
using System.IO;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 11
    /// </summary>
    public class Day11
    {
        public (int, int) Solve()
        {
            string input = File.ReadAllText("inputs/day11.txt");
            return this.Solve(input);
        }

        /// <summary>
        /// Calculate the Manhattan Distance between two hextiles in a hex grid
        /// by following movement instructions
        /// </summary>
        /// <param name="input">Input string of movement directions</param>
        /// <returns>Distance between the starting and finishing hextile</returns>
        public (int, int) Solve(string input)
        {
            (int x, int y, int z) = (0, 0, 0);
            (int mx, int my, int mz) = (0, 0, 0);

            // perform all the moves
            foreach (string direction in input.Split(','))
            {
                switch (direction)
                {
                    case "ne":
                        x++;
                        z--;
                        break;
                    case "se":
                        x++;
                        y--;
                        break;
                    case "s":
                        y--;
                        z++;
                        break;
                    case "sw":
                        x--;
                        z++;
                        break;
                    case "nw":
                        x--;
                        y++;
                        break;
                    case "n":
                        y++;
                        z--;
                        break;
                    default:
                        throw new ArgumentException($"Unknown direction: {direction}");
                }

                mx = Math.Max(mx, Math.Abs(x));
                my = Math.Max(my, Math.Abs(y));
                mz = Math.Max(mz, Math.Abs(z));
            }

            // number of steps required is the maximum axis from the origin
            var dx = Math.Abs(x);
            var dy = Math.Abs(y);
            var dz = Math.Abs(z);

            int distance = Math.Max(dx, Math.Max(dy, dz));
            int max = Math.Max(mx, Math.Max(my, mz));

            return (distance, max);
        }
    }
}
