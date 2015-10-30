namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Print all students that have email at google Use LINQ.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Filter Students by Email Domain ");

            IList<Student> students = helper.GetRandomListOfStudents(75);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
            }

            string domain = "google";

            var extractedStudents =
                from s in students
                where EmailContainsDomain(s.Email, domain)
                select s;

            Console.WriteLine(new string('-', Console.WindowWidth));

            helper.ConsoleMio.PrintColorText(
                string.Format("Students with e-mail at {0}:\n\n", domain), ConsoleColor.Green);

            foreach (var e in extractedStudents)
            {
                Console.WriteLine(e);
            }

            helper.ConsoleMio.Restart(Main);
        }

        private static bool EmailContainsDomain(string email, string domain)
        {
            string[] userAndDomain = email.Split('@');

            if (userAndDomain.Length != 2)
            {
                return false;
            }
            else
            {
                return userAndDomain[1].StartsWith(domain);
            }
        }
    }
}
