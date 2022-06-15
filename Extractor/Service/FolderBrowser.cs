using System;
using System.IO;
using System.Windows.Forms;

namespace Extractor.Service
{
    class OpenFolderDialog
    {
        public string FolderPath { get; private set; }

        public OpenFolderDialog(string initialDir) =>
            FolderPath = initialDir;

        public DialogResult Browse()
        {
            var desktop = Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop);
            var dialog = new FolderPicker
            {
                InputPath = Directory.Exists(FolderPath)
                    ? FolderPath : desktop
            };
            if (dialog.ShowDialog(IntPtr.Zero) == true)
            {
                FolderPath = Directory.Exists(dialog.ResultPath)
                    ? dialog.ResultPath : desktop;
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }
    }
}