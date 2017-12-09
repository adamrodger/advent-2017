using Xunit;

namespace AdventOfCode.Tests
{
    public class Day09Tests
    {
        [Theory]
        [InlineData("{}", 1)]
        [InlineData("{{{}}}", 6)]
        [InlineData("{{},{}}", 5)]
        [InlineData("{{{},{},{{}}}}", 16)]
        [InlineData("{<a>,<a>,<a>,<a>}", 1)]
        [InlineData("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
        [InlineData("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
        [InlineData("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
        public void Part1_ValidInput_ProducesCorrectSolution(string input, int expected)
        {
            (int actual, int _) = new Day09().Solve(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectSolution()
        {
            (int actual, int _) = new Day09().Solve();

            Assert.Equal(9251, actual);
        }

        [Theory]
        [InlineData("<>", 0)]
        [InlineData("<abcde>", 5)]
        [InlineData("<<<<>", 3)]
        [InlineData("<{!>}>", 2)]
        [InlineData("<!!>", 0)]
        [InlineData("<!!!>>", 0)]
        [InlineData("<{o\"i!a,<{i<a>", 10)]
        public void Part2_ValidInput_ProducesCorrectGarbageTotal(string input, int expected)
        {
            (int _, int actual) = new Day09().Solve(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectGarbageTotal()
        {
            (int _, int actual) = new Day09().Solve();

            Assert.Equal(4322, actual);
        }
    }
}