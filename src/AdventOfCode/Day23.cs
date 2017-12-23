using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for day 23
    /// </summary>
    public class Day23
    {
        /// <summary>
        /// Run the instructions from the real input file and count the number of multiply instructions executed
        /// </summary>
        /// <returns>Number of multiply instructions executed</returns>
        public int Part1()
        {
            string[] instructions = File.ReadAllLines("inputs/day23.txt");
            return this.Part1(instructions);
        }

        /// <summary>
        /// Run the instructions and count the number of multiply instructions executed
        /// </summary>
        /// <param name="instructions">Instructions to follow</param>
        /// <returns>Number of multiply instructions executed</returns>
        public int Part1(string[] instructions)
        {
            var buffer = new Queue<long>();
            var duet = new Duet(instructions, buffer, buffer, 0);

            // wait until the first message is successfully received
            while (!duet.Finished)
            {
                duet.Step();
            }

            return duet.CommandCount["mul"];
        }

        /// <summary>
        /// Run an optimised version of the program compiled from the instructions in the input file
        /// </summary>
        /// <returns>Final value of the h register after running the program</returns>
        public int Part2()
        {
            // compiled (by hand!!) and optimised from the assembly code
            const int b = 57 * 100 + 100000;
            const int c = b + 17000;
            int h = 0;

            // h is the total number of non-primes in increments of 17 from b to c (inclusive), calculated using trial division
            for (int i = b; i <= c; i += 17)
            {
                for (int d = 2; d < i; d++)
                {
                    if (i % d == 0)
                    {
                        h++;
                        break;
                    }
                }
            }

            return h;
        }
    }
}