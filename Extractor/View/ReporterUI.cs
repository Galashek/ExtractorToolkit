using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extractor.Service;
using Extractor.View.ControlSets;
using static Extractor.Service.Logger;
using static Extractor.Program;
using Message = Extractor.Service.Message;

namespace Extractor.View
{
    class ReporterUI
    {
        private readonly ReporterControls Controls;

        public ReporterUI(ReporterControls controls)
        {
            Controls = controls;
            Controls.OutputPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Controls.BrowseInputPath.Click += (s, e) =>
            {
                var dialog = new OpenFolderDialog(Controls.InputPath.Text);
                if (dialog.Browse() != DialogResult.OK) return;
                Controls.InputPath.Text = dialog.FolderPath;
            };
            Controls.BrowseOutputPath.Click += (s, e) =>
            {
                var dialog = new OpenFolderDialog(Controls.OutputPath.Text);
                if (dialog.Browse() != DialogResult.OK) return;
                Controls.OutputPath.Text = dialog.FolderPath;
            };
            Controls.Generate.Click += (s, e) => Generate();
            Controls.InputPath.TextChanged += (s, e) => UpdateGenerateBtnState();
            Controls.OutputPath.TextChanged += (s, e) => UpdateGenerateBtnState();
        }

        private void UpdateGenerateBtnState()
        {
            Controls.Generate.Enabled =
                Controls.InputPath.Text != string.Empty && Controls.OutputPath.Text != string.Empty;
        }

        private async void Generate()
        {
            var progress = new Progress<int>(v => Controls.ProgressBar.Value = v);

            Controls.ProgressBar.Value = 0;
            Controls.Generate.Visible = false;
            try
            {
                await Task.Run(() => Reporter.Generate(
                    Controls.InputPath.Text,
                    Controls.OutputPath.Text,
                    Controls.Description.Text, progress));
            }
            catch (Exception ex)
            {
                Log($"[ERROR]: {ex.Message}");
                Message.Error(ex.Message);
            }

            if (Controls.OpenAfter.Checked)
                Process.Start(Reporter.Result);

            Controls.Generate.Visible = true;
            UpdateGenerateBtnState();
        }
    }
}
