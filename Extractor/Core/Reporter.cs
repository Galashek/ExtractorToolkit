using System;
using System.IO;
using System.Linq;
using System.Text;
using static Extractor.Service.HtmlHelper;
using static Extractor.Service.Logger;
using static Extractor.Program;

namespace Extractor.Core
{
    class Reporter
    {
        private const string Report = "report.html";
        private const string ReportDir = "Report";
        private const string PagesDir = "pages";
        private const string Styles = "styles.css";

        public string Result { get; private set; }
        
        public void Generate(string inputDir, string outputDir, string description, IProgress<int> progress)
        {
            var reportDirPath = Path.Combine(outputDir, ReportDir);
            var pagesDirPath = Path.Combine(reportDirPath, PagesDir);
            var stylesPath = Path.Combine(reportDirPath, Styles);
            var reportFilePath = Path.Combine(reportDirPath, Report);

            Directory.CreateDirectory(reportDirPath);
            Directory.CreateDirectory(pagesDirPath);
            
            File.Copy(ReportStyles, stylesPath, true);

            ToolInfo previous = null;

            var toolInfos = new DirectoryInfo(inputDir)
                .GetDirectories()
                .SelectMany(d => d.GetFiles().Select(file =>
                {
                    var id = file.Name.Split('.')[0];
                    var tool = Program.ToolManager.GetToolById(id) ?? new Tool(id);

                    var toolInfo = new ToolInfo
                    {
                        File = file,
                        Tool = tool,
                        Category = file.Directory?.Name,
                        Previous = previous
                    };

                    if (previous != null)
                        previous.Next = toolInfo;
                    previous = toolInfo;

                    return toolInfo;
                }))
                .ToArray();

            using (var writer = new StreamWriter(reportFilePath, false))
            {
                GenerateHomePage(writer, toolInfos, description);
            }

            var i = 1;
            foreach (var info in toolInfos)
            {
                var toolPagePath = Path.Combine(pagesDirPath, $"{info.Tool.Id}.html");
                using (var writer = new StreamWriter(toolPagePath, false))
                {
                    GenerateToolPage(writer, info);
                }
                progress?.Report(i++ * 100 / toolInfos.Length);
            }

            Result = reportFilePath;
        }

        private static void GenerateHomePage(StreamWriter writer, ToolInfo[] toolInfos, string description)
        {
            void Write(string s) => writer.WriteLine(s);

            Write(StartTag("!DOCTYPE html"));
            Write(StartTag("html"));
            Write(PairTags("title", "Отчет"));
            Write(PairTags("head", $"<link rel=\"stylesheet\" href=\"{Styles}\">"));
            Write(StartTag("body"));
            Write(StartTagWithClass("div", "main"));
            Write(PairTagsWithClass("div", "header", "Отчет"));
            Write(StartTagWithClass("div", "content"));
            Write(PairTags("b", "-- Описание"));
            Write(PairTags("p", description));
            Write(PairTags("b", "-- Дата создания отчета"));
            Write(PairTags("p", DateTime.Now.ToString()));
            Write(EndTag("div"));

            Write(PairTagsWithClass("div", "header", "Инструменты"));
            Write(StartTagWithClass("div", "content"));
            
            foreach (var dir in toolInfos.GroupBy(i => i.Category))
            {
                Write(PairTagsWithClass("div", "category", dir.Key));
                foreach (var info in dir)
                {
                    Write(Link($".\\pages\\{info.Tool.Id}.html", 
                        PairTagsWithClass("div", "tool", info.Tool.ToolName)));
                }
            }
            Write(EndTag("div"));
            Write(EndTag("div"));
            Write(EndTag("body"));
            Write(EndTag("html"));
        }
        private static void GenerateToolPage(StreamWriter writer, ToolInfo toolInfo)
        {
            void Write(string s) => writer.WriteLine(s);
            
            Write(StartTag("!DOCTYPE html"));
            Write(StartTag("html"));
            Write(PairTags("title", toolInfo.Tool.ToolName));
            Write(PairTags("head", $"<link rel=\"stylesheet\" href=\"..\\{Styles}\">"));
            Write(StartTag("body"));

            Write(StartTagWithClass("div", "navbar"));
            Write(LinkWithClass($"..\\{Report}", "button home", "На главную"));
            if (toolInfo.Previous != null)
                Write(LinkWithClass($"{toolInfo.Previous.Tool.Id}.html", "button previous", "Предыдущий"));
            if (toolInfo.Next != null)
                Write(LinkWithClass($"{toolInfo.Next.Tool.Id}.html", "button next", "Следующий"));
            Write(EndTag("div"));

            Write(StartTagWithClass("div", "main"));

            Write(PairTagsWithClass("div", "header", "Описание"));
            Write(StartTagWithClass("div", "content"));
            Write(PairTags("b", "-- Имя"));
            Write(PairTags("p", toolInfo.Tool.ToolName));
            Write(PairTags("b", "-- Команда"));
            Write(PairTags("p", $"{toolInfo.Tool.AppName} {toolInfo.Tool.Arguments}" ));
            Write(PairTags("b", "-- Категория"));
            Write(PairTags("p", toolInfo.Tool.Category));
            Write(PairTags("b", "-- Подробное описание"));
            Write(PairTags("p", toolInfo.Tool.Description));
            Write(EndTag("div"));

            Write(PairTagsWithClass("div", "header", "Вывод"));
            Write(StartTagWithClass("div", "content"));

            Write(StartTag("pre"));
            using (var reader = new StreamReader(toolInfo.File.FullName, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    Write(reader.ReadLine());
                }
            }
            Write(EndTag("pre"));

            Write(EndTag("div"));
            Write(EndTag("div"));
            Write(EndTag("body"));
            Write(EndTag("html"));
        }
        
        class ToolInfo
        {
            public FileInfo File;
            public Tool Tool;
            public string Category;
            public ToolInfo Previous;
            public ToolInfo Next;
        }
    }
}
