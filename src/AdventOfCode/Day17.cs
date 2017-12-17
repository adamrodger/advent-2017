using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 17
    /// </summary>
    public class Day17
    {
        /// <summary>
        /// Create the 2018-element buffer using the allocation algorithm
        /// and return the number after the 2018th inserted value
        /// </summary>
        /// <param name="step">Algorithm step value</param>
        /// <returns>Number after the 2018th inserted value</returns>
        public int Part1(int step)
        {
            List<int> buffer = new List<int> { 0 };
            int current = 0;

            for (int i = 1; i <= 2017; i++)
            {
                current = ((current + step) % i) + 1;
                buffer.Insert(current, i);
            }

            return buffer[(current + 1) % 2018];
        }

        /*
         * This is impractical to calculate even on a good CPU
        public int Part2BruteForce(int step)
        {
            List<int> buffer = new List<int> { 0 };
            int current = 0;

            for (int i = 1; i <= 50000000; i++)
            {
                current = ((current + step) % i) + 1;
                buffer.Insert(current, i);
            }

            return buffer[1];
        }*/

        /// <summary>
        /// Perform the step algorithm 50,000,000 times and return the value
        /// after 0
        /// </summary>
        /// <remarks>
        /// Since 0 is always at index 0, you only need to keep track of index 1
        /// </remarks>
        /// <param name="step">Algorithm step value</param>
        /// <returns>Number after 0</returns>
        public int Part2(int step)
        {
            int current = 0;
            int indexOne = 0;

            for (int i = 1; i <= 50000000; i++)
            {
                current = ((current + step) % i) + 1;
                
                if (current == 1)
                {
                    indexOne = i;
                }
            }

            return indexOne;
        }
    }
}