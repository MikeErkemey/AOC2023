using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day03 : AOCPuzzle
    {
        public day03() : base("3")
        {
            
        }

        public override void Part1(string[] input)
        {
            var matrix = input.Select(x => x.ToCharArray()).ToArray();

            HashSet<Point2d> points = new HashSet<Point2d>();
            
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (char.IsDigit(matrix[i][j]) | matrix[i][j] == '.') continue;
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1; l++)
                        {
                            points.Add(new Point2d(k,l));
                        }
                    }
                }
            }
            

            int answer = 0;
            
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (!char.IsDigit(matrix[i][j])) continue;

                    int number = 0;
                    bool connected = false;
                    while (j < matrix[i].Length && char.IsDigit(matrix[i][j]))
                    {
                        if (points.Contains(new Point2d(i, j)))
                        {
                            connected = true;
                        }
                        number = number * 10 + int.Parse(matrix[i][j++].ToString());
                        
                    }
                    answer += connected ? number : 0;
                }
            }
            
            PrintAnswer(answer);
        }


        public override void Part2(string[] input)
        {
            var matrix = input.Select(x => x.ToCharArray()).ToArray();

            int answer = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] != '*') continue;
                    int mul = 1;
                    int adjecent = 0;
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1; l++)
                        {
                            if (!char.IsDigit(matrix[k][l])) continue;
                            string number = matrix[k][l].ToString();
                            int left = l - 1;
                            while (left >= 0 && char.IsDigit(matrix[k][left]))
                            {
                                number = matrix[k][left--] + number;
                            }
                            
                            while (++l < matrix[k].Length && char.IsDigit(matrix[k][l]))
                            {
                                number += matrix[k][l];
                            }

                            mul *= int.Parse(number);
                            adjecent++;

                        }
                        
                    }
                    
                    answer += adjecent == 2 ? mul : 0;
                }
                
            }
            
            PrintAnswer(answer);
        }
    }
}