namespace FunctionalProgramming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ExcelLibrary.SpreadSheet;
    using HomeworkHelpers;
    using Students;
    using Students.Enumerations;
    using Students.Generators;

    /// <summary>
    /// Write a C# program to create an Excel file like the one below using an external
    /// library such as excellibrary, EPPlus, etc.
    /// 
    /// You are given as input course data about 1000 students in a .txt file
    /// (tab-separated values). Each line in the input holds ID, first name, last name,
    /// email, gender, student type, exam result, homework sent, homework evaluated,
    /// teamwork score, attendances count, bonus.
    /// 
    ///     Create a class Student that holds all aforementioned data fields from the file.
    ///     Add a field Result and a method CalculateResult() that calculates the total
    ///     course result of a student using the formula (exam result + homework sent +
    ///     homework evaluated + teamwork + attendances + bonus) / 5.
    /// 
    ///     Create a Student object for each student from the .txt file and store it in
    ///     some collection. Filter only the online students and sort them by their course
    ///     result. Print the resulting student collection in an Excel table. Styling the
    ///     table is not required.
    /// </summary>
    class Program
    {
        static StreamHomeworkHelper helper = new StreamHomeworkHelper();

        [STAThread]
        static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("LINQ to Excel ");

            var studentsExtractor = new TextDocumentParser();

            IList<XcelStudent> students = 
                studentsExtractor.Genereate(1000).Cast<XcelStudent>().ToList();

            var sheet = new Worksheet("Online Students");
            string[] sheetHeader = studentsExtractor.HeaderLine;

            for (int i = 0; i < sheetHeader.Length; i++)
            {
                sheet.Cells[0, i] = new Cell(sheetHeader[i]);
            }

            // Add the result filed in the end
            sheet.Cells[0, sheetHeader.Length] = new Cell("Result");

            helper.ConsoleMio.PrintColorText("Proccessing information...\n\n", ConsoleColor.DarkCyan);
            var onlineStudents = students
                .Where(student => student.StudentType == StudentType.Online)
                .OrderByDescending(student => student.CalculateResult())
                .ToArray();
            
            for (int i = 0; i < onlineStudents.Length; i++)
            {
                XcelStudent student = onlineStudents[i];
                AddStudentDataToSheet(sheet, i + 1, student);
            }

            var workbook = new Workbook();
            workbook.Worksheets.Add(sheet);

            helper.ConsoleMio.PrintColorText(
                "Query Completed\n", ConsoleColor.Green);
            
            string saveLocation = helper.SelectSaveLocation("Xcel Files|*.xls");
            workbook.Save(saveLocation);

            helper.ConsoleMio.PrintColorText("Done", ConsoleColor.Green);

            helper.ConsoleMio.Restart(Main);
        }

        private static void AddStudentDataToSheet(Worksheet sheet, int row, XcelStudent student)
        {
            sheet.Cells[row, 0] = new Cell(student.ID);
            sheet.Cells[row, 1] = new Cell(student.FirstName);
            sheet.Cells[row, 2] = new Cell(student.LastName);
            sheet.Cells[row, 3] = new Cell(student.Email);
            sheet.Cells[row, 4] = new Cell(student.Gender.ToString());
            sheet.Cells[row, 5] = new Cell(student.StudentType.ToString());
            sheet.Cells[row, 6] = new Cell(student.ExamResult);
            sheet.Cells[row, 7] = new Cell(student.HomeworksSent);
            sheet.Cells[row, 8] = new Cell(student.HomeworksEvaluated);
            sheet.Cells[row, 9] = new Cell(student.Teamwork);
            sheet.Cells[row, 10] = new Cell(student.Attendance);
            sheet.Cells[row, 11] = new Cell(student.Bonus);
            sheet.Cells[row, 12] = new Cell(student.CalculateResult());
        }
    }
}
