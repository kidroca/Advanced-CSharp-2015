namespace HomeworkHelpers.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class TraverseUsingXmlWriter : DirectoryTraverse
    {
        private XmlWriter xmlWriter;

        public TraverseUsingXmlWriter(XmlWriter writer)
        {
            this.xmlWriter = writer;
        }

        protected override void CloseCurrentDirectory(string directoryName)
        {
            this.xmlWriter.WriteEndElement();
        }

        protected override void LogPathToLoog(string fullName, PathTooLongException ex)
        {
            this.xmlWriter.WriteStartElement("pathTooLong");
            this.xmlWriter.WriteAttributeString("path", fullName);
            this.xmlWriter.WriteAttributeString("message", ex.Message);
            this.xmlWriter.WriteEndElement();
        }

        protected override void LogUnauthorizedAccess(string fullName, UnauthorizedAccessException ex)
        {
            this.xmlWriter.WriteStartElement("accessDenied");
            this.xmlWriter.WriteAttributeString("path", fullName);
            this.xmlWriter.WriteAttributeString("message", ex.Message);
            this.xmlWriter.WriteEndElement();
        }

        protected override void WriteDirectoryToDocument(string directoryName, string directoryPath)
        {
            this.xmlWriter.WriteStartElement("dir");
            this.xmlWriter.WriteAttributeString("name", directoryName);
            this.xmlWriter.WriteAttributeString("path", directoryPath);
        }

        protected override void WriteFileInformation(Dictionary<string, List<FileInfo>> directoryFiles)
        {
            foreach (var key in directoryFiles.Keys)
            {
                this.xmlWriter.WriteStartElement("extension");
                this.xmlWriter.WriteAttributeString(
                    "type"
                    , string.Format(".{0}", key));

                foreach (var info in directoryFiles[key])
                {
                    this.xmlWriter.WriteStartElement("file");
                    this.xmlWriter.WriteAttributeString("name", helper.GetFileName(info.Name));
                    this.xmlWriter.WriteAttributeString(
                        "size"
                        , string.Format(
                            "{0:F3} kb"
                            , this.helper.ConvertFileLength(info.Length, FileLength.KB)));
                    this.xmlWriter.WriteEndElement();
                }

                this.xmlWriter.WriteEndElement();
            }
        }
    }
}
