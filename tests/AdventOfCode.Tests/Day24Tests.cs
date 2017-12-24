using Xunit;

namespace AdventOfCode.Tests
{
    public class Day24Tests
    {
        private static readonly string[] SampleInput =
        {
            "0/2",
            "2/2",
            "2/3",
            "3/4",
            "3/5",
            "0/1",
            "10/1",
            "9/10"
        };

        [Fact]
        public void Part1_SampleInput_ReturnsStrongestPossibleBridge()
        {
            (int actual, int _) = new Day24().Solve(SampleInput);

            Assert.Equal(31, actual);
        }

        [Fact]
        public void Part1_RealInput_ReturnsStrongestPossibleBridge()
        {
            (int actual, int _) = new Day24().Solve();

            Assert.Equal(1868, actual);
        }

        [Fact]
        public void Part2_SampleInput_ReturnsStrengthOfLongestPossibleBridge()
        {
            (int _, int actual) = new Day24().Solve(SampleInput);

            Assert.Equal(19, actual);
        }

        [Fact]
        public void Part2_RealInput_ReturnsStrengthOfLongestPossibleBridge()
        {
            (int _, int actual) = new Day24().Solve();

            Assert.Equal(1841, actual);
        }
    }
}