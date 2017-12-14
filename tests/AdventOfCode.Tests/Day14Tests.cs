using Xunit;

namespace AdventOfCode.Tests
{
    public class Day14Tests
    {
        private const string RealInput = "jzgqcdpd";

        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            
            int actual = new Day14().Part1(RealInput);

            Assert.Equal(8074, actual);
        }

        [Fact]
        public void Part2_KnownInput_ProducesCorrectSolution()
        {
            int actual = new Day14().Part2("flqrgnkx");

            Assert.Equal(1242, actual);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectSolution()
        {
            int actual = new Day14().Part2(RealInput);

            Assert.Equal(1212, actual);
        }
    }
}
