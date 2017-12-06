using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solve for Day 6
    /// </summary>
    public class Day06
    {
        /// <summary>
        /// A debugger program here is having an issue: it is trying to repair a memory reallocation routine, but it keeps getting
        /// stuck in an infinite loop.
        /// 
        /// In this area, there are sixteen memory banks; each memory bank can hold any number of blocks. The goal of the
        /// reallocation routine is to balance the blocks between the memory banks.
        /// 
        /// The reallocation routine operates in cycles. In each cycle, it finds the memory bank with the most blocks (ties won by
        /// the lowest-numbered memory bank) and redistributes those blocks among the banks. To do this, it removes all of the
        /// blocks from the selected bank, then moves to the next (by index) memory bank and inserts one of the blocks. It continues
        /// doing this until it runs out of blocks; if it reaches the last memory bank, it wraps around to the first one.
        /// 
        /// The debugger would like to know how many redistributions can be done before a blocks-in-banks configuration is produced
        /// that has been seen before.
        /// 
        /// For example, imagine a scenario with only four memory banks:
        /// 
        /// - The banks start with 0, 2, 7, and 0 blocks. The third bank has the most blocks, so it is chosen for redistribution.
        /// - Starting with the next bank (the fourth bank) and then continuing to the first bank, the second bank, and so on, the 7
        ///   blocks are spread out over the memory banks. The fourth, first, and second banks get two blocks each, and the third
        ///   bank gets one back. The final result looks like this: 2 4 1 2.
        /// - Next, the second bank is chosen because it contains the most blocks (four). Because there are four memory banks, each
        ///   gets one block. The result is: 3 1 2 3.
        /// - Now, there is a tie between the first and fourth memory banks, both of which have three blocks. The first bank wins
        ///   the tie, and its three blocks are distributed evenly over the other three banks, leaving it with none: 0 2 3 4.
        /// - The fourth bank is chosen, and its four blocks are distributed such that each of the four banks receives one: 1 3 4 1.
        /// - The third bank is chosen, and the same thing happens: 2 4 1 2.
        /// 
        /// At this point, we've reached a state we've seen before: 2 4 1 2 was already seen. The infinite loop is detected after
        /// the fifth block redistribution cycle, and so the answer in this example is 5.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Number of iterations before a cycle is created</returns>
        public int Part1(string input)
        {
            return Solve(input, (_, results) => results.Count);
        }

        /// <summary>
        /// Out of curiosity, the debugger would also like to know the size of the loop: starting from a state that has already
        /// been seen, how many block redistribution cycles must be performed before that same state is seen again?
        /// 
        /// In the example above, 2 4 1 2 is seen again after four cycles, and so the answer in that example would be 4.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Number of links in the formed cycle</returns>
        public int Part2(string input)
        {
            return Solve(input, (cycle, results) => results.Count - results.IndexOf(cycle));
        }

        /// <summary>
        /// Solve the memory redistribution problem and return the appropriate result according to the
        /// supplied result calculation function
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="calculateResult">Function to calculate the resultant answer once a cycle has been found, from the cycle solution and all previous results</param>
        /// <returns></returns>
        private static int Solve(string input, Func<string, IList<string>, int> calculateResult)
        {
            List<string> results = new List<string> { input };

            var banks = input.Split(' ').Select(int.Parse).ToArray();

            while (true)
            {
                int index = FindBiggestBank(banks);
                string result = Redistribute(banks, index);

                if (results.Contains(result))
                {
                    return calculateResult(result, results);
                }

                results.Add(result);
            }
        }

        /// <summary>
        /// Find the bank with the most cells, with the lowest index winning in the event of a tie
        /// </summary>
        /// <param name="banks">Current memory banks</param>
        /// <returns>Index of the biggest bank</returns>
        private static int FindBiggestBank(IList<int> banks)
        {
            int biggest = -1;
            int cells = -1;

            for (int i = 0; i < banks.Count; i++)
            {
                if (banks[i] > cells)
                {
                    biggest = i;
                    cells = banks[i];
                }
            }

            return biggest;
        }

        /// <summary>
        /// Redistribute the banks from the index in a round-robin fashion
        /// </summary>
        /// <param name="banks">Current memory banks</param>
        /// <param name="index">Index to redistribute</param>
        /// <returns>Solution string</returns>
        private static string Redistribute(IList<int> banks, int index)
        {
            int cells = banks[index];
            banks[index] = 0;

            while (cells > 0)
            {
                index = (index + 1) % banks.Count;
                banks[index]++;
                cells--;
            }

            return string.Join(" ", banks);
        }
    }
}
