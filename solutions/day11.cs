using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day11 : AOCPuzzle
    {
        public day11() : base("11")
        {
            
        }

        public override void Part1(string[] input)
        {
            long actualY = 0;

            var matrix = input.Select(s => s.ToCharArray()).ToArray();

            HashSet<Point2d> points = new HashSet<Point2d>();
            
            for (long y = 0; y < matrix.Length; y++)
            {
                long actualX = 0;
                if (matrix[y].Count(c => c == '#') == 0)
                {
                    actualY += 2;
                    continue;
                }
                for (long x = 0; x < matrix[y].Length; x++)
                {
                    if (matrix[y][x] == '#')
                    {
                        points.Add(new Point2d(actualX, actualY));
                    }
                    actualX += matrix.Count(c => c[x] == '#') == 0 ? 2 : 1;
                }
                actualY++;
            }
            //82000210

            long answer = 0;
            foreach (var point2d in points)
            {
                answer += points.Where(p => !p.Equals(point2d)).Sum(p => point2d.DistanceRoute(p));
            }
            
            
            PrintAnswer(answer/2);
        }


        public override void Part2(string[] input)
        {
            long times = 1000000;
            long actualY = 0;

            var matrix = input.Select(s => s.ToCharArray()).ToArray();

            HashSet<Point2d> points = new HashSet<Point2d>();
            
            for (long y = 0; y < matrix.Length; y++)
            {
                long actualX = 0;
                if (matrix[y].Count(c => c == '#') == 0)
                {
                    actualY += times;
                    continue;
                }
                for (long x = 0; x < matrix[y].Length; x++)
                {
                    if (matrix[y][x] == '#')
                    {
                        points.Add(new Point2d(actualX, actualY));
                    }
                    actualX += matrix.Count(c => c[x] == '#') == 0 ? times : 1;
                }
                actualY++;
            }
            
            long answer = 0;
            foreach (var point2d in points)
            {
                answer += points.Where(p => !p.Equals(point2d)).Sum(p => point2d.DistanceRoute(p));
            }
            
            
            PrintAnswer(answer/2);
        }
    }
}