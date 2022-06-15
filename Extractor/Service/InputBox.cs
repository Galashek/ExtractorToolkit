using System.Drawing;
using System.Windows.Forms;

namespace Extractor.Service
{
    public class InputBox
    {
        public string Input { get; private set; }

        public bool Show(string promptText, string defaultText = "", string caption = "")
        {
            using(var form = new Form())
            {
                var label = new Label();
                var textBox = new TextBox();
                var okBtn = new Button();
                var cancelBtn = new Button();
                
                textBox.Text = defaultText;
                textBox.SetBounds(12, 36, 372, 20);
                textBox.Select(textBox.Text.Length, 0);
                textBox.Anchor |= AnchorStyles.Right;

                okBtn.Text = "OK";
                okBtn.SetBounds(228, 72, 75, 23);
                okBtn.DialogResult = DialogResult.OK;
                okBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                okBtn.Enabled = Check();

                cancelBtn.Text = "Отмена";
                cancelBtn.SetBounds(309, 72, 75, 23);
                cancelBtn.DialogResult = DialogResult.Cancel;
                cancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                label.Text = promptText;
                label.SetBounds(9, 20, 372, 13);
                label.AutoSize = true;
                
                form.Text = caption;
                form.ClientSize = new Size(400, 115);
                form.Controls.AddRange(new Control[] {label, textBox, okBtn, cancelBtn});
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowInTaskbar = false;

                textBox.TextChanged += (s, e) => Check();

                DialogResult dialogResult = form.ShowDialog();
                Input = textBox.Text;
                return dialogResult == DialogResult.OK;

                bool Check() => 
                    okBtn.Enabled = textBox.Text.Replace(" ", string.Empty) != string.Empty;
            }
        }
    }
}