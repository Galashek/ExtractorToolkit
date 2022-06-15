using System;
using System.ComponentModel;

namespace Extractor
{
    [Serializable] public class Tool
    {
        [Browsable(false)] public string Id { get; }
        [DisplayName("Имя")] public string ToolName { get; set; }
        [DisplayName("Приложение")] public string AppName { get; set; }
        [DisplayName("Аргументы")] public string Arguments { get; set; }
        [Browsable(false)] public string Description { get; set; }
        [Browsable(false)] public string Category { get; set; }
        [Browsable(false)] public bool OutputToFile { get; set; }

        public Tool(string id) => Id = id;
        public override bool Equals(object obj)
        {
            if (!(obj is Tool)) return false;
            return Id == ((Tool)obj).Id;
        }
        public override int GetHashCode() => Id.GetHashCode();
    }
}