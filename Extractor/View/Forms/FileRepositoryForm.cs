using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Extractor.Entities;
using Extractor.Service;
using Message = Extractor.Service.Message;
using static Extractor.Program;
using FileInfo = Extractor.Entities.FileInfo;

namespace Extractor
{
    public partial class FileRepositoryForm : Form
    {
        private readonly Dictionary<string, Executable> fileListEntriesToFiles;
        private readonly Dictionary<string, FileInfo> reqListEntriesToFiles;

        private string SelectedApp
        {
            get => appList.SelectedItem.ToString();
            set => appList.SelectedItem = value;
        }
        private string SelectedFile
        {
            get => fileList.SelectedItem.ToString();
            set => fileList.SelectedItem = value;
        }
        private string SelectedRequiredFile => reqList.SelectedItem.ToString();

        public FileRepositoryForm()
        {
            InitializeComponent();
            fileListEntriesToFiles = new Dictionary<string, Executable>();
            reqListEntriesToFiles = new Dictionary<string, FileInfo>();

            addApp_btn.Click += (s, e) => AddApplication();
            removeApp_btn.Click += (s, e) => RemoveApplication();
            addFile_btn.Click += (s, e) => AddFile();
            removeFile_btn.Click += (s, e) => RemoveFile();
            changeVersions_btn.Click += (s, e) => ChangeVersions();
            addReq_btn.Click += (s, e) => AddRequired();
            removeReq_btn.Click += (s, e) => RemoveRequired();
            appList.SelectedIndexChanged += (s, e) =>
            {
                removeApp_btn.Enabled =
                    addFile_btn.Enabled =
                        appList.SelectedIndex >= 0;
                UpdateFileList();
            };
            fileList.SelectedIndexChanged += (s, e) =>
            {
                removeFile_btn.Enabled =
                    changeVersions_btn.Enabled =
                        addReq_btn.Enabled = 
                            fileList.SelectedIndex >= 0;
                UpdateRequiredList();
            };
            reqList.SelectedIndexChanged += (s, e) =>
            {
                removeReq_btn.Enabled =
                        reqList.SelectedIndex >= 0;
            };
            UpdateAppList();
        }

        private void AddApplication()
        {
            try
            {
                var fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() != DialogResult.OK) return;

                var inputBox = new InputBox();
                if (!inputBox.Show("Введите название приложения", fileDialog.SafeFileName))
                    return;
                var appName = inputBox.Input;
                if (FileRepository.Contains(appName))
                {
                    Message.Error($"Приложение {appName} уже существует");
                    return;
                }
                var versionBox = new VersionBox();
                if (!versionBox.Show()) return;

                var file = new Executable(fileDialog.SafeFileName, versionBox.Versions);
                //FileRepository.LoadExecutableFile(file, fileDialog.FileName);
                //var app = new Application(appName);
                //app.AddFile(file);
                FileRepository.AddApplication(appName, file, fileDialog.FileName);
                UpdateAppList();
                SelectedApp = appName;
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
            }
        }
        private void RemoveApplication()
        {
            try
            {
                var appName = SelectedApp;
                if (!Message.WarningAsk($"Remove \"{appName}\"?"))
                    return;
                FileRepository.RemoveApplication(appName);
                UpdateAppList();
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
            }
        }

        private void AddFile()
        {
            try
            {
                var fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                var versionBox = new VersionBox();
                if (!versionBox.Show()) return;
                
                var file = new Executable(fileDialog.SafeFileName, versionBox.Versions);
                var appName = SelectedApp;
                //FileRepository.LoadExecutableFile(file, fileDialog.FileName);
                //FileRepository.GetApp(appName).AddFile(file);
                //FileRepository.Update();
                FileRepository.AddFileToApp(appName, file, fileDialog.FileName);
                UpdateAppList();
                SelectedApp = appName;
                //fileList.SelectedItem = 
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
            }
        }

        private void RemoveFile()
        {
            try
            {
                var file = fileListEntriesToFiles[SelectedFile];
                var appName = SelectedApp;
                FileRepository.RemoveFileFromApp(appName, file);
                //FileRepository.GetApp(appName).RemoveFile(file);
                //FileRepository.Update();
                UpdateAppList();
                SelectedApp = appName;
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
            }
        }

        private void ChangeVersions()
        {
            try
            {
                var file = fileListEntriesToFiles[SelectedFile];
                var appName = SelectedApp;
                var versionBox = new VersionBox(file.Versions);
                if (!versionBox.Show()) return;
                //FileRepository.GetApp(appName)
                //    .ChangeFileVersions(file, versionBox.Versions);
                //FileRepository.Update();
                FileRepository.ChangeFileVersions(appName, file, versionBox.Versions);
                UpdateAppList();
                SelectedApp = appName;
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
            }
        }

        private void UpdateAppList()
        {
            removeApp_btn.Enabled = false;
            appList.DataSource = FileRepository.ApplicationNames;
            appCount_label.Text = $"{FileRepository.Applications.Count.ToString()} apps in repository";
            UpdateFileList();
        }

        private void UpdateFileList()
        {
            fileListEntriesToFiles.Clear();
            fileList.Items.Clear();
            removeFile_btn.Enabled = false;
            changeVersions_btn.Enabled = false;

            if (appList.Items.Count == 0)
            {
                addFile_btn.Enabled = false;
                addReq_btn.Enabled = false;
                return;
            }

            var appName = SelectedApp;
            var app = FileRepository.GetApp(appName);

            var i = 1;
            foreach (var file in app.Files)
            {
                var entry = $"{i}: {file.FileName} - {file.Versions}";
                fileListEntriesToFiles[entry] = file;
                fileList.Items.Add(entry);
                i++;
            }
            UpdateRequiredList();
        }

        private void UpdateRequiredList()
        {
            reqList.Items.Clear();
            reqListEntriesToFiles.Clear();
            removeReq_btn.Enabled = false;

            if (fileList.Items.Count == 0 || fileList.SelectedIndex < 0)
            {
                addReq_btn.Enabled = false;
                return;
            }

            var file = fileListEntriesToFiles[SelectedFile];

            foreach (var req in file.RequiredFiles)
            {
                reqList.Items.Add(req.FileName);
                reqListEntriesToFiles[req.FileName] = req;
            }
        }
        
        private void AddRequired()
        {
            try
            {
                var fileDialog = new OpenFileDialog {Multiselect = true};
                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                var appName = SelectedApp;
                var curFile = SelectedFile;
                foreach (var filename in fileDialog.FileNames)
                {
                    var safeFileName = Path.GetFileName(filename);
                    string relativeFolder = "";
                    if (Message.Ask($"Specify folder for {safeFileName}?"))
                    {
                        while (true)
                        {
                            var inputBox = new InputBox();
                            if (!inputBox.Show("Format: [folder1/folder2/folder3]",
                                Path.GetFileName(Path.GetDirectoryName(filename)))) break;
                            relativeFolder = inputBox.Input;
                            var valid = Uri.IsWellFormedUriString(relativeFolder.Replace('\\', '/'), UriKind.Relative);
                            if (!valid) Message.Error("Invalid input");
                            else break;
                        }
                    }
                    var relativePath = Path.Combine(relativeFolder, safeFileName);
                    var req = new FileInfo(relativePath);
                    var file = fileListEntriesToFiles[curFile];
                    file.RequiredFiles.Add(req);
                    FileRepository.LoadRequiredFile(req, filename);
                }
                FileRepository.Update();
                UpdateAppList();
                SelectedApp = appName;
                SelectedFile = curFile;
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
            }
        }

        private void RemoveRequired()
        {
            try
            {
                var appName = SelectedApp;
                var curFile = SelectedFile;
                var file = fileListEntriesToFiles[SelectedFile];
                var reqFile = reqListEntriesToFiles[SelectedRequiredFile];
                file.RequiredFiles.Remove(reqFile);
                FileRepository.RemoveRequiredFile(reqFile);
                FileRepository.Update();
                UpdateAppList();
                SelectedApp = appName;
                SelectedFile = curFile;
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
            }
        }
    }
}
