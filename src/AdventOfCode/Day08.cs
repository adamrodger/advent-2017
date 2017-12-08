using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Solve for the given input
        /// </summary>
        /// <returns>Highest at the end, highest ever</returns>
        public (int, int) Solve(ICollection<string> input)
        {
            var registers = new Dictionary<string, int>();
            int max = 0;

            foreach (string line in input)
            {
                var instruction = Instruction.Parse(line);
                if (instruction.Condition(registers))
                {
                    instruction.Action(registers);
                    max = Math.Max(max, registers.Values.Max());
                }
            }

            return (registers.Values.Max(), max);
        }
    }

    /// <summary>
    /// Instruction
    /// </summary>
    public class Instruction
    {
        private static readonly Regex ParseRegex = new Regex(@"(\w+) (inc|dec) (-?\d+) if (\w+) ([!<>!=]+) (-?\d+)", RegexOptions.Compiled);

        /// <summary>
        /// Action to perform if the condition is met
        /// </summary>
        public Action<IDictionary<string, int>> Action { get; private set; }

        /// <summary>
        /// Condition of the action
        /// </summary>
        public Predicate<IDictionary<string, int>> Condition { get; private set; }

        /// <summary>
        /// Parse an instruction from the input string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Parsed instruction</returns>
        public static Instruction Parse(string input)
        {
            Match match = ParseRegex.Match(input);

            string target = match.Groups[1].Value;
            bool increment = match.Groups[2].Value.Equals("inc");
            int step = int.Parse(match.Groups[3].Value);

            if (!increment)
            {
                step *= -1;
            }

            Action<IDictionary<string, int>> action = registers =>
            {
                registers.TryGetValue(target, out int value);
                registers[target] = value + step;
            };

            string conditionRegister = match.Groups[4].Value;
            string conditionOperation = match.Groups[5].Value;
            int conditionValue = int.Parse(match.Groups[6].Value);

            Predicate<int> condition;

            switch (conditionOperation)
            {
                case "<":
                    condition = x => x < conditionValue;
                    break;
                case ">":
                    condition = x => x > conditionValue;
                    break;
                case "<=":
                    condition = x => x <= conditionValue;
                    break;
                case ">=":
                    condition = x => x >= conditionValue;
                    break;
                case "==":
                    condition = x => x == conditionValue;
                    break;
                case "!=":
                    condition = x => x != conditionValue;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown condition: {conditionOperation}");
            }

            Predicate<IDictionary<string, int>> predicate = registers =>
            {
                registers.TryGetValue(conditionRegister, out int value);
                return condition(value);
            };

            return new Instruction
            {
                Action = action,
                Condition = predicate
            };
        }
    }
}
