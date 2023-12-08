using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day07 : AOCPuzzle
    {
        public day07() : base("7")
        {
            
        }

        public override void Part1(string[] input)
        {
            List<Char> order = new List<Char> { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
            
            pokerCards = pokerCards.OrderBy(p => p[0].Select(c => p[0].Count(x => x == c)).Max())
                .ThenByDescending(p =>  p[0].Select(c => p[0].Count(x => x == c)).Count(x => x == 1))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[0]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[1]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[2]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[3]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[4]).ToString()))
                .ToList();

            PrintAnswer(pokerCards.Sum(p => (pokerCards.IndexOf(p) + 1) * int.Parse(p[1])));
        }


        public override void Part2(string[] input)
        {
            List<List<String>> pokerCards = input.Select(s => s.Split(' ').ToList()).ToList();
            List<Char> order = new List<Char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };
            
            pokerCards = pokerCards
                .OrderBy(p => p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Max() + p[0].Count(c => c == 'J'))
                .ThenBy(p => p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Count(x => x == 2) == 4 ||
                             (p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Count(x => x == 3) == 3 &&
                              p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Count(x => x == 2) == 2)
                             )
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[0]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[1]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[2]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[3]).ToString()))
                .ThenByDescending(p => int.Parse(order.IndexOf(p[0].ToCharArray()[4]).ToString()))
                .ToList();

            PrintAnswer(pokerCards.Sum(p => (pokerCards.IndexOf(p) + 1) * int.Parse(p[1])));
        }
    }
}