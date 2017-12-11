using Xunit;

namespace AdventOfCode.Tests
{
    public class Day11Tests
    {
        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 0)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Solve_KnownInput_ProducesCorrectDistance(string input, int expected)
        {
            (int actual, int _) = new Day11().Solve(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Solve_RealInput_ProducesCorrectDistance()
        {
            (int actual, int _) = new Day11().Solve();

            Assert.Equal(715, actual);
        }

        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 2)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Solve_KnownInput_ProducesCorrectMaximum(string input, int expected)
        {
            (int _, int actual) = new Day11().Solve(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Solve_RealInput_ProducesCorrectMaximum()
        {
            (int _, int actual) = new Day11().Solve();

            Assert.Equal(1512, actual);
        }
    }
}
