using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day20 : AOCPuzzle
    {
        public day20() : base("20")
        {
            
        }

        public override void Part1(string[] input)
        {
            var splitInput = input.Select(s => s.Split(new []{" -> "}, StringSplitOptions.None)).ToList();
            Dictionary<string, FlipFlop> dict = new Dictionary<string, FlipFlop>();
            
            foreach (var strings in splitInput)
            {
                var name = Regex.Match(strings[0], @"[a-zA-Z]+").ToString().Trim();
                var kind = strings[0][0];
                var modules = strings[1].Split(new[] { ", " }, StringSplitOptions.None).Select(s => s.Trim()).ToList();
                FlipFlop f = new FlipFlop(name, kind, modules);
                
                dict.Add(name, f);
            }
            
            foreach (var dictKey in dict.Values)
            {
                foreach (var s in dictKey.modules.Where(m => dict.ContainsKey(m) && dict[m].kind == '&'))
                {
                    dict[s].previousPulses.Add(dictKey.name, false);
                }
            }
    
            Queue<FlipFlop> q = new Queue<FlipFlop>();
            long low = 0;
            long high = 0;
            for(int i = 0; i < 1000; i++){
                q.Enqueue(new FlipFlop("broadcaster", false, "button"));
                while (q.Count != 0)
                {
                    FlipFlop pulse = q.Dequeue();
                    if (pulse.on) high++;
                    else low++;
                    if (!dict.ContainsKey(pulse.name)) continue;
                    FlipFlop current = dict[pulse.name];
                    
                    

                    if (current.name == "broadcaster")
                    {
                        foreach (var currentModule in current.modules)
                        {
                            q.Enqueue(new FlipFlop(currentModule, pulse.on, current.name));
                        }
                    }
                    else if (current.kind == '%' && !pulse.on)
                    {
                        current.on = !current.on;
                        foreach (var currentModule in current.modules)
                        {
                            q.Enqueue(new FlipFlop(currentModule, current.on, current.name));
                        }
                    }
                    else if (current.kind == '&')
                    {
                        current.previousPulses[pulse.previousName] = pulse.on;
                        bool send = current.previousPulses.Values.Any(p => !p);
                        foreach (var currentModule in current.modules)
                        {
                            q.Enqueue(new FlipFlop(currentModule, send, current.name));
                        }
                        
                    }
                }
            }
            
            PrintAnswer(low * high + " - " + high + " - " + low);
        }


        public override void Part2(string[] input)
        { 
            var splitInput = input.Select(s => s.Split(new []{" -> "}, StringSplitOptions.None)).ToList();
            Dictionary<string, FlipFlop> dict = new Dictionary<string, FlipFlop>();
            
            foreach (var strings in splitInput)
            {
                var name = Regex.Match(strings[0], @"[a-zA-Z]+").ToString().Trim();
                var kind = strings[0][0];
                var modules = strings[1].Split(new[] { ", " }, StringSplitOptions.None).Select(s => s.Trim()).ToList();
                FlipFlop f = new FlipFlop(name, kind, modules);
                
                dict.Add(name, f);
            }
            
            foreach (var dictKey in dict.Values)
            {
                foreach (var s in dictKey.modules.Where(m => dict.ContainsKey(m) && dict[m].kind == '&'))
                {
                    dict[s].previousPulses.Add(dictKey.name, false);
                }
            }
    
            Queue<FlipFlop> q = new Queue<FlipFlop>();
            long low = 0;
            long high = 0;
            int i = 0;
            Dictionary<string, long> d = new Dictionary<string, long>();
            while (d.Count < 4)
            {
                i++;
                q.Enqueue(new FlipFlop("broadcaster", false, "button"));
                while (q.Count != 0)
                {
                    FlipFlop pulse = q.Dequeue();

                    if (pulse.name.Equals("kz") && pulse.on && !d.ContainsKey(pulse.previousName))
                    {
                        
                        Console.WriteLine(pulse.previousName);
                        d.Add(pulse.previousName,i);
                    }
                    if (pulse.on) high++;
                    else low++;
                    if (!dict.ContainsKey(pulse.name)) continue;
                    FlipFlop current = dict[pulse.name];

                    if (current.name == "broadcaster")
                    {
                        foreach (var currentModule in current.modules)
                        {
                            q.Enqueue(new FlipFlop(currentModule, pulse.on, current.name));
                        }
                    }
                    else if (current.kind == '%' && !pulse.on)
                    {
                        current.on = !current.on;
                        foreach (var currentModule in current.modules)
                        {
                            q.Enqueue(new FlipFlop(currentModule, current.on, current.name));
                        }
                    }
                    else if (current.kind == '&')
                    {
                        current.previousPulses[pulse.previousName] = pulse.on;
                        bool send = current.previousPulses.Values.Any(p => !p);
                        foreach (var currentModule in current.modules)
                        {
                            q.Enqueue(new FlipFlop(currentModule, send, current.name));
                        }

                    }
                }
                
                
                
            }

            PrintAnswer(LCM(d.Values.ToList()));
        }
        
        private long LCM(List<long> numbers)
        {
            return numbers.Aggregate((x, y) => x * y / GCD(x, y));
        }

        private long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        
            
    }
}