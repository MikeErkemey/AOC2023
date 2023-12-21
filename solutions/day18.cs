using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AOC2023
{
    class day18 : AOCPuzzle
    {
        public day18() : base("18")
        {
            
        }

        public override void Part1(string[] input)
        {
            var split = input.Select(s => s.Split(' ')).ToList();
            var list = new HashSet<Point2d>();
            int x = 0;
            int y = 0;
            foreach (var strings in split)
            {
                int startX = x;
                int startY = y;
                if (strings[0].Equals("D")) y += int.Parse(strings[1]);
                else if (strings[0].Equals("U")) y -= int.Parse(strings[1]);
                else if (strings[0].Equals("R")) x += int.Parse(strings[1]);
                else if (strings[0].Equals("L")) x -= int.Parse(strings[1]);

                for (int i = Math.Min(startX, x); i <= Math.Max(startX, x); i++)
                {
                    list.Add(new Point2d(i, y));
                }

                for (int i = Math.Min(startY, y); i <= Math.Max(startY, y); i++)
                {
                    list.Add(new Point2d(x, i));
                }
            }
            
            int counter = 0;
            for (long i = list.Min(p => p.y); i <= list.Max(p => p.y); i++)
            {
                bool inside = false;
                for (long j = list.Min(p => p.x); j <= list.Max(p => p.x); j++)
                {
                    if (list.Contains(new Point2d(j, i)))
                    {
                        if (list.Contains(new Point2d(j, i - 1)) && list.Contains(new Point2d(j, i + 1)))
                        {
                            inside = !inside;
                        }
                        else if (list.Contains(new Point2d(j, i + 1)) && list.Contains(new Point2d(j + 1, i)))
                        {
                            inside = !inside;
                        }
                        else if (list.Contains(new Point2d(j, i + 1)) && list.Contains(new Point2d(j - 1, i)))
                        {
                            inside = !inside;
                        }
                        counter++;
                    }
                    else if (inside)
                    {
                        counter++;
                    }
                }
            }


            PrintAnswer(counter);
        }


        public override void Part2(string[] input)
        {
            
            var split = input.Select(s => s.Replace("(#", String.Empty).Replace(")", String.Empty ))
                .Select(s => s.Split(' '))
                .ToList();
            var set = new HashSet<Point2d>();
            long x = 0;
            long y = 0;
            long perim = 0;
            foreach (var strings in split)
            {
                long length = Convert.ToInt64(new String(strings[2].Take(5).ToArray()), 16);

                perim += length;
                
                if (strings[2][5] == '0') x += length; //R
                else if (strings[2][5] == '1') y += length; //D
                else if (strings[2][5] == '2') x -= length; //L
                else if (strings[2][5] == '3') y -= length; //U
                
                set.Add(new Point2d(x, y));
            }

            List<Point2d> points = set.ToList();
            long area = 0;
            for (var i = 0; i < points.Count() - 1; i++)
            {
                var p = points[i];
                var p2 = points[i + 1];
                var sum = (p2.x + p.x) * (p2.y - p.y);
                area += sum;
            }
            
            PrintAnswer(Math.Floor((Math.Abs(area) + perim)/(double)2) + 1);
        }
    }
}