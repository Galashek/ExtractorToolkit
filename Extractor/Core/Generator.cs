using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Extractor.Service.Logger;
using static Extractor.Program;
using Message = Extractor.Service.Message;

namespace Extractor.Core
{
    public class Generator
    {
        private const string MainFolderName = "Extractor";
        private const string FilesFolderName = "files";

        public void Generate(string path, Profile profile, VersionList versions, IProgress<int> progress)
        {
            var mainDirPath = Path.Combine(path, MainFolderName);
            var filePath = Path.Combine(mainDirPath, FilesFolderName);

            Directory.CreateDirectory(mainDirPath);
            Directory.CreateDirectory(filePath);

            foreach (var src in File.ReadAllLines(ExtractorSrcList))
            {
                var srcPath = Path.Combine(SourceDir, src);
                var destPath = Path.Combine(mainDirPath, src);
                File.Copy(srcPath, destPath, true);
            }

            var tools = Program.ToolManager.GetToolsByProfile(profile);

            var skip = new List<string>();
            var i = 1;
            foreach (var version in versions)
            {
                var runlist = Path.Combine(mainDirPath, $"run_{version}.txt");
                var versionPath = Path.Combine(filePath, version);
                Directory.CreateDirectory(versionPath);
                using (var writer = new StreamWriter(runlist, false, Encoding.Default))
                {
                    foreach (var tool in tools)
                    {
                        var app = Program.FileRepository.GetApp(tool.AppName);

                        if (!app.Supports(version))
                        {
                            skip.Add($"{tool.ToolName} для {version}");
                            continue;
                        }
                        var bin = app.GetFileByVersion(version);
                        var srcPath = Path.Combine(FilesDir, bin.FileSource);
                        var destPath = Path.Combine(versionPath, bin.FileName);
                        File.Copy(srcPath, destPath, true);

                        foreach (var req in bin.RequiredFiles)
                        {
                            var reqSrcPath = Path.Combine(FilesReqDir, req.FileSource);
                            var reqDestPath = Path.Combine(versionPath, req.FileName);
                            Directory.CreateDirectory(Path.GetDirectoryName(reqDestPath));
                            File.Copy(reqSrcPath, reqDestPath, true);
                        }
                        writer.WriteLine(string.Join(";",
                            tool.ToolName,
                            bin.FileName,
                            tool.Arguments,
                            tool.Category,
                            tool.Id));
                    }
                }
                progress?.Report(i++ * 100 / versions.Count);
            }

            if (skip.Count > 0)
            {
                skip.Insert(0, "Несколько инструментов не были загружены:");
                Message.Warning(string.Join(Environment.NewLine, skip));
            }
        }
    }
}