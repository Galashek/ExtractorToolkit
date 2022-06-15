using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Extractor
{
    [Serializable] public class VersionList : IEnumerable<string>
    {
        HashSet<string> versions;
        public VersionList() => versions = new HashSet<string>();
        public VersionList(IEnumerable<string> versions)
        {
            this.versions = versions.ToHashSet();
        }
        public bool Contains(string version) => versions.Contains(version);
        public int Count => versions.Count;
        public override string ToString() => string.Join(", ", versions);
        public IEnumerator<string> GetEnumerator() => 
            versions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}