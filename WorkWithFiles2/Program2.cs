using System;
using System.IO;

namespace WorkWithFiles2
{
    public class Program2
    {
        public static long DirSize(DirectoryInfo dir)
        {
            long size = 0;
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo f in files)
            {
                size += f.Length;
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo d in dirs)
            {
                size += DirSize(d);
            }
            return size;
        }
        static void Main(string[] args)
        {
            string path = @"D:\Learning\C++\code\SkillFactory\c#\WorkWithFiles1\WorkWithFiles2\tmp";
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                long size = DirSize(dir);
                Console.WriteLine($"Size of folder: {size}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
