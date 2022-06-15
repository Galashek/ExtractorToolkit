using System;
using System.Collections.Generic;

namespace Extractor.Entities
{
    [Serializable] class Executable : FileInfo
    {
        public VersionList Versions { get; set; }
        public List<FileInfo> RequiredFiles;

        public Executable(string filename, VersionList versions) : base(filename)
        {
            Versions = versions;
            RequiredFiles = new List<FileInfo>();
        }
    }
}