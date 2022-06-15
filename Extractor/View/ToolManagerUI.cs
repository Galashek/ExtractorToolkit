using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Extractor.Service;
using Extractor.View.ControlSets;
using Message = Extractor.Service.Message;
using static Extractor.Program;

namespace Extractor.View
{
    class ToolManagerUI
    {
        private const string AllToolsCategory = "Все инструменты";
        private readonly ToolManagerControls Controls;
        private string SelectedCategory
        {
            get => Controls.CategorySelector.SelectedItem.ToString();
            set => Controls.CategorySelector.SelectedItem = value;
        }
        private Tool SelectedTool => (Tool)Controls.ToolList.SelectedRows[0].DataBoundItem;

        public ToolManagerUI(ToolManagerControls controls)
        {
            Controls = controls;
            Controls.AddTool.Click += (s, e) => AddTool();
            Controls.RemoveTool.Click += (s, e) => RemoveTool();
            Controls.ChangeTool.Click += (s, e) => ChangeTool();
            Controls.ToolList.CellDoubleClick += (s, e) => ChangeTool();

            #region FamilySelector

            var categoryContextMenu = new ContextMenuStrip();
            var addCategoryMenuItem = new ToolStripMenuItem("Добавить категорию");
            addCategoryMenuItem.Image = Properties.Resources.Add;
            var removeCategoryMenuItem = new ToolStripMenuItem("Удалить");
            removeCategoryMenuItem.Image = Properties.Resources.Remove;
            categoryContextMenu.Items.Add(addCategoryMenuItem);
            categoryContextMenu.Items.Add(removeCategoryMenuItem);
            Controls.CategorySelector.ComboBox.ContextMenuStrip = categoryContextMenu;
            
            addCategoryMenuItem.Click += (s, e) =>
            {
                var inputBox = new InputBox();
                if (!inputBox.Show("Введите название", "Новая категория")) return;
                var category = inputBox.Input;
                if (Controls.CategorySelector.Items.Contains(category))
                {
                    Message.Warning($"Категория \"{category}\" уже существует");
                    return;
                }
                ToolManager.AddCategory(category);
                UpdateCategoryList();
                SelectedCategory = category;
            };
            removeCategoryMenuItem.Click += (s, e) =>
            {
                var category = SelectedCategory;
                if (category == AllToolsCategory) return;
                if (Controls.ToolList.RowCount != 0)
                {
                    Message.Warning($"Нельзя удалить \"{category}\". Один или несколько инструментов принадлежат этой категории");
                    return;
                }
                ToolManager.RemoveCategory(category);
                UpdateCategoryList();
                SelectedCategory = AllToolsCategory;
            };

            Controls.CategorySelector.ComboBox.ContextMenuStrip.Opening += (s, e) =>
                removeCategoryMenuItem.Enabled = SelectedCategory != AllToolsCategory;
            Controls.CategorySelector.SelectedIndexChanged += (s, e) => UpdateToolList();

            UpdateCategoryList();
            SelectedCategory = AllToolsCategory;

            #endregion

            #region ToolList context menu

            var toolsContextMenu = new ContextMenuStrip();
            var addToolMenuItem = new ToolStripMenuItem("Добавить");
            addToolMenuItem.Image = Properties.Resources.Add;
            var removeToolMenuItem = new ToolStripMenuItem("Удалить");
            removeToolMenuItem.Image = Properties.Resources.Remove;
            var sortAscMenuItem = new ToolStripMenuItem("Sort ascending");
            var sortDescMenuItem = new ToolStripMenuItem("Sort descending");
            var toolPropertiesMenuItem = new ToolStripMenuItem("Свойства");
            toolPropertiesMenuItem.Image = Properties.Resources.Change;
            toolsContextMenu.Items.Add(addToolMenuItem);
            toolsContextMenu.Items.Add(removeToolMenuItem);
            toolsContextMenu.Items.Add("-");
            toolsContextMenu.Items.Add(sortAscMenuItem);
            toolsContextMenu.Items.Add(sortDescMenuItem);
            toolsContextMenu.Items.Add("-");
            toolsContextMenu.Items.Add(toolPropertiesMenuItem);
            Controls.ToolList.ContextMenuStrip = toolsContextMenu;

            Controls.ToolList.CellMouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    int rowSelected = e.RowIndex;
                    if (rowSelected != -1)
                    {
                        Controls.ToolList.ClearSelection();
                        Controls.ToolList.Rows[rowSelected].Selected = true;
                    }
                }
            };

            sortAscMenuItem.Click += (s, e) => SortToolList(true);
            sortDescMenuItem.Click += (s, e) => SortToolList(false);
            addToolMenuItem.Click += (s, e) => AddTool();
            removeToolMenuItem.Click += (s, e) => RemoveTool();
            toolPropertiesMenuItem.Click += (s, e) => ChangeTool();
            #endregion
            
            Controls.ToolList.SelectionChanged += (s, e) =>
            {
                Controls.ChangeTool.Enabled =
                    Controls.RemoveTool.Enabled =
                        toolPropertiesMenuItem.Enabled =
                            removeToolMenuItem.Enabled =
                                Controls.ToolList.SelectedRows.Count != 0;
                ShowToolInfo();
            };
            UpdateToolList();
        }

        private void UpdateCategoryList()
        {
            var categories = new [] {AllToolsCategory}.Concat(ToolManager.Categories).ToList();
            Controls.CategorySelector.ComboBox.DataSource = categories;
        }
        private void UpdateToolList()
        {
            var category = SelectedCategory;
            var list = category == AllToolsCategory ? 
                ToolManager.Tools : ToolManager.GetToolsByCategory(category);
            Controls.ToolList.DataSource = list.OrderBy(x => x.AppName).ThenBy(x => x.Arguments).ToList();
            Controls.ToolsCountLabel.Text = $"{Controls.ToolList.Rows.Count.ToString()} tools in list";
        }
        private void SortToolList(bool ascending)
        {
            var list = (List<Tool>)Controls.ToolList.DataSource;
            Controls.ToolList.DataSource = ascending 
                ? list.OrderBy(x => x.AppName).ThenBy(x => x.Arguments).ToList() 
                : list.OrderByDescending(x => x.AppName).ThenByDescending(x => x.Arguments).ToList();
        }

        private void AddTool()
        {
            try
            {
                var curTool = SelectedTool;
                using (var form = new ToolPropertiesForm())
                {
                    form.categorySelector.DataSource = ToolManager.Categories.Append("None").ToList();
                    form.Category = SelectedCategory == AllToolsCategory ? 
                        "None" : SelectedCategory;
                    if (FileRepository.Contains(curTool.AppName))
                        form.AppName = curTool.AppName;

                    if (form.ShowDialog() != DialogResult.OK) return;
                    
                    var tool = new Tool(DateTime.Now.Ticks.ToString())
                    {
                        AppName = form.AppName,
                        Arguments = form.Arguments,
                        ToolName = form.ToolName,
                        Description = form.Description,
                        Category = form.Category == "None" ? string.Empty : form.Category,
                        OutputToFile = form.OutputToFile
                    };
                    ToolManager.SaveOrUpdate(tool);
                }
            }
            catch (Exception e)
            {
                Message.Error(e.Message);
                return;
            }
            UpdateToolList();
        }

        private void RemoveTool()
        {
            var tool = SelectedTool;
            if (!Message.WarningAsk($"Удалить {tool.ToolName}?")) return;
            ToolManager.Remove(tool);
            UpdateToolList();
        }

        private void ChangeTool()
        {
            var tool = SelectedTool;
            var curIndex = Controls.ToolList.SelectedRows[0].Index;

            using (var form = new ToolPropertiesForm())
            {
                form.categorySelector.DataSource = ToolManager.Categories.Append("None").ToList();

                form.AppName = tool.AppName;
                form.ToolName = tool.ToolName;
                form.Arguments = tool.Arguments;
                form.Category = tool.Category == string.Empty ? "None" : tool.Category;
                form.Description = tool.Description;
                form.outToFile_check.Checked = tool.OutputToFile;
                form.okButton.Enabled = true;

                if (form.ShowDialog() != DialogResult.OK) return;

                var newTool = new Tool(tool.Id)
                {
                    AppName = form.AppName,
                    Arguments = form.Arguments,
                    ToolName = form.ToolName,
                    Description = form.Description,
                    Category = form.Category == "None" ? string.Empty : form.Category,
                    OutputToFile = form.OutputToFile
                };
                ToolManager.SaveOrUpdate(newTool);
            }
            UpdateToolList();
            Controls.ToolList.ClearSelection();
            Controls.ToolList.Rows[curIndex].Selected = true;
        }

        private void ShowToolInfo()
        {

            if (Controls.ToolList.SelectedRows.Count == 0)
            {
                Controls.CategoryLabel.Text = 
                Controls.DescriptionLabel.Text =
                Controls.VersionsLabel.Text = "";
                return;
            }
            var tool = SelectedTool;
            Controls.CategoryLabel.Text = tool.Category;
            Controls.DescriptionLabel.Text = tool.Description;
            Controls.VersionsLabel.Text = 
                FileRepository.GetApp(tool.AppName).SupportedSystems.ToString();
        }
    }
}
