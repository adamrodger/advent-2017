using Xunit;

namespace AdventOfCode.Tests
{
    public class Day12Tests
    {
        private static readonly string[] KnownInput = 
        {
            "0 <-> 2",
            "1 <-> 1",
            "2 <-> 0, 3, 4",
            "3 <-> 2, 4",
            "4 <-> 2, 3, 6",
            "5 <-> 6",
            "6 <-> 4, 5"
        };

        [Fact]
        public void Part1_KnownInput_ProducesCorrectSolution()
        {
            (int actual, int _) = new Day12().Solve(KnownInput);

            Assert.Equal(6, actual);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            (int actual, int _) = new Day12().Solve();

            Assert.Equal(128, actual);
        }

        [Fact]
        public void Part2_KnownInput_ProducesCorrectSolution()
        {
            (int _, int actual) = new Day12().Solve(KnownInput);

            Assert.Equal(2, actual);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectSolution()
        {
            (int _, int actual) = new Day12().Solve();

            Assert.Equal(209, actual);
        }
    }
}
