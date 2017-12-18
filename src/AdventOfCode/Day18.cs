using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for day 18
    /// </summary>
    public class Day18
    {
        /// <summary>
        /// Run the instructions from the real input file until a message is received and then return the last sent message
        /// </summary>
        /// <returns>Last sent message</returns>
        public int Part1()
        {
            string[] instructions = File.ReadAllLines("inputs/day18.txt");
            return this.Part1(instructions);
        }

        /// <summary>
        /// Run the instructions until a message is received and then return the last sent message
        /// </summary>
        /// <param name="instructions">Instructions to follow</param>
        /// <returns>Last sent message</returns>
        public int Part1(string[] instructions)
        {
            var buffer = new Queue<long>();
            var duet = new Duet(instructions, buffer, buffer, 0);

            // wait until the first message is successfully received
            while (duet.ReceivedMessages == 0)
            {
                duet.Step();
            }

            return (int)duet.LastSent;
        }

        /// <summary>
        /// Run the instructions from the real input file in two Duet instances and return the
        /// number of times the second instance sent a message
        /// </summary>
        /// <returns>Number of sent messages by the second instance</returns>
        public int Part2()
        {
            string[] instructions = File.ReadAllLines("inputs/day18.txt");
            return this.Part2(instructions);
        }

        /// <summary>
        /// Run the instructions from the real input file in two Duet instances and return the
        /// number of times the second instance sent a message
        /// </summary>
        /// <param name="instructions">Instructions to follow</param>
        /// <returns>Number of sent messages by the second instance</returns>
        public int Part2(string[] instructions)
        {
            var inputA = new Queue<long>();
            var inputB = new Queue<long>();

            var a = new Duet(instructions, inputA, inputB, 0);
            var b = new Duet(instructions, inputB, inputA, 1);

            // wait for either deadlock or both terminated normally
            while (!(a.Waiting && b.Waiting) && !(a.Finished && b.Finished))
            {
                while (!a.Finished && !a.Waiting)
                {
                    a.Step();
                }

                while (!b.Finished && !b.Waiting)
                {
                    b.Step();
                }
            }

            return b.SentMessages;
        }
    }

    /// <summary>
    /// Interpreter for the Duet instructions
    /// </summary>
    public class Duet
    {
        private readonly IList<string> instructions;
        private readonly Queue<long> input;
        private readonly Queue<long> output;
        private readonly IDictionary<char, long> registers;

        private long pointer;

        /// <summary>
        /// Value of the message that was last sent by this instance
        /// </summary>
        public long LastSent { get; private set; }

        /// <summary>
        /// Total number of sent message
        /// </summary>
        public int SentMessages { get; private set; }

        /// <summary>
        /// Total number of received messages
        /// </summary>
        public int ReceivedMessages { get; private set; }

        /// <summary>
        /// Is this instance finished executing?
        /// </summary>
        public bool Finished => this.pointer < 0 || this.pointer >= this.instructions.Count;

        /// <summary>
        /// Is this instance waiting for input from another instance?
        /// </summary>
        public bool Waiting => this.input.Count == 0 && this.instructions[(int)this.pointer].StartsWith("rcv");

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
            long value = parts.Length == 3 ? this.GetValue(parts[2]) : 0;

            switch (command)
            {
                case "snd":
                    value = this.GetValue(parts[1]);
                    this.output.Enqueue(value);
                    this.SentMessages++;
                    this.LastSent = value;
                    break;
                case "rcv":
                    this.registers[register] = this.input.Dequeue();
                    this.ReceivedMessages++;

                    break;
                case "set":
                    this.registers[register] = value;
                    break;
                case "add":
                    this.registers[register] += value;
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
