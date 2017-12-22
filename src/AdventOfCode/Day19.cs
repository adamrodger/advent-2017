using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for day 19
    /// </summary>
    public partial class Day19
    {
        private const char Vertical = '|';
        private const char Horizontal = '-';
        private const char Corner = '+';
        private const char Empty = ' ';
        private static readonly char[] SpecialCharacters = { Vertical, Horizontal, Corner };

        /// <summary>
        /// Follow the path set out in the path from the input file and 'collect' letters
        /// along the way
        /// </summary>
        /// <returns>The resultant string of all collected letters and the total number of steps</returns>
        public (string, int) Solve()
        {
            string[] lines = File.ReadAllLines("inputs/day19.txt");
            return this.Solve(lines);
        }

        /// <summary>
        /// Follow the path set out in the path from the given input and 'collect' letters
        /// along the way
        /// </summary>
        /// <param name="lines">Lines drawing the path</param>
        /// <returns>The resultant string of all collected letters and the total number of steps</returns>
        public (string, int) Solve(string[] lines)
        {
            int y = 0;
            int x = lines[y].IndexOf(Vertical);
            int steps = 0;
            
            var direction = Direction.Down;
            var output = new List<char>();

            while (true)
            {
                steps++;

                (x, y) = Move(direction, x, y);
                if (y < 0 || y >= lines.Length || x < 0 || x >= lines[y].Length)
                {
                    break;
                }

                char current = lines[y][x];
                if (current == Empty)
                {
                    break;
                }

                if (!SpecialCharacters.Contains(current))
                {
                    output.Add(current);
                }

                direction = CheckDirection(lines, direction, x, y);
            }

            return (new string(output.ToArray()), steps);
        }

        /// <summary>
        /// Move in the current direction from the current co-ordinates
        /// </summary>
        /// <param name="direction">Direction to move</param>
        /// <param name="x">Current X co-ordinate</param>
        /// <param name="y">Current Y co-ordinate</param>
        /// <returns>New co-ordinates</returns>
        private static (int x, int y) Move(Direction direction, int x, int y)
        {
            switch (direction)
            {
                case Direction.Down:
                    y++;
                    break;
                case Direction.Up:
                    y--;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Left:
                    x--;
                    break;
            }

            return (x, y);
        }

        /// <summary>
        /// Check if the current direction needs to change because a corner is found
        /// </summary>
        /// <param name="lines">Puzzle lines</param>
        /// <param name="current">Current direction</param>
        /// <param name="x">Current X co-ordinate</param>
        /// <param name="y">Current Y co-ordinate</param>
        /// <returns>New direction to travel (which will be the same as current unless a corner was found)</returns>
        private static Direction CheckDirection(string[] lines, Direction current, int x, int y)
        {
            if (lines[y][x] != Corner)
            {
                return current;
            }

            if (current == Direction.Down || current == Direction.Up)
            {
                return lines[y][x - 1] == Empty ? Direction.Right : Direction.Left;
            }

            return lines[y - 1][x] == Empty ? Direction.Down : Direction.Up;
        }
    }
}
