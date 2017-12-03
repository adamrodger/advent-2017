using System.Collections.Generic;
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
    }
}