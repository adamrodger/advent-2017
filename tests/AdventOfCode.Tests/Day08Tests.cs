using Xunit;

namespace AdventOfCode.Tests
{
    public class Day08Tests
    {
        private static readonly string[] Input =
        {
            "b inc 5 if a > 1",
            "a inc 1 if b < 5",
            "c dec -10 if a >= 1",
            "c inc -20 if c == 10"
        };

        [Fact]
        public void Part1_KnownInput_ReturnsLargestRegisterAtTheEnd()
        {
            (int actual, int _) = new Day08().Solve(Input);

            Assert.Equal(1, actual);
        }

        [Fact]
        public void Part1_InputFile_ReturnsLargestRegisterAtTheEnd()
        {
            (int actual, int _) = new Day08().Solve();

            Assert.Equal(5075, actual);
        }

        [Fact]
        public void Part2_KnownInput_ReturnsLargestRegisterAtAnyPoint()
        {
            (int _, int actual) = new Day08().Solve(Input);

            Assert.Equal(10, actual);
        }

        [Fact]
        public void Part2_InputFile_ReturnsLargestRegisterAtAnyPoint()
        {
            (int _, int actual) = new Day08().Solve();

            Assert.Equal(7310, actual);
        }
    }
}
