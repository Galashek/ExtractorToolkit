using System;
using System.IO;
using System.Windows.Forms;
using Extractor.Core;

namespace Extractor
{
    static class Program
    {
        [STAThread] static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static ToolManager ToolManager { get; }
        public static ProfileManager ProfileManager { get; }
        public static Generator Generator { get; }
        public static Reporter Reporter { get; }
        public static FileRepository FileRepository { get; }

        public static readonly string FilesDir;
        public static readonly string FilesReqDir;
        public static readonly string ToolsDir;
        public static readonly string ProfilesDir;
        public static readonly string DataDir;
        public static readonly string SourceDir;

        public static readonly string CategoriesFile;
        public static readonly string VersionsPath;
        public static readonly string FileRepo;
        public static readonly string ExtractorSrcList;
        public static readonly string ReportStyles;
        public static readonly string ReferenceFile;

        static Program()
        {
            var cur = Environment.CurrentDirectory;
            FilesDir = Path.Combine(cur, "files");
            FilesReqDir = Path.Combine(FilesDir, "required");
            ToolsDir = Path.Combine(cur, "tools");
            ProfilesDir = Path.Combine(cur, "profiles");
            DataDir = Path.Combine(cur, "data");
            SourceDir = Path.Combine(cur, "src");

            CategoriesFile = Path.Combine(DataDir, "categories.txt");
            VersionsPath = Path.Combine(DataDir, "os.txt");
            ExtractorSrcList = Path.Combine(SourceDir, "ext_src_list.txt");
            ReportStyles = Path.Combine(SourceDir, "styles.css");
            FileRepo = Path.Combine(DataDir, "fileRepository.dat");
            ReferenceFile = Path.Combine(cur, "UserGuide.pdf");

            ToolManager = new ToolManager();
            ProfileManager = new ProfileManager();
            Generator = new Generator();
            Reporter = new Reporter();
            FileRepository = new FileRepository();
        }
    }
}