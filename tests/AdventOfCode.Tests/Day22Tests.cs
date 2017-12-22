using Xunit;

namespace AdventOfCode.Tests
{
    public class Day22Tests
    {
        [Fact]
        public void Part1_RealInput()
        {
            int actual = new Day22().Part1();

            Assert.Equal(5330, actual);
        }

        [Fact]
        public void Part2_RealInput()
        {
            int actual = new Day22().Part2();

            Assert.Equal(2512103, actual);
        }
    }
}
