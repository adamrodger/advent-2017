using System.Collections.Generic;
using System.IO;

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
            while (duet.CommandCount["rcv"] == 0)
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

            return b.CommandCount["snd"];
        }
    }
}
