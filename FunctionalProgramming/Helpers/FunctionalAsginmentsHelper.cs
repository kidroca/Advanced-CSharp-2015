namespace HomeworkHelpers
{
    using System;
    using System.Collections.Generic;
    using FunctionalProgramming.Students;
    using FunctionalProgramming.Students.Generators;

    public class FunctionalAsginmentsHelper : BaseHelper
    {
        private const int TotalStudents = 1000;

        private static Random rnd;

        private static SimpleGenerator studentGen;

        private Random Random
        {
            get
            {
                if (rnd == null)
                {
                    rnd = new Random();
                }

                return rnd;
            }
        }

        private SimpleGenerator StudentGenerator
        {
            get
            {
                if (studentGen == null)
                {
                    studentGen = new SimpleGenerator();
                }

                return studentGen;
            }
        }

        public IList<Student> GetRandomListOfStudents(int count)
        {
            int startFromStudentWithID = this.Random.Next(1, TotalStudents - count);

            return this.StudentGenerator.Genereate(count, startFromStudentWithID);
        }
    }
}
