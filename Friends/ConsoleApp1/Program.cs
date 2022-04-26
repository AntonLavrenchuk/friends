using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Student>()
            {
                new Student()
                {
                    id = 1
                },
                new Student()
                {
                    id = 2
                },
                new Student()
                {
                    id = 3
                }
            };
            foreach (var stud in list)
            {
                Console.WriteLine($"{ stud.id}\n");
            }

            Console.WriteLine(  "---------\n");

            var stud2 = list.FirstOrDefault(st => st.id == 2);

            stud2 = new Student()
            {
                id = 5
            };

            foreach(var stud in list)
            {
                Console.WriteLine($"{ stud.id}\n");
            }
        }

        public class Student
        {
            public int id { get; set; } 
        }
    }
}
