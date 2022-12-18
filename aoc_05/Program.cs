//#define PART_ONE

using System;
using System.Collections.Generic;
using System.IO;

internal class Program
{
    public static void Part1(string filepath)
    {
        var stacks = new List<Stack<char>>();
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                //initial state
                
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (int.TryParse(line.TrimStart()[0].ToString(), out int res))
                    {
                        if (res == 1) break;
                    }

                    int stackCol = 0;
                    int colCount = (line.Length / 4) + 1;
                    for (int i = 0; i < colCount; i++)
                    {
                        string item = line.Substring(i*4, 3);
                        if (stacks.Count < stackCol+1)
                        {
                            stacks.Add(new Stack<char>());
                        }

                        string trimmed = item.Trim('[', ']', ' ');
                        if (trimmed != "")
                        {
                            stacks[stackCol].Push(trimmed[0]);
                        }
                        stackCol++;
                    }
                    line = reader.ReadLine();
                }

                //flip stacks
                for (int i = 0; i < stacks.Count; i++)
                {
                    int count = stacks[i].Count;
                    var newStack = new Stack<char>(count);
                    for (int j = 0; j < count; j++)
                    {
                        newStack.Push(stacks[i].Pop());
                    }
                    stacks[i] = newStack;
                }
                
                // perform instructions
                line = reader.ReadLine();
                while (line != null)
                {
                    if (line != "")
                    {
                        var steps = line.Split(' ');
                        int move = int.Parse(steps[1]);
                        int from = int.Parse(steps[3])-1;
                        int to = int.Parse(steps[5])-1;

                        for (int i = 0; i < move; i++)
                        {
                            stacks[to].Push(stacks[from].Pop());
                        }
                    }
                    line = reader.ReadLine();
                }
            }
        }
        
        char[] output = new char[stacks.Count];
        for (int i = 0; i < stacks.Count; i++)
        {
            output[i] = stacks[i].Peek();
        }

        string outStr = new string(output);
        Console.Out.WriteLine($"New Re-arranged Stacks: {outStr}");
    } 
    
    public static void Part2(string filepath)
    {
        var stacks = new List<Stack<char>>();
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                //initial state
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (int.TryParse(line.TrimStart()[0].ToString(), out int res))
                    {
                        if (res == 1) break;
                    }

                    int stackCol = 0;
                    int colCount = (line.Length / 4) + 1;
                    for (int i = 0; i < colCount; i++)
                    {
                        string item = line.Substring(i*4, 3);
                        if (stacks.Count < stackCol+1)
                        {
                            stacks.Add(new Stack<char>());
                        }

                        string trimmed = item.Trim('[', ']', ' ');
                        if (trimmed != "")
                        {
                            stacks[stackCol].Push(trimmed[0]);
                        }
                        stackCol++;
                    }
                    line = reader.ReadLine();
                }
                
                //flip stacks
                for (int i = 0; i < stacks.Count; i++)
                {
                    int count = stacks[i].Count;
                    var newStack = new Stack<char>(count);
                    for (int j = 0; j < count; j++)
                    {
                        newStack.Push(stacks[i].Pop());
                    }
                    stacks[i] = newStack;
                }
                
                // perform instructions
                var grabStack = new Stack<char>();
                line = reader.ReadLine();
                while (line != null)
                {
                    if (line != "")
                    {
                        var steps = line.Split(' ');
                        int move = int.Parse(steps[1]);
                        int from = int.Parse(steps[3])-1;
                        int to = int.Parse(steps[5])-1;

                        //grab 
                        grabStack.Clear();
                        for (int i = 0; i < move; i++)
                        {
                            grabStack.Push(stacks[from].Pop());
                        }
                        
                        //place
                        for (int i = 0; i < move; i++)
                        {
                            stacks[to].Push(grabStack.Pop());
                        }
                    }
                    line = reader.ReadLine();
                }
            }
        }
        
        char[] output = new char[stacks.Count];
        for (int i = 0; i < stacks.Count; i++)
        {
            output[i] = stacks[i].Peek();
        }

        string outStr = new string(output);
        Console.Out.WriteLine($"New Re-arranged Stacks: {outStr}");
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
