﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2023
{
    abstract class AOCPuzzle
    {
        private int part = 1;
        public AOCPuzzle(string day)
        {
            
                string[] input = File.ReadAllLines($"input/day{day.PadLeft(2, '0')}.txt");
                Part1(input);
                Part2(input);
            
        }  
        
        public abstract void Part1(string[] input);
        public abstract void Part2(string[] input);

        
        public void PrintAnswer(long answer)
        {
            PrintAnswer(answer.ToString());
        }
        public void PrintAnswer(int answer)
        {
            PrintAnswer(answer.ToString());
        }
        public void PrintAnswer(double answer)
        {
            PrintAnswer(answer.ToString());
        }
        public void PrintAnswer(String answer)
        {
            Console.WriteLine($"Part {part++}:" + answer);
        }
    }
}