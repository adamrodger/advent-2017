using Xunit;

namespace AdventOfCode.Tests
{
    public class Day06Tests
    {
        private const string Input = "14 0 15 12 11 11 3 5 1 6 8 4 9 1 8 4";

        [Theory]
        [InlineData("0 2 7 0", 5)]
        [InlineData(Input, 11137)]
        public void Part1_ValidInput_ProducesCorrectSolution(string input, int expected)
        {
            var actual = new Day06().Part1(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0 2 7 0", 4)]
        [InlineData(Input, 1037)]
        public void Part2_ValidInput_ProducesCorrectSolution(string input, int expected)
        {
            var actual = new Day06().Part2(input);

            Assert.Equal(expected, actual);
        }
    }
}
