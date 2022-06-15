using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extractor.Service;
using Extractor.View.ControlSets;
using static Extractor.Service.Logger;
using static Extractor.Program;
using Message = Extractor.Service.Message;

namespace Extractor.View
{
    public class GeneratorUI
    {
        private readonly GeneratorControls Controls;
        private VersionList versions = new VersionList(new[]{"Win10"});

        public GeneratorUI(GeneratorControls controls)
        {
            Controls = controls;
            Controls.ProfileSelector.DisplayMember = "Name";
            Controls.VersionLabel.Text = versions.ToString();
            Controls.OutputPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Controls.BrowsePath.Click += (s, e) =>
            {
                var dialog = new OpenFolderDialog(Controls.OutputPath.Text);
                if (dialog.Browse() != DialogResult.OK) return;
                Controls.OutputPath.Text =  dialog.FolderPath;
            };
            Controls.SelectVersions.Click += (s, e) =>
            {
                var versionBox = new VersionBox(versions);
                if (!versionBox.Show()) return;
                versions = versionBox.Versions;
                Controls.VersionLabel.Text = versionBox.Versions.ToString();
            };
            Controls.Generate.Click += (s, e) => Generate();
            UpdateProfiles();
        }

        public void UpdateProfiles()
        {
            Controls.ProfileSelector.DataSource = ProfileManager.Profiles;
        }

        private async void Generate()
        {
            Controls.Generate.Visible = false;
            var progress = new Progress<int>(v => Controls.ProgressBar.Value = v);
            try
            {
                var profile = (Profile)Controls.ProfileSelector.SelectedItem;
                await Task.Run(() => Generator.Generate(
                    Controls.OutputPath.Text,
                    profile,
                    versions,
                    progress));
            }
            catch (Exception ex)
            {
                Log($"[ERROR]: {ex.Message}");
                Message.Error(ex.Message);
            }
            Controls.Generate.Visible = true;
        }
    }
}
