using System;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 2
    /// </summary>
    public class Day02
    {
        /// <summary>
        /// The spreadsheet consists of rows of apparently-random numbers. To make sure the
        /// recovery process is on the right track, they need you to calculate the spreadsheet's
        /// checksum. For each row, determine the difference between the largest value and the
        /// smallest value; the checksum is the sum of all of these differences.
        /// 
        /// For example, given the following spreadsheet:
        /// 
        /// 5 1 9 5
        /// 7 5 3
        /// 2 4 6 8
        /// 
        /// - The first row's largest and smallest values are 9 and 1, and their difference is 8.
        /// - The second row's largest and smallest values are 7 and 3, and their difference is 4.
        /// - The third row's difference is 6.
        /// 
        /// In this example, the spreadsheet's checksum would be 8 + 4 + 6 = 18.
        /// </summary>
        public int Part1(string[] input)
        {
            var total = 0;

            foreach (string line in input)
            {
                var numbers = line.Split('\t').Select(int.Parse).ToArray();
                var min = numbers.Min();
                var max = numbers.Max();
                var diff = max - min;
                total += diff;
            }

            return total;
        }

        /// <summary>
        /// It sounds like the goal is to find the only two numbers in each row where one
        /// evenly divides the other - that is, where the result of the division operation
        /// is a whole number. They would like you to find those numbers on each line,
        /// divide them, and add up each line's result.
        /// 
        /// For example, given the following spreadsheet:
        /// 
        /// 5 9 2 8
        /// 9 4 7 3
        /// 3 8 6 5
        /// 
        /// - In the first row, the only two numbers that evenly divide are 8 and 2; the
        ///   result of this division is 4.
        /// - In the second row, the two numbers are 9 and 3; the result is 3.
        /// - In the third row, the result is 2.
        /// 
        /// In this example, the sum of the results would be 4 + 3 + 2 = 9.
        /// </summary>
        public int Part2(string[] input)
        {
            return input.Aggregate(0, (total, line) => total + CheckLine(line));
        }

        private static int CheckLine(string line)
        {
            var numbers = line.Split('\t').Select(double.Parse).ToArray();

            // O(n^2)... Hmmmm...
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    var x = numbers[i];
                    var y = numbers[j];

                    var divide = (x > y) ? (x / y) : (y / x);

                    if ((int)divide == divide)
                    {
                        return (int)divide;
                    }
                }
            }

            throw new ArgumentException("Line doesn't contain two divisible numbers");
        }
    }
}