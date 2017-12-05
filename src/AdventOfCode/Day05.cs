using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solve for Day 5
    /// </summary>
    public class Day05
    {
        /// <summary>
        /// The message includes a list of the offsets for each jump. Jumps are relative: -1 moves to the previous instruction,
        /// and 2 skips the next one. Start at the first instruction in the list. The goal is to follow the jumps until one leads
        /// outside the list.
        /// 
        /// In addition, these instructions are a little strange; after each jump, the offset of that instruction increases by 1.
        /// So, if you come across an offset of 3, you would move three instructions forward, but change it to a 4 for the next
        /// time it is encountered.
        /// 
        /// For example, consider the following list of jump offsets:
        /// 
        /// (0) 3  0  1  -3  - before we have taken any steps.
        /// (1) 3  0  1  -3  - jump with offset 0 (that is, don't jump at all). Fortunately, the instruction is then incremented to 1.
        ///  2 (3) 0  1  -3  - step forward because of the instruction we just modified. The first instruction is incremented again, now to 2.
        ///  2  4  0  1 (-3) - jump all the way to the end; leave a 4 behind.
        ///  2 (4) 0  1  -2  - go back to where we just were; increment -3 to -2.
        ///  2  5  0  1  -2  - jump 4 steps forward, escaping the maze.
        /// 
        /// In this example, the exit is reached in 5 steps.
        /// </summary>
        /// <param name="input">Input sequence string</param>
        /// <returns>Number of steps to exit</returns>
        public int Part1(string input)
        {
            var numbers = input.Split(' ').Select(int.Parse).ToArray();
            int i = 0;
            int count = 0;

            while (i < numbers.Length)
            {
                i += numbers[i]++;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Now, the jumps are even stranger: after each jump, if the offset was three or more, instead decrease it by 1.
        /// Otherwise, increase it by 1 as before.
        /// 
        /// Using this rule with the above example, the process now takes 10 steps, and the offset values after finding the
        /// exit are left as 2 3 2 3 -1.
        /// </summary>
        /// <param name="input">Input sequence string</param>
        /// <returns>Number of steps to exit</returns>
        public int Part2(string input)
        {
            var numbers = input.Split(' ').Select(int.Parse).ToArray();
            int i = 0;
            int count = 0;

            while (i < numbers.Length)
            {
                i += numbers[i] >= 3 ? numbers[i]-- : numbers[i]++;
                count++;
            }

            return count;
        }
    }
}
