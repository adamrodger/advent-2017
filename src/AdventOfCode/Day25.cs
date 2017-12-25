using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 25
    /// </summary>
    public class Day25
    {
        /// <summary>
        /// Using the instructions for a Turing machine from the input file, execute the program
        /// and return the checksum
        /// </summary>
        /// <returns>Program checksum</returns>
        public int Solve()
        {
            string[] instructions = File.ReadAllLines("inputs/day25.txt");
            return this.Solve(instructions);
        }

        /// <summary>
        /// Using the instructions for a Turing machine from the given instructions, execute the program
        /// and return the checksum
        /// </summary>
        /// <param name="instructions">Program instructions</param>
        /// <returns>Program checksum</returns>
        public int Solve(string[] instructions)
        {
            char state = instructions[0][instructions[0].Length - 2];
            int iterations = int.Parse(instructions[1].Split(' ')[5]);

            bool[] tape = new bool[iterations];
            int index = tape.Length / 2;

            Dictionary<char, TuringInstruction> stateMap = ParseInstructions(instructions);

            for (int i = 0; i < iterations; i++)
            {
                var instruction = stateMap[state];

                if (tape[index])
                {
                    tape[index] = instruction.TrueOutput;
                    index += instruction.TrueDirection == Direction.Left ? -1 : 1;
                    state = instruction.TrueNextState;
                }
                else
                {
                    tape[index] = instruction.FalseOutput;
                    index += instruction.FalseDirection == Direction.Left ? -1 : 1;
                    state = instruction.FalseNextState;
                }
            }

            return tape.Count(b => b);
        }

        /// <summary>
        /// Parse the given instructions to Turing instructions
        /// </summary>
        /// <param name="instructions">Instructions to parse</param>
        /// <returns>Parsed instructions</returns>
        private static Dictionary<char, TuringInstruction> ParseInstructions(ICollection<string> instructions)
        {
            var stateMap = new Dictionary<char, TuringInstruction>();

            for (int i = 3; i < instructions.Count; i += 10)
            {
                var t = TuringInstruction.Parse(instructions.Skip(i).Take(9).ToArray());
                stateMap[t.State] = t;
            }

            return stateMap;
        }
    }

    /// <summary>
    /// Instruction for a Turing machine
    /// </summary>
    public class TuringInstruction
    {
        /// <summary>
        /// Input state
        /// </summary>
        public char State { get; private set; }

        /// <summary>
        /// Value to write when input is false
        /// </summary>
        public bool FalseOutput { get; private set; }

        /// <summary>
        /// Direction to move when input is false
        /// </summary>
        public Direction FalseDirection { get; private set; }

        /// <summary>
        /// State to move to next when input is false
        /// </summary>
        public char FalseNextState { get; private set; }

        /// <summary>
        /// Value to write when input is true
        /// </summary>
        public bool TrueOutput { get; private set; }

        /// <summary>
        /// Direction to move when input is true
        /// </summary>
        public Direction TrueDirection { get; private set; }

        /// <summary>
        /// State to move to next when input is true
        /// </summary>
        public char TrueNextState { get; private set; }

        /// <summary>
        /// Parse an instruction from the given lines
        /// </summary>
        /// <param name="lines">Lines to parse</param>
        /// <returns>Parsed instruction</returns>
        public static TuringInstruction Parse(string[] lines)
        {
            var turing = new TuringInstruction
            {
                State = lines[0][lines[0].Length - 2],
                FalseOutput = lines[2][lines[2].Length - 2] == '1',
                FalseDirection = lines[3][lines[3].Length - 3] == 'f' ? Direction.Left : Direction.Right,
                FalseNextState = lines[4][lines[4].Length - 2],
                TrueOutput = lines[6][lines[6].Length - 2] == '1',
                TrueDirection = lines[7][lines[7].Length - 3] == 'f' ? Direction.Left : Direction.Right,
                TrueNextState = lines[8][lines[8].Length - 2]
            };

            return turing;
        }
    }
}
