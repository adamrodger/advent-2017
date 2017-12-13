using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 13
    /// </summary>
    public class Day13
    {
        /// <summary>
        /// Find the total 'severity' of the first run through the firewall for the real input file
        /// </summary>
        /// <returns>Total severity</returns>
        public int Part1()
        {
            string[] input = File.ReadAllLines("inputs/day13.txt");
            return this.Part1(input);
        }

        /// <summary>
        /// Find the total 'severity' of the first run through the firewall for the sample input
        /// </summary>
        /// <param name="input">Sample input</param>
        /// <returns>Total severity</returns>
        public int Part1(ICollection<string> input)
        {
            Dictionary<int, int> layers = ParseLayers(input);

            return layers.Where(pair => CalculatePosition(pair.Key, pair.Value) == 0)
                         .Sum(pair => pair.Key * pair.Value);
        }

        /// <summary>
        /// Find the minimum delay required to do a clean run of the firewall using the real input file
        /// </summary>
        /// <returns>Minimum delay</returns>
        public int Part2()
        {
            string[] input = File.ReadAllLines("inputs/day13.txt");
            return this.Part2(input);
        }

        /// <summary>
        /// Find the minimum delay required to do a clean run of the firewall using the sample input
        /// </summary>
        /// <param name="input">Sample input</param>
        /// <returns>Minimum delay</returns>
        public int Part2(ICollection<string> input)
        {
            Dictionary<int, int> layers = ParseLayers(input);
            bool clean = false;
            int delay = 1; // always get caught at delay=0

            while (!clean)
            {
                clean = layers.All(pair => CalculatePosition(pair.Key, pair.Value, delay) != 0);
                delay++;
            }

            return delay - 1;
        }

        /// <summary>
        /// Parse the input in format "layer: depth" into a layer:depth dictionary
        /// </summary>
        /// <param name="input">Input strings</param>
        /// <returns>Layer:depth map</returns>
        private static Dictionary<int, int> ParseLayers(ICollection<string> input)
        {
            var layers = input.Select(i => i.Split(new[] { ": " }, StringSplitOptions.None))
                              .ToDictionary(parts => int.Parse(parts[0]), parts => int.Parse(parts[1]));
            return layers;
        }

        /// <summary>
        /// Calculates the position of a layer during a test run, optionally with a delayed starting time
        /// </summary>
        /// <param name="layer">Layer index</param>
        /// <param name="depth">Layer depth</param>
        /// <param name="delay">Time delay (defaults to 0 for no delay)</param>
        /// <returns>Position</returns>
        private static int CalculatePosition(int layer, int depth, int delay = 0)
        {
            // (depth - 1) because the minimum position is 0, not 1, and 2x to allow for going up and back down again
            int position = (layer + delay) % (2 * (depth - 1));
            return position;
        }
    }
}
