namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Print all students with phones in Sofia (starting with 02 / +3592 / +359 2). 
    /// Use LINQ.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Filter Students by Phone");

            IList<Student> students = helper.GetRandomListOfStudents(25);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
                Console.WriteLine("Phone: {0}", s.Phone);
            }

            string[] phoneCodes = { "02", "+3592", "+359 2" };

            var extractedStudents = students
                .Where(student => phoneCodes
                    .Any(phoneCode => student.Phone.StartsWith(phoneCode)));

            Console.WriteLine(new string('-', Console.WindowWidth));

            helper.ConsoleMio.PrintColorText(
                "Students with phones in Sofia:\n\n", ConsoleColor.Green);

            foreach (var e in extractedStudents)
            {
                Console.WriteLine(e);
                Console.WriteLine("Phone: {0}", e.Phone);
            }

            helper.ConsoleMio.Restart(Main);
        }
    }
}
