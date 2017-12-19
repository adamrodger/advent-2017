using Xunit;

namespace AdventOfCode.Tests
{
    public class Day19Tests
    {
        private static readonly string[] SampleInput =
        {
            "    |         ",
            "    |  +--+   ",
            "    A  |  C   ",
            "F---|----E|--+",
            "    |  |  |  D",
            "    +B-+  +--+"
        };

        [Fact]
        public void Part1_SampleInput()
        {
            (string actual, int _) = new Day19().Solve(SampleInput);

            Assert.Equal("ABCDEF", actual);
        }

        [Fact]
        public void Part1_RealInput()
        {
            (string actual, int _) = new Day19().Solve();

            Assert.Equal("GPALMJSOY", actual);
        }

        [Fact]
        public void Part2_SampleInput()
        {
            (string _, int actual) = new Day19().Solve(SampleInput);

            Assert.Equal(38, actual);
        }

        [Fact]
        public void Part2_RealInput()
        {
            (string _, int actual) = new Day19().Solve();

            Assert.Equal(16204, actual);
        }
    }
}
