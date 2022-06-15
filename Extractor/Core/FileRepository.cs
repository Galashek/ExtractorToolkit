using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Extractor.Entities;
using Extractor.Service;
using static Extractor.Service.Logger;
using static Extractor.Program;
using FileInfo = Extractor.Entities.FileInfo;

namespace Extractor.Core
{
    class FileRepository
    {
        private static Dictionary<string, Application> applications;
        public List<Application> Applications => applications.Values.ToList();
        public List<string> ApplicationNames => applications.Keys.OrderBy(x => x).ToList();
        public bool Contains(string appName) => applications.ContainsKey(appName);
        public VersionList Versions { get; }
        
        public FileRepository()
        {
            applications = ReadRepo();
            Versions = ReadVersions();
        }
        public void AddApplication(string appName, Executable file, string src)
        {
            var app = new Application(appName, file);
            //app.AddFile(file);
            LoadExecutableFile(file, src);
            applications[app.Name] = app;
            Update();
        }
        public void RemoveApplication(string appName)
        {
            foreach (var file in GetApp(appName).Files)
            {
                foreach (var req in file.RequiredFiles)
                {
                    RemoveRequiredFile(req);
                }
                RemoveExecutableFile(file);
            }
            applications.Remove(appName);
            Update();
        }
        public Application GetApp(string appName)
        {
            if (!applications.ContainsKey(appName))
                throw new Exception($"Приложение \"{appName}\" не найдено");
            return applications[appName];
        }
        public void AddFileToApp(string appName, Executable file, string src)
        {
            GetApp(appName).AddFile(file);
            LoadExecutableFile(file, src);
            Update();
        }
        public void RemoveFileFromApp(string appName, Executable file)
        {
            GetApp(appName).RemoveFile(file);
            foreach (var req in file.RequiredFiles)
                RemoveRequiredFile(req);
            RemoveExecutableFile(file);
            Update();
        }
        public void ChangeFileVersions(string appName, Executable file, VersionList versions)
        {
            GetApp(appName).ChangeFileVersions(file, versions);
            Update();
        }

        private void LoadExecutableFile(Executable file, string srcPath)
        {
            var destPath = Path.Combine(FilesDir, file.FileSource);
            LoadFile(srcPath, destPath);
        }
        public void LoadRequiredFile(FileInfo file, string srcPath)
        {
            var destPath = Path.Combine(FilesReqDir, file.FileSource);
            LoadFile(srcPath, destPath);
        }
        private static void LoadFile(string srcFilePath, string destFilePath)
        {
            if (!File.Exists(srcFilePath))
                throw new Exception("Исходный файл не найден!");
            if (srcFilePath == destFilePath) return;
            Directory.CreateDirectory(Path.GetDirectoryName(destFilePath));
            File.Copy(srcFilePath, destFilePath, true);
        }
        private void RemoveExecutableFile(Executable file)
        {
            RemoveFile(Path.Combine(FilesDir, file.FileSource));
        }
        public void RemoveRequiredFile(FileInfo file)
        {
            RemoveFile(Path.Combine(FilesReqDir, file.FileSource));
        }
        private static void RemoveFile(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }
        public void Update() => WriteRepo();
        
        private static Dictionary<string, Application> ReadRepo()
        {
            try
            {
                using (var stream = new FileStream(FileRepo, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    return (Dictionary<string, Application>)new BinaryFormatter().Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Log($"Ошибка чтения {FileRepo}: {e.Message}");
            }
            return new Dictionary<string, Application>();
        }
        private static void WriteRepo()
        {
            try
            {

                using (var stream = new FileStream(FileRepo, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(stream, applications);
                }
            }
            catch (Exception e)
            {
                Log($"Ошибка записи {FileRepo}: {e.Message}");
            }
        }
        private static VersionList ReadVersions()
        {
            if (!File.Exists(VersionsPath))
                File.Create(VersionsPath);
            return new VersionList(File.ReadAllLines(VersionsPath));
        }
    }
}