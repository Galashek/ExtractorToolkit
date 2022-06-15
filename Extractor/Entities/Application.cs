using System;
using System.Collections.Generic;
using System.Linq;
using Extractor.Entities;

namespace Extractor.Service
{
    [Serializable] class Application
    {
        public readonly string Name;
        private Dictionary<string, Executable> files;
        public List<Executable> Files => files.Values.ToList();
        public VersionList SupportedSystems { get; private set; }
        public bool Supports(string version) => SupportedSystems.Contains(version);

        public Application(string name, Executable file)
        {
            Name = name;
            files = new Dictionary<string, Executable>{[file.Id] = file};
        }
        public void AddFile(Executable file)
        {
            files[file.Id] = file;
            UpdateSupportedSystems();
        }
        public void RemoveFile(Executable file)
        {
            files.Remove(file.Id);
            UpdateSupportedSystems();
        }
        public void ChangeFileVersions(Executable file, VersionList versions)
        {
            files[file.Id].Versions = versions;
            UpdateSupportedSystems();
        }
        private void UpdateSupportedSystems()
        {
            SupportedSystems = new VersionList(
                files.Values.SelectMany(f => f.Versions));
        }
        public Executable GetFileByVersion(string version) =>
            files.Values.FirstOrDefault(x => x.Versions.Contains(version));
        public override bool Equals(object obj)
        {
            if (!(obj is Application)) return false;
            return Name == ((Application)obj).Name;
        }
        public override int GetHashCode() => Name.GetHashCode();
    }
}