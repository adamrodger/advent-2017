using Xunit;

namespace AdventOfCode.Tests
{
    public class Day03Tests
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 1)]
        [InlineData(5, 2)]
        [InlineData(6, 1)]
        [InlineData(7, 2)]
        [InlineData(8, 1)]
        [InlineData(9, 2)]
        [InlineData(23, 2)]
        [InlineData(28, 3)]
        [InlineData(1024, 31)]
        [InlineData(361527, 326)]
        public void Part1_WhenCalled_ProducesCorrectSolution(int input, int expected)
        {
            var solve = new Day03();

            int actual = solve.Part1(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 4)]
        [InlineData(4, 5)]
        [InlineData(5, 10)]
        [InlineData(10, 11)]
        [InlineData(11, 23)]
        [InlineData(23, 25)]
        [InlineData(25, 26)]
        [InlineData(26, 54)]
        [InlineData(747, 806)]
        [InlineData(361527, 363010)]
        public void Part2_WhenCalled_ProducesCorrectSolution(int input, int expected)
        {
            var solve = new Day03();

            int actual = solve.Part2(input);

            Assert.Equal(expected, actual);
        }
    }
}