namespace AssembleSlicesAsync
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    class Program
    {
        [STAThread]
        static void Main()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file from the files to assemble";
            openFileDialog.InitialDirectory = new DirectoryInfo("../../../").FullName;

            string filePath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            else
            {
                throw new ApplicationException("No file selected");
            }

            string slicesDirectroy = new FileInfo(filePath).DirectoryName;

            List<FileInfo> files = Directory
                .GetFiles(slicesDirectroy)
                .OrderBy(name => name)
                .Select(file => new FileInfo(file))
                .ToList();

            openFileDialog.Title = "Select a directroy to save the assembled file";

            string outputDirectory;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            else
            {
                throw new ApplicationException("No file selected");
            }

            Console.WriteLine("Assembling Files...");
            var sw = new Stopwatch();
            sw.Start();
            Task asyncTask = AssembleAsync(files, "../../../Assembled");

            int left = Console.CursorLeft;
            double currentSecond = sw.Elapsed.TotalSeconds;

            while (!asyncTask.IsCompleted)
            {
                if (sw.Elapsed.TotalSeconds >= currentSecond + 1)
                {
                    currentSecond = sw.Elapsed.TotalSeconds;
                    Console.Write("{0}", Math.Floor(currentSecond));
                    Console.CursorLeft = left;
                }
            }
            
            Console.WriteLine("Completed... {0}", sw.Elapsed);
        }

        private async static Task AssembleAsync(List<FileInfo> files, string outputDir)
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            await AssembleAll(files, outputDir);
        }

        private static Task AssembleAll(List<FileInfo> files, string outputDir)
        {
            string fileName = files.First().Name;

            string outputPath = 
                string.Format(
                    "{0}/{1}"
                    , outputDir
                    , fileName.Substring(0, fileName.IndexOf("part")));

            var task = Task.Run(() =>
            {
                using (var destinationStream = File.OpenWrite(outputPath))
                {
                    foreach (var fInfo in files)
                    {
                        using (var sourceStream = File.OpenRead(fInfo.FullName))
                        {
                            sourceStream.CopyTo(destinationStream);
                        }
                    }
                }
            });

            return task;
        }
    }
}
