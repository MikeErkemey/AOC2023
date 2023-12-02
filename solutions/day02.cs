using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day02 : AOCPuzzle
    {
        
        public day02() : base("2")
        {
            
        }

        public override void Part1(string[] input)
        {
            
            Dictionary<string, int> rgbDict = new Dictionary<string, int> {{"red", 12},{"green", 13}, {"blue", 14}};
            int sumIDS = 0;
            foreach (string s in input)
            {
                string[] game = Regex.Split(s.Replace(" ", ""), @"(:|;|,)").Where(x => Regex.IsMatch(x, @"(\d)")).ToArray();
                
                for (int i = 1; i < game.Length; i++)
                {
                    string[] values = Regex.Split(game[i],@"(blue|green|red)");

                    if (int.Parse(values[0]) > rgbDict[values[1]])
                    {
                        game[0] = "0";
                        break; 
                    }
                }
                sumIDS += int.Parse(Regex.Match(game[0], @"(\d+)").ToString());
            }
            
            
            PrintAnswer(sumIDS);
        }


        public override void Part2(string[] input)
        {
            int sumCubes = 0;
            foreach (string s in input)
            {
                string[] game = Regex.Split(s.Replace(" ", ""), @"(:|;|,)").Where(x => Regex.IsMatch(x, @"(\d)")).ToArray();
                Dictionary<string, int> rgbDict = new Dictionary<string, int> {{"red", int.MinValue},{"green", int.MinValue}, {"blue", int.MinValue}};
                
                for (int i = 1; i < game.Length; i++)
                {
                    string[] values = Regex.Split(game[i],@"(blue|green|red)");
                    
                    rgbDict[values[1]] = Math.Max(rgbDict[values[1]], int.Parse(values[0]));
                }

                sumCubes += rgbDict.Values.Aggregate(1, (current, value) => current * value);
            }
            
            PrintAnswer(sumCubes);
        }
    }
}