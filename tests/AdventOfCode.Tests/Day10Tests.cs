using Xunit;

namespace AdventOfCode.Tests
{
    public class Day10Tests
    {
        [Fact]
        public void Part1_SampleInput_ProducesCorrectSolution()
        {
            var inputs = new[] { 3, 4, 1, 5 };

            int actual = new Day10().Solve(5, inputs);

            Assert.Equal(12, actual);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            var inputs = new[] { 129,154,49,198,200,133,97,254,41,6,2,1,255,0,191,108 };

            int actual = new Day10().Solve(256, inputs);

            Assert.Equal(19591, actual);
        }
    }
}