using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day04 : AOCPuzzle
    {
        public day04() : base("4")
        {
            
        }

        public override void Part1(string[] input)
        {
            double answer = 0;
            foreach (String s in input)
            {
                string[] split = s.Split(new char[] { ':', '|' });
                // int cardNumber = int.Parse(Regex.Match(s, @"(\d+)").ToString());
                int[] winningNumbers = split[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                int[] ownNumbers = split[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                int matches = ownNumbers.Intersect(winningNumbers).ToArray().Length;
                answer += matches == 0 ? 0 : Math.Pow(2, matches - 1);
            }
            PrintAnswer(answer);
        }


        public override void Part2(string[] input)
        {
            int[] cardAmounts = Enumerable.Repeat(1, input.Length + 1).ToArray();
            foreach (String s in input)
            {
                string[] split = s.Split(new char[] { ':', '|' });
                int cardNumber = int.Parse(Regex.Match(s, @"(\d+)").ToString());
                int[] winningNumbers = split[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                int[] ownNumbers = split[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                int matches = ownNumbers.Intersect(winningNumbers).ToArray().Length;
                Enumerable.Range(cardNumber + 1, matches).ToList().ForEach(i => cardAmounts[i] += cardAmounts[cardNumber]);
            }
            PrintAnswer(cardAmounts.Skip(1).Sum());
        }
    }
}