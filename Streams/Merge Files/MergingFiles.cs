namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that takes any file and slices it to n parts. Write the following methods:
    /// 
    ///     Slice(string sourceFile, string destinationDirectory, int parts) - slices the given
    ///     file into n parts and saves them in destinationDirectory.
    /// 
    ///     Assemble(List<string> files, string destinationDirectory) - combines all files into
    ///     one, in the order they are passed, and saves the result in destinationDirectory.
    /// 
    /// Use FileStreams. You are not allowed to use the File class or similar helper classes.
    /// </summary>
    public class MergingFiles
    {
        private static StreamHomeworkHelper instance;

        private static StreamHomeworkHelper helper
        {
            get
            {
                if (instance == null)
                {
                    instance = new StreamHomeworkHelper();
                }

                return instance;
            }
        }

        private static Regex fileNameFromPart = new Regex(@"(?<=\d_).+(?=\.)");

        [STAThread]
        private static void Main()
        {
            helper.Console.Setup();

            helper.Console.PrintHeading("Assembling Files");

            Console.WriteLine("Press a key to select a file from the files to merge");
            Console.ReadKey(true);

            var dlgOpen = new OpenFileDialog();
            dlgOpen.Title = "Select a file from the files to merge";

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                List<string> filesForAssebly = GetPartialFiles(dlgOpen.FileName);

                var dlgFolder = new FolderBrowserDialog();
                dlgFolder.Description = "Select Destination Directory";

                string outputFolder = "../../";

                if (dlgFolder.ShowDialog() == DialogResult.OK)
                {
                    outputFolder = dlgFolder.SelectedPath;
                }

                AssebleParts(filesForAssebly, outputFolder, MergeToOutput);

                helper.Console.PrintColorText("File Assembled", ConsoleColor.DarkGreen);
            }
            else
            {
                helper.Console.PrintColorText("\nError you dindn't select a folder\n", ConsoleColor.Red);
            }

            helper.Console.Restart(Main);
        }

        public static void AssebleParts(
            List<string> filesForAssebly, string outputFolder, Action<Stream, byte[], string> MergeCallback)
        {
            int bufferSize = 4096;

            Regex filenamePattern = fileNameFromPart;
            string filename = filenamePattern.Match(filesForAssebly[0]).Value,
                filePath = string.Format("{0}\\{1}", outputFolder, filename);

            using (var outputStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                byte[] buffer = new byte[bufferSize];

                int pieceNumber = 1;
                foreach (string file in filesForAssebly)
                {
                    Console.WriteLine("Merging piece number {0}", pieceNumber);
                    MergeCallback(outputStream, buffer, file);

                    pieceNumber++;
                }
            }      
        }

        public static List<string> GetPartialFiles(string selectedFile)
        {
            var fileInfo = new FileInfo(selectedFile);
            var dirInfo = fileInfo.Directory;

            Regex pattern = fileNameFromPart;
            string searchPattern = "*" + pattern.Match(selectedFile).Value + "*";

            return dirInfo.GetFiles(searchPattern, SearchOption.TopDirectoryOnly)
                .OrderBy(info => info.CreationTime)
                .Select(info => info.FullName)
                .ToList();
        }

        private static void MergeToOutput(Stream outputStream, byte[] buffer, string file)
        {
            using (var inputSteam = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                int bytesCount;
                while ((bytesCount = inputSteam.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, bytesCount);
                }
            }
        }
    }
}