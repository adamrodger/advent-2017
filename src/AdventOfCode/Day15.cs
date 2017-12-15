namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 15
    /// </summary>
    public class Day15
    {
        private const int FactorA = 16807;
        private const int FactorB = 48271;

        /// <summary>
        /// Generate sequences A and B using the given seeds for the given iterations and return the number
        /// of combinations in which the lower 16bits match
        /// </summary>
        /// <param name="seedA">Seed for sequence A</param>
        /// <param name="seedB">Seed for sequence B</param>
        /// <param name="iterations">Number of iterations</param>
        /// <returns>Count of matching combinations</returns>
        public int Part1(ulong seedA, ulong seedB, int iterations)
        {
            return Check(seedA, seedB, 1, 1, iterations);
        }

        /// <summary>
        /// Generate sequences A and B using the given seeds for the given iterations, accepting only numbers
        /// which match a known divisor (4 for A and 8 for B), and return the number of combinations in which
        /// the lower 16bits match
        /// </summary>
        /// <param name="seedA">Seed for sequence A</param>
        /// <param name="seedB">Seed for sequence B</param>
        /// <param name="iterations">Number of iterations</param>
        /// <returns>Count of matching combinations</returns>
        public int Part2(ulong seedA, ulong seedB, int iterations)
        {
            return Check(seedA, seedB, 4, 8, iterations);
        }

        /// <summary>
        /// Using the given seeds and divisors, walk sequences A and B for the given number of iterations and
        /// return the number of times their lower 16bits coincide
        /// </summary>
        /// <param name="seedA">Seed for sequence A</param>
        /// <param name="seedB">Seed for sequence B</param>
        /// <param name="divisorA">Divisor for sequence A</param>
        /// <param name="divisorB">Divisor for sequence B</param>
        /// <param name="iterations">Number of iterations</param>
        /// <returns>Number of times the lower 16bits of the two sequences coincide</returns>
        private static int Check(ulong seedA, ulong seedB, ulong divisorA, ulong divisorB, int iterations)
        {
            int count = 0;
            ulong prevA = seedA;
            ulong prevB = seedB;

            for (int i = 0; i < iterations; i++)
            {
                ulong a = Generate(prevA, FactorA, divisorA);
                ulong b = Generate(prevB, FactorB, divisorB);

                if ((a & 0xFFFF) == (b & 0xFFFF))
                {
                    count++;
                }

                prevA = a;
                prevB = b;
            }

            return count;
        }

        /// <summary>
        /// Generate the next number in the sequence which can be divided exactly by the divisor
        /// given the previous number and unique sequence factor 
        /// </summary>
        /// <param name="previous">Previous number</param>
        /// <param name="factor">Sequence factor</param>
        /// <param name="divisor">Divisor</param>
        /// <returns>Next number in the sequence that can be exactly divided by the divisor</returns>
        private static ulong Generate(ulong previous, ulong factor, ulong divisor)
        {
            ulong next = previous;

            do
            {
                next = (next * factor) % 2147483647;
            }
            while (next % divisor > 0);

            return next;
        }
    }
}
