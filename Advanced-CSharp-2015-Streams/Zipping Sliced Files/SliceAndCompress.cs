namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{ 
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using HomeworkHelpers;

    /// <summary>
    /// Modify your previous program to also compress the bytes while slicing parts and decompress
    /// them when assembling them back to the original file. Use GzipStream.
    /// 
    /// Tip: When getting files from directory, make sure you only get files with.gz extension(there
    /// might be hidden files).
    /// </summary>
    public class SliceAndCompress : SlicingFile
    {
        private static StreamHomeworkHelper helper = new StreamHomeworkHelper();

        private static Regex fileNameFromPart = new Regex(@"(?<=\d_).+(?=\.)");

        [STAThread]
        private static void Main()
        {
            helper.Console.Setup();

            helper.Console.PrintHeading("Zipping Sliced Files");

            Console.Write("To how many pieces to slice: ");

            int pieces = int.Parse(
                helper.Console.ReadInColor(ConsoleColor.DarkBlue));

            var dlgOpen = new OpenFileDialog();
            dlgOpen.Title = "Select the file to Slice up";

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                var dlgFolder = new FolderBrowserDialog();
                dlgFolder.Description = "Select Destination Directory";

                string outputFolder = "../../";

                if (dlgFolder.ShowDialog() == DialogResult.OK)
                {
                    outputFolder = dlgFolder.SelectedPath;
                }

                SlicingFile.SliceFile(dlgOpen.FileName, pieces, outputFolder, CreateZippedPiece);
                helper.Console.PrintColorText("\nSuccessfully Sliced\n", ConsoleColor.DarkGreen);
            }
            else
            {
                helper.Console.PrintColorText("\nError you didn't select a file\n", ConsoleColor.Red);
            }

            helper.Console.Restart(Main);
        }

        public static void CreateZippedPiece(Stream inputStream, byte[] buffer, string path, long pieceLength)
        {
            using (var outputStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                using (var compresorStream = new GZipStream(outputStream, CompressionLevel.Optimal))
                {
                    long endPosition = inputStream.Position + pieceLength;
                    int bytesCount;

                    while (inputStream.Position < endPosition &&
                        (bytesCount = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        compresorStream.Write(buffer, 0, bytesCount);
                    }
                }
            }
        }
    }
}