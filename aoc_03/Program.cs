//#define PART_ONE

using System;
using System.Collections.Generic;
using System.IO;

public static class Ext
{
    public static void SafeAdd(this Dictionary<char, int> dict, char key, int inc)
    {
        if (!dict.ContainsKey(key))
        {
            dict[key] = inc;
        }
        else
        {
            dict[key] += inc;
        }
        
    }
}


internal class Program
{
    public static int GetCharVal(char c)
    {
        return (c >= 97) ? c - 96 : (c - 64) + 26;
    }
    
    public static void Part1(string filepath)
    {
        var totalPriorityTally = new Dictionary<char, int>();

        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    int halfLength = line.Length / 2;
                    string left = line.Substring(0, halfLength);
                    string right = line.Substring(halfLength);

                    var leftTally = new Dictionary<char, int>();
                    var rightTally = new Dictionary<char, int>();

                    for (int i = 0; i < halfLength; i++)
                    {
                        leftTally.SafeAdd(left[i], 1);
                        rightTally.SafeAdd(right[i], 1);
                    }

                    foreach (var pair in leftTally)
                    {
                        if (rightTally.TryGetValue(pair.Key, out var val))
                        {
                            totalPriorityTally.SafeAdd(pair.Key, 1/*pair.Value + val*/);
                        }
                    }
                    
                    line = reader.ReadLine();
                }
            }
        }

        // sum discrepancies
        int total = 0;
        foreach (var pair in totalPriorityTally)
        {
            int charVal = GetCharVal(pair.Key);
            int result = pair.Value * charVal;
            total += result;
        }
        
        
        Console.Out.WriteLine($"Sum of priorities: {total}");
    } 
    
    public static void Part2(string filepath)
    {
        var totalPriorityTally = new Dictionary<char, int>();

        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                var groupTally = new Dictionary<char, int>();
                var lineTally = new HashSet<char>();
                
                string line = reader.ReadLine();
                while (line != null)
                {
                    groupTally.Clear();
                    for (int i = 0; i < 3 && line != null; i++)
                    {
                        lineTally.Clear();
                        foreach (var c in line)
                        {
                            lineTally.Add(c);
                        }

                        foreach (var item in lineTally)
                        {
                            groupTally.SafeAdd(item, 1);
                        }
                        line = reader.ReadLine();
                    }

                    foreach (var pair in groupTally)
                    {
                        if (pair.Value >= 3)
                        {
                            totalPriorityTally.SafeAdd(pair.Key, 1);
                        }
                    }
                }
            }
        }
        
        // sum priorities
        int total = 0;
        foreach (var pair in totalPriorityTally)
        {
            int charVal = GetCharVal(pair.Key);
            int result = pair.Value * charVal;
            total += result;
        }
        
        
        Console.Out.WriteLine($"Sum of priorities: {total}");
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
