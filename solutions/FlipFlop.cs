using System.Collections.Generic;

namespace AOC2023
{
    public class FlipFlop
    {
        public string name;
        public string previousName;
        public char kind;
        public bool on;
        public List<string> modules;
        public Dictionary<string, bool> previousPulses;


        public FlipFlop(string name)
        {
            this.name = name;
            on = false;
        }
        
        public FlipFlop(string name, bool on, string previousName)
        {
            this.previousName = previousName;
            this.name = name;
            this.on = on;
        }

        public FlipFlop(string name, char kind, List<string> modules)
        {
            this.name = name;
            this.kind = kind;
            this.modules = modules;
            on = false;
            previousPulses = new Dictionary<string, bool>();
        }
        
        
    }
}