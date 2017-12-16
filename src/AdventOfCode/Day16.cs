using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 16
    /// </summary>
    public class Day16
    {
        /// <summary>
        /// Perform the instructions in the input file and return the result
        /// </summary>
        public string Part1()
        {
            string input = File.ReadAllText("inputs/day16.txt");
            string[] instructions = input.Split(',');
            return this.Dance("abcdefghijklmnop", instructions);
        }

        /// <summary>
        /// Perform the instructions in the input file until a cycle is found
        /// and return the permutation which would be present on the 1000000000th
        /// iteration
        /// </summary>
        public string Part2()
        {
            string input = File.ReadAllText("inputs/day16.txt");
            string[] instructions = input.Split(',');
            string result = "abcdefghijklmnop";

            List<string> results = new List<string>();
            
            for (int i = 1; i < 1000000000; i++)
            {
                results.Add(result);
                result = this.Dance(result, instructions);

                if (results.Contains(result))
                {
                    // entire cycle calculated - for my input at 35 iterations
                    return results[1000000000 % i];
                }
            }

            throw new InvalidOperationException("No cycle found");
        }

        /// <summary>
        /// Perform the instructions on the input
        /// </summary>
        /// <returns>Result after performing the operation</returns>
        public string Dance(string input, string[] instructions)
        {
            char[] chars = input.ToArray();
            int start, end, size;
            string[] split;
            char temp;

            foreach (string instruction in instructions)
            {
                switch(instruction[0])
                {
                    case 's':
                        // s5 - move final 5 chars to the start
                        size = int.Parse(instruction.Substring(1));
                        chars = chars.Skip(input.Length - size).Take(size).Concat(chars.Take(input.Length - size)).ToArray();
                        break;
                    case 'x':
                        // x1/10 - swap index 1 and 10
                        split = instruction.Substring(1).Split('/');
                        start = int.Parse(split[0]);
                        end = int.Parse(split[1]);
                        temp = chars[start];
                        chars[start] = chars[end];
                        chars[end] = temp;
                        break;
                    case 'p':
                        // pa/b - swap chars a and b
                        start = Array.FindIndex(chars, 0, input.Length, c => c == instruction[1]);
                        end = Array.FindIndex(chars, 0, input.Length, c => c == instruction[3]);
                        temp = chars[start];
                        chars[start] = chars[end];
                        chars[end] = temp;
                        break;
                }
            }

            return new string(chars);
        }
    }
}