﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2023
{
    abstract class AOCPuzzle
    {
        public AOCPuzzle(string day)
        {
            try
            {
                string[] input = File.ReadAllLines($"../input/day{day.PadLeft(2, '0')}.txt");
                Part1(input);
                Part2(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Handle the exception as needed
            }
        }  
        
        public abstract void Part1(string[] input);
        public abstract void Part2(string[] input);
    }
}