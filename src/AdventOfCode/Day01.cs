using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 1
    /// </summary>
    public class Day01
    {
        /// <summary>
        /// ASCII character index for 0
        /// </summary>
        private const int AsciiIndex = 48;

        /// <summary>
        /// The captcha requires you to review a sequence of digits (your puzzle input) and find the sum of all digits that match the next digit
        /// in the list. The list is circular, so the digit after the last digit is the first digit in the list.
        ///
        /// For example:
        ///
        /// 1122 produces a sum of 3 (1 + 2) because the first digit(1) matches the second digit and the third digit(2) matches the fourth digit.
        /// 1111 produces 4 because each digit (all 1) matches the next.
        /// 1234 produces 0 because no digit matches the next.
        /// 91212129 produces 9 because the only digit that matches the next one is the last digit, 9. 
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Solution</returns>
        public int Part1(string input)
        {
            // always check the next number
            return this.CheckInput(input, _ => 1);
        }

        /// <summary>
        /// Now, instead of considering the next digit, it wants you to consider the digit halfway around the circular list. That is, if your
        /// list contains 10 items, only include a digit in your sum if the digit 10/2 = 5 steps forward matches it. Fortunately, your list
        /// has an even number of elements.
        ///
        /// For example:
        /// 
        /// 1212 produces 6: the list contains 4 items, and all four digits match the digit 2 items ahead.
        /// 1221 produces 0, because every comparison is between a 1 and a 2.
        /// 123425 produces 4, because both 2s match each other, but no other digit has a match.
        /// 123123 produces 12.
        /// 12131415 produces 4.
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Solution</returns>
        public int Part2(string input)
        {
            return this.CheckInput(input, numbers => numbers.Length / 2);
        }

        /// <summary>
        /// Checks the input by comparing each one to a given offset
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="offsetFunc">Function to generate the offset from the number list</param>
        /// <returns>Solution total</returns>
        private int CheckInput(string input, Func<int[], int> offsetFunc)
        {
            var numbers = input.Select(c => c - AsciiIndex).ToArray();
            var offset = offsetFunc(numbers);
            var total = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                int n = numbers[i];
                int m = numbers[(i + offset) % numbers.Length];

                if (n == m)
                {
                    total += n;
                }
            }

            return total;
        }
    }
}
