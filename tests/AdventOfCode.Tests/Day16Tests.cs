using Xunit;

namespace AdventOfCode.Tests
{
    public class Day16Tests
    {
        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            string actual = new Day16().Part1();

            Assert.Equal("bijankplfgmeodhc", actual);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectSolution()
        {
            string actual = new Day16().Part2();

            Assert.Equal("bpjahknliomefdgc", actual);
        }
    }
}