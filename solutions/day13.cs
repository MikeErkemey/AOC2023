using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day13 : AOCPuzzle
    {
        public day13() : base("13")
        {
            
        }

        public override void Part1(string[] input)
        {
            List<List<string>> matrixes = new List<List<string>>();
            List<string> matrix = new List<string>();
            foreach (var s in input)
            {
                if (s.Equals(""))
                {
                    matrixes.Add(matrix);
                    matrix = new List<string>();
                    continue;
                }
                matrix.Add(s);
            }
            matrixes.Add(matrix);
            int sum = 0;
            foreach (var m in matrixes)
            {
                for (var i = 1; i < m[0].Length; i++)
                {
                    if (m.All(l => MirrorsVertical(l, i)))
                    {
                        sum += i;
                    }
                }
                
                for (var i = 1; i < m.Count; i++)
                {
                    if (MirrorsHorizontal(m, i))
                    {
                        sum += i * 100;
                    }
                }
                
            }
            
            PrintAnswer(sum);
        }

        private bool MirrorsVertical(string s, int i)
        {
            var left = new string(s.Substring(0,i).Reverse().ToArray());
            var right = s.Substring(i);

            for (var j = 0; j < Math.Min(left.Length, right.Length); j++)
            {
                if (left[j] != right[j]) return false;
            }

            return true;
        }
        
        private bool MirrorsHorizontal(List<string> s, int i)
        {
            var left = s.Take(i).ToList();
            left.Reverse();
            var right = s.Skip(i).ToList();
            for (var j = 0; j < Math.Min(left.Count, right.Count); j++)
            {
                if (left[j] != right[j]) return false;
            }
            
            return true;
        }


        public override void Part2(string[] input)
        {
            List<List<string>> matrixes = new List<List<string>>();
            List<string> matrix = new List<string>();
            foreach (var s in input)
            {
                if (s.Equals(""))
                {
                    matrixes.Add(matrix);
                    matrix = new List<string>();
                    continue;
                }
                matrix.Add(s);
            }
            matrixes.Add(matrix);
            int sum = 0;
            foreach (var m in matrixes)
            {
                for (var i = 1; i < m[0].Length; i++)
                {
                    if (m.Select(l => MirrorsVertical2(l, i)).Sum() == 1)
                    {
                        sum += i;
                    }
                }
                
                for (var i = 1; i < m.Count; i++)
                {
                    if (MirrorsHorizontal2(m, i) == 1)
                    {
                        sum += i * 100;
                    }
                }
                
            }
            
            PrintAnswer(sum);
        }
        
        private int MirrorsVertical2(string s, int i)
        {
            var left = new string(s.Substring(0,i).Reverse().ToArray());
            var right = s.Substring(i);
            int sum = 0;
            for (var j = 0; j < Math.Min(left.Length, right.Length); j++)
            {
                if (left[j] != right[j]) sum++;
            }

            return sum;
        }
        
        private int MirrorsHorizontal2(List<string> s, int i)
        {
            var left = s.Take(i).ToList();
            left.Reverse();
            var right = s.Skip(i).ToList();
            int sum = 0;
            for (var j = 0; j < Math.Min(left.Count, right.Count); j++)
            {
                for (var k = 0; k < left[0].Length; k++)
                { 
                    if (left[j][k] != right[j][k]) sum++;
                }
            }
            
            return sum;
        }
    }
}