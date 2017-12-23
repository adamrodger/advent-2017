using Xunit;

namespace AdventOfCode.Tests
{
    public class Day23Tests
    {
        [Fact]
        public void Part1_RealInput()
        {
            int actual = new Day23().Part1();

            Assert.Equal(3025, actual);
        }

        [Fact]
        public void Part2_RealInput()
        {
            long actual = new Day23().Part2();

            Assert.Equal(915, actual);
        }
    }
}