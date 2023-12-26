using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day23 : AOCPuzzle
    {
        public day23() : base("23")
        {
            
        }

        Dictionary<char, int[]> n = new Dictionary<char, int[]>
        {
            { 'v', new[] { 0, 1 } },
            { '^',new[] { 0, -1 } },
            { '>',new[] { 1, 0 } },
            { '<',new[] { -1, 0 } },
        };
        
        public override void Part1(string[] input)
        {
            var matrix = input.Select(s => s.ToCharArray()).ToArray();
            var numM = input.Select(s => s.Select(l => 0).ToArray()).ToArray();
            
            Point2d start = new Point2d(1, 0);
            Point2d end = new Point2d(matrix[0].Length - 2, matrix.Length - 1);

            Queue<Point2d> q = new Queue<Point2d>();
            q.Enqueue(start);
            while(q.Count != 0)
            {
                Point2d cur = q.Dequeue();
                long x = cur.x;
                long y = cur.y;
                char c = matrix[y][x];

                if (n.ContainsKey(c))
                {
                    int[] dir = n[c];
                    Point2d o = new Point2d(x + dir[0], y + dir[1], cur);
                    if(numM[o.y][o.x] > numM[cur.y][cur.x] + 1) continue;
                    if(o.Equals(cur.prev)) continue;
                    numM[o.y][o.x] = numM[cur.y][cur.x] + 1;
                    q.Enqueue(o);
                }
                else
                {
                    foreach (var dir in n.Values)
                    {
                        Point2d o = new Point2d(x + dir[0], y + dir[1], cur);
                        if(o.x < 0 || o.y < 0 || o.y >= matrix.Length || o.x >= matrix[o.y].Length) continue;
                        if(matrix[o.y][o.x] == '#') continue;
                        if(numM[o.y][o.x] >= numM[cur.y][cur.x] + 1) continue;
                        if(o.Equals(cur.prev)) continue;
                        numM[o.y][o.x] = numM[cur.y][cur.x] + 1;
                        q.Enqueue(o);
                    }
                }
            }
            
            
            
            PrintAnswer(numM[end.y][end.x]);
        }


        public override void Part2(string[] input)
        {
            Dictionary<Point2d, List<Point2d>> dict = new Dictionary<Point2d, List<Point2d>>();
            var matrix = input.Select(s => s.ToCharArray()).ToArray();
            var numM = input.Select(s => s.Select(l => 0).ToArray()).ToArray();
            
                        
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == '#') continue;

                    List<Point2d> nb = new List<Point2d>();
                    
                    foreach (var nd in n.Values)
                    {
                        
                        Point2d o = new Point2d(i + nd[1], j + nd[0]);
                        if(o.x < 0 || o.y < 0 || o.y >= matrix.Length || o.x >= matrix[o.y].Length) continue;
                        if (matrix[i + nd[1]][j + nd[0]] != '.') nb.Add(new Point2d(j + nd[0], i +nd[1]));
                    }
                    if(nb.Count >= 3) dict.Add(new Point2d(j,i), nb);
                }
            }
            
            Point2d start = new Point2d(1, 0, new List<Point2d>());
            Point2d end = new Point2d(matrix[0].Length - 2, matrix.Length - 1);

            Stack<Point2d> stack = new Stack<Point2d>();
            stack.Push(start);
            while(stack.Count != 0)
            {
                Point2d cur = stack.Pop();
                long x = cur.x;
                long y = cur.y;
                char c = matrix[y][x];
                
                foreach (var dir in n.Values)
                {
                    Point2d o = new Point2d(x + dir[0], y + dir[1], cur);
                    if(o.x < 0 || o.y < 0 || o.y >= matrix.Length || o.x >= matrix[y].Length) continue;
                    if(matrix[o.y][o.x] == '#') continue;
                    if(numM[o.y][o.x] >= numM[cur.y][cur.x] + 1) continue;
                    if(o.Equals(cur.prev)) continue;
                    numM[o.y][o.x] = numM[cur.y][cur.x] + 1;
                    stack.Push(o);
                }
            }
            
            
            PrintAnswer("");
        }

    }
}