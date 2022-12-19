//#define PART_ONE

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
    public static void Part1(string filepath)
    {
        int streamCount = 0;
        var charQueue = new Queue<char>(4);
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (var c in line)
                    {
                        if (charQueue.Count >= 4)
                        {
                            charQueue.Dequeue();
                        }
                        charQueue.Enqueue(c);
                        streamCount++;
                        
                        if (charQueue.Count < 4) continue;
                        
                        //is match?
                        bool matchedPattern = true;
                        var testArray = charQueue.ToArray();
                        for (int i = 0; i < testArray.Length; i++)
                        {
                            for (int j = 0; j < testArray.Length; j++)
                            {
                                if (i==j)continue;
                                if (testArray[i] == testArray[j])
                                {
                                    matchedPattern = false;
                                    break;
                                }
                            }
                        }

                        if (matchedPattern)
                        {
                            goto done;
                        }
                    }
                    line = reader.ReadLine();
                }
            }
        }
        done:
        Console.Out.WriteLine($"total chars read before pattern found: {streamCount}");
    } 
    
    public static void Part2(string filepath)
    {
        int streamCount = 0;
        var charQueue = new Queue<char>(4);
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (var c in line)
                    {
                        if (charQueue.Count >= 14)
                        {
                            charQueue.Dequeue();
                        }
                        charQueue.Enqueue(c);
                        streamCount++;
                        
                        if (charQueue.Count < 14) continue;
                        
                        //is match?
                        bool matchedPattern = true;
                        var testArray = charQueue.ToArray();
                        for (int i = 0; i < testArray.Length; i++)
                        {
                            for (int j = 0; j < testArray.Length; j++)
                            {
                                if (i==j)continue;
                                if (testArray[i] == testArray[j])
                                {
                                    matchedPattern = false;
                                    break;
                                }
                            }
                        }

                        if (matchedPattern)
                        {
                            goto done;
                        }
                    }
                    line = reader.ReadLine();
                }
            }
        }
        done:
        Console.Out.WriteLine($"total chars read before pattern found: {streamCount}");

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