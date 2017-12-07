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
            /// <summary>The name of the node nodes</summary>
            public string Name { get; }

            /// <summary>The weight of the node</summary>
            public int Weight { get; }

            /// <summary>The node's parent (or null if no parent)</summary>
            public string Parent { get; set; }

            /// <summary>Children of the node (might be empty)</summary>
            public ICollection<string> Children { get; }

            /// <summary>The total weight of all child nodes plus this node</summary>
            public int TotalWeight { get; set; }

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
        public string Part1(ICollection<string> input)
        {
            IDictionary<string, Node> tree = this.GetTree(input);

            Node root = tree.Values.Single(n => string.IsNullOrEmpty(n.Parent) && n.Children.Any());

            return root.Name;
        }

        /// <summary>
        /// Find the 'root' of a tree formed by parsing the input file
        /// </summary>
        /// <returns>Required new weight</returns>
        public int Part2()
        {
            string[] lines = File.ReadAllLines("inputs/day07.txt");
            return this.Part2(lines);
        }

        /// <summary>
        /// Find the 'root' of a tree formed by parsing the input
        /// </summary>
        /// <param name="input">Input lines</param>
        /// <returns>Required new weight</returns>
        public int Part2(ICollection<string> input)
        {
            IDictionary<string, Node> tree = this.GetTree(input);
            string root = this.Part1(input);
            Node rootNode = tree[root];

            this.CalculateTotalWeight(tree, rootNode);

            return this.FindCorrectedWeight(tree, rootNode);
        }

        /// <summary>
        /// Parse the input into a tree of nodes
        /// </summary>
        private IDictionary<string, Node> GetTree(ICollection<string> input)
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

            return tree;
        }

        /// <summary>
        /// Recurse into every child depth-first and calculate the total weight
        /// of the root node and all child nodes
        /// </summary>
        /// <param name="tree">Tree of weighted nodes</param>
        /// <param name="root">Root node</param>
        private void CalculateTotalWeight(IDictionary<string, Node> tree, Node root)
        {
            foreach (string child in root.Children)
            {
                Node childNode = tree[child];
                CalculateTotalWeight(tree, childNode);
                root.TotalWeight += childNode.TotalWeight;
            }

            root.TotalWeight += root.Weight;
        }

        /// <summary>
        /// Find the deepest node with unbalanced children and calculate the weight
        /// required to make it balanced
        /// </summary>
        /// <param name="tree">Tree of weighted nodes</param>
        /// <returns>Corrected weight</returns>
        private int FindCorrectedWeight(IDictionary<string, Node> tree, Node root)
        {
            Node current = root;
            int weightDiff = 0;

            // follow the tree along the imbalanced path until the final imbalanced node
            while (true)
            {
                var children = current.Children.Select(c => tree[c]).ToArray();
                var weights = children.Select(c => c.TotalWeight).ToArray();

                if (weights.Distinct().Count() > 1)
                {
                    current = children.First(c => c.TotalWeight == weights.Max());
                    weightDiff = weights.Max() - weights.Min();
                }
                else
                {
                    return current.Weight - weightDiff;
                }
            }
        }
    }
}
