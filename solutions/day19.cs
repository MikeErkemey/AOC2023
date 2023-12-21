using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day19 : AOCPuzzle
    {
        public day19() : base("19")
        {
            
        }

        public override void Part1(string[] input)
        {
            int split = input.ToList().IndexOf("");
            var flows = input.ToList().Take(split).ToDictionary(x => x.Split('{')[0],x => x.Split('{')[1].Replace("}","") );
            var ratingsList = input.ToList().Skip(split+1).ToList();
            int answer = 0;
            foreach (var rating in ratingsList)
            {
                string currentFlow = "in";
                var ratings =  Regex.Matches(rating, @"(\d+)").Cast<Match>().Select(m => m.Value).ToArray();
                
                Dictionary<string, int> dict = new Dictionary<string, int>
                {
                    {"x",int.Parse(ratings[0])},
                    {"m",int.Parse(ratings[1])},
                    {"a",int.Parse(ratings[2])},
                    {"s",int.Parse(ratings[3])}
                };
                
                while (!currentFlow.Equals("R") && !currentFlow.Equals("A"))
                {
                    var checks = flows[currentFlow].Split(',');
                    foreach (var s in checks)
                    {
                        string r = Regex.Match(s,@"[a-zA-Z]+").Value;
                        string check = Regex.Match(s,@"(<|>)").Value;
                        if (!dict.Keys.Contains(r))
                        {
                            currentFlow = r.ToString();
                            break;
                        }

                        int value = int.Parse(Regex.Match(s, @"(\d+)").Value);
                        if (check.Equals(">"))
                        {
                            if (dict[r] > value)
                            {
                                currentFlow = s.Split(':')[1];
                                break;
                            }
                        }
                        else if (dict[r] < value)
                        {
                            currentFlow = s.Split(':')[1];
                            break;
                        }
                    }
                }

                if (currentFlow.Equals("A"))
                {
                    answer += dict.Values.Sum();
                }
            }
            
            PrintAnswer(answer);
        }


        public override void Part2(string[] input)
        {
            int split = input.ToList().IndexOf("");
            var flows = input.ToList().Take(split).ToDictionary(x => x.Split('{')[0],x => x.Split('{')[1].Replace("}","") );

            long answer = helper(Enumerable.Range(1, 4000).ToArray(),Enumerable.Range(1, 4000).ToArray(),Enumerable.Range(1, 4000).ToArray(),Enumerable.Range(1, 4000).ToArray(), "in", flows);
            
            PrintAnswer(answer);
            
            //287106896578654
            PrintAnswer(167409079868000);
        }

        private long helper(int[] x, int[] m, int[] a, int[] s, string currentFlow, Dictionary<string, string> flows)
        {
            if (x.Length == 0 || a.Length == 0 || a.Length == 0 || s.Length == 0) return 0;
            if (currentFlow.Equals("R")) return 0;
            if (currentFlow.Equals("A"))
            {
                
                Console.WriteLine(x.Length + ", " + m.Length + ", " + a.Length + ", " + s.Length);
                return (long)x.Length * m.Length * a.Length * s.Length;
            }
            long answer = 0;
            var checks = flows[currentFlow].Split(',');
            Dictionary<string, int[]> dict = new Dictionary<string, int[]> {{"x",x}, {"m",m}, {"a",a}, {"s",s} };
            foreach (var c in checks)
            {
                string r = Regex.Match(c,@"[a-zA-Z]+").Value;
                string check = Regex.Match(c,@"(<|>)").Value;
                if (!new []{"x","m","a","s"}.Contains(r))
                {
                    answer += helper(x, m, a, s, r, flows);
                    continue;
                }
                int value = int.Parse(Regex.Match(c, @"(\d+)").Value);
                var arr = dict[r];
                if (check.Equals(">"))
                {
                    var split = arr.Skip(arr.ToList().IndexOf(value) + 1).ToArray();
                    if (r.Equals("x"))
                    {
                        answer += helper(split, m, a, s, c.Split(':')[1], flows);
                        x = arr.Take(arr.ToList().IndexOf(value) + 1).ToArray();
                    }

                    if (r.Equals("m"))
                    {
                        answer += helper(x, split, a, s, c.Split(':')[1], flows);
                        m = arr.Take(arr.ToList().IndexOf(value) + 1).ToArray();
                    }

                    if (r.Equals("a"))
                    {
                        answer += helper(x, m, split, s, c.Split(':')[1], flows);
                        a = arr.Take(arr.ToList().IndexOf(value) + 1).ToArray();
                    }

                    if (r.Equals("s"))
                    {
                        answer += helper(x, m, a, split, c.Split(':')[1], flows);
                        s = arr.Take(arr.ToList().IndexOf(value) + 1).ToArray();
                    }
                }
                else
                {
                    var split = arr.Take(arr.ToList().IndexOf(value)).ToArray();
                    if (r.Equals("x"))
                    {
                        answer += helper(split, m, a, s, c.Split(':')[1], flows);
                        x = arr.Skip(arr.ToList().IndexOf(value)).ToArray();
                    }

                    if (r.Equals("m"))
                    {
                        answer += helper(x, split, a, s, c.Split(':')[1], flows);
                        m = arr.Skip(arr.ToList().IndexOf(value)).ToArray();
                    }

                    if (r.Equals("a"))
                    {
                        answer += helper(x, m, split, s, c.Split(':')[1], flows);
                        a = arr.Skip(arr.ToList().IndexOf(value)).ToArray();
                    }

                    if (r.Equals("s"))
                    {
                        answer += helper(x, m, a, split, c.Split(':')[1], flows);
                        s = arr.Skip(arr.ToList().IndexOf(value)).ToArray();
                    }
                }
            }
            return answer;
        }
    }
}