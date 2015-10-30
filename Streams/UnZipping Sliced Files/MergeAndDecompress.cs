namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Windows.Forms;
    using HomeworkHelpers;

    /// <summary>
    /// Modify your previous program to also compress the bytes while slicing parts and decompress
    /// them when assembling them back to the original file. Use GzipStream.
    /// 
    /// Tip: When getting files from directory, make sure you only get files with.gz extension(there
    /// might be hidden files).
    /// </summary>
    public class MergeAndDecompress : MergingFiles
    {
        private static StreamHomeworkHelper helper = new StreamHomeworkHelper();

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

                MergingFiles.AssebleParts(filesForAssebly, outputFolder, UnzipAndMerge);

                helper.Console.PrintColorText("File Assembled", ConsoleColor.DarkGreen);
            }
            else
            {
                helper.Console.PrintColorText("\nError you dindn't select a folder\n", ConsoleColor.Red);
            }

            helper.Console.Restart(Main);
        }

        private static void UnzipAndMerge(Stream output, byte[] buffer, string partPath)
        {
            using (var inputStream = new FileStream(partPath, FileMode.Open, FileAccess.Read))
            {
                using (var decompStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    int bytesCount;
                    while ((bytesCount = decompStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, bytesCount);
                    }
                }
            }
        }
    }
}