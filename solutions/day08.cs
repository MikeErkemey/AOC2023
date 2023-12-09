using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day08 : AOCPuzzle
    {
        public day08() : base("8")
        {
            
        }

        public override void Part1(string[] input)
        {
            char[] directions  = input.First().ToCharArray();
            
            Dictionary<String, String[]> path = new Dictionary<String, String[]>();
            
            foreach (var s in input.Skip(2))
            {
                String[] split = s.Split('=');
                path.Add(split[0].Trim(), Regex.Replace(split[1],@"(\s|\(|\))", "").Split(','));
            }

            String current = "AAA";
            String final = "ZZZ";
            int answer = 0;
            
            while (!current.Equals(final))
            {
                current = directions[answer++ % directions.Length] == 'L' ? path[current][0] : path[current][1];
            }
            
            PrintAnswer(answer);
        }


        public override void Part2(string[] input)
        {
            char[] directions  = input.First().ToCharArray();
            
            Dictionary<String, String[]> path = new Dictionary<String, String[]>();
            
            foreach (var s in input.Skip(2))
            {
                String[] split = s.Split('=');
                path.Add(split[0].Trim(), Regex.Replace(split[1],@"(\s|\(|\))", "").Split(','));
            }

            List<String> nodes = path.Keys.Where(s => Regex.IsMatch(s, @"A$")).ToList();
            List<List<int>> nodeLoops = new List<List<int>>();
            foreach (var node in nodes)
            {
                List<int> list = new List<int>();
                int step = 0;
                Dictionary<String, List<int>> dict= new Dictionary<String,List<int>>{ {node, new List<int>{ step}}};
                var current = directions[step++ % directions.Length] == 'L' ? path[node][0] : path[node][1];
                while (!dict.ContainsKey(current) || !dict[current].Contains(step%directions.Length))
                {
                    if (!dict.ContainsKey(current))
                    {
                        dict.Add(current, new List<int>());
                    }
                    
                    dict[current].Add(step%directions.Length);
                    current = directions[step++ % directions.Length] == 'L' ? path[current][0] : path[current][1];

                    if (!(!dict.ContainsKey(current) || !dict[current].Contains(step % directions.Length)))
                    {
                        list.Add(step);
                        list.Add(step % directions.Length);
                    }
                }
                nodeLoops.Add(list);
            }

            long lcm = LCM(nodeLoops.Select(l => long.Parse((l[0] - l[1]).ToString())).ToList());

            PrintAnswer(lcm);
        }

        private long LCM(List<long> numbers)
        {
            return numbers.Aggregate((x, y) => x * y / GCD(x, y));
        }

        private long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}