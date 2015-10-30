namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Using the extension methods OrderBy() and ThenBy() with lambda expressions sort
    /// the students by first name and last name in descending order.
    /// Rewrite the same with LINQ query syntax.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Sort Students LINQ syntax ");

            IList<Student> students = helper.GetRandomListOfStudents(12);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
            }

            var extractedStudents =
                from s in students
                orderby s.LastName descending
                orderby s.FirstName descending 
                select s;

            Console.WriteLine(new string('-', Console.WindowWidth));

            helper.ConsoleMio.PrintColorText(
                "Students descending by first and last names:\n\n", ConsoleColor.Green);

            foreach (var e in extractedStudents)
            {
                Console.WriteLine(e);
            }

            helper.ConsoleMio.Restart(Main);
        }
    }
}
