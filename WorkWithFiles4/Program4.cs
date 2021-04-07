using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorkWithFiles4
{
    class Program4
    {
        [Serializable]
        public class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }
            public Student(string name, string group, DateTime dateofbirth)
            {
                Name = name;
                Group = group; 
                DateOfBirth = dateofbirth;
            }
        }

        public static Student[] CreateFileWithStudents()
        {
            Student[] student = new Student[]
            {
                new Student("oleg", "2", new DateTime(1955, 3, 6)),
                new Student("viktor", "2", new DateTime(1964, 11, 7)),
                new Student("elena", "3", new DateTime(1966, 6, 16)),
                new Student("olga", "3", new DateTime(1960, 11, 17)),
                new Student("alexey", "1", new DateTime(1992, 12, 26)),
                new Student("irina", "1", new DateTime(1993, 6, 16))
            };
            return student;
        }

        public static void CreateGroupFiles(string folderName, int amount)
        {
            for(int i = 1; i <= amount; ++i)
            {
                File.Create(folderName + @"/Group_" + i.ToString() + ".txt").Close();
                // or better to use using
            }
        }

        static void Main(string[] args)
        {
            const int amountOfGroups = 3;
            string dir = "students";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
           
            CreateGroupFiles(dir, amountOfGroups);
            Student[] students = CreateFileWithStudents();

            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream("students.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, students);
                Console.WriteLine("serialization done");
            }

            using (var fs = new FileStream("students.dat", FileMode.OpenOrCreate))
            {
                var newStudents = (Student[])formatter.Deserialize(fs);
                
                if(newStudents != null)
                {
                    Console.WriteLine("deserialization done");
                    foreach (Student s in newStudents)
                    {
                        var fileInfo = new FileInfo(dir + @"/Group_" + s.Group + ".txt");
                        using (StreamWriter sw = fileInfo.AppendText())
                        {
                            sw.WriteLine(s.Name + ", " + s.DateOfBirth);
                        }
                        
                        Console.WriteLine($"Name: {s.Name} --- Group: {s.Group} --- Date of birth: {s.DateOfBirth}");
                    }
                }                
            }
        }
    }
}
