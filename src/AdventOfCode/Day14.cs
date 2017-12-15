using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 14
    /// </summary>
    public class Day14
    {
        /// <summary>
        /// Find how many 1s there are in the knot hashes of the given input string from
        /// "input-0" to "0-127" when expressed in binary
        /// </summary>
        /// <param name="input">Input string to hash</param>
        /// <returns>Number of 1s</returns>
        public int Part1(string input)
        {
            return string.Join(string.Empty, BuildMap(input)).Count(c => c == '1');
        }

        /// <summary>
        /// Find how many distinct contiguous regions of 1s there are in the knot hashes of the
        /// given input string from "input-0" to "0-127" when expressed in binary and laid out
        /// as a 128x128 grid
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Number of contiguous regions of 1s</returns>
        public int Part2(string input)
        {
            string[] hashes = BuildMap(input);

            bool[,] visited = new bool[128, 128];
            int regions = 0;

            for (int y = 0; y < visited.GetLength(1); y++) // rows
            {
                for (int x = 0; x < visited.GetLength(0); x++) // columns
                {
                    if (visited[x, y] || hashes[x][y] == '0')
                    {
                        continue;
                    }

                    // found an unvisited 1, mark new region
                    this.Visit(x, y, hashes, visited);
                    regions++;
                }
            }

            return regions;
        }

        /// <summary>
        /// Build a 128-element array of 128-character binary strings for the knot hashes
        /// of the given input string from "input-0" to "input-127"
        /// </summary>
        /// <param name="input">Input string to hash</param>
        /// <returns>128x128 binary grid of the hashes</returns>
        private static string[] BuildMap(string input)
        {
            var hexMap = new Dictionary<char, string>
            {
                { '0', "0000" }, { '1', "0001" }, { '2', "0010" }, { '3', "0011" },
                { '4', "0100" }, { '5', "0101" }, { '6', "0110" }, { '7', "0111" },
                { '8', "1000" }, { '9', "1001" }, { 'a', "1010" }, { 'b', "1011" },
                { 'c', "1100" }, { 'd', "1101" }, { 'e', "1110" }, { 'f', "1111" }
            };

            var hasher = new Day10();
            var hashes = Enumerable.Range(0, 128)
                                   .Select(i => $"{input}-{i}")
                                   .Select(hasher.Part2)
                                   .Select(hash => string.Join(string.Empty, hash.Select(c => hexMap[c])))
                                   .ToArray();
            return hashes;
        }

        /// <summary>
        /// Visit the cell at (x,y) if it hasn't already been visited and follow all neighbours
        /// as long as they haven't also been visited and they are a 1
        /// </summary>
        /// <param name="x">X co-ordinate</param>
        /// <param name="y">Y co-ordinate</param>
        /// <param name="input">Input grid</param>
        /// <param name="visited">Visited grid</param>
        private void Visit(int x, int y, string[] input, bool[,] visited)
        {
            if (visited[x, y])
            {
                return;
            }

            visited[x, y] = true;

            // stop following if you find a 0
            if (input[x][y] == '0')
            {
                return;
            }

            // visit neighbours
            if (x > 0)   this.Visit(x - 1, y, input, visited); // left
            if (x < 127) this.Visit(x + 1, y, input, visited); // right
            if (y > 0)   this.Visit(x, y - 1, input, visited); // up
            if (y < 127) this.Visit(x, y + 1, input, visited); // down
        }
    }
}
