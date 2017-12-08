using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 8
    /// </summary>
    public class Day08
    {
        /// <summary>
        /// Solve for the input file
        /// </summary>
        /// <returns>Highest at the end, highest ever</returns>
        public (int, int) Solve()
        {
            string[] lines = File.ReadAllLines("inputs/day08.txt");
            return this.Solve(lines);
        }

        public (int, int) Solve(ICollection<string> input)
        {
            var conditions = new Dictionary<string, Func<int, int, bool>>
            {
                [">"] = (reg, x) => reg > x,
                ["<"] = (reg, x) => reg <= x && reg != x, // Visual Studio thinks this is a syntax error if you use '<' but it compiles and works fine!
                [">="] = (reg, x) => reg >= x,
                ["<="] = (reg, x) => reg <= x,
                ["=="] = (reg, x) => reg == x,
                ["!="] = (reg, x) => reg != x
            };

            var operations = new Dictionary<string, Func<int, int, int>>
            {
                ["inc"] = (reg, val) => reg + val,
                ["dec"] = (reg, val) => reg - val
            };

            var registers = new Dictionary<string, int>();
            int max = 0;

            foreach (string line in input)
            {
                string[] parts = line.Split(' ');
                (string reg, string op, int step, string condReg, string con, int conVal) = (parts[0], parts[1], int.Parse(parts[2]), parts[4], parts[5], int.Parse(parts[6]));

                registers.TryGetValue(condReg, out int checkVal);

                if (conditions[con](checkVal, conVal))
                {
                    registers.TryGetValue(reg, out int regVal);
                    registers[reg] = operations[op](regVal, step);
                    max = Math.Max(max, registers[reg]);
                }
            }

            return (registers.Values.Max(), max);
        }
    }
}
