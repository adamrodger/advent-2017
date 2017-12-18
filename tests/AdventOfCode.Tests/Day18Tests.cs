using Xunit;

namespace AdventOfCode.Tests
{
    public class Day18Tests
    {
        private static readonly string[] SampleInput =
        {
            "set a 1",
            "add a 2",
            "mul a a",
            "mod a 5",
            "snd a",
            "set a 0",
            "rcv a",
            "jgz a -1",
            "set a 1",
            "jgz a -2"
        };

        [Fact]
        public void Part1_SampleInput()
        {
            long actual = new Day18().Part1(SampleInput);

            Assert.Equal(4, actual);
        }

        [Fact]
        public void Part1_RealInput()
        {
            long actual = new Day18().Part1();

            Assert.Equal(3423, actual);
        }

        [Fact]
        public void Part2_SampleInput()
        {
            int actual = new Day18().Part2(new[]
            {
                "snd 1",
                "snd 2",
                "snd p",
                "rcv a",
                "rcv b",
                "rcv c",
                "rcv d"
            });

            Assert.Equal(3, actual);
        }

        [Fact]
        public void Part2_RealInput()
        {
            int actual = new Day18().Part2();

            Assert.Equal(7493, actual);
        }
    }
}
