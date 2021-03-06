﻿using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 4
    /// </summary>
    public class Day04
    {
        /// <summary>
        /// A new system policy has been put in place that requires all accounts to use a passphrase instead of
        /// simply a password. A passphrase consists of a series of words (lowercase letters) separated by spaces.
        /// 
        /// To ensure security, a valid passphrase must contain no duplicate words.
        /// 
        /// For example:
        /// 
        /// - aa bb cc dd ee is valid.
        /// - aa bb cc dd aa is not valid - the word aa appears more than once.
        /// - aa bb cc dd aaa is valid - aa and aaa count as different words.
        /// 
        /// The system's full passphrase list is available as your puzzle input. How many passphrases are valid?
        /// </summary>
        /// <returns>Count of valid passphrases</returns>
        public static int Part1()
        {
            string[] lines = File.ReadAllLines("inputs/day04.txt");

            var valid = lines.Select(line => line.Split(' ').ToArray())
                             .Where(words => words.Length == words.Distinct().Count());

            return valid.Count();
        }

        /// <summary>
        /// For added security, yet another system policy has been put in place. Now, a valid passphrase must contain no
        /// two words that are anagrams of each other - that is, a passphrase is invalid if any word's letters can be
        /// rearranged to form any other word in the passphrase.
        /// 
        /// For example:
        /// 
        /// - abcde fghij is a valid passphrase.
        /// - abcde xyz ecdab is not valid - the letters from the third word can be rearranged to form the first word.
        /// - a ab abc abd abf abj is a valid passphrase, because all letters need to be used when forming another word.
        /// - iiii oiii ooii oooi oooo is valid.
        /// - oiii ioii iioi iiio is not valid - any of these words can be rearranged to form any other word.
        /// 
        /// Under this new system policy, how many passphrases are valid?
        /// </summary>
        /// <returns>Count of valid passphrases</returns>
        public static int Part2()
        {
            string[] lines = File.ReadAllLines("inputs/day04.txt");

            var valid = lines.Select(line => line.Split(' '))
                             // sort each word alphabetically to check for anagrams
                             .Select(words => words.Select(w => Sorted(w)).ToArray())
                             .Where(words => words.Length == words.Distinct().Count());

            return valid.Count();
        }

        /// <summary>
        /// Sort the characters in a string
        /// </summary>
        /// <param name="input">String to sort</param>
        /// <returns>Sorted string</returns>
        /// <example>
        /// string foo = "abfedc";
        /// string sorted = Sorted(foo);
        /// Console.WriteLine(sorted); /// abcdef
        /// </example>
        private static string Sorted(string input)
        {
            char[] c = input.ToArray();
            Array.Sort(c);
            return new string(c);
        }
    }
}
