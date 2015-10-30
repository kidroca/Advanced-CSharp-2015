namespace HomeworkHelpers.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Implemented Template Method Pattern :)
    /// </summary>
    public abstract class DirectoryTraverse
    {
        protected StreamHomeworkHelper helper = new StreamHomeworkHelper();
        
        public void TraverseFolder(DirectoryInfo dirInfo)
        {
            string directoryName = dirInfo.Name,
                directoryPath = dirInfo.FullName;

            Dictionary<string, List<FileInfo>> directoryFiles = helper.GetDirectoryFiles(dirInfo);

            WriteDirectoryToDocument(directoryName, directoryPath);
            WriteFileInformation(directoryFiles);
                       
            foreach (var dir in dirInfo.GetDirectories())
            {
                try
                {
                    TraverseFolder(dir);
                }
                catch (UnauthorizedAccessException ex)
                {
                    LogUnauthorizedAccess(dir.FullName, ex);
                }
                catch (PathTooLongException ex)
                {
                    LogPathToLoog(dir.FullName, ex);
                }
            }

            CloseCurrentDirectory(directoryName);
        }

        protected abstract void WriteDirectoryToDocument(string directoryName, string directoryPath);

        protected abstract void WriteFileInformation(Dictionary<string, List<FileInfo>> directoryFiles);

        protected abstract void CloseCurrentDirectory(string directoryName);

        protected abstract void LogPathToLoog(string fullName, PathTooLongException ex);

        protected abstract void LogUnauthorizedAccess(string fullName, UnauthorizedAccessException ex);
    }
}
