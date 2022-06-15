using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using static Extractor.Program;
using static Extractor.Service.Logger;

namespace Extractor.Core
{
    public class ToolManager
    {
        private readonly Dictionary<string, Tool> tools;
        private readonly SortedSet<string> categories;
        public List<Tool> Tools => tools.Values.ToList();
        public List<Tool> GetToolsByProfile(Profile profile) => profile.ToolIds.Select(GetToolById).Where(t => t != null).ToList();
        public List<Tool> GetToolsByCategory(string category) => tools.Values.Where(t => t.Category == category).ToList();
        public List<string> Categories => categories.ToList();

        public ToolManager()
        {
            tools = new Dictionary<string, Tool>();
            foreach (var tool in Deserialize())
                tools[tool.Id] = tool;
            categories = new SortedSet<string>(ReadCategories());
        }
        public Tool GetToolById(string id)
        {
            return tools.ContainsKey(id) ? tools[id] : null;
        }
        public void AddCategory(string category)
        {
            if (categories.Add(category))
                WriteCategories(categories.ToArray());
        }
        public void RemoveCategory(string category)
        {
            if (categories.Remove(category))
                WriteCategories(categories.ToArray());
        }
        public void SaveOrUpdate(Tool tool)
        {
            tools[tool.Id] = tool;
            Serialize(tool);
        }
        public void Remove(Tool tool)
        {
            tools.Remove(tool.Id);
            var file = Path.Combine(ToolsDir, $"{tool.Id}.dat");
            if (File.Exists(file))
                File.Delete(file);
        }
        private static void Serialize(Tool tool)
        {
            try
            {
                using (var stream = new FileStream(Path.Combine(ToolsDir, $"{tool.Id}.dat"), 
                    FileMode.OpenOrCreate, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(stream, tool);
                }
            }
            catch (Exception e)
            {
                Log($"Ошибка чтения инструментов: {e.Message}");
            }
        }
        public static List<Tool> Deserialize()
        {
            var list = new List<Tool>();
            try
            {
                var formatter = new BinaryFormatter();
                foreach (var file in Directory.GetFiles(ToolsDir, "*.dat"))
                {
                    using (var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        list.Add((Tool)formatter.Deserialize(stream));
                    }
                }
            }
            catch (Exception e)
            {
                Log($"Ошибка записи инструментов: {e.Message}");
            }
            return list;
        }
        public static List<string> ReadCategories()
        {
            var file = CategoriesFile;
            if (!File.Exists(file))
                File.Create(file);
            return File.ReadAllLines(file).ToList();
        }
        public static void WriteCategories(string[] categories)
        {
            var file = CategoriesFile;
            if (!File.Exists(file))
                File.Create(file);
            File.WriteAllLines(file, categories);
        }
    }
}