using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AOC2023
{
    class day05 : AOCPuzzle
    {
        public day05() : base("5")
        {
            
        }

        public override void Part1(string[] input)
        {
            var seeds = Regex.Matches(input[0], @"(\d+)").Cast<Match>().Select(m => long.Parse(m.Value)).ToArray();

            for (var i = 0; i < input.Length; i++)
            {
                if (!input[i++].Contains("map")) continue;
                
                var maps = new List<long[]>();
                while (i < input.Length && !input[i].Equals(""))
                {
                    maps.Add(input[i++].Split(' ').Select(long.Parse).ToArray());
                }

                for (var j = 0; j < seeds.Length; j++)
                {
                    var map = maps.FirstOrDefault(m => m[1] <= seeds[j] && seeds[j] < m[1] + m[2]);
                    seeds[j] = map == null ? seeds[j] : map[0] + seeds[j] - map[1];
                }
            }
            PrintAnswer(seeds.Min());
        }


        public override void Part2(string[] input)
        {
            var seeds = Regex.Matches(input[0], @"(\d+)").Cast<Match>().Select(m => long.Parse(m.Value)).ToArray();
            Dictionary<long, long> seedRange = new Dictionary<long, long>();
            for (var i = 0; i < seeds.Length; i++)
            {
                seedRange.Add(seeds[i++], seeds[i]);
            }

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i++].Contains("map"))
                {
                    Dictionary<long, long> seedRangeCopy = new Dictionary<long, long>();
                    var maps = new List<long[]>();
                    while (i < input.Length && !input[i].Equals(""))
                    {
                        maps.Add(input[i++].Split(' ').Select(long.Parse).ToArray());
                    }

                    long[] seedStarts = seedRange.Keys.ToArray();
                    for (var j = 0; j < seedStarts.Length; j++)
                    {
                        long seedStart = seedStarts[j];
                        long range = seedRange[seedStart];

                        while (range != 0)
                        {
                            long[] map = maps.FirstOrDefault(m => m[1] <= seedStart && seedStart < m[1] + m[2]);
                            if (map == null)
                            {
                                List<long[]> list = maps.Where(m => seedStart <= m[1] && m[1] < seedStart + range)
                                    .ToList();
                                if (list.Count == 0)
                                {
                                    seedRangeCopy.Add(seedStart, range);
                                    break;
                                }

                                map = list.OrderBy(x => x[1]).First();
                                long rangeCopy = map[1] - seedStart;
                                range -= rangeCopy;
                                seedRangeCopy.Add(seedStart, rangeCopy);
                                seedStart += rangeCopy;
                            }
                            else
                            {
                                seedRangeCopy.Add(map[0] + seedStart - map[1],
                                    Math.Min(map[2] + map[1] - seedStart, range));
                                range -= Math.Min(map[2] + map[1] - seedStart, range);
                                seedStart = map[2] + map[1];
                            }
                        }
                    }

                    seedRange = seedRangeCopy;
                }
            }

            PrintAnswer(seedRange.Keys.Min());
        }
    }
}