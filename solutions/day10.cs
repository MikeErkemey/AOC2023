using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day10 : AOCPuzzle
    {
        private readonly Dictionary<char, int[]> _up = new Dictionary<char, int[]> {
            { '|', new[] { -1, 0, 'U'} },
            { '7', new[] { 0, -1, 'L' } },
            { 'F', new[] { 0, 1,  'R' } },
            {'S', new[] { -1, 0,  'U' } }
        };
        
        private readonly Dictionary<char, int[]> _down = new Dictionary<char, int[]>
        {
            { '|', new[] { 1, 0, 'D' } },
            { 'J', new[] { 0, -1, 'L' } },
            { 'L', new[] { 0, 1, 'R' } },
            {'S', new[] { 1, 0,  'D' } }
        };
        private readonly Dictionary<char, int[]> _left = new Dictionary<char, int[]>
        {
            { '-', new[] { 0, -1, 'L' } },
            { 'L', new[] { -1, 0, 'U'} },
            { 'F', new[] { 1, 0, 'D' } },
            {'S', new[] { 0, -1,  'L' } }
        };
        private readonly Dictionary<char, int[]> _right = new Dictionary<char, int[]>
        {
            { '-', new[] { 0, 1, 'R' } },
            { 'J', new[] { -1, 0, 'U'  } },
            { '7', new[] { 1, 0, 'D' } },
            {'S', new[] { 0, 1,  'R' } }
        };

        
        public day10() : base("10")
        {
            
        }

        public override void Part1(string[] input)
        {
            var matrix = input.Select(c => c.ToCharArray()).ToArray();
            
            Dictionary<int, Dictionary<char, int[]>> movements = new Dictionary<int, Dictionary<char, int[]>>()
            {
                { 'R', _right },
                { 'L', _left },
                { 'U', _up },
                { 'D', _down }
            };
            
            int y = matrix.Select((c, i) => c.Contains('S') ? i : -1).Max();
            int x = Array.IndexOf(matrix[y], 'S');
            int[] coords = { y, x, 'S' };

            if (_up.ContainsKey(matrix[Math.Max(0, coords[0] - 1)][coords[1]]) && matrix[Math.Max(0, coords[0] - 1)][coords[1]] != 'S') coords[2] = 'U';
            else if (_down.ContainsKey(matrix[0][Math.Max(0, coords[1] - 1)]) && matrix[0][Math.Max(0, coords[1] - 1)] != 'S' ) coords[2] = 'L';
            else coords[2] = 'D';
            
            int step = 0;
            while (step == 0 || matrix[coords[0]][coords[1]] != 'S')
            {
                direction(matrix, coords, movements[coords[2]]);
                step++;
            }
            
            
            PrintAnswer(step/2);
        }
        
        private void direction(char[][] matrix, int[] coords, Dictionary<char, int[]> movement)
        {
            int[] t = movement[matrix[coords[0]][coords[1]]];
            coords[0] += t[0];
            coords[1] += t[1];
            coords[2] = t[2];   
        }
        public override void Part2(string[] input)
        {
            var matrix = input.Select(c => c.ToCharArray()).ToArray();

            Dictionary<int, Dictionary<char, int[]>> movements = new Dictionary<int, Dictionary<char, int[]>>()
            {
                { 'R', _right },
                { 'L', _left },
                { 'U', _up },
                { 'D', _down }
            };
            
            int y = matrix.Select((c, i) => c.Contains('S') ? i : -1).Max();
            int x = Array.IndexOf(matrix[y], 'S');
            int[] coords = { y, x, 'S' };
            int[] coords2 = { y, x, 'S' };

            if (_up.ContainsKey(matrix[Math.Max(0, coords[0] - 1)][coords[1]]) && matrix[Math.Max(0, coords[0] - 1)][coords[1]] != 'S') coords[2] = 'U';
            else if (_down.ContainsKey(matrix[0][Math.Max(0, coords[1] - 1)]) && matrix[0][Math.Max(0, coords[1] - 1)] != 'S' ) coords[2] = 'L';
            else coords[2] = 'D';

            bool first = true;
            var points = new HashSet<Point2d>();
            while (first || matrix[coords[0]][coords[1]] != 'S')
            {
                first = false;
                points.Add(new Point2d(coords[0], coords[1]));
                direction(matrix, coords, movements[coords[2]]);
            }
            
            
            int answer = 0;
            char[] sides = { 'F', '|', '7' }; //add S if F or 7
            
            for (var i = 0; i < matrix.Length; i++)
            {
                bool inside = false;
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    if(points.Contains(new Point2d(i,j)) && sides.Contains(matrix[i][j]))
                    {
                        inside = !inside;
                    }
                    else
                    {
                        if (inside && !points.Contains(new Point2d(i,j)))
                        {
                            answer++;
                        }
                    }
                }
            }
            PrintAnswer(answer);
        }
    }
}