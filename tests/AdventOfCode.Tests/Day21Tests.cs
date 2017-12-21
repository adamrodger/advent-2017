using Xunit;

namespace AdventOfCode.Tests
{
    public class Day21Tests
    {
        [Fact]
        public void Part1_RealInput()
        {
            int actual = new Day21().Solve(5);

            Assert.Equal(208, actual);
        }

        [Fact]
        public void Part2_RealInput()
        {
            int actual = new Day21().Solve(18);

            Assert.Equal(2480380, actual);
        }
    }
}