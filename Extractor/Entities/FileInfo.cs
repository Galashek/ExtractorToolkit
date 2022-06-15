using System;

namespace Extractor.Entities
{
    [Serializable] class FileInfo
    {
        public string Id { get; }
        public string FileSource => Id;
        public string FileName { get; }

        public FileInfo(string filename)
        {
            Id = Guid.NewGuid().ToString();
            FileName = filename;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FileInfo)) return false;
            var other = (FileInfo)obj;
            return Id == other.Id;
        }
        public override int GetHashCode() => Id.GetHashCode();
    }
}