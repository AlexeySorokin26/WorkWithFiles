using System;
using System.Threading;
using System.IO;

namespace WorkWithFiles1
{
    
    public class Program1
    {
        const int minutesToCleanUp = 10;
        public static bool CheckModificationFileTime(FileInfo f)
        {
            DateTime modificationTime = f.LastWriteTime;
            DateTime nowTime = DateTime.Now;
            if (nowTime - modificationTime > TimeSpan.FromMinutes(minutesToCleanUp))
            {
                return true;
            }
            return false;
        }

        public static bool CheckModificationDirTime(DirectoryInfo d)
        {
            DateTime modificationTime = d.LastWriteTime;
            DateTime nowTime = DateTime.Now;
            if (nowTime - modificationTime > TimeSpan.FromSeconds(minutesToCleanUp))
            {
                return true;
            }
            return false;
        }

        public static void CleanUpFolder(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            foreach(FileInfo f in files)
            {
                if (f.Exists && CheckModificationFileTime(f))
                {
                    f.Delete();                    
                }
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo d in dirs)
            {       
                if (d.Exists)
                {
                    CleanUpFolder(d);
                    if (CheckModificationDirTime(d))
                    {
                        d.Delete();
                    }
                    
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"D:\Learning\C++\code\SkillFactory\c#\WorkWithFiles1\WorkWithFiles1\tmp";
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                CleanUpFolder(dir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
