using System.Windows.Forms;
using Message = Extractor.Service.Message;
using static Extractor.Program;

namespace Extractor
{
    public partial class ToolPropertiesForm : Form
    {
        public string AppName
        {
            get => appSelector.SelectedItem.ToString();
            set => appSelector.SelectedItem = value;
        }
        public string Arguments
        {
            get => argsBox.Text;
            set => argsBox.Text = value;
        }
        public string ToolName
        {
            get => nameBox.Text;
            set => nameBox.Text = value;
        }
        public string Description
        {
            get => descBox.Text;
            set => descBox.Text = value;
        }
        public string Category
        {
            get => categorySelector.SelectedItem.ToString();
            set => categorySelector.SelectedItem = value;
        }
        public bool OutputToFile => outToFile_check.Checked;
        
        public ToolPropertiesForm()
        {
            InitializeComponent();
            cancelButton.DialogResult = DialogResult.Cancel;
            if (FileRepository.Applications.Count == 0)
            {
                Message.Warning("Не загружено ни одного приложения!");
                return;
            }
            appSelector.DataSource = FileRepository.ApplicationNames;

            outToFile_check.CheckedChanged += (s, e) =>
            {
                placeholder_box.Visible = 
                    placeholder_label.Visible = 
                        outToFile_check.Checked;
            };
            okButton.Click += (s, e) =>
            {
                if (!PropertiesAreValid()) return;
                DialogResult = DialogResult.OK;
                Close();
            };
        }

        private bool PropertiesAreValid()
        {
            bool nameIsFilled = nameBox.Text.Replace(" ", string.Empty) != string.Empty;
            if (!nameIsFilled)
            {
                Message.Warning("Введите имя инструмента");
                return false;
            }

            bool argsContainsPlaceholder = !OutputToFile || argsBox.Text.Contains(placeholder_box.Text);
            if (!argsContainsPlaceholder)
            {
                Message.Warning("При собственном выводе в файл, аргументы приложения должны включать указанный плейсхолдер имени файла!");
                return false;
            }
            return true;
        }
    }
}