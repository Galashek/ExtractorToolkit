using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Extractor.Service;
using Extractor.View.ControlSets;
using Message = Extractor.Service.Message;
using static Extractor.Program;

namespace Extractor.View
{
    class ProfileManagerUI
    {
        private readonly ProfileManagerControls Controls;
        private List<Tool> profileTools;
        private List<Tool> tools;
        private const string AllToolsCategory = "Все инструменты";
        private Profile SelectedProfile
        {
            get => (Profile) Controls.ProfileSelector.SelectedItem;
            set => Controls.ProfileSelector.SelectedItem = value;
        }

        public ProfileManagerUI(ProfileManagerControls controls)
        {
            Controls = controls;

            Controls.ProfileSelector.ComboBox.DisplayMember = "Name";
            Controls.ProfileSelector.SelectedIndexChanged += (s, e) =>
            {
                LoadSelectedProfileTools();
                CheckToolsUsedInSelectedProfile();
                UpdateMoveButtonsState();
                Controls.SaveProfile.Enabled = false;
                Controls.DeleteProfile.Enabled = 
                    ((Profile) Controls.ProfileSelector.SelectedItem).Name != "Default";
            };

            Controls.MoveInAll.Click += (s, e) => MoveInAllTools();
            Controls.MoveOutAll.Click += (s, e) => MoveOutAllTools();
            Controls.MoveIn.Click += (s, e) => MoveInTool();
            Controls.MoveOut.Click += (s, e) => MoveOutTool();

            Controls.ToolList.CellDoubleClick += (s, e) =>
            {
                if (Controls.ToolList.CurrentRow.DefaultCellStyle.ForeColor == Color.Black)
                {
                    MoveInTool();
                    Controls.ToolList.ClearSelection();
                }
            };
            Controls.ProfileToolsList.CellDoubleClick += (s, e) => MoveOutTool();

            Controls.SaveProfile.Click += (s, e) =>
            {
                SaveProfile();
                Controls.SaveProfile.Enabled = false;
                LoadProfileList();
            };
            Controls.NewProfile.Click += (s, e) =>
            {
                var inputBox = new InputBox();
                if (!inputBox.Show("Введите имя профиля", "Новый профиль")) return;
                var newProfile = new Profile(inputBox.Input);
                if (AddProfile(newProfile))
                {
                    LoadProfileList();
                    SelectedProfile = newProfile;
                }
            };
            Controls.DeleteProfile.Click += (s, e) =>
            {
                if (DeleteProfile(SelectedProfile))
                {
                    LoadProfileList();
                    LoadSelectedProfileTools();
                    CheckToolsUsedInSelectedProfile();
                    UpdateMoveButtonsState();
                }
            };

            Controls.ToolList.SelectionChanged += (s, e) => UpdateMoveButtonsState();
            Controls.CategorySelector.SelectedIndexChanged += (s, e) =>
            {
                UpdateToolList();
                CheckToolsUsedInSelectedProfile();
                UpdateMoveButtonsState();
            };
            Controls.ProfileSelector.ComboBox.MouseWheel += (s, e) => 
                ((HandledMouseEventArgs) e).Handled = true;
        }

        public void Initialize()
        {
            LoadCategories();
            UpdateToolList();

            LoadProfileList();
            LoadSelectedProfileTools();

            CheckToolsUsedInSelectedProfile();
            UpdateMoveButtonsState();
        }

        private void LoadCategories()
        {
            var categories = new[] { AllToolsCategory }.Concat(ToolManager.Categories).ToList();
            Controls.CategorySelector.ComboBox.DataSource = categories;
        }

        private void UpdateToolList()
        {
            var category = Controls.CategorySelector.SelectedItem.ToString();
            tools = category == AllToolsCategory ?
                ToolManager.Tools : ToolManager.GetToolsByCategory(category);
            Controls.ToolList.DataSource = tools.OrderBy(x => x.AppName).ThenBy(x => x.Arguments).ToList();
        }

        private void LoadProfileList()
        {
            Controls.ProfileSelector.ComboBox.DataSource = ProfileManager.Profiles;
        }

        private void LoadSelectedProfileTools()
        {
            profileTools = ToolManager.GetToolsByProfile(SelectedProfile);
            Controls.ProfileToolsList.DataSource = profileTools;
            Controls.ProfileToolsCountLabel.Text = $"{profileTools.Count.ToString()} tools";
        }

        private void UpdateProfileToolsView()
        {
            Controls.ProfileToolsList.DataSource = null;
            Controls.ProfileToolsList.DataSource = profileTools;
            Controls.SaveProfile.Enabled = true;
            CheckToolsUsedInSelectedProfile();
            UpdateMoveButtonsState();
        }

        private void CheckToolsUsedInSelectedProfile()
        {
            foreach (DataGridViewRow row in Controls.ToolList.Rows)
            {
                row.DefaultCellStyle.ForeColor =
                    (profileTools ?? new List<Tool>()).Contains((Tool)row.DataBoundItem) 
                        ? Color.LightGray : Color.Black;
            }
        }

        private void UpdateMoveButtonsState()
        {
            Controls.MoveOutAll.Enabled = Controls.ProfileToolsList.Rows.Count > 0;
            Controls.MoveOut.Enabled = Controls.ProfileToolsList.SelectedRows.Count > 0;
            Controls.MoveIn.Enabled = Controls.ToolList.SelectedRows.Cast<DataGridViewRow>()
                .Any(x => x.DefaultCellStyle.ForeColor == Color.Black);
            Controls.MoveInAll.Enabled = Controls.ToolList.Rows.Cast<DataGridViewRow>()
                .Any(x => x.DefaultCellStyle.ForeColor == Color.Black);
        }
        
        private void MoveInAllTools()
        {
            foreach (var tool in tools)
            {
                if (!profileTools.Contains(tool))
                    profileTools.Add(tool);
            }
            UpdateProfileToolsView();
        }

        private void MoveInTool()
        {
            var tool = (Tool)Controls.ToolList.CurrentRow.DataBoundItem;
            if (!profileTools.Contains(tool))
                profileTools.Add(tool);
            UpdateProfileToolsView();
        }

        private void MoveOutTool()
        {
            var tool = (Tool)Controls.ProfileToolsList.CurrentRow.DataBoundItem;
            profileTools.Remove(tool);
            UpdateProfileToolsView();
        }

        private void MoveOutAllTools()
        {
            profileTools.Clear();
            UpdateProfileToolsView();
        }

        private void SaveProfile()
        {
            var newProfile = new Profile(SelectedProfile.Name, profileTools.Select(t => t.Id).ToList());
            ProfileManager.SaveOrUpdate(newProfile);
        }

        private bool AddProfile(Profile profile)
        {
            if (ProfileManager.Contains(profile))
            {
                Message.Warning($"Профиль \"{profile.Name}\" уже существует");
                return false;
            }
            ProfileManager.SaveOrUpdate(profile);
            return true;
        }

        private bool DeleteProfile(Profile profile)
        {
            if (!Message.WarningAsk($"Удалить {profile.Name}?")) return false;
            ProfileManager.Remove(profile);
            return true;
        }
    }
}