using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solve for Day 10
    /// </summary>
    public class Day10
    {
        /// <summary>
        /// Perform the swap function and return the first 2 numbers multiplied
        /// </summary>
        /// <param name="size">Size of the input buffer</param>
        /// <param name="lengths">Lengths to use when hashing</param>
        /// <returns>First two numbers multiplied after hashing</returns>
        public int Part1(int size, int[] lengths)
        {
            int[] buffer = Enumerable.Range(0, size).ToArray();
            
            int position = 0;
            int skip = 0;

            this.Swap(buffer, lengths, ref position, ref skip);

            return buffer[0] * buffer[1];
        }

        /// <summary>
        /// Hash the given input
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Hex-encoded hash</returns>
        public string Part2(string input)
        {
            int[] buffer = Enumerable.Range(0, 256).ToArray();
            int[] lengths = input.Select(c => (int)c).ToArray();
            lengths = lengths.Concat(new[] { 17, 31, 73, 47, 23 }).ToArray();

            int position = 0;
            int skip = 0;

            // 64 rounds of swapping
            for (int i = 0; i < 64; i++)
            {
                this.Swap(buffer, lengths, ref position, ref skip);
            }

            // to hex
            int[] dense = this.Reduce(buffer);
            string hash = string.Join(string.Empty, dense.Select(d => d.ToString("x2")));

            return hash;
        }

        /// <summary>
        /// Swap segments of the buffer according to the supplied lengths, position and skip count
        /// </summary>
        /// <param name="buffer">Input buffer</param>
        /// <param name="lengths">Lengths to swap within the circular buffer</param>
        /// <param name="position">Start position (will be set to end position afterwards)</param>
        /// <param name="skip">Constantly incrementing skip count</param>
        public void Swap(int[] buffer, int[] lengths, ref int position, ref int skip)
        {
            foreach (int length in lengths)
            {
                var end = (position + length - 1) % buffer.Length;

                // reverse the slice, wrapping around the end
                for (int i = 0; i < length / 2; i++)
                {
                    int swapStart = (position + i) % buffer.Length;
                    int swapEnd = (end + buffer.Length - i) % buffer.Length;
                    int temp = buffer[swapStart];
                    buffer[swapStart] = buffer[swapEnd];
                    buffer[swapEnd] = temp;
                }

                position = (position + length + skip) % buffer.Length;
                skip++;
            }
        }

        /// <summary>
        /// Reduce a 256-element input array to a 16-element output array by XOR'ing groups of 16 elements
        /// </summary>
        /// <param name="input">Input array</param>
        /// <returns>Output array</returns>
        private int[] Reduce(int[] input)
        {
            int[] output = new int[16];

            for (int i = 0; i < 16; i++)
            {
                output[i] = 
                    input[i * 16 + 0] ^
                    input[i * 16 + 1] ^
                    input[i * 16 + 2] ^
                    input[i * 16 + 3] ^
                    input[i * 16 + 4] ^
                    input[i * 16 + 5] ^
                    input[i * 16 + 6] ^
                    input[i * 16 + 7] ^
                    input[i * 16 + 8] ^
                    input[i * 16 + 9] ^
                    input[i * 16 + 10] ^
                    input[i * 16 + 11] ^
                    input[i * 16 + 12] ^
                    input[i * 16 + 13] ^
                    input[i * 16 + 14] ^
                    input[i * 16 + 15];
            }

            return output;
        }
    }
}