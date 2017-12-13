using Xunit;

namespace AdventOfCode.Tests
{
    public class Day13Tests
    {
        private static readonly string[] KnownInput =
        {
            "0: 3",
            "1: 2",
            "4: 4",
            "6: 4"
        };

        [Fact]
        public void Part1_KnownInput_ProducesCorrectSolution()
        {
            int actual = new Day13().Part1(KnownInput);

            Assert.Equal(24, actual);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            int actual = new Day13().Part1();

            Assert.Equal(2160, actual);
        }

        [Fact]
        public void Part2_KnownInput_ProducesCorrectSolution()
        {
            int actual = new Day13().Part2(KnownInput);

            Assert.Equal(10, actual);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectSolution()
        {
            int actual = new Day13().Part2();

            Assert.Equal(3907470, actual);
        }
    }
}
