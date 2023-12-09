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
            PrintAnswer(input.Select(s => s.Split(' ').ToList())
                .OrderBy(p => p[0].Select(c => p[0].Count(x => x == c)).Max())
                .ThenByDescending(p =>  p[0].Select(c => p[0].Count(x => x == c)).Count(x => x == 1))
                .ThenByDescending(p => p[0].Select((c, i) => new List<char> { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' }.IndexOf(c) * Math.Pow(14, 4 - i)).Sum())
                .Select((x, y) => (y + 1) * int.Parse(x[1])).Sum());
        }


        public override void Part2(string[] input)
        {
            PrintAnswer(input.Select(s => s.Split(' ').ToList())
                .OrderBy(p => p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Max() + p[0].Count(c => c == 'J'))
                .ThenBy(p => p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Count(x => x == 2) == 4 ||
                             (p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Count(x => x == 3) == 3 &&
                              p[0].Select(c => c == 'J' ? 0 : p[0].Count(x => x == c)).Count(x => x == 2) == 2))
                .ThenByDescending(p => p[0].Select((c, i) => new List<char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' }.IndexOf(c) * Math.Pow(14, 4 - i)).Sum())
                .Select((x, y) => (y + 1) * int.Parse(x[1])).Sum());
        }
    }
}