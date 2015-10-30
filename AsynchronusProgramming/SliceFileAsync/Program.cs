namespace SliceFileAsync
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using System.Windows.Forms;

    class Program
    {
        static List<Task> tasks = new List<Task>();

        [STAThread]
        static void Main()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to slice";
            string filePath;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            else
            {
                throw new ApplicationException("No file selected");
            }

            string resultDirectory = "../../../SLices";

            var info = new FileInfo(filePath);

            var sw = new Stopwatch();

            Console.WriteLine("Starting to slice the file");
            sw.Start();
            SliceFileAsynchronous(info, 5, resultDirectory);
            Console.WriteLine("All threads sent... waiting");

            while (tasks.Any(t => t.IsCompleted != true))
            {
                Console.WriteLine("Wainting tasks execution, write me something");
                Console.ReadLine();
            }

            Console.WriteLine("Completed... {0}", sw.Elapsed);
        }

        private static void SliceFileAsynchronous(FileInfo info, int pieces, string resultDirectory = null)
        {
            if (resultDirectory == null)
            {
                resultDirectory = info.Directory.FullName;
            }
            else if (!Directory.Exists(resultDirectory))
            {
                Directory.CreateDirectory(resultDirectory);
            }

            long pieceLength = GetPieceLength(info, pieces);

            long position = 0;
            for (int i = 1; i <= pieces; i++)
            {
                // Because the loop runs faster than task are sent working there is a 
                // chance the task will received the incorect values of the after they are
                // increased in the loop, so a local scope variable can be introduced to help
                // with this problem
                long currentPosition = position;
                int currentPieceNumber = i;
                var task = Task.Run(() => SliceFile(
                    info, resultDirectory, currentPosition, pieceLength, currentPieceNumber));
                tasks.Add(task);

                position += pieceLength;
            }
        }

        private static void SliceFile(
            FileInfo info, string resultDirectory, long position, long pieceLength, int pieceNumber)
        {
            var buffer = new byte[4096];
            int readLength = buffer.Length;
            long endPosition = position + pieceLength;

            if (endPosition > info.Length)
            {
                endPosition = info.Length;
            }

            using (var fileReader = File.OpenRead(info.FullName))
            {
                var fileWriter = File.OpenWrite(
                    string.Format("{0}/{1}.{2}{3}", resultDirectory, info.Name, "part", pieceNumber));

                fileReader.Position = position;
                using (fileWriter)
                {
                    while (fileReader.Position < endPosition)
                    {
                        if (fileReader.Position + readLength > endPosition)
                        {
                            readLength = (int)(endPosition - fileReader.Position);
                        }

                        fileReader.Read(buffer, 0, readLength);
                        fileWriter.Write(buffer, 0, readLength);
                    }
                }
            }
        }

        private static long GetPieceLength(FileInfo info, int pieces)
        {
            long fileLength = info.Length;
            long pieceLength = (info.Length / pieces) + 1;

            return pieceLength;
        }
    }
}
