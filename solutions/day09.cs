using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day09 : AOCPuzzle
    {
        public day09() : base("9")
        {
            
        }

        public override void Part1(string[] input)
        {
            List<List<int>> historyList = input.Select(s => s.Split(' ').Select(int.Parse).ToList()).ToList();
            PrintAnswer(historyList.Select(HelperLast).Sum());
        }

        private int HelperLast(List<int> history)
        {
            if (history.All(x => x == 0))
            {
                return history.Last();
            }

            return history.Last() + HelperLast(history.Select((x, y) => x - history[Math.Max(0, y - 1)]).ToList().Skip(1).ToList());
        }


        public override void Part2(string[] input)
        {
            List<List<int>> historyList = input.Select(s => s.Split(' ').Select(int.Parse).ToList()).ToList();
            PrintAnswer(historyList.Select(HelperFirst).Sum());
        }
        
        private int HelperFirst(List<int> history)
        {
            if (history.All(x => x == 0))
            {
                return history.First();
            }

            return history.First() - HelperFirst(history.Select((x, y) => x - history[Math.Max(0, y - 1)]).ToList().Skip(1).ToList());
        }
        
    }
}