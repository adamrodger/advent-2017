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

            int actual = new Day20().Part1(pixels);

            Assert.Equal(0, actual);
        }

        [Fact]
        public void Part1_RealInput()
        {
            int actual = new Day20().Part1();

            Assert.Equal(308, actual);
        }

        [Fact]
        public void Part2_SampleInput()
        {
            string[] pixels =
            {
                "p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>",
                "p=<-4,0,0>, v=< 2,0,0>, a=< 0,0,0>",
                "p=<-2,0,0>, v=< 1,0,0>, a=< 0,0,0>",
                "p=< 3,0,0>, v=<-1,0,0>, a=< 0,0,0>"
            };

            int actual = new Day20().Part2(pixels);

            Assert.Equal(1, actual);
        }

        [Fact]
        public void Part2_RealInput()
        {
            int actual = new Day20().Part2();

            Assert.Equal(504, actual);
        }
    }
}
