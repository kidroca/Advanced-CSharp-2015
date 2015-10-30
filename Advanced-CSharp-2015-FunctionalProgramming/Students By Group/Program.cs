namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using Students;

    /// <summary>
    /// Print all students from group number 2. Use a LINQ query. Order the students by FirstName.
    /// </summary>
    class Program
    {
        static FunctionalAsginmentsHelper helper = new FunctionalAsginmentsHelper();

        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Students By Group ");

            IList<Student> students = helper.GetRandomListOfStudents(12);

            helper.ConsoleMio.PrintColorText(
                "Working with this set of students:\n\n", ConsoleColor.DarkCyan);

            foreach (var s in students)
            {
                Console.WriteLine(s);
                Console.WriteLine("Group: {0}\n", s.GroupNumber);
            }

            int groupNumber = 2;

            var targetedGroup =
                from student in students
                where student.GroupNumber == groupNumber
                orderby student.FirstName
                select student;

            Console.WriteLine(new string('-', Console.WindowWidth));

            helper.ConsoleMio.PrintColorText(
                string.Format("Students from group {0}:\n\n", groupNumber), ConsoleColor.Green);

            foreach (var t in targetedGroup)
            {
                Console.WriteLine(t);
                Console.WriteLine("Group: {0}\n", t.GroupNumber);
            }
                
            helper.ConsoleMio.Restart(Main);
        }
    }
}
