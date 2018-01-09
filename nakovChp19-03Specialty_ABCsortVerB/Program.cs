using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//It is given a text file containing information about students
//and their specialty in the following format:
//Steven Davis | Computer Science
//Joseph Johnson | Software Engeneering
//Helen Mitchell | Public Relations
//Nicolas Carter | Computer Science
//Susan Green | Public Relations
//William Johnson | Software Engeneering
//Using SortedDictionary<K, T> print on the console the specialties in an
//alphabetical order and for each of them print the names of the students,
//firstly sorted by family name and secondly – by first name, as shown:
//Computer Sciences: Nicolas Carter, Steven Davis

namespace PrintSpecialtyList_AbcOrder
{
    public class Student : IComparable<Student>
    {
        private string firstName;
        private string lastName;
        public Student(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public int CompareTo(Student student)
        {
            int result = lastName.CompareTo(student.lastName);
            if (result == 0)
            {
                result = firstName.CompareTo(student.firstName);
            }

            return result;
        }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }

    public class SpecialtyAbcOrder
    {
        public void PrintDataAbcOrder()
        {
            SortedDictionary<string, List<Student>> major = new SortedDictionary<string, List<Student>>();
            StreamReader reader = new StreamReader(@"C:\Users\ntb\Desktop\test.txt", Encoding.GetEncoding("windows-1251"));
            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == "")
                    {
                        break;
                    }

                    string[] entry = line.Split('|');
                    string fullName = entry[0].Trim();
                    string specialty = entry[1].Trim();
                    
                    string firstName = "";
                    string lastName = "";

                    var splitName = fullName.Split(new char[] { ' ' }, 2);

                    if (splitName.Length == 1)
                    {
                        firstName = "";
                        lastName = splitName[0];
                    }

                    else
                    {
                        firstName = splitName[0];
                        lastName = splitName[1];
                    }
                    
                    List<Student> students;
                    if (!major.TryGetValue(specialty, out students))
                    {
                        students = new List<Student>();
                        major.Add(specialty, students);
                    }

                    Student student = new Student(firstName, lastName);
                    students.Add(student);
                }
            }

            foreach (string studyField in major.Keys)
            {
                Console.Write(studyField + ": ");
                List<Student> students = major[studyField];
                students.Sort();
                int i = 0;
                foreach (Student student in students)
                {
                    Console.Write(student);
                    if (students.Count - 1 != i)
                    {
                        Console.Write(", ");
                    }

                    i++;
                }

                Console.WriteLine();
            }
        }
        
    }

    public class Main_program
    {
        public static void Main(string[] args)
        {
            SpecialtyAbcOrder printList = new SpecialtyAbcOrder();
            printList.PrintDataAbcOrder();
        }
    }

}