namespace FunctionalProgramming.Students.Generators
{
    using System;
    using Enumerations;

    public class TextDocumentParser : SimpleGenerator
    {
        private const int dataHeadersCount = 12;
        
        public TextDocumentParser(
            string txtFilePath = "../../../Students-data.txt", string[] splitPattern = null) 
            : base(txtFilePath, splitPattern)
        {
        }

        public override Student StudentParser(string[] tokens)
        {
            if (tokens.Length < dataHeadersCount)
            {
                return null;
            }

            try {
                var student = new XcelStudent()
                {
                    ID = int.Parse(tokens[0]),
                    FirstName = tokens[1],
                    LastName = tokens[2],
                    Email = tokens[3],
                    Gender = (Gender)Enum.Parse(typeof(Gender), tokens[4], true),
                    StudentType = (StudentType)Enum.Parse(typeof(StudentType), tokens[5], true),
                    ExamResult = int.Parse(tokens[6]),
                    HomeworksSent = int.Parse(tokens[7]),
                    HomeworksEvaluated = int.Parse(tokens[8]),
                    Teamwork = double.Parse(tokens[9]),
                    Attendance = int.Parse(tokens[10]),
                    Bonus = double.Parse(tokens[11])
                };

                return student;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}