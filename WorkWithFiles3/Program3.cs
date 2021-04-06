using System;
using System.IO;
using WorkWithFiles2;
using WorkWithFiles1;

namespace WorkWithFiles3
{
    class Program3
    {
        static void Main(string[] args)
        {
            string path = @"D:\Learning\C++\code\SkillFactory\c#\WorkWithFiles1\WorkWithFiles2\tmp";
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                
                long size = Program2.DirSize(dir);
                Console.WriteLine($"Size of folder before: {size}");
                Program1.CleanUpFolder(dir);
                size = Program2.DirSize(dir);
                Console.WriteLine($"Size of folder after: {size}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
