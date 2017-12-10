using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solve for Day 10
    /// </summary>
    public class Day10
    {
        /// <summary>
        /// Perform the hash function and return the first 2 numbers multiplied
        /// </summary>
        /// <param name="lengths">Lengths to use when hashing</param>
        /// <returns>First two numbers multiplied after hashing</returns>
        public int Solve(int size, int[] lengths)
        {
            int[] buffer = Enumerable.Range(0, size).ToArray();

            int skip = 0;
            int position = 0;

            foreach (int length in lengths)
            {
                var end = (position + length - 1) % size;

                // reverse the slice, wrapping around the end
                for (int i = 0; i < length / 2; i++)
                {
                    int swapStart = (position + i) % size;
                    int swapEnd = (end + size - i) % size;
                    int temp = buffer[swapStart];
                    buffer[swapStart] = buffer[swapEnd];
                    buffer[swapEnd] = temp;
                }

                position = (position + length + skip) % size;
                skip++;
            }

            return buffer[0] * buffer[1];
        }
    }
}