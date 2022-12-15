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
    private class Range
    {
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
        public int Min { get; set; }
        public int Max { get; set; }

        public bool Contains(Range right)
        {
            if (Min < right.Min) return false;
            if (Max > right.Max) return false;
            return true;
        }

        public bool Overlaps(Range right)
        {
            if (Min > right.Max || Max < right.Min) return false;
            return true;
        }
    }
    
    public static void Part1(string filepath)
    {
        int totalFullyContainedPairs = 0;

        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    var pair = line.Split(',');
                    var left = pair[0].Split('-');
                    Range lRange = new Range(int.Parse(left[0]), int.Parse(left[1]));

                    var right = pair[1].Split('-');
                    Range rRange = new Range(int.Parse(right[0]), int.Parse(right[1]));

                    if (lRange.Contains(rRange) || rRange.Contains(lRange))
                    {
                        totalFullyContainedPairs++;
                    }
                    line = reader.ReadLine();
                }
            }
        }
        
        Console.Out.WriteLine($"total pairs that fully contain the other: {totalFullyContainedPairs}");
    } 
    
    public static void Part2(string filepath)
    {
        int totalOverlappingPairs = 0;
        
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    var pair = line.Split(',');
                    var left = pair[0].Split('-');
                    Range lRange = new Range(int.Parse(left[0]), int.Parse(left[1]));

                    var right = pair[1].Split('-');
                    Range rRange = new Range(int.Parse(right[0]), int.Parse(right[1]));

                    if (lRange.Overlaps(rRange))
                    {
                        totalOverlappingPairs++;
                    }
                    line = reader.ReadLine();
                }
            }
        }
        
        Console.Out.WriteLine($"Total overlapping pairs: {totalOverlappingPairs}");
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
