namespace Extractor
{
    partial class ToolPropertiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox4;
            System.Windows.Forms.GroupBox groupBox5;
            this.nameBox = new System.Windows.Forms.TextBox();
            this.appSelector = new System.Windows.Forms.ComboBox();
            this.argsBox = new System.Windows.Forms.TextBox();
            this.outToFile_check = new System.Windows.Forms.CheckBox();
            this.placeholder_box = new System.Windows.Forms.TextBox();
            this.placeholder_label = new System.Windows.Forms.Label();
            this.categorySelector = new System.Windows.Forms.ComboBox();
            this.descBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.nameBox);
            groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox1.Location = new System.Drawing.Point(12, 11);
            groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox1.Size = new System.Drawing.Size(401, 54);
            groupBox1.TabIndex = 26;
            groupBox1.TabStop = false;
            groupBox1.Text = "Имя инструмента";
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.SystemColors.Window;
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameBox.Location = new System.Drawing.Point(7, 23);
            this.nameBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(387, 23);
            this.nameBox.TabIndex = 3;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.appSelector);
            groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox2.Location = new System.Drawing.Point(12, 69);
            groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox2.Size = new System.Drawing.Size(401, 54);
            groupBox2.TabIndex = 27;
            groupBox2.TabStop = false;
            groupBox2.Text = "Приложение";
            // 
            // appSelector
            // 
            this.appSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.appSelector.FormattingEnabled = true;
            this.appSelector.Location = new System.Drawing.Point(7, 20);
            this.appSelector.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.appSelector.Name = "appSelector";
            this.appSelector.Size = new System.Drawing.Size(387, 24);
            this.appSelector.TabIndex = 20;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.argsBox);
            groupBox3.Controls.Add(this.outToFile_check);
            groupBox3.Controls.Add(this.placeholder_box);
            groupBox3.Controls.Add(this.placeholder_label);
            groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox3.Location = new System.Drawing.Point(12, 127);
            groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox3.Size = new System.Drawing.Size(401, 86);
            groupBox3.TabIndex = 28;
            groupBox3.TabStop = false;
            groupBox3.Text = "Аргументы";
            // 
            // argsBox
            // 
            this.argsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.argsBox.Location = new System.Drawing.Point(7, 23);
            this.argsBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.argsBox.Name = "argsBox";
            this.argsBox.Size = new System.Drawing.Size(387, 23);
            this.argsBox.TabIndex = 2;
            // 
            // outToFile_check
            // 
            this.outToFile_check.AutoSize = true;
            this.outToFile_check.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.outToFile_check.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outToFile_check.Location = new System.Drawing.Point(7, 53);
            this.outToFile_check.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.outToFile_check.Name = "outToFile_check";
            this.outToFile_check.Size = new System.Drawing.Size(119, 21);
            this.outToFile_check.TabIndex = 22;
            this.outToFile_check.Text = "Вывод в файл";
            this.outToFile_check.UseVisualStyleBackColor = true;
            // 
            // placeholder_box
            // 
            this.placeholder_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.placeholder_box.Location = new System.Drawing.Point(233, 51);
            this.placeholder_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.placeholder_box.Name = "placeholder_box";
            this.placeholder_box.ReadOnly = true;
            this.placeholder_box.Size = new System.Drawing.Size(86, 23);
            this.placeholder_box.TabIndex = 23;
            this.placeholder_box.Text = "%OUTFILE%";
            this.placeholder_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.placeholder_box.Visible = false;
            // 
            // placeholder_label
            // 
            this.placeholder_label.AutoSize = true;
            this.placeholder_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.placeholder_label.Location = new System.Drawing.Point(153, 54);
            this.placeholder_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.placeholder_label.Name = "placeholder_label";
            this.placeholder_label.Size = new System.Drawing.Size(82, 17);
            this.placeholder_label.TabIndex = 24;
            this.placeholder_label.Text = "Имя файла";
            this.placeholder_label.Visible = false;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.categorySelector);
            groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox4.Location = new System.Drawing.Point(12, 217);
            groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox4.Size = new System.Drawing.Size(401, 54);
            groupBox4.TabIndex = 29;
            groupBox4.TabStop = false;
            groupBox4.Text = "Категория";
            // 
            // categorySelector
            // 
            this.categorySelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categorySelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.categorySelector.FormattingEnabled = true;
            this.categorySelector.Location = new System.Drawing.Point(6, 20);
            this.categorySelector.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categorySelector.Name = "categorySelector";
            this.categorySelector.Size = new System.Drawing.Size(387, 24);
            this.categorySelector.TabIndex = 17;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(this.descBox);
            groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox5.Location = new System.Drawing.Point(13, 275);
            groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox5.Size = new System.Drawing.Size(401, 155);
            groupBox5.TabIndex = 30;
            groupBox5.TabStop = false;
            groupBox5.Text = "Описание";
            // 
            // descBox
            // 
            this.descBox.AcceptsReturn = true;
            this.descBox.AcceptsTab = true;
            this.descBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.descBox.Location = new System.Drawing.Point(5, 23);
            this.descBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.descBox.Multiline = true;
            this.descBox.Name = "descBox";
            this.descBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descBox.Size = new System.Drawing.Size(387, 125);
            this.descBox.TabIndex = 13;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.okButton.Location = new System.Drawing.Point(234, 437);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 29);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelButton.Location = new System.Drawing.Point(328, 437);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 29);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ToolPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(424, 480);
            this.Controls.Add(groupBox5);
            this.Controls.Add(groupBox4);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolPropertiesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Свойства инструмента";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox argsBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox descBox;
        public System.Windows.Forms.Button okButton;
        public System.Windows.Forms.ComboBox categorySelector;
        public System.Windows.Forms.ComboBox appSelector;
        private System.Windows.Forms.TextBox placeholder_box;
        private System.Windows.Forms.Label placeholder_label;
        public System.Windows.Forms.CheckBox outToFile_check;
    }
}