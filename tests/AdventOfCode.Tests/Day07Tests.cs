using Xunit;

namespace AdventOfCode.Tests
{
    public class Day07Tests
    {
            private static readonly string[] SimpleInput =
            {
                "pbga (66)",
                "xhth (57)",
                "ebii (61)",
                "havc (66)",
                "ktlj (57)",
                "fwft (72) -> ktlj, cntj, xhth",
                "qoyq (66)",
                "padx (45) -> pbga, havc, qoyq",
                "tknk (41) -> ugml, padx, fwft",
                "jptl (61)",
                "ugml (68) -> gyxo, ebii, jptl",
                "gyxo (61)",
                "cntj (57)"
            };

        [Fact]
        public void Part1_KnownInput_ReturnsRootNode()
        {
            string actual = new Day07().Part1(SimpleInput);

            Assert.Equal("tknk", actual);
        }

        [Fact]
        public void Part1_InputFile_ReturnsRootNode()
        {
            string actual = new Day07().Part1();

            Assert.Equal("aapssr", actual);
        }

        [Fact]
        public void Part2_KnownInput_ReturnsCorrectionFactor()
        {
            int actual = new Day07().Part2(SimpleInput);

            Assert.Equal(60, actual);
        }

        [Fact]
        public void Part2_InputFile_ReturnsCorrectionFactor()
        {
            int actual = new Day07().Part2();

            Assert.Equal(1458, actual);
        }
    }
}
