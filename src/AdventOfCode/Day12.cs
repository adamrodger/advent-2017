using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 12
    /// </summary>
    public class Day12
    {
        /// <summary>
        /// Represents a node in the graph
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Node ID
            /// </summary>
            public int Id { get; private set; }

            /// <summary>
            /// Reachable nodes directly from this node
            /// </summary>
            public ICollection<int> Adjacent { get; private set; }

            /// <summary>
            /// Has this node been visited yet?
            /// </summary>
            public bool Visited => this.Group != 0;

            /// <summary>
            /// Which group is this node a part of?
            /// </summary>
            public int Group { get; set; }

            /// <summary>
            /// Create a new node instance from the given input string in the format "0 &lt;--&gt; 1, 2, 3"
            /// </summary>
            /// <param name="input">Input string</param>
            /// <returns>Parsed node</returns>
            public static Node FromInput(string input)
            {
                string[] parts = input.Split(new[] { " <-> " }, StringSplitOptions.RemoveEmptyEntries);

                return new Node
                {
                    Id = int.Parse(parts[0]),
                    Adjacent = parts[1].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                };
            }
        }

        /// <summary>
        /// Calculate the number of reachable nodes from node 0 and the number of unique
        /// groups of nodes from the real input file
        /// </summary>
        /// <returns>(number reachable from 0, number of groups)</returns>
        public (int, int) Solve()
        {
            string[] input = File.ReadAllLines("inputs/day12.txt");
            return this.Solve(input);
        }

        /// <summary>
        /// Calculate the number of reachable nodes from node 0 and the number of unique
        /// groups of nodes from the given input
        /// </summary>
        /// <returns>(number reachable from 0, number of groups)</returns>
        public (int, int) Solve(ICollection<string> input)
        {
            IDictionary<int, Node> nodes = input.Select(Node.FromInput).ToDictionary(n => n.Id, n => n);

            int groups = 0;

            while (nodes.Values.Any(n => !n.Visited))
            {
                groups++;
                MarkReachable(nodes, nodes.Values.First(n => !n.Visited).Id, groups);
            }

            Node zero = nodes[0];
            int reachableFromZero = nodes.Values.Count(n => n.Group == zero.Group);

            return (reachableFromZero, groups);
        }

        /// <summary>
        /// Mark all the nodes that are reachable from the given node as being a part of the given group
        /// </summary>
        /// <param name="nodes">All nodes, indexed by ID</param>
        /// <param name="start">Starting index</param>
        /// <param name="group">Group number</param>
        private void MarkReachable(IDictionary<int, Node> nodes, int start, int group)
        {
            var node = nodes[start];
            nodes[start].Group = group;

            foreach (int adjacent in node.Adjacent)
            {
                if (!nodes[adjacent].Visited)
                {
                    MarkReachable(nodes, adjacent, group);
                }
            }
        }
    }
}
