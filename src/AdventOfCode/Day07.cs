using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solve for Day 7
    /// </summary>
    public class Day07
    {
        private class Node
        {
            public string Name { get; }

            public int Weight { get; }

            public string Parent { get; set; }

            public ICollection<string> Children { get; }

            public Node(string name, int weight, ICollection<string> children = null)
            {
                this.Name = name;
                this.Weight = weight;
                this.Children = children ?? new List<string>();
            }

            public static Node FromInput(string input)
            {
                string[] parts = input.Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);

                int openingBracket = parts[0].IndexOf('(');
                int closingBracket = parts[0].IndexOf(')');
                string name = parts[0].Substring(0, openingBracket - 1);
                string weight = parts[0].Substring(openingBracket + 1, closingBracket - openingBracket - 1);

                string[] children = parts.Length > 1
                                            ? parts[1].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToArray()
                                            : new string[0];

                return new Node(name, int.Parse(weight), children);
            }
        }

        /// <summary>
        /// Find the 'root' of a tree formed by parsing the input file
        /// </summary>
        /// <returns>Name of the root node</returns>
        public string Part1()
        {
            string[] lines = File.ReadAllLines("inputs/day07.txt");
            return this.Part1(lines);
        }

        /// <summary>
        /// Find the 'root' of a tree formed by parsing the input
        /// </summary>
        /// <param name="input">Input lines</param>
        /// <returns>Name of the root node</returns>
        public string Part1(string[] input)
        {
            IList<Node> nodes = input.Select(Node.FromInput).ToArray();
            IDictionary<string, Node> tree = nodes.ToDictionary(n => n.Name, n => n);

            // set parents
            foreach (Node node in nodes.Where(n => n.Children.Any()))
            {
                foreach (string child in node.Children)
                {
                    tree[child].Parent = node.Name;
                }
            }

            Node root = nodes.Single(n => string.IsNullOrEmpty(n.Parent) && n.Children.Any());

            return root.Name;
        }
    }
}
