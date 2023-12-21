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
        }

        private long helper(int[] x, int[] m, int[] a, int[] s, string currentFlow, Dictionary<string, string> flows)
        {
            if (x.Length == 0 || a.Length == 0 || a.Length == 0 || s.Length == 0) return 0;
            if (currentFlow.Equals("R")) return 0;
            if (currentFlow.Equals("A")) return (long)x.Length * m.Length * a.Length * s.Length;
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
                    if (r.Equals("x"))
                    {
                        answer += helper(x.Skip(x.ToList().IndexOf(value) + 1).ToArray(), m, a, s, c.Split(':')[1], flows);
                        x = x.Take(x.ToList().IndexOf(value) + 1).ToArray();
                    }

                    if (r.Equals("m"))
                    {
                        answer += helper(x, m.Skip(m.ToList().IndexOf(value) + 1).ToArray(), a, s, c.Split(':')[1], flows);
                        m = m.Take(m.ToList().IndexOf(value) + 1).ToArray();
                    }

                    if (r.Equals("a"))
                    {
                        answer += helper(x, m, a.Skip(a.ToList().IndexOf(value) + 1).ToArray(), s, c.Split(':')[1], flows);
                        a = a.Take(a.ToList().IndexOf(value) + 1).ToArray();
                    }

                    if (r.Equals("s"))
                    {
                        answer += helper(x, m, a, s.Skip(s.ToList().IndexOf(value) + 1).ToArray(), c.Split(':')[1], flows);
                        s = s.Take(s.ToList().IndexOf(value) + 1).ToArray();
                    }
                }
                else
                {
                    if (r.Equals("x"))
                    {
                        answer += helper(x.Take(x.ToList().IndexOf(value)).ToArray(), m, a, s, c.Split(':')[1], flows);
                        x = x.Skip(x.ToList().IndexOf(value)).ToArray();
                    }

                    if (r.Equals("m"))
                    {
                        answer += helper(x, m.Take(m.ToList().IndexOf(value)).ToArray(), a, s, c.Split(':')[1], flows);
                        m = m.Skip(m.ToList().IndexOf(value)).ToArray();
                    }

                    if (r.Equals("a"))
                    {
                        answer += helper(x, m, a.Take(a.ToList().IndexOf(value)).ToArray(), s, c.Split(':')[1], flows);
                        a = a.Skip(a.ToList().IndexOf(value)).ToArray();
                    }

                    if (r.Equals("s"))
                    {
                        answer += helper(x, m, a, s.Take(s.ToList().IndexOf(value)).ToArray(), c.Split(':')[1], flows);
                        s = s.Skip(s.ToList().IndexOf(value)).ToArray();
                    }
                }
            }
            return answer;
        }
    }
}