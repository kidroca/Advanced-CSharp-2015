namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Write a LINQ query that finds the first name and last name of all students with
    /// age between 18 and 24. The query should return only the first name, last name and age.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Students by Age ");

            IList<Student> students = helper.GetRandomListOfStudents(25);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
                Console.WriteLine("Age: {0}", s.Age);
            }

            Console.WriteLine(new string('-', Console.WindowWidth));

            int minAge = 18,
                maxAge = 24;

            var extractedStudents =
                from student in students
                where 18 <= student.Age && student.Age <= 24
                orderby student.Age
                select new
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age
                };

            helper.ConsoleMio.PrintColorText(
                string.Format(
                    "Students between {0} and {1}:\n\n", minAge, maxAge), ConsoleColor.Green);

            foreach (var e in extractedStudents)
            {
                Console.WriteLine(e);
            }

            helper.ConsoleMio.Restart(Main);
        }
    }
}
