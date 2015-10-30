namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using HomeworkHelpers;

    /// <summary>
    /// Traverse a given directory for all files with the given extension. Search through the
    /// first level of the directory only and write information about each found file in report.txt.
    /// 
    /// The files should be grouped by their extension.Extensions should be ordered by the count of
    /// their files (from most to least). If two extensions have equal number of files, order them by name.
    /// 
    /// Files under an extension should be ordered by their size.
    /// 
    /// report.txt should be saved on the Desktop. Ensure the desktop path is always valid, regardless
    /// of the user.
    /// </summary>
    public class DirectoryTraversal
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

        [STAThread]
        private static void Main()
        {
            helper.Console.Setup();

            helper.Console.PrintHeading("Directory Traversal ");

            string directoryPath = GetDirectory();

            var groups = GetDirectoryFiles(directoryPath);

            SaveResult(groups);

            helper.Console.PrintColorText("\nCompleted Successfully - See result at desktop\\report.txt", ConsoleColor.DarkGreen);

            helper.Console.Restart(Main);
        }

        private static void SaveResult(Dictionary<string, List<FileInfo>> orderedGroups)
        {
            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string filename = string.Format("{0}\\report.txt", pathToDesktop);

            using (var outputStream = new StreamWriter(filename, false, Encoding.UTF8))
            {
                WriteFileInformation(outputStream, orderedGroups);
            }
        }

        public static Dictionary<string, List<FileInfo>> GetDirectoryFiles(string directoryPath)
        {
            var dirInfo = new DirectoryInfo(directoryPath);

            return dirInfo
                .GetFiles()
                .OrderBy(file => helper.GetFileExtension(file.Name))
                .ThenBy(file => file.Length)
                .GroupBy(file => helper.GetFileExtension(file.Name))
                .OrderByDescending(group => group.Count())
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        public static string GetDirectory()
        {
            Console.Write("Type path to directory or press enter to select from a menu: ");

            var dlgOpen = new FolderBrowserDialog();
            dlgOpen.Description = "Select a folder to traverse";

            string input = Console.ReadLine();
            while (!Directory.Exists(input))
            {
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    input = dlgOpen.SelectedPath;
                }
                else
                {
                    helper.Console.PrintColorText("Invalid path, try again: ", ConsoleColor.Red);
                    input = Console.ReadLine();
                }            
            }

            return input;
        }

        public static void WriteFileInformation(
            StreamWriter output, Dictionary<string, List<FileInfo>> orderedGroups, string leadingChars = "")
        {
            foreach (var key in orderedGroups.Keys)
            {
                output.WriteLine("{0}.{1}", leadingChars, key);
                foreach (var fileInfo in orderedGroups[key])
                {
                    output.WriteLine(
                        "{0}--> {1} - {2:F3}kb"
                        , leadingChars
                        , fileInfo.Name
                        , helper.ConvertFileLength(fileInfo.Length, FileLength.KB));
                }
            }
        }
    }
}