namespace HomeworkHelpers
{
    using System;
    using System.IO;
    using System.Security.AccessControl;
    using System.Text.RegularExpressions;

    public class StreamHomeworkHelper
    {
        private ConsoleHelper console;

        public ConsoleHelper Console
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
            System.Console.Write("Enter the path to the file {0}", continuation);
            this.Console.PrintColorText(
                "(You can drag and drop here): ",
                ConsoleColor.DarkCyan);

            string pathToDesiredFile = string.Empty;

            bool fileExists = false;
            while (!fileExists)
            {
                pathToDesiredFile = this.Console.ReadInColor(ConsoleColor.DarkBlue);
                pathToDesiredFile = Regex.Replace(pathToDesiredFile, "\"", string.Empty);

                fileExists = File.Exists(pathToDesiredFile);

                if (!fileExists)
                {
                    this.Console.PrintColorText("Invalid path! Try Again: ", ConsoleColor.DarkRed);
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
            var fileNameExtractor = new Regex(@"(?<=\/|^|\\)(?!.*(?:\/|\\)).+(?=\.)", RegexOptions.RightToLeft);

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
    }
}
