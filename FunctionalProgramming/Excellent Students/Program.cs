namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Print all students that have at least one mark Excellent (6). Using LINQ first
    /// select them into a new anonymous class that holds { FullName + Marks}.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Excellent Students");

            IList<Student> students = helper.GetRandomListOfStudents(25);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
                Console.WriteLine("Grades: {0}", string.Join(", ", s.Marks));
            }

            int excellent = 6;

            var extractedStudents =
                from s in students
                where s.Marks.Contains(excellent)
                orderby s.Marks.Sum() descending
                select new
                {
                    FullName = string.Format("{0} {1}", s.FirstName, s.LastName),
                    Marks = string.Join(", ", s.Marks)
                };
                
            Console.WriteLine(new string('-', Console.WindowWidth));

            helper.ConsoleMio.PrintColorText(
                "Students with at least one excellent:\n\n", ConsoleColor.Green);

            foreach (var e in extractedStudents)
            {
                Console.WriteLine(e);
            }

            helper.ConsoleMio.Restart(Main);
        }
    }
}
