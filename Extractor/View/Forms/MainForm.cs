using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Extractor.Service;
using Extractor.View;
using Extractor.View.ControlSets;
using Message = Extractor.Service.Message;

namespace Extractor
{
    public partial class MainForm : Form
    {
        private GeneratorUI generatorUI;
        private ToolManagerUI toolManagerUI;
        private ProfileManagerUI profileManagerUI;
        private ReporterUI reporterUI;

        public MainForm()
        {
            InitializeComponent();
            toolManagerUI = new ToolManagerUI(new ToolManagerControls
            {
                AddTool = addTool_btn,
                RemoveTool = removeTool_btn,
                ChangeTool = changeTool_Btn,
                ToolList = toolManager_tools_dgv,
                CategorySelector = categorySelector,
                CategoryLabel = categoryLabel,
                DescriptionLabel = descLabel,
                VersionsLabel = versionsLabel,
                ToolsCountLabel = toolCount_label
            });
            profileManagerUI = new ProfileManagerUI(new ProfileManagerControls
            {
                ToolList = profMan_tools_dgv,
                ProfileToolsList = profMan_profTools_dgv,
                ProfileSelector = profileComboBox,
                CategorySelector = profMan_categorySelector,
                MoveIn = moveIn_btn,
                MoveInAll = moveInAll_btn,
                MoveOut = moveOut_btn,
                MoveOutAll = moveOutAll_btn,
                DeleteProfile = deleteProfileBtn,
                NewProfile = newProfileBtn,
                SaveProfile = saveProfileBtn,
                ProfileToolsCountLabel = profileToolsCount_label
            });
            generatorUI = new GeneratorUI(new GeneratorControls
            {
                Generate = generateBtn_gen,
                BrowsePath = browseOutputPathBtn_gen,
                OutputPath = outputPathBox_gen,
                ProfileSelector = genProfileSelector,
                VersionLabel = version_label,
                SelectVersions = selectVersion_btn,
                ProgressBar = genExt_progressBar
            });
            reporterUI = new ReporterUI(new ReporterControls
            {
                BrowseInputPath = browseInputPathBtn_rep,
                BrowseOutputPath = browseOutputPathBtn_rep,
                Description = descriptionBox_rep,
                Generate = generateBtn_rep,
                InputPath = inputPathBox_rep,
                OutputPath = outputPathBox_rep,
                ProgressBar = genRep_progressBar,
                OpenAfter = openAfter_check
            });
            fileRepo_btn.Click += (s, e) => OpenFileRepo();
            log_menuBtn.Click += (s, e) => Process.Start(Logger.LogFile);
            reference_menuBtn.Click += (s, e) =>
            {
                if (File.Exists(Program.ReferenceFile))
                    Process.Start(Program.ReferenceFile);
                else Message.Error($"Missing {Program.ReferenceFile}");
            };
            exit_menuBtn.Click += (s, e) => System.Windows.Forms.Application.Exit();
        }
        private void OpenFileRepo()
        {
            using (var form = new FileRepositoryForm())
            {
                form.ShowDialog();
            }
        }
        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == profilesPage)
                profileManagerUI.Initialize();
            else if (tabs.SelectedTab == generatorPage)
                generatorUI.UpdateProfiles();
        }
    }
}