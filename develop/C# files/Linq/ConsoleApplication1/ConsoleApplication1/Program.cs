using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Student[] studentArray = { 
                    new Student() { StudentID = 1, StudentName = "John", age = 18 } ,
                    new Student() { StudentID = 2, StudentName = "Steve",  age = 21 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  age = 25 } ,
                    new Student() { StudentID = 4, StudentName = "Ram" , age = 20 } ,
                    new Student() { StudentID = 5, StudentName = "Ron" , age = 31 } ,
                    new Student() { StudentID = 6, StudentName = "Chris",  age = 17 } ,
                    new Student() { StudentID = 7, StudentName = "Rob",age = 19  } ,
                };

            // Use LINQ to find teenager students
            Student[] teenAgerStudents = studentArray.Where(s => s.age > 12 && s.age < 20).ToArray();
            foreach (Student s in teenAgerStudents)
            {
                Console.WriteLine(s.StudentID.ToString() + " " + s.StudentName + " " + s.age.ToString());
            }
            // Use LINQ to find first student whose name is Bill 
            Student bill = studentArray.Where(s => s.StudentName == "Bill").FirstOrDefault();

            // Use LINQ to find student whose StudentID is 5
            Student student5 = studentArray.Where(s => s.StudentID == 5).FirstOrDefault();
        }
    }

    class Student
    {
        public int StudentID { get; set; }
        public String StudentName { get; set; }
        public int age { get; set; }
    }
}
