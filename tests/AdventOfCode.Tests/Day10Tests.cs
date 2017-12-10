using Xunit;

namespace AdventOfCode.Tests
{
    public class Day10Tests
    {
        [Fact]
        public void Part1_SampleInput_ProducesCorrectSolution()
        {
            var inputs = new[] { 3, 4, 1, 5 };

            int actual = new Day10().Part1(5, inputs);

            Assert.Equal(12, actual);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            var inputs = new[] { 129,154,49,198,200,133,97,254,41,6,2,1,255,0,191,108 };

            int actual = new Day10().Part1(256, inputs);

            Assert.Equal(19591, actual);
        }

        [Theory]
        [InlineData("", "a2582a3a0e66e6e86e3812dcb672a272")]
        [InlineData("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd")]
        [InlineData("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d")]
        [InlineData("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e")]
        public void Part2_KnownInput_ProducesCorrectOutput(string input, string expected)
        {
            string actual = new Day10().Part2(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectSolution()
        {
            string actual = new Day10().Part2("129,154,49,198,200,133,97,254,41,6,2,1,255,0,191,108");

            Assert.Equal("62e2204d2ca4f4924f6e7a80f1288786", actual);
        }
    }
}