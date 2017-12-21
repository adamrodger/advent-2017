using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 21
    /// </summary>
    public class Day21
    {
        /// <summary>
        /// Transform the starting image using the instructions from the real input file
        /// </summary>
        /// <param name="iterations">Number of transformations to perform</param>
        /// <returns>Total number of # symbols after all transformations are complete</returns>
        public int Solve(int iterations)
        {
            string[] lines = File.ReadAllLines("inputs/day21.txt");
            return this.Solve(lines, iterations);
        }

        /// <summary>
        /// Transform the starting image using the instructions from the given input
        /// </summary>
        /// <param name="lines">Instruction lines to parse</param>
        /// <param name="iterations">Number of transformations to perform</param>
        /// <returns>Total number of # symbols after all transformations are complete</returns>
        public int Solve(string[] lines, int iterations)
        {
            IDictionary<string, char[][]> rules = ParseRules(lines);

            char[][] image = 
            {
                new[] { '.', '#', '.' },
                new[] { '.', '.', '#' },
                new[] { '#', '#', '#' }
            };

            for (int i = 0; i < iterations; i++)
            {
                image = Transform(image, rules);
            }

            return image.SelectMany(c => c).Count(c => c == '#');
        }

        /// <summary>
        /// Parse the rules lines to a lookup of input pattern to output image transformation
        /// </summary>
        /// <param name="lines">Lines to parse</param>
        /// <returns>Rules dictionary</returns>
        private static Dictionary<string, char[][]> ParseRules(string[] lines)
        {
            var rules = new Dictionary<string, char[][]>();

            foreach (string[] line in lines.Select(l => l.Split(new[] { " => " }, StringSplitOptions.None)))
            {
                string input = line[0];
                char[][] output = line[1].ToPattern();

                char[][] pattern = input.ToPattern();

                // add each of the 4 rotations, then flip and add each of those 4 rotations
                for (int flip = 0; flip < 2; flip++)
                {
                    rules[pattern.ToPatternString()] = output;

                    for (int i = 0; i < 3; i++)
                    {
                        pattern = pattern.Rotate();
                        rules[pattern.ToPatternString()] = output;
                    }

                    pattern = pattern.Flip();
                }
            }

            return rules;
        }

        /// <summary>
        /// Transform the input image to a new output image by splitting the input to 2x2 or 3x3
        /// sub-images and converting them to 3x3 or 4x4 sub-images using the supplied rules and
        /// then stitching all the sub-images back to a new larger image
        /// </summary>
        /// <param name="image">Input image</param>
        /// <param name="rules">Rules to apply</param>
        /// <returns>Output image</returns>
        private static char[][] Transform(char[][] image, IDictionary<string, char[][]> rules)
        {
            // whether to do 2x2 or 3x3
            int step = (image.Length % 2) + 2;

            int segments = image.Length / step;
            int newSize = segments * (step + 1);
            char[][] newImage = Enumerable.Range(0, newSize).Select(_ => new char[newSize]).ToArray();

            for (int y = 0; y < segments; y++)
            {
                for (int x = 0; x < segments; x++)
                {
                    // chop the image into segments and transform to the new image
                    var rows = image.Skip(y * step).Take(step);
                    var segment = rows.Select(r => r.Skip(x * step).Take(step));
                    string pattern = segment.ToPatternString();
                    
                    char[][] output = rules[pattern];

                    // copy in to new image
                    for (int i = 0; i < output.Length; i++)
                    {
                        for (int j = 0; j < output.Length; j++)
                        {
                            newImage[(y * (step + 1)) + i][x * (step + 1) + j] = output[i][j];
                        }
                    }
                }
            }

            return newImage;
        }
    }

    /// <summary>
    /// Extension methods to make generating and using image patterns easier
    /// </summary>
    public static class PatternExtensions
    {
        /// <summary>
        /// Convert an input string to an output pattern
        /// </summary>
        /// <param name="input">Input pattern string</param>
        /// <returns>Output pattern array</returns>
        public static char[][] ToPattern(this string input)
        {
            IEnumerable<IEnumerable<char>> data = input.Split('/');
            return data.ToPattern();
        }

        /// <summary>
        /// Enumerate the input to convert to a pattern
        /// </summary>
        /// <param name="input">Input pattern</param>
        /// <returns>Output pattern</returns>
        public static char[][] ToPattern(this IEnumerable<IEnumerable<char>> input)
        {
            return input.Select(x => x.ToArray()).ToArray();
        }

        /// <summary>
        /// Convert the pattern image to a string representation
        /// </summary>
        /// <param name="pattern">Input pattern</param>
        /// <returns>Output string representation</returns>
        public static string ToPatternString(this IEnumerable<IEnumerable<char>> pattern)
        {
            return string.Join("/", pattern.Select(c => new string(c.ToArray())));
        }

        /// <summary>
        /// Rotate the pattern 90deg
        /// </summary>
        /// <param name="pattern">Input pattern</param>
        /// <returns>Rotated pattern</returns>
        public static char[][] Rotate(this char[][] pattern)
        {
            var n = pattern.Length;
            char[][] ret = Enumerable.Range(0, n).Select(_ => new char[n]).ToArray();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ret[i][j] = pattern[n - j - 1][i];
                }
            }

            return ret;
        }

        /// <summary>
        /// Flip the pattern along the X axis
        /// </summary>
        /// <param name="pattern">Input pattern</param>
        /// <returns>Output pattern flipped along the x axis</returns>
        public static char[][] Flip(this char[][] pattern)
        {
            IEnumerable<IEnumerable<char>> data = pattern.Select(c => c.Reverse());
            return data.ToPattern();
        }
    }
}
