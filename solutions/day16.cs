using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace AOC2023
{
    class day16 : AOCPuzzle
    {
        private Dictionary<char, long[]> moveDict = new Dictionary<char, long[]>
        {
            { 'U', new long[] { -1, 0 } },
            { 'D', new long[] { 1, 0 } },
            { 'L', new long[] { 0, -1 } },
            { 'R', new long[] { 0, 1 } }
        };

        private Dictionary<Point2d, List<char>> dict = new Dictionary<Point2d, List<char>>();

        public day16() : base("16")
        {

        }

        public override void Part1(string[] input)
        {
            var matrix = input.Select(s => s.ToCharArray()).ToArray();
            Energize(new Point2d(0, 0, 'R'), matrix);
            PrintAnswer(dict.Keys.Count);
        }
        
        public override void Part2(string[] input)
        {
            var matrix = input.Select(s => s.ToCharArray()).ToArray();

            int answer = 0;
            
            for (var j = 0; j < matrix[0].Length; j++)
            {
                if (j != 0) answer = Math.Max(answer, Energize(new Point2d(j, 0, 'L'), matrix));
                if (j == matrix[0].Length - 1) answer = Math.Max(answer, Energize(new Point2d(j, 0, 'R'), matrix));
                answer = Math.Max(answer, Energize(new Point2d(j, 0, 'D'), matrix));
            }
            
            for (var j = 0; j < matrix[0].Length; j++)
            {
                if (j != 0) answer = Math.Max(answer, Energize(new Point2d(j, matrix.Length - 1, 'L'), matrix));
                if (j == matrix[0].Length - 1) answer = Math.Max(answer, Energize(new Point2d(j, matrix.Length - 1, 'R'), matrix));
                answer = Math.Max(answer, Energize(new Point2d(j, matrix.Length - 1, 'U'), matrix));
            }
            
            for (var j = 0; j < matrix.Length; j++)
            {
                if (j != 0) answer = Math.Max(answer, Energize(new Point2d(0, j, 'D'), matrix));
                if (j == matrix[0].Length - 1) answer = Math.Max(answer, Energize(new Point2d(0, j, 'U'), matrix));
                answer = Math.Max(answer, Energize(new Point2d(0, j, 'R'), matrix));
            }

            for (var j = 0; j < matrix.Length; j++)
            {
                if (j != 0) answer = Math.Max(answer, Energize(new Point2d(matrix[0].Length - 1, j, 'D'), matrix));
                if (j == matrix[0].Length - 1) answer = Math.Max(answer, Energize(new Point2d(matrix[0].Length - 1, j, 'U'), matrix));
                answer = Math.Max(answer, Energize(new Point2d(matrix[0].Length - 1, j, 'L'), matrix));
            }
            PrintAnswer(answer);
        }
        
        
        private int Energize(Point2d start, char[][] matrix)
        {
            dict = new Dictionary<Point2d, List<char>>();
            Stack<Point2d> point2ds = new Stack<Point2d>();
            point2ds.Push(start);
            long x = start.x;
            long y = start.y;
            while (point2ds.Count != 0)
            {
                Point2d current = point2ds.Pop();
                x = current.x;
                y = current.y;

                if (x < 0 || y < 0 || y >= matrix.Length || x >= matrix[y].Length) continue;
                if (dict.ContainsKey(current) && dict[current].Contains(current.move)) continue;
                if (!dict.ContainsKey(current)) dict.Add(current, new List<char>());
                dict[current].Add(current.move);
                switch (matrix[y][x])
                {
                    case '/':
                        if (current.move == 'U')
                            point2ds.Push(new Point2d(x + moveDict['R'][1], y + moveDict['R'][0], 'R'));
                        else if (current.move == 'D')
                            point2ds.Push(new Point2d(x + moveDict['L'][1], y + moveDict['L'][0], 'L'));
                        else if (current.move == 'L')
                            point2ds.Push(new Point2d(x + moveDict['D'][1], y + moveDict['D'][0], 'D'));
                        else if (current.move == 'R')
                            point2ds.Push(new Point2d(x + moveDict['U'][1], y + moveDict['U'][0], 'U'));
                        break;
                    case '\\':
                        if (current.move == 'U')
                            point2ds.Push(new Point2d(x + moveDict['L'][1], y + moveDict['L'][0], 'L'));
                        else if (current.move == 'D')
                            point2ds.Push(new Point2d(x + moveDict['R'][1], y + moveDict['R'][0], 'R'));
                        else if (current.move == 'L')
                            point2ds.Push(new Point2d(x + moveDict['U'][1], y + moveDict['U'][0], 'U'));
                        else if (current.move == 'R')
                            point2ds.Push(new Point2d(x + moveDict['D'][1], y + moveDict['D'][0], 'D'));
                        break;
                    case '-':
                        if (current.move == 'U' || current.move == 'D')
                        {
                            point2ds.Push(new Point2d(x + moveDict['L'][1], y + moveDict['L'][0], 'L'));
                            point2ds.Push(new Point2d(x + moveDict['R'][1], y + moveDict['R'][0], 'R'));
                        }
                        else
                        {
                            point2ds.Push(new Point2d(x + moveDict[current.move][1], y + moveDict[current.move][0],
                                current.move));
                        }

                        break;
                    case '|':
                        if (current.move == 'L' || current.move == 'R')
                        {
                            point2ds.Push(new Point2d(x + moveDict['U'][1], y + moveDict['U'][0], 'U'));
                            point2ds.Push(new Point2d(x + moveDict['D'][1], y + moveDict['D'][0], 'D'));
                        }
                        else
                        {
                            point2ds.Push(new Point2d(x + moveDict[current.move][1], y + moveDict[current.move][0],
                                current.move));
                        }

                        break;
                    case '.':
                        point2ds.Push(new Point2d(x + moveDict[current.move][1], y + moveDict[current.move][0],
                            current.move));
                        break;
                }
            }

            return dict.Keys.Count;
        }
    }
}