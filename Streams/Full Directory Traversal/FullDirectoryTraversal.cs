namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using HomeworkHelpers;

    /// <summary>
    /// Modify your previous program to recursively traverse the sub-directories of the starting directory as well.
    /// </summary>
    public class FullDirectoryTraversal : DirectoryTraversal
    {
        private static StreamHomeworkHelper helper = new StreamHomeworkHelper();

        [STAThread]
        private static void Main()
        {
            helper.Console.Setup();

            helper.Console.PrintHeading("Directory Traversal ");

            string directoryPath = GetDirectory();

            SaveResult(directoryPath);

            helper.Console.PrintColorText("\nCompleted Successfully - See result at desktop\\report.txt", ConsoleColor.DarkGreen);

            helper.Console.Restart(Main);
        }

        private static void SaveResult(string directoryPath)
        {
            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = string.Format("{0}\\report.txt", pathToDesktop);

            using (var outputStream = new StreamWriter(filename, false, Encoding.UTF8))
            {
                var dirInfo = new DirectoryInfo(directoryPath);

                outputStream.WriteLine("Initial Path: {0}\n", dirInfo.FullName);

                TraverseDirectoryAndSaveResult(dirInfo, outputStream);
            }
        }

        private static void TraverseDirectoryAndSaveResult(DirectoryInfo dirInfo, StreamWriter outputStream, string leadingChars = "|")
        {
            string path = dirInfo.FullName;

            Dictionary<string, List<FileInfo>> directoryFiles = GetDirectoryFiles(path);

            string directoryName = dirInfo.Name;

            outputStream.WriteLine("{0}------> dir: {1}", leadingChars, directoryName);
            outputStream.WriteLine("|");
            WriteFileInformation(outputStream, directoryFiles, leadingChars);
            outputStream.WriteLine("|");

            foreach (var dir in dirInfo.GetDirectories())
            {
                try
                {
                    leadingChars += "---";

                    TraverseDirectoryAndSaveResult(dir, outputStream, leadingChars);

                    leadingChars = leadingChars.Replace("---", String.Empty);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (PathTooLongException ex)
                {
                    outputStream.WriteLine("{0} PATH TOO LONG: {1}", leadingChars, ex.Message);
                    continue;
                }
            }
        }
    }
}