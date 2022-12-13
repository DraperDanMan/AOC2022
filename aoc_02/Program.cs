//#define PART_ONE

using System;
using System.Collections.Generic;
using System.IO;

internal class Program
{
    public static void Part1(string filepath)
    {
        int totalPoints = 0;

        var outcomeLookup = new Dictionary<string, int>()
        {
            { "AX", 4 },
            { "AY", 8 },
            { "AZ", 3 },

            { "BX", 1 },
            { "BY", 5 },
            { "BZ", 9 },

            { "CX", 7 },
            { "CY", 2 },
            { "CZ", 6 },
        };
        
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string combined = line.Replace(" ", "");
                    totalPoints += outcomeLookup[combined];
                    line = reader.ReadLine();
                }
            }
        }
        
        Console.Out.WriteLine($"Total Score: {totalPoints}");
    }
    
    public static void Part2(string filepath)
    {
        int totalPoints = 0;

        var outcomeLookup = new Dictionary<string, int>()
        {
            { "AX", 3 },
            { "AY", 4 },
            { "AZ", 8 },

            { "BX", 1 },
            { "BY", 5 },
            { "BZ", 9 },

            { "CX", 2 },
            { "CY", 6 },
            { "CZ", 7 },
        };
        
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string combined = line.Replace(" ", "");
                    totalPoints += outcomeLookup[combined];
                    line = reader.ReadLine();
                }
            }
        }
        
        Console.Out.WriteLine($"Total Score: {totalPoints}");
    }

    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            return;
        }

        FileInfo fileInfo = new FileInfo(args[0]);
        if (!fileInfo.Exists)
        {
            return;
        }
        
#if PART_ONE
        Part1(args[0]);
#else
        Part2(args[0]);
#endif
    }
}
