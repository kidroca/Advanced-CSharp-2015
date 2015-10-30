namespace SoftUni.Homeworks.AdvancedCSharp.Streams
{
    using System;
    using System.IO;
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
    public class SlicingFile
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

            helper.Console.PrintHeading("Slicing File");

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

                SliceFile(dlgOpen.FileName, pieces, outputFolder, CreatePiece);
                helper.Console.PrintColorText("\nSuccessfully Sliced\n", ConsoleColor.DarkGreen);
            }
            else
            {
                helper.Console.PrintColorText("\nError you didn't select a file\n", ConsoleColor.Red);
            }

            helper.Console.Restart(Main);
        }

        public static void SliceFile(string pathToFile, int pieces, string outputFolder, Action<Stream, byte[], string, long> CreatePieceCallback)
        {
            int bufferSize = 4096;

            var inputStream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);

            long totalSizeInBytes = inputStream.Length,
                pieceLength = (totalSizeInBytes / pieces) + 1;

            byte[] buffer = new byte[bufferSize];

            using (inputStream)
            {
                string fileName = helper.GetFileNameAndExtension(pathToFile);

                for (int i = 1; i <= pieces; i++)
                {
                    Console.WriteLine("Creating piece number {0}", i);
                    string currentPiecePath =
                        string.Format("{0}\\Piece_{1}_{2}.part", outputFolder, i, fileName);

                    CreatePieceCallback(inputStream, buffer, currentPiecePath, pieceLength);
                }
            }
        }

        private static void CreatePiece(
            Stream inputStream, byte[] buffer, string filePath, long pieceLength)
        {           
            using (var outputStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                int bytesCount;

                while (outputStream.Position < pieceLength && 
                    (bytesCount = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, bytesCount);
                }
            }    
        }
    }
}