using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day01 : AOCPuzzle
    {
        public day01() : base("1")
        {
            
        }

        public override void Part1(string[] input)
        {
            PrintAnswer(input.Select(s => Regex.Replace(s, @"[^\d]", "")).Sum(s => int.Parse(s[0].ToString() + s[s.Length - 1])));
        }


        public override void Part2(string[] input)
        {
            Dictionary<string, char> numberDict = new Dictionary<string, char>{  
                {"one",'1'},{"two",'2'},{"three",'3'},
                {"four",'4'},{"five",'5'},{"six",'6'},  
                {"seven",'7'},{"eight",'8'},{"nine",'9'}
                };
            int sum = 0;
            foreach(string s in input) {
                char first = '\0';
                char last = '\0';
                string potentialNumber = "";
                foreach(char c in s.ToCharArray()) {
                    if(Char.IsDigit(c)) {
                        if(first == '\0') {
                            first = c;
                        }
                        last = c;
                        potentialNumber = "";
                    } else {
                        potentialNumber += c;
                        foreach(string number in numberDict.Keys) {
                            if(potentialNumber.Contains(number)) {
                                if(first == '\0') {
                                    first = numberDict[number];
                                }
                                last = numberDict[number];
                                potentialNumber = potentialNumber.Substring(potentialNumber.Length - number.Length + 1);
                                break;
                            }
                        }
                    }
                }
                sum += int.Parse(new string(new char[] { first, last }));
            }
            PrintAnswer(sum);
        }
    }
}