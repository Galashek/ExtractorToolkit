using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Runner
{
    class Extractor
    {
        private static Logger logger;
        private const string OutputFilenamePlaceholder = "%OUTFILE%";
        private static string FilesDir;
        private static string OutputDir;

        static void Main()
        {
            try
            {
                var version = GetOsVersion();
                if (version == "Other")
                    throw new Exception("Данная версия Windows не поддерживается");

                var cur = Environment.CurrentDirectory;
                var runlist = Path.Combine(cur, $"run_{version}.txt");
                if (!File.Exists(runlist))
                    throw new Exception("Не найден список запуска для текущей версии Windows");

                FilesDir = Path.Combine(Path.Combine(cur, "files"), version);
                OutputDir = Path.Combine(cur, $"out-{version}-{DateTime.Now.ToString().Trim(':', ' ', '.')}");

                logger = new Logger(OutputDir);
            
                using (var reader = new StreamReader(runlist, Encoding.Default))
                {
                    while (!reader.EndOfStream)
                    {
                        var app = ReadToolEntry(reader.ReadLine());
                        if (app == null) continue;
                        PrintTitle(app.Name);
                        RunApplication(app);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private static AppInfo ReadToolEntry(string entry)
        {
            var props = entry.Split(';');
            if (props.Length < 5) return null;
            var name = props[0];
            var fileName = props[1];
            var arguments = props[2];
            var category = props[3];
            var id = props[4];

            var filePath = Path.Combine(FilesDir, fileName);

            var outputSubdir = Path.Combine(OutputDir, category == string.Empty ? "Other" : category);
            if (!Directory.Exists(outputSubdir))
                Directory.CreateDirectory(outputSubdir);

            var outputFilePath = Path.Combine(outputSubdir, $"{id}.txt");

            var outputToFile = arguments.Contains(OutputFilenamePlaceholder);
            if (outputToFile)
            {
                arguments = arguments.Replace(OutputFilenamePlaceholder, $"\"{outputFilePath}\"");
            }

            return new AppInfo
            {
                Path = filePath,
                Name = name,
                FileName = fileName,
                Arguments = arguments,
                Category = category,
                Id = id,
                OutputToFile = outputToFile,
                OutputFilePath = outputFilePath
            };
        }

        private static void RunApplication(AppInfo app)
        {
            try
            {
                if (!File.Exists(app.Path))
                    throw new Exception($"Файл не найден: {app.Path}");

                var startinfo = new ProcessStartInfo
                {
                    FileName = app.Path,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding =
                        Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage),
                    Arguments = app.Arguments
                };
                var process = new Process {StartInfo = startinfo};
                process.Start();
                Process.GetProcessById(process.Id);

                if (!app.OutputToFile)
                    logger.Write(app, process.StandardOutput);
                process.Close();
            }
            catch (Exception e)
            {
                var error = e.Message + e.InnerException;
                logger.WriteError(app, error);
            }
        }

        private static void PrintTitle(string name)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\t" + name);
            Console.WriteLine(new string('-', 50));
        }
        private static string GetOsVersion()
        {
            var version = Environment.OSVersion.Version;
            switch (version.Major)
            {
                case 5:
                    return version.Minor == 0 ? "Other" : "WinXP";
                case 6:
                    switch (version.Minor)
                    {
                        case 0:
                            return "WinVista";
                        case 1:
                            return "Win7";
                        case 2:
                            return "Win8";
                        case 3:
                            return "Win8";
                        default:
                            return "Other";
                    }
                case 10:
                    return version.Build >= 22000 ? "Win11" : "Win10";
                default:
                    return "Other";
            }
        }
    }
}