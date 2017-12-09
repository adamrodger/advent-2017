using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solve for Day 9
    /// </summary>
    public class Day09
    {
        /// <summary>
        /// Find the total depth and total garbage amount of the real input file
        /// </summary>
        /// <returns>(total depth, total garbage)</returns>
        public (int, int) Solve()
        {
            string input = File.ReadAllText("inputs/day09.txt");
            return this.Solve(input);
        }

        /// <summary>
        /// Find the total depth and total garbage amount of the given input
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>(total depth, total garbage)</returns>
        public (int, int) Solve(string input)
        {
            int start = 0;
            int garbage = 0;
            int total = 0;

            // remove cancelled characters
            while ((start = input.IndexOf('!')) > -1)
            {
                input = input.Remove(start, 2);
            }

            // remove and count garbage
            while ((start = input.IndexOf('<')) > -1)
            {
                int end = input.IndexOf('>') + 1;
                garbage += end - start - 2; // exclude leading/trailing chars
                input = input.Remove(start, end - start);
            }

            // find each closing brace and use position to determine depth
            input = input.Replace(",", string.Empty);

            while ((start = input.IndexOf('}')) > -1)
            {
                total += start;
                input = input.Remove(start - 1, 2);
            }

            return (total, garbage);
        }
    }
}