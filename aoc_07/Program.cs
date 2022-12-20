//#define PART_ONE

using System;
using System.Collections.Generic;
using System.IO;

public class DFile
{
    public string Name { get; set; }
    public int Size { get; set; }
}


public class Dir
{
    public string Name { get; set; }
    public Dir Parent { get; set; }
    public List<Dir> SubDirs { get; set; }
    public List<DFile> Files { get; set; }

    public int GetSize()
    {
        int totalSize = 0;
        Files.ForEach(file => totalSize += file.Size);
        SubDirs.ForEach(dir => totalSize += dir.GetSize());
        return totalSize;
    }
}


internal class Program
{
    public static Dir ParseDirFile(StreamReader stream)
    {
        Dir root = null;
        Dir currentDir = null;

        string line = stream.ReadLine();
        while (line != null)
        {
            var lineParts = line.Split(' ');
            if (lineParts[0] == "$")
            {
                if (lineParts[1] == "cd")
                {
                    if (currentDir == null)
                    {
                        root = new Dir
                        {
                            Name = lineParts[2],
                            Parent = null,
                            SubDirs = new List<Dir>(),
                            Files = new List<DFile>(),
                        };
                        currentDir = root;
                    }
                    else if (lineParts[2] == "..")
                    {
                        currentDir = currentDir.Parent;
                    }
                    else
                    {
                        foreach (var dir in currentDir.SubDirs)
                        {
                            if (dir.Name != lineParts[2]) continue;
                            currentDir = dir;
                            break;
                        }
                    }
                }
            }
            else if (lineParts[0] == "dir")
            {
                var newDir = new Dir
                {
                    Name = lineParts[1],
                    Parent = currentDir,
                    SubDirs = new List<Dir>(),
                    Files = new List<DFile>()
                };
                currentDir.SubDirs.Add(newDir);
            }
            else
            {
                var newFile = new DFile
                {
                    Name = lineParts[1],
                    Size = int.Parse(lineParts[0]),
                };
                currentDir.Files.Add(newFile);
            }

            line = stream.ReadLine();
        }

        return root;
    }

    public static int SumDirsUnder100000(Dir dir)
    {
        int result = 0;
        foreach (var dirSubDir in dir.SubDirs)
        {
            result += SumDirsUnder100000(dirSubDir);
        }
        int thisDirSize = dir.GetSize();
        if (thisDirSize <= 100000)
        {
            result += thisDirSize;
        }
        return result;
    }
    
    public static void Part1(string filepath)
    {
        Dir tree;
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                tree = ParseDirFile(reader);
            }
        }
        
        Console.Out.WriteLine($"Sum of Dirs under 100,000: {SumDirsUnder100000(tree)}");
    }
    
    public static int FindDirForFreeSpace(Dir dir, int needed)
    {
        int closestSize = int.MaxValue;
        foreach (var subDir in dir.SubDirs)
        {
            int subDirSize = subDir.GetSize();
            if (subDirSize > needed)
            {
                int childClosest = FindDirForFreeSpace(subDir, needed);
                int closest = (childClosest < subDirSize) ? childClosest : subDirSize;
                if (closest < closestSize)
                {
                    closestSize = closest;
                }
            }
        }

        return closestSize;
    }
    
    public static void Part2(string filepath)
    {
        Dir tree;
        using (var file = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new StreamReader(file))
            {
                tree = ParseDirFile(reader);
            }
        }

        int totalUsed = tree.GetSize();
        int required = totalUsed - (70000000 - 30000000);
        int space = FindDirForFreeSpace(tree, required);
        
        Console.Out.WriteLine($"Total Used: {totalUsed}, Smallest Dir to delete for enough space: {space}");
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