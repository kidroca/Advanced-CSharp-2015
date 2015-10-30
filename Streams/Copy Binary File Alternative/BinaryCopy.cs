namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.IO;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that copies the contents of a binary file (e.g. image, video,
    /// etc.) to another using FileStream. You are not allowed to use the File class
    /// or similar helper classes.
    /// </summary>
    internal class BinaryCopy
    {
        private static StreamHomeworkHelper helper = new StreamHomeworkHelper();

        private static void Main()
        {
            helper.Console.Setup();

            helper.Console.PrintHeading("Copy Binary File (Alternative)");

            string pathToDesiredFile = helper.ReadPathToFile("to be copied");

            string destinationPath = string.Format("../../COPY_{0}.part", helper.GetFileNameAndExtension(pathToDesiredFile));

            CopyFile(pathToDesiredFile, destinationPath);

            helper.Console.PrintColorText(
                "\nSuccess - File copied to the root of the project: "
                , ConsoleColor.DarkGreen);

            helper.Console.PrintColorText
                (string.Format("{0}\n", new DirectoryInfo("../../").FullName), ConsoleColor.Blue);

            helper.Console.Restart(Main);
        }

        private static void CopyFile(string path, string destination)
        {
            var originalStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var copyStream = new FileStream(destination, FileMode.Create, FileAccess.Write);

            using (originalStream)
            {
                using (copyStream)
                {
                    originalStream.CopyTo(copyStream);
                }
            }
        }
    }
}