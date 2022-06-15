using System;
using System.IO;
using System.Text;

namespace Runner
{
    class Logger
    {
        private readonly string OutputDir;
        private readonly string ResultsFile;

        public Logger(string outputDir)
        {
            OutputDir = outputDir;
            ResultsFile = Path.Combine(OutputDir, "results.txt");
            if (File.Exists(ResultsFile))
                File.Delete(ResultsFile);
        }

        public void Write(AppInfo app, StreamReader reader)
        {
            using (var writer = new StreamWriter(app.OutputFilePath, false, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    writer.WriteLine(line);
                    Console.WriteLine(line);
                }
            }
        }

        public void WriteError(AppInfo app, string error)
        {
            using (var writer = new StreamWriter(app.OutputFilePath, false, Encoding.Default))
            {
                writer.WriteLine(error);
            }
        }
    }
}