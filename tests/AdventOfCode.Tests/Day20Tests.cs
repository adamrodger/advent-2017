using Xunit;

namespace AdventOfCode.Tests
{
    public class Day20Tests
    {
        [Fact]
        public void Part1_SampleInput()
        {
            string[] pixels =
            {
                "p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>",
                "p=< 4,0,0>, v=< 0,0,0>, a=<-2,0,0>"
            };

            int actual = new Day20().Solve(pixels);

            Assert.Equal(0, actual);
        }

        [Fact]
        public void Part1_RealInput()
        {
            int actual = new Day20().Solve();

            Assert.Equal(308, actual);
        }
    }
}
