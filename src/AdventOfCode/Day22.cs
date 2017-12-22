using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 22
    /// </summary>
    public class Day22
    {
        private const char Clear = '.';
        private const char Infected = '#';
        private const char Weakened = 'W';
        private const char Flagged = 'F';

        /// <summary>
        /// Map of which direction to travel next given the current cell state and the current direction
        /// </summary>
        private static readonly IDictionary<(char, Direction), Direction> MoveMap = new Dictionary<(char, Direction), Direction>
        {
            // Infected turns right
            [(Infected, Direction.Up)]    = Direction.Right,
            [(Infected, Direction.Right)] = Direction.Down,
            [(Infected, Direction.Down)]  = Direction.Left,
            [(Infected, Direction.Left)]  = Direction.Up,
            // Clear turns left
            [(Clear, Direction.Up)]    = Direction.Left,
            [(Clear, Direction.Right)] = Direction.Up,
            [(Clear, Direction.Down)]  = Direction.Right,
            [(Clear, Direction.Left)]  = Direction.Down,
            // Weakened maintains direction
            [(Weakened, Direction.Up)]    = Direction.Up,
            [(Weakened, Direction.Right)] = Direction.Right,
            [(Weakened, Direction.Down)]  = Direction.Down,
            [(Weakened, Direction.Left)]  = Direction.Left,
            // Flagged reverses
            [(Flagged, Direction.Up)]    = Direction.Down,
            [(Flagged, Direction.Right)] = Direction.Left,
            [(Flagged, Direction.Down)]  = Direction.Up,
            [(Flagged, Direction.Left)]  = Direction.Right
        };

        /// <summary>
        /// From the centre of the grid parsed from the real file, perform 10000 iterations of
        /// the simple infection process, which is:
        /// - Turn left if clear or right if infected
        /// - Flip the state of the current cell
        /// - Move in the new direction
        /// </summary>
        /// <returns>Number of infections caused</returns>
        public int Part1()
        {
            string[] lines = File.ReadAllLines("inputs/day22.txt");
            return this.Solve(lines, 10000, SimpleInfection);
        }

        /// <summary>
        /// From the centre of the grid parsed from the real file, perform 10000000 iterations of
        /// the complex infection process, which is:
        /// - Turn left if clear, right if infected, turn around if weakened or don't turn if flagged
        /// - Change the cell state: clear -> weakened -> infected -> flagged -> clear -> ...,
        /// - Move in the new direction
        /// </summary>
        /// <returns>Number of infections caused</returns>
        public int Part2()
        {
            string[] lines = File.ReadAllLines("inputs/day22.txt");
            return this.Solve(lines, 10000000, ComplexInfection);
        }

        /// <summary>
        /// Starting in the middle facing upwards, perform the infection algorithm using the supplied
        /// infection process, iterations and grid input to parse
        /// </summary>
        /// <param name="lines">Input to parse into the starting grid</param>
        /// <param name="iterations">Number of iterations to perform</param>
        /// <param name="infectionProcess">Infection process routine</param>
        /// <returns>Number of infections caused</returns>
        public int Solve(string[] lines, int iterations, Func<char[][], int, int, bool> infectionProcess)
        {
            char[][] grid = ParseGrid(lines);

            int x = grid.Length / 2;
            int y = x;
            var direction = Direction.Up;
            int infections = 0;

            for (int i = 0; i < iterations; i++)
            {
                direction = Turn(grid, x, y, direction);
                if (infectionProcess(grid, x, y))
                {
                    infections++;
                }
                (x, y) = Move(x, y, direction);
            }

            return infections;
        }

        /// <summary>
        /// Parse the source grid into a much larger grid but maintain the central positioning
        /// </summary>
        /// <param name="lines">Source lines to parse</param>
        /// <returns>Parsed grid centralised in a much larger grid</returns>
        private static char[][] ParseGrid(string[] lines)
        {
            char[][] baseGrid = lines.Select(l => l.ToArray()).ToArray();

            int size = lines.Length * 16 + 1;
            int offset = (size - baseGrid.Length) / 2;

            char[][] grid = Enumerable.Range(0, size)
                                      .Select(_ => Enumerable.Range(0, size).Select(__ => Clear).ToArray())
                                      .ToArray();

            for (int i = 0; i < baseGrid.Length; i++)
            {
                Array.Copy(baseGrid[i], 0, grid[offset + i], offset, baseGrid.Length);
            }

            return grid;
        }

        /// <summary>
        /// From the current state on the grid and the current direction, turn to the new direction
        /// </summary>
        /// <param name="grid">Infection grid</param>
        /// <param name="x">Current x position</param>
        /// <param name="y">Current y position</param>
        /// <param name="direction">Current direction</param>
        /// <returns>New direction</returns>
        private static Direction Turn(char[][] grid, int x, int y, Direction direction)
        {
            char state = grid[y][x];
            return MoveMap[(state,direction)];
        }

        /// <summary>
        /// Simple infection routine used in part 1
        /// </summary>
        /// <param name="grid">Infection grid</param>
        /// <param name="x">Current x position</param>
        /// <param name="y">Current y position</param>
        /// <returns>Infection caused</returns>
        private static bool SimpleInfection(char[][] grid, int x, int y)
        {
            bool infected = grid[y][x] == Infected;
            grid[y][x] = infected ? Clear : Infected;
            return !infected;
        }

        /// <summary>
        /// Complex infection routine used in part 2
        /// </summary>
        /// <param name="grid">Infection grid</param>
        /// <param name="x">Current x position</param>
        /// <param name="y">Current y position</param>
        /// <returns>Infection caused</returns>
        private static bool ComplexInfection(char[][] grid, int x, int y)
        {
            char state = grid[y][x];
            char newState = Clear;

            switch (state)
            {
                case Clear:
                    newState = Weakened;
                    break;
                case Weakened:
                    newState = Infected;
                    break;
                case Infected:
                    newState = Flagged;
                    break;
                case Flagged:
                    newState = Clear;
                    break;
            }

            grid[y][x] = newState;
            return newState == Infected;
        }

        /// <summary>
        /// Move from the given co-ordinates in the given direction
        /// </summary>
        /// <param name="x">Current x position</param>
        /// <param name="y">Current y position</param>
        /// <param name="direction">Direction to move</param>
        /// <returns>New co-ordinates</returns>
        private static (int x, int y) Move(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return (x, y + 1);
                case Direction.Up:
                    return (x, y - 1);
                case Direction.Left:
                    return (x - 1, y);
                case Direction.Right:
                    return (x + 1, y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
