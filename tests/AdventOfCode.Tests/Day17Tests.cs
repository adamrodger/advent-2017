using Xunit;

namespace AdventOfCode.Tests
{
    public class Day17Tests
    {
        [Fact]
        public void Part1_SampleInput_ProducesCorrectSolution()
        {
            int actual = new Day17().Part1(3);

            Assert.Equal(638, actual);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            int actual = new Day17().Part1(356);

            Assert.Equal(808, actual);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectSolution()
        {
            int actual = new Day17().Part2(356);

            Assert.Equal(47465686, actual);
        }
    }
}