using Xunit;

namespace AdventOfCode.Tests
{
    public class Day15Tests
    {
        [Fact]
        public void Part1_Known1_ProducesCorrectSolution()
        {
            int actual = new Day15().Part1(65, 8921, 5);

            Assert.Equal(1, actual);
        }

        [Fact]
        public void Part1_Known40mil_ProducesCorrectSolution()
        {
            int actual = new Day15().Part1(65, 8921, 40000000);

            Assert.Equal(588, actual);
        }

        [Fact]
        public void Part1_Real_ProducesCorrectSolution()
        {
            int actual = new Day15().Part1(679, 771, 40000000);

            Assert.Equal(626, actual);
        }

        [Fact]
        public void Part2_Known5mil_ProducesCorrectSolution()
        {
            int actual = new Day15().Part2(65, 8921, 5000000);

            Assert.Equal(309, actual);
        }

        [Fact]
        public void Part2_Real5mil_ProducesCorrectSolution()
        {
            int actual = new Day15().Part2(679, 771, 5000000);

            Assert.Equal(306, actual);
        }
    }
}
