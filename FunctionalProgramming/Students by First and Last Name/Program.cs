namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Print all students whose first name is before their last name alphabetically. 
    /// Use a LINQ query.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Students by First and Last Name");

            IList<Student> students = helper.GetRandomListOfStudents(12);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
            }

            var extractedStudents = students
                .Where(s => string.CompareOrdinal(s.FirstName, s.LastName) < 0)
                .OrderBy(s => s.FirstName);

            Console.WriteLine(new string('-', Console.WindowWidth));

            helper.ConsoleMio.PrintColorText(
                "Students whose first name is before their last:\n\n", ConsoleColor.Green);

            foreach (var e in extractedStudents)
            {
                Console.WriteLine(e);
            }

            helper.ConsoleMio.Restart(Main);
        }
    }
}
