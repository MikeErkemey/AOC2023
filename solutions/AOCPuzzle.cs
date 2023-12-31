using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC2023
{
    abstract class AOCPuzzle
    {
        private int part = 1;
        private Stopwatch watch = new System.Diagnostics.Stopwatch();
        
        public AOCPuzzle(string day)
        {
                string[] input = File.ReadAllLines($"input/day{day.PadLeft(2, '0')}.txt");
                watch.Start();
                Part1(input);
                watch.Reset();
                watch.Start();
                Part2(input);
        }  
        
        public abstract void Part1(string[] input);
        public abstract void Part2(string[] input);
        
        public void printList(List<String> list)
        {
            Console.WriteLine(String.Join(" - ", list.ToArray()));
        }
        public void printList(List<int> list)
        {
            printList(list.Select(x => x.ToString()).ToList());
        }
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
            watch.Stop();
            Console.WriteLine($"Part {part++}: " + answer + $" & Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}