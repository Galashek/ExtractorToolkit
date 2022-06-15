using System.Windows.Forms;

namespace Extractor.Service
{
    static class Message
    {
        public static void Warning(string message, string caption = "")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Error(string message, string caption = "")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool WarningAsk(string question, string caption = "")
        {
            return MessageBox.Show(question, caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }
        public static bool Ask(string question, string caption = "")
        {
            return MessageBox.Show(question, caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }
    }
}