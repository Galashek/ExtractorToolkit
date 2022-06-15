using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using static Extractor.Program;
using static Extractor.Service.Logger;

namespace Extractor.Core
{
    public class ProfileManager
    {
        readonly Dictionary<string, Profile> profiles;
        public List<Profile> Profiles => profiles.Values.ToList();
        public bool Contains(Profile profile) => profiles.ContainsKey(profile.Name);

        public ProfileManager()
        {
            profiles = new Dictionary<string, Profile>();
            foreach (var profile in Deserialize())
                profiles[profile.Name] = profile;
            if (profiles.Count == 0)
                SaveOrUpdate(new Profile("Default"));
        }
        public void SaveOrUpdate(Profile profile)
        {
            profiles[profile.Name] = profile;
            Serialize(profile);
        }
        public void Remove(Profile profile)
        {
            profiles.Remove(profile.Name);
            var file = Path.Combine(ProfilesDir, $"{profile.Name}.dat");
            if (File.Exists(file))
                File.Delete(file);
        }
        private static void Serialize(Profile profile)
        {
            try
            {
                using (var stream = new FileStream(Path.Combine(ProfilesDir, $"{profile.Name}.dat"), 
                    FileMode.OpenOrCreate, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(stream, profile);
                }
            }
            catch (Exception e)
            {
                Log($"Ошибка чтения профилей: {e.Message}");
            }
        }
        public static List<Profile> Deserialize()
        {
            var list = new List<Profile>();
            try
            {
                var formatter = new BinaryFormatter();
                foreach (var file in Directory.GetFiles(ProfilesDir, "*.dat"))
                {
                    using (var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        list.Add((Profile)formatter.Deserialize(stream));
                    }
                }
            }
            catch (Exception e)
            {
                Log($"Ошибка записи профилей: {e.Message}");
            }
            return list;
        }
    }
}