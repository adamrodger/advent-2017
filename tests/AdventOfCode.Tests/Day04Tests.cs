using Xunit;

namespace AdventOfCode.Tests
{
    public class Day04Tests
    {
        [Fact]
        public void Part1_WhenCalled_ReturnsCorrectCount()
        {
            const int expected = 386;

            int actual = Day04.Part1();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Part2_WhenCalled_ReturnsCorrectCount()
        {
            const int expected = 208;

            int actual = Day04.Part2();

            Assert.Equal(expected, actual);
        }
    }
}
