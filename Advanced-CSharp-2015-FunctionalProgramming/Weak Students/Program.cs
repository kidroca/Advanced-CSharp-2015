namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Write a similar program to the previous one to extract the students with exactly
    /// two marks "2". Use extension methods.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Weak Students");

            IList<Student> students = helper.GetRandomListOfStudents(25);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
                Console.WriteLine("Grades: {0}", string.Join(", ", s.Marks));
            }

            int bad = 2;

            var extractedStudents =
                from s in students
                where s.Marks.HasExcatlyTwoBadMarks(bad)
                orderby s.FirstName
                select new
                {
                    FullName = string.Format("{0} {1}", s.FirstName, s.LastName),
                    Marks = string.Join(", ", s.Marks)
                };

            Console.WriteLine(new string('-', Console.WindowWidth));

            helper.ConsoleMio.PrintColorText(
                "Students with exactly 2 bad marks:\n\n", ConsoleColor.Green);

            foreach (var e in extractedStudents)
            {
                Console.WriteLine(e);
            }

            helper.ConsoleMio.Restart(Main);
        }
    }
}
