using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static Extractor.Program;

namespace Extractor.Service
{
    public class VersionBox
    {
        public VersionList Versions;

        public VersionBox(IEnumerable<string> versions) =>
            Versions = new VersionList(versions);
        public VersionBox() => Versions = new VersionList();
        
        public bool Show()
        {
            using(var form = new Form())
            {
                var okBtn = new Button();
                var cancelBtn = new Button();
                var checkBoxes = new List<CheckBox>();
                
                var y = 10;
                foreach (var version in FileRepository.Versions)
                {
                    var check = new CheckBox();
                    check.Text = version;
                    check.Top = y;
                    check.Left = 15;
                    check.AutoSize = true;
                    check.Checked = Versions.Contains(version);
                    check.CheckedChanged += (s, e) => 
                        okBtn.Enabled = checkBoxes.Any(x => x.Checked);
                    form.Controls.Add(check);
                    checkBoxes.Add(check);
                    y += 20;
                }

                okBtn.Text = "OK";
                okBtn.SetBounds(130, 10, 75, 23);
                okBtn.DialogResult = DialogResult.OK;
                okBtn.Enabled = Versions.Count > 0;

                cancelBtn.Text = "Отмена";
                cancelBtn.SetBounds(130, 40, 75, 23);
                cancelBtn.DialogResult = DialogResult.Cancel;

                form.Text = "Версия ОС";
                form.ClientSize = new Size(230, (checkBoxes.Count + 1) * 20);
                form.Controls.AddRange(new Control[] { okBtn, cancelBtn});
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowInTaskbar = false;

                DialogResult dialogResult = form.ShowDialog();
                Versions = new VersionList(checkBoxes
                    .Where(x => x.Checked)
                    .Select(x => x.Text));
                return dialogResult == DialogResult.OK;
            }
        }
    }
}