using System;
using System.Collections.Generic;

namespace Extractor
{
    [Serializable] public class Profile
    {
        public string Name { get; }
        public List<string> ToolIds { get; }

        public Profile(string name, List<string> toolIds = null)
        {
            Name = name;
            ToolIds = toolIds ?? new List<string>();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Profile)) return false;
            return Name == ((Profile)obj).Name;
        }
        public override int GetHashCode() => Name.GetHashCode();
    }
}