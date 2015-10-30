namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.Collections.Generic;
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

            helper.Console.PrintHeading("Copy Binary File");

            string pathToDesiredFile = helper.ReadPathToFile("to be copied");

            string destinationPath = string.Format("../../COPY_{0}", helper.GetFileNameAndExtension(pathToDesiredFile));

            List<byte> binaryData = ReadFile(pathToDesiredFile);

            CreateNewFile(binaryData.ToArray(), destinationPath);

            helper.Console.PrintColorText(
                "\nSuccess - File copied to the root of the project: "
                , ConsoleColor.DarkGreen);

            helper.Console.PrintColorText
                (string.Format("{0}\n", new DirectoryInfo("../../").FullName), ConsoleColor.Blue);

            helper.Console.Restart(Main);
        }

        private static void CreateNewFile(byte[] binaryData, string path)
        {
            var fileStream = new FileStream(path, FileMode.Create);

            try
            {
                fileStream.Write(binaryData, 0, binaryData.Length);
            }
            finally
            {
                fileStream.Close();
            }
        }

        private static List<byte> ReadFile(string path)
        {
            var readData = new List<byte>();

            var fileStream = new FileStream(path, FileMode.Open);

            try
            {
                int readBytes = 1;
                while (readBytes > 0)
                {
                    byte[] currentBlock = new byte[1024];
                    readBytes = fileStream.Read(currentBlock, 0, currentBlock.Length);

                    readData.AddRange(currentBlock);
                }
            }
            finally
            {
                fileStream.Close();
            }

            return readData;
        }
    }
}