using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day14 : AOCPuzzle
    {
        public day14() : base("14")
        {
            
        }

        public override void Part1(string[] input)
        {
            var matrix = input.Select(s => s.ToCharArray()).ToArray();
            
            North(matrix);

            int answer = 0;
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix[0].Count(); j++)
                {
                    if (matrix[i][j] == 'O')
                    {
                        answer += matrix.Count() - i;
                    }
                }
            }
            
            PrintAnswer(answer);
        }

        public override void Part2(string[] input)
        {
            List<int> cycles = new List<int>();
            int cycle = 0;
            int offset = 0;
            var matrix = input.Select(s => s.ToCharArray()).ToArray();
            
            for (int i = 0; i < 1000000000; i++)
            {
                if (i % 4 == 0) North(matrix);
                if (i % 4 == 1) West(matrix);
                if (i % 4 == 2) South(matrix);
                if (i % 4 == 3)
                {
                    East(matrix);
                    int sum = 0;
                    for (int k = 0; k < matrix.Count(); k++)
                    {
                        for (int j = 0; j < matrix[0].Count(); j++)
                        {
                            if (matrix[k][j] == 'O')
                            {
                                sum += matrix.Count() - k;
                            }
                        }
                    }

                    if (cycles.Contains(sum))
                    {
                        if (cycle == 0)
                        {
                            offset = cycles.IndexOf(sum);
                        }
                        if (cycle != 0 && cycles[cycles.Count - cycle] == sum)
                        {
                            break;
                        }
                        cycle = cycles.LastIndexOf(sum) - cycles.IndexOf(sum);
                    }
                    
                    cycles.Add(sum);
                }
            }

            var answer = 1000000000 - offset;
            answer %= cycle;
                
            PrintAnswer(cycles[offset + answer - 1]);
        }
        
        
        public void North(char[][] matrix)
        {
            for (int i = 0; i < matrix[0].Count(); i++)
            {
                int y = 0;
                for (int j = 0; j < matrix.Count(); j++)
                {
                    if (matrix[j][i] == 'O')
                    { 
                        matrix[j][i] = '.';
                        matrix[y][i] = 'O';
                        y += 1;
                    } else if (matrix[j][i] == '#')
                    {
                        y = j + 1;
                    }
                }
            }
        }
        
        public void West(char[][] matrix)
        {
            for (int i = 0; i < matrix.Count(); i++)
            {
                int y = 0;
                for (int j = 0; j < matrix[i].Count(); j++)
                {
                    if (matrix[i][j] == 'O')
                    { 
                        matrix[i][j] = '.';
                        matrix[i][y] = 'O';
                        y += 1;
                    } else if (matrix[i][j] == '#')
                    {
                        y = j + 1;
                    }
                }
            }
        }
        
        public void South(char[][] matrix)
        {
            for (int i = 0; i < matrix[0].Count(); i++)
            {
                int y = matrix.Length - 1;
                for (int j = matrix.Count() - 1; j >=0 ; j--)
                {
                    if (matrix[j][i] == 'O')
                    { 
                        matrix[j][i] = '.';
                        matrix[y][i] = 'O';
                        y -= 1;
                    } else if (matrix[j][i] == '#')
                    {
                        y = j - 1;
                    }
                }
            }
        }
        
        public void East(char[][] matrix)
        {
            for (int i = 0; i < matrix[0].Count(); i++)
            {
                int y = matrix.Count() - 1;
                for (int j = matrix[i].Count() - 1; j >=0 ; j--)
                {
                    if (matrix[i][j] == 'O')
                    { 
                        matrix[i][j] = '.';
                        matrix[i][y] = 'O';
                        y -= 1;
                    } else if (matrix[i][j] == '#')
                    {
                        y = j - 1;
                    }
                }
            }
        }

    }
}