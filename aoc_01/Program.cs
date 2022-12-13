//#define PART_ONE

using System;
using System.Collections.Generic;
using System.IO;

internal class Program
{
    public static void Part1(string filepath)
    {
        int elfNumber = 0;
        int elfCalories = 0;
        int totalCalories = 0;
        
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                int num = 1;
                int calories = 0;
                
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line == "")
                    {
                        if (calories > elfCalories)
                        {
                            elfCalories = calories;
                            elfNumber = num;
                        }

                        totalCalories += calories;
                        calories = 0;
                        num++;
                    }
                    else if (int.TryParse(line, out var cal))
                    {
                        calories += cal;
                    }
                    line = reader.ReadLine();
                }
            }
        }
        
        Console.Out.WriteLine($"Total Calories is:{totalCalories}, elf {elfNumber} is carrying the most with: {elfCalories}");
    }
    
    public static void Part2(string filepath)
    {
        var totals = new List<int>(512);
        
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                int calories = 0;
                
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line == "")
                    {
                        totals.Add(calories);
                        calories = 0;
                    }
                    else if (int.TryParse(line, out var cal))
                    {
                        calories += cal;
                    }
                    line = reader.ReadLine();
                }
            }
        }

        int totalCalories = 0;
        totals.Sort( (a, b) => b - a );
        for (int i = 0; i < 3; i++)
        {
            totalCalories += totals[i];
        }
        Console.Out.WriteLine($"Total Calories is:{totalCalories}");
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
