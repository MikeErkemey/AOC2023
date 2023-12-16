using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day15 : AOCPuzzle
    {
        public day15() : base("15")
        {

        }

        public override void Part1(string[] input)
        {
            int answer = 0;

            foreach (var s in input)
            {
                foreach (var c in s.Split(','))
                {
                    answer += HashFun(c);
                }
            }

            PrintAnswer(answer);
        }

        public int HashFun(String code)
        {

            int sum = 0;
            foreach (var c in code.ToCharArray())
            {
                sum = ((sum + c) * 17) % 256;
            }

            return sum;
        }

        public override void Part2(string[] input)
        {
            var boxes = new List<LinkedList<string>>();
            for (int i = 0; i < 256; i++)
            {
                boxes.Add(new LinkedList<string>());
            }

            foreach (var s in input)
            {
                foreach (var code in s.Split(','))
                {
                    string c = Regex.Match(code, @"[a-z]+").ToString();
                    int label = HashFun(c);
                    bool changed = false;
                    LinkedList<String> box = boxes[label];

                    for (var i = 0; i < box.Count; i++)
                    {
                        if (Regex.Match(box.ToList()[i], @"[a-z]+").ToString().Equals(c))
                        {
                            var n = box.Find(box.ToList()[i]);
                            if (code.Contains('='))
                            {
                                n.Value = code;
                            }
                            else
                            {
                                box.Remove(n);
                            }
                            changed = true;
                        }
                    }

                    if (!changed)
                    {
                        if (code.Contains('='))
                        {
                            box.AddLast(code);
                        }
                    }
                }
            }

            int answer = 0;
            
            for (var i = 0; i < boxes.Count; i++)
            {
                int j = 1;
                foreach (var s in boxes[i])
                {
                    answer += (i+1) * j * int.Parse(Regex.Match(s, @"\d").ToString());
                    j++;
                }
            }
                

            PrintAnswer(answer);
        }
    }
}