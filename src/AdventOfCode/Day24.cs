using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for day 24
    /// </summary>
    public class Day24
    {
        /// <summary>
        /// Calculate the strongest and longest bridges available given the available poles from the real input file
        /// </summary>
        /// <returns>(strangth of strongest bridge, strength of longest bridge)</returns>
        public (int, int) Solve()
        {
            string[] instructions = File.ReadAllLines("inputs/day24.txt");
            return this.Solve(instructions);
        }

        /// <summary>
        /// Calculate the strongest and longest bridges available given the available poles in the instructions
        /// </summary>
        /// <param name="instructions">Pole instructions</param>
        /// <returns>(strangth of strongest bridge, strength of longest bridge)</returns>
        public (int, int) Solve(string[] instructions)
        {
            var poles = instructions.Select(i => i.Split('/'))
                                    .Select(parts => new Pole
                                    {
                                        End1 = int.Parse(parts[0]),
                                        End2 = int.Parse(parts[1])
                                    })
                                    .ToArray();
            var tree = new Dictionary<int, ICollection<Pole>>();

            // build a map of which ends can connect to which other poles
            foreach (Pole p in poles)
            {
                if (!tree.ContainsKey(p.End1))
                {
                    tree[p.End1] = new HashSet<Pole>();
                }
                if (!tree.ContainsKey(p.End2))
                {
                    tree[p.End2] = new HashSet<Pole>();
                }
                tree[p.End1].Add(p);
                tree[p.End2].Add(p);
            }

            IEnumerable<ICollection<Pole>> bridges = tree[0].SelectMany(p => this.BuildBridge(tree, p, p.End1 != 0, new List<Pole> { p })).ToList();
            var max = 0;
            var longest = 0;
            var longestSum = 0;

            foreach (ICollection<Pole> bridge in bridges)
            {
                var sum = bridge.Select(p => p.End1 + p.End2).Sum();

                if (sum > max)
                {
                    max = sum;
                }

                if (bridge.Count > longest || (bridge.Count == longest && sum > longestSum))
                {
                    longest = bridge.Count;
                    longestSum = sum;
                }
            }

            return (max, longestSum);
        }

        /// <summary>
        /// Recursively generate all possible bridges when connecting together the available poles
        /// </summary>
        /// <param name="poles">Map of which poles contain a given pin configuration</param>
        /// <param name="current">Current pole</param>
        /// <param name="useEnd1">True = try and connect to end 1, False try and connect to end 2</param>
        /// <param name="bridge">Current bridge configuration</param>
        /// <returns>All available valid configurations of bridges</returns>
        private IEnumerable<IList<Pole>> BuildBridge(IDictionary<int, ICollection<Pole>> poles, Pole current, bool useEnd1, IList<Pole> bridge)
        {
            var candidates = useEnd1 ? poles[current.End1] : poles[current.End2];
            var next = candidates.Except(bridge).ToArray();

            // for each possible child pole, recurse to add more and more valid poles until exhausted
            foreach (Pole pole in next)
            {
                bool connectedToEnd1 = useEnd1 ? pole.End2 == current.End1 : pole.End2 == current.End2;
                var childBridges = this.BuildBridge(poles, pole, connectedToEnd1, bridge.Concat(new[] { pole }).ToList());
                
                foreach (var b in childBridges)
                {
                    yield return b;
                }
            }

            // no more valid poles in the list, end recursion
            yield return bridge;
        }
    }

    /// <summary>
    /// Represents a pole with 2 ends, each with a separate pin configuration that can
    /// connect to others of the same kind
    /// </summary>
    public class Pole
    {
        /// <summary>
        /// Number of pins on end 1
        /// </summary>
        public int End1 { get; set; }

        /// <summary>
        /// Number of pins on end 2
        /// </summary>
        public int End2 { get; set; }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns>String representation of the object</returns>
        public override string ToString()
        {
            return $"{this.End1}/{this.End2}";
        }
    }
}