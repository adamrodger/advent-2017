using System;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 3
    /// </summary>
    public class Day03
    {
        /// <summary>
        /// Each square on the grid is allocated in a spiral pattern starting at a location marked 1
        /// and then counting up while spiraling outward. For example, the first few squares are
        /// allocated like this:
        /// 
        /// 17  16  15  14  13
        /// 18   5   4   3  12
        /// 19   6   1   2  11
        /// 20   7   8   9  10
        /// 21  22  23---> ...
        /// 
        /// While this is very space-efficient (no squares are skipped), requested data must be carried
        /// back to square 1 (the location of the only access port for this memory system) by programs
        /// that can only move up, down, left, or right. They always take the shortest path: the
        /// Manhattan Distance between the location of the data and square 1.
        /// 
        /// For example:
        /// 
        /// - Data from square 1 is carried 0 steps, since it's at the access port.
        /// - Data from square 12 is carried 3 steps, such as: down, left, left.
        /// - Data from square 23 is carried only 2 steps: up twice.
        /// - Data from square 1024 must be carried 31 steps
        /// </summary>
        public int Part1(int input)
        {
            int squareSize = (int)Math.Ceiling(Math.Sqrt(input));
            double halfSquare = squareSize / 2.0;
            int shortest = (int)Math.Floor(halfSquare);
            
            int previousSquare = (int)Math.Pow(squareSize - 1, 2);
            int midCorner = previousSquare + squareSize;

            // I'm sure this bit can be simplified...
            int midpoint = input < midCorner
                            ? midCorner - shortest
                            : midCorner + shortest;
            int offset = Math.Abs(input - midpoint);

            int steps = shortest + offset;
            return steps;
        }

        /// <summary>
        /// As a stress test on the system, the programs here clear the grid and then store the value 1 in
        /// square 1. Then, in the same allocation order as shown above, they store the sum of the values
        /// in all adjacent squares, including diagonals.
        /// 
        /// So, the first few squares' values are chosen as follows:
        /// 
        /// - Square 1 starts with the value 1.
        /// - Square 2 has only one adjacent filled square (with value 1), so it also stores 1.
        /// - Square 3 has both of the above squares as neighbors and stores the sum of their values, 2.
        /// - Square 4 has all three of the aforementioned squares as neighbors and stores the sum of their
        ///   values, 4.
        /// - Square 5 only has the first and fourth squares as neighbors, so it gets the value 5.
        /// 
        /// Once a square is written, its value does not change. Therefore, the first few squares would
        /// receive the following values:
        /// 
        /// 147  142  133  122   59
        /// 304    5    4    2   57
        /// 330   10    1    1   54
        /// 351   11   23   25   26
        /// 362  747  806--->   ...
        /// 
        /// What is the first value written that is larger than your puzzle input?
        /// </summary>
        /// <param name="input">Puzzle input</param>
        /// <returns>First value greater than the input</returns>
        public int Part2(int input)
        {
            // can't think of a generic mathematical solution right now, so just brute force

            int diameter = (int)Math.Ceiling(Math.Sqrt(input));
            int radius = (int)Math.Ceiling(diameter / 2.0);

            // make a grid which is big enough to make sure we don't go out of bounds looking for adjacent
            int gridDiameter = Math.Max(diameter + 3, 5);
            int[,] grid = new int[gridDiameter, gridDiameter];

            // start in the centre and spiral outwards
            int steps = 1;
            int x = radius + 1;
            int y = radius + 1;
            grid[x, y] = 1;

            int horizontalDirection = 1; // right
            int verticatalDirection = -1; // up

            while (true)
            {
                // horizontal
                for (int i = 0; i < steps; i++)
                {
                    x += horizontalDirection;
                    int result = CalculateSquare(grid, x, y);
                    if (result > input)
                    {
                        return result;
                    }
                }

                // vertical
                for (int i = 0; i < steps; i++)
                {
                    y += verticatalDirection;
                    int result = CalculateSquare(grid, x, y);
                    if (result > input)
                    {
                        return result;
                    }
                }

                // flip to the other direction
                horizontalDirection *= -1;
                verticatalDirection *= -1;
                steps++;
            }
        }

        /// <summary>
        /// Set the value at (x,y) by adding up all the adjacent cells
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <param name="x">X co-ordinate</param>
        /// <param name="y">Y co-ordinate</param>
        /// <returns>Result</returns>
        private static int CalculateSquare(int[,] grid, int x, int y)
        {
            grid[x, y] = grid[x,     y - 1] + // up
                         grid[x + 1, y - 1] + // up-right
                         grid[x + 1, y    ] + // right
                         grid[x + 1, y + 1] + // down-right
                         grid[x,     y + 1] + // down
                         grid[x - 1, y + 1] + // down-left
                         grid[x - 1, y    ] + // left
                         grid[x - 1, y - 1];  // up-left

            return grid[x, y];
        }
    }
}