using Xunit;

namespace AdventOfCode.Tests
{
    public class Day25Tests
    {
        private static readonly string[] SampleInput =
        {
            "Begin in state A.",
            "Perform a diagnostic checksum after 6 steps.",
            "",
            "In state A:",
            "  If the current value is 0:",
            "    - Write the value 1.",
            "    - Move one slot to the right.",
            "    - Continue with state B.",
            "  If the current value is 1:",
            "    - Write the value 0.",
            "    - Move one slot to the left.",
            "    - Continue with state B.",
            "",
            "In state B:",
            "  If the current value is 0:",
            "    - Write the value 1.",
            "    - Move one slot to the left.",
            "    - Continue with state A.",
            "  If the current value is 1:",
            "    - Write the value 1.",
            "    - Move one slot to the right.",
            "    - Continue with state A."
        };

        [Fact]
        public void Part1_SampleInput_ReturnsChecksum()
        {
            int actual = new Day25().Solve(SampleInput);

            Assert.Equal(3, actual);
        }

        [Fact]
        public void Part1_RealInput_ReturnsChecksum()
        {
            int actual = new Day25().Solve();

            Assert.Equal(5593, actual);
        }
    }
}