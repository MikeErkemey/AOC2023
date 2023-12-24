using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day22 : AOCPuzzle
    {
        Dictionary<List<Point3d>, List<List<Point3d>>> below;
        Dictionary<List<Point3d>, List<List<Point3d>>> above;
        public day22() : base("22")
        {
            
        }

        public override void Part1(string[] input)
        {
            above = new Dictionary<List<Point3d>, List<List<Point3d>>>();
            below = new Dictionary<List<Point3d>, List<List<Point3d>>>();
            List<List<Point3d>> bricks = new List<List<Point3d>>();
            foreach (var s in input)
            {
                var split = s.Split('~');
                var b = split[0].Split(',').Select(int.Parse).ToArray();
                var e = split[1].Split(',').Select(int.Parse).ToArray();

                List<Point3d> brick = new List<Point3d>();
                for (int i = b[0]; i <= e[0]; i++)
                {
                    for (int j = b[1]; j <= e[1]; j++)
                    {
                        for (int k = b[2]; k <= e[2]; k++)
                        {
                            brick.Add(new Point3d(i,j,k));
                        }
                    }
                }
                bricks.Add(brick);
            }
            
            var set = new HashSet<Point3d>();
            foreach (var brick in bricks.OrderBy(l => l.Min(p => p.z)))
            {
                int zMax = 0;
                var pMax = set.Where(p1 => brick.Any(p2 => p1.x == p2.x && p1.y == p2.y)).OrderByDescending(p => p.z).FirstOrDefault();
               
                if (pMax != null)
                {
                    var pMin = brick.Where(p => pMax.x == p.x && pMax.y == p.y).OrderBy(p => p.z).First();
                    zMax = pMin.z - (pMax.z + 1);
                }
                else
                {
                    var pMin = brick.Min(p => p.z);
                    zMax = pMin - 1;
                }
                
                foreach (var point3d in brick)
                {
                    point3d.z -= zMax;
                    set.Add(point3d);
                }
            }
            
            
            foreach (var b1 in bricks)
            {
                above.Add(b1, new List<List<Point3d>>());
                below.Add(b1, new List<List<Point3d>>());
                foreach (var b2 in bricks)
                {
                    if(b1 == b2) continue;
                    if (b2.Any(p2 => b1.Any(p1 => p2.x == p1.x && p2.y == p1.y && p1.z + 1 == p2.z)))
                    {
                        above[b1].Add(b2);
                    }
                    if (b2.Any(p2 => b1.Any(p1 => p2.x == p1.x && p2.y == p1.y && p1.z - 1 == p2.z)))
                    {
                        below[b1].Add(b2);
                    }
                }
            }

            int answer = 0;
            foreach (var aboveKey in above.Keys)
            {
                var list = above[aboveKey];
                
                if(list.All(p => below[p].Count > 1)) answer++;
            }

            PrintAnswer(answer);
        }


        public override void Part2(string[] input)
        {
            above = new Dictionary<List<Point3d>, List<List<Point3d>>>();
            below = new Dictionary<List<Point3d>, List<List<Point3d>>>();
            List<List<Point3d>> bricks = new List<List<Point3d>>();
            foreach (var s in input)
            {
                var split = s.Split('~');
                var b = split[0].Split(',').Select(int.Parse).ToArray();
                var e = split[1].Split(',').Select(int.Parse).ToArray();

                List<Point3d> brick = new List<Point3d>();
                for (int i = b[0]; i <= e[0]; i++)
                {
                    for (int j = b[1]; j <= e[1]; j++)
                    {
                        for (int k = b[2]; k <= e[2]; k++)
                        {
                            brick.Add(new Point3d(i,j,k));
                        }
                    }
                }
                bricks.Add(brick);
            }
            
            var set = new HashSet<Point3d>();
            foreach (var brick in bricks.OrderBy(l => l.Min(p => p.z)))
            {
                int zMax = 0;
                var pMax = set.Where(p1 => brick.Any(p2 => p1.x == p2.x && p1.y == p2.y)).OrderByDescending(p => p.z).FirstOrDefault();
               
                if (pMax != null)
                {
                    var pMin = brick.Where(p => pMax.x == p.x && pMax.y == p.y).OrderBy(p => p.z).First();
                    zMax = pMin.z - (pMax.z + 1);
                }
                else
                {
                    var pMin = brick.Min(p => p.z);
                    zMax = pMin - 1;
                }
                
                foreach (var point3d in brick)
                {
                    point3d.z -= zMax;
                    set.Add(point3d);
                }
            }
            
            
            foreach (var b1 in bricks)
            {
                above.Add(b1, new List<List<Point3d>>());
                below.Add(b1, new List<List<Point3d>>());
                foreach (var b2 in bricks)
                {
                    if(b1 == b2) continue;
                    if (b2.Any(p2 => b1.Any(p1 => p2.x == p1.x && p2.y == p1.y && p1.z + 1 == p2.z)))
                    {
                        above[b1].Add(b2);
                    }
                    if (b2.Any(p2 => b1.Any(p1 => p2.x == p1.x && p2.y == p1.y && p1.z - 1 == p2.z)))
                    {
                        below[b1].Add(b2);
                    }
                }
            }

            int answer = 0;
            foreach (var aboveKey in above.Keys)
            {
                answer += helper(aboveKey);
            }

            PrintAnswer(answer);
        }

        public int helper(List<Point3d> start)
        {
            List<List<Point3d>> hasFallen = new List<List<Point3d>>();
            Queue<List<Point3d>> q = new Queue<List<Point3d>>();
            q.Enqueue(start);
            int answer = 0;
            List<Point3d> cur = start;
            while (q.Count != 0)
            {
                cur = q.Dequeue();
                if(hasFallen.Contains(cur)) continue;
                if(below[cur].Count != 0 && cur != start)
                {
                    var und = below[cur];
                    if (und.Any(p => !hasFallen.Contains(p))) continue;
                    answer++;
                }
                hasFallen.Add(cur);
                var connected = above[cur];
                foreach (var p in connected)
                {
                    q.Enqueue(p);
                }
            }

            return answer;


        }
        //97198 too low
    }
}