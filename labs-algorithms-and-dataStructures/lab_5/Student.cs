using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    class Student
    {
        public string LastName { get; set; }  
        public string FirstName { get; set; }  
        public uint TaxCode { get; set; }   
        public string StudyForm { get; set; }  

        public Student(string ln, string fn, uint code, string form)
        {
            LastName  = ln;
            FirstName = fn;
            TaxCode   = code;
            StudyForm = form;
        }

        public bool IsExtramural => StudyForm.Equals("заочна",
            StringComparison.OrdinalIgnoreCase);

        public override string ToString()
            => $"{TaxCode,12} | {LastName,-12} | {FirstName,-10} | {StudyForm}";
    }

    static class T
    {
        const int W = 56;
        public static void Header()
        {
            Console.WriteLine(new string('-', W));
            Console.WriteLine($"{"ІД КОД",12} | {"Прізвище",-12} | {"Ім'я",-10} | Форма");
            Console.WriteLine(new string('-', W));
        }
        public static void Footer() => Console.WriteLine(new string('-', W));
        public static void Print(Student[] arr)
        {
            Header();
            foreach (var s in arr) if (s != null) Console.WriteLine(s);
            Footer();
        }
    }
}
