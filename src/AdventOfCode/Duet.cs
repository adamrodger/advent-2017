using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interpreter for the Duet instructions
    /// </summary>
    public class Duet
    {
        private readonly IList<string> instructions;
        private readonly Queue<long> input;
        private readonly Queue<long> output;
        private readonly IDictionary<char, long> registers;
        private readonly IDictionary<string, int> commandCounts;

        private long pointer;

        /// <summary>
        /// Value of the message that was last sent by this instance
        /// </summary>
        public long LastSent { get; private set; }

        /// <summary>
        /// Is this instance finished executing?
        /// </summary>
        public bool Finished => this.pointer < 0 || this.pointer >= this.instructions.Count;

        /// <summary>
        /// Is this instance waiting for input from another instance?
        /// </summary>
        public bool Waiting => this.input.Count == 0 && this.instructions[(int)this.pointer].StartsWith("rcv");

        /// <summary>
        /// Current value of all registers
        /// </summary>
        public IReadOnlyDictionary<char, long> Registers => new ReadOnlyDictionary<char, long>(this.registers);

        /// <summary>
        /// Count of the number of executions of each command
        /// </summary>
        public IReadOnlyDictionary<string, int> CommandCount => new ReadOnlyDictionary<string, int>(this.commandCounts);

        /// <summary>
        /// Initialises a new instance of the <see cref="Duet"/> class.
        /// </summary>
        /// <param name="instructions">Instructions to follow</param>
        /// <param name="input">Input buffer</param>
        /// <param name="output">Output buffer</param>
        /// <param name="id">Instance ID</param>
        public Duet(IList<string> instructions, Queue<long> input, Queue<long> output, int id)
        {
            this.instructions = instructions;
            this.input = input;
            this.output = output;

            this.registers = Enumerable.Range('a', 'z' - 'a').ToDictionary(c => (char)c, c => 0L);
            this.registers['p'] = id;

            this.commandCounts = new Dictionary<string, int>
            {
                ["snd"] = 0,
                ["rcv"] = 0,
                ["set"] = 0,
                ["add"] = 0,
                ["sub"] = 0,
                ["mul"] = 0,
                ["mod"] = 0,
                ["jgz"] = 0,
                ["jnz"] = 0,
            };
        }

        /// <summary>
        /// Execute the current instruction and advance the pointer to the next instruction
        /// </summary>
        public void Step()
        {
            if (this.Finished)
            {
                return;
            }

            string instruction = this.instructions[(int)this.pointer];
            string[] parts = instruction.Split(' ');

            string command = parts[0];
            char register = parts[1][0];
            long value = parts.Length > 2 ? this.GetValue(parts[2]) : 0;

            this.commandCounts[command]++;

            switch (command)
            {
                case "snd":
                    value = this.GetValue(parts[1]);
                    this.output.Enqueue(value);
                    this.LastSent = value;
                    break;
                case "rcv":
                    this.registers[register] = this.input.Dequeue();
                    break;
                case "set":
                    this.registers[register] = value;
                    break;
                case "add":
                    this.registers[register] += value;
                    break;
                case "sub":
                    this.registers[register] -= value;
                    break;
                case "mul":
                    this.registers[register] *= value;
                    break;
                case "mod":
                    this.registers[register] %= value;
                    break;
                case "jgz":
                    if (this.GetValue(parts[1]) > 0)
                    {
                        this.pointer += value;
                        return;
                    }

                    break;
                case "jnz":
                    if (this.GetValue(parts[1]) != 0)
                    {
                        this.pointer += value;
                        return;
                    }

                    break;
                default:
                    throw new FormatException();
            }

            this.pointer++;
        }

        /// <summary>
        /// Get the label either as a literal value or the value of the labelled register
        /// </summary>
        /// <param name="label">Label to parse</param>
        /// <returns>Either the literal value if a number or the register value if a character</returns>
        private long GetValue(string label)
        {
            if (long.TryParse(label, out long value))
            {
                return value;
            }

            char register = label[0];
            return this.registers[register];
        }
    }
}