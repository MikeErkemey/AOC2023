using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day06 : AOCPuzzle
    {
        public day06() : base("6")
        {
            
        }

        public override void Part1(string[] input)
        {
            int[] times =  Regex.Matches(input[0], @"(\d+)").Cast<Match>().Select(m => int.Parse(m.Value)).ToArray();
            int[] distances =  Regex.Matches(input[1], @"(\d+)").Cast<Match>().Select(m => int.Parse(m.Value)).ToArray();

            int answer = 1;
            
            for (int i = 0; i < times.Length; i++)
            {
                int time = times[i];
                int wins = 0;

                for (int j = 1; j < time; j++)
                {
                    if (j * (time - j) > distances[i])
                    {
                        wins++;
                    }
                }

                answer *= wins;
            }
            
            PrintAnswer(answer);
        }


        public override void Part2(string[] input)
        {
            long time = long.Parse(Regex.Match(input[0].Replace(" ", ""), @"(\d+)").Value);
            long distance = long.Parse(Regex.Match(input[1].Replace(" ", ""), @"(\d+)").Value);
            long start = 0;
            long end = 0;
                
            for (long j = 1; j < time; j++)
            {
                if (j * (time - j) > distance)
                {
                    start = j;
                    break;
                }
            }
            
            for (long j = time - 1; j >= start; j--)
            {
                if (j * (time - j) > distance)
                {
                    end = j;
                    break;
                }
            }
            
            
            PrintAnswer(end - start + 1);
        }
    }
}