namespace HomeworkHelpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class StreamHomeworkHelper
    {
        private ConsoleHelper console;

        public ConsoleHelper ConsoleMio
        {
            get
            {
                if (this.console == null)
                {
                    this.console = new ConsoleHelper();
                }

                return this.console;
            }
        }

        public string ReadPathToFile(string continuation)
        {
            Console.Write("Enter the path to the file {0}", continuation);
            this.ConsoleMio.PrintColorText(
                "(You can drag and drop here): ",
                ConsoleColor.DarkCyan);

            string pathToDesiredFile = string.Empty;

            bool fileExists = false;
            while (!fileExists)
            {
                pathToDesiredFile = this.ConsoleMio.ReadInColor(ConsoleColor.DarkBlue);
                pathToDesiredFile = Regex.Replace(pathToDesiredFile, "\"", string.Empty);

                fileExists = File.Exists(pathToDesiredFile);

                if (!fileExists)
                {
                    this.ConsoleMio.PrintColorText("Invalid path! Try Again: ", ConsoleColor.DarkRed);
                }
            }

            return pathToDesiredFile;
        }

        public string GetFileNameAndExtension(string pathToFile)
        {
            var fileNameExtractor = new Regex(@"(?<=\/|^|\\)(?!.*(?:\/|\\)).+$", RegexOptions.RightToLeft);

            return fileNameExtractor.Match(pathToFile).Value;
        }

        public string GetFileName(string pathToFile)
        {
            var fileNameExtractor = new Regex(
                @"(?<=\/|^|\\)(?!.*(?:\/|\\)).+?(?=\.|$)");

            return fileNameExtractor.Match(pathToFile).Value;
        }

        public string GetFileExtension(string pathToFile)
        {
            var fileExtensionExtractor = new Regex(@"(?<=\.)(?!.+\.).+$", RegexOptions.RightToLeft);

            return fileExtensionExtractor.Match(pathToFile).Value;
        }

        public double ConvertFileLength(long lengthInBytes, FileLength converTo)
        {
            const double Factor = 1024;

            switch (converTo)
            {
                case FileLength.KB:
                    return lengthInBytes / Factor;
                case FileLength.MB:
                    return lengthInBytes / Math.Pow(2, Factor);
                default:
                    return lengthInBytes;
            }
        }

        public string SelectSaveLocation(string filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*")
        {
            Console.WriteLine("Press a key to select a save location from a menu.");
            Console.ReadKey(true);

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select a place to save";
            saveFileDialog.Filter = filter;

            this.PromptDialog(saveFileDialog);

            return saveFileDialog.FileName;
        }

        public string SelectFileToOpen(string filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*")
        {
            Console.WriteLine("Press a key to select a file from a menu.");
            Console.ReadKey(true);

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to open";
            openFileDialog.Filter = filter;

            this.PromptDialog(openFileDialog);

            return openFileDialog.FileName;
        }

        public string GetDirectory()
        {
            Console.Write("Type path to directory or press enter to select from a menu: ");

            var selectFolderDialog = new FolderBrowserDialog();
            selectFolderDialog.Description = "Select a folder";
            selectFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;

            string input = Console.ReadLine();
            while (!Directory.Exists(input))
            {
                if (selectFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    input = selectFolderDialog.SelectedPath;
                }
                else
                {
                    this.ConsoleMio.PrintColorText("Invalid path, try again: ", ConsoleColor.Red);
                    input = Console.ReadLine();
                }
            }

            return input;
        }

        private void PromptDialog(FileDialog dialog)
        {
            // Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.InitialDirectory = Directory.GetParent("../../").FullName; 

            while (dialog.ShowDialog() != DialogResult.OK)
            {
                Console.WriteLine("You have to select a file, try again.");
            }
        }

        /// <summary>
        /// Extract files from directory and sorts them by size and name
        /// </summary>
        /// <param name="dirInfo">directory to work in</param>
        /// <returns>
        /// Dictionary of key - the file extension and value - list of files under that extension
        /// </returns>
        public Dictionary<string, List<FileInfo>> GetDirectoryFiles(DirectoryInfo dirInfo)
        {
            return dirInfo
                .GetFiles()
                .OrderBy(file => this.GetFileExtension(file.Name))
                .ThenBy(file => file.Length)
                .GroupBy(file => this.GetFileExtension(file.Name))
                .OrderByDescending(group => group.Count())
                .ToDictionary(group => group.Key, group => group.ToList());
        }
    }
}
