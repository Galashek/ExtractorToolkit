namespace Extractor
{
    partial class FileRepositoryForm
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
            System.Windows.Forms.ToolStrip appList_btns;
            System.Windows.Forms.ToolStrip toolStrip1;
            System.Windows.Forms.ToolStrip toolStrip2;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.StatusStrip statusStrip1;
            this.addApp_btn = new System.Windows.Forms.ToolStripButton();
            this.removeApp_btn = new System.Windows.Forms.ToolStripButton();
            this.addFile_btn = new System.Windows.Forms.ToolStripButton();
            this.removeFile_btn = new System.Windows.Forms.ToolStripButton();
            this.changeVersions_btn = new System.Windows.Forms.ToolStripButton();
            this.addReq_btn = new System.Windows.Forms.ToolStripButton();
            this.removeReq_btn = new System.Windows.Forms.ToolStripButton();
            this.reqList = new System.Windows.Forms.ListBox();
            this.fileList = new System.Windows.Forms.ListBox();
            this.appList = new System.Windows.Forms.ListBox();
            this.appCount_label = new System.Windows.Forms.ToolStripStatusLabel();
            appList_btns = new System.Windows.Forms.ToolStrip();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStrip2 = new System.Windows.Forms.ToolStrip();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            appList_btns.SuspendLayout();
            toolStrip1.SuspendLayout();
            toolStrip2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // appList_btns
            // 
            appList_btns.Dock = System.Windows.Forms.DockStyle.Bottom;
            appList_btns.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            appList_btns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addApp_btn,
            this.removeApp_btn});
            appList_btns.Location = new System.Drawing.Point(3, 392);
            appList_btns.Name = "appList_btns";
            appList_btns.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            appList_btns.ShowItemToolTips = false;
            appList_btns.Size = new System.Drawing.Size(281, 25);
            appList_btns.TabIndex = 5;
            appList_btns.TabStop = true;
            // 
            // addApp_btn
            // 
            this.addApp_btn.Image = global::Extractor.Properties.Resources.Add;
            this.addApp_btn.Name = "addApp_btn";
            this.addApp_btn.Size = new System.Drawing.Size(79, 22);
            this.addApp_btn.Text = "Добавить";
            // 
            // removeApp_btn
            // 
            this.removeApp_btn.Enabled = false;
            this.removeApp_btn.Image = global::Extractor.Properties.Resources.Remove;
            this.removeApp_btn.Name = "removeApp_btn";
            this.removeApp_btn.Size = new System.Drawing.Size(71, 22);
            this.removeApp_btn.Text = "Удалить";
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFile_btn,
            this.removeFile_btn,
            this.changeVersions_btn});
            toolStrip1.Location = new System.Drawing.Point(3, 312);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            toolStrip1.ShowItemToolTips = false;
            toolStrip1.Size = new System.Drawing.Size(659, 25);
            toolStrip1.TabIndex = 6;
            toolStrip1.TabStop = true;
            // 
            // addFile_btn
            // 
            this.addFile_btn.Image = global::Extractor.Properties.Resources.Add;
            this.addFile_btn.Name = "addFile_btn";
            this.addFile_btn.Size = new System.Drawing.Size(79, 22);
            this.addFile_btn.Text = "Добавить";
            // 
            // removeFile_btn
            // 
            this.removeFile_btn.Enabled = false;
            this.removeFile_btn.Image = global::Extractor.Properties.Resources.Remove;
            this.removeFile_btn.Name = "removeFile_btn";
            this.removeFile_btn.Size = new System.Drawing.Size(71, 22);
            this.removeFile_btn.Text = "Удалить";
            // 
            // changeVersions_btn
            // 
            this.changeVersions_btn.Enabled = false;
            this.changeVersions_btn.Image = global::Extractor.Properties.Resources.Change;
            this.changeVersions_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.changeVersions_btn.Name = "changeVersions_btn";
            this.changeVersions_btn.Size = new System.Drawing.Size(123, 22);
            this.changeVersions_btn.Text = "Изменить версии";
            // 
            // toolStrip2
            // 
            toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addReq_btn,
            this.removeReq_btn});
            toolStrip2.Location = new System.Drawing.Point(3, 188);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            toolStrip2.ShowItemToolTips = false;
            toolStrip2.Size = new System.Drawing.Size(212, 25);
            toolStrip2.TabIndex = 6;
            toolStrip2.TabStop = true;
            // 
            // addReq_btn
            // 
            this.addReq_btn.Image = global::Extractor.Properties.Resources.Add;
            this.addReq_btn.Name = "addReq_btn";
            this.addReq_btn.Size = new System.Drawing.Size(79, 22);
            this.addReq_btn.Text = "Добавить";
            // 
            // removeReq_btn
            // 
            this.removeReq_btn.Enabled = false;
            this.removeReq_btn.Image = global::Extractor.Properties.Resources.Remove;
            this.removeReq_btn.Name = "removeReq_btn";
            this.removeReq_btn.Size = new System.Drawing.Size(71, 22);
            this.removeReq_btn.Text = "Удалить";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(this.fileList);
            groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox1.Location = new System.Drawing.Point(305, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(665, 340);
            groupBox1.TabIndex = 30;
            groupBox1.TabStop = false;
            groupBox1.Text = "Файлы";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(toolStrip2);
            groupBox3.Controls.Add(this.reqList);
            groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox3.Location = new System.Drawing.Point(441, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(218, 216);
            groupBox3.TabIndex = 32;
            groupBox3.TabStop = false;
            groupBox3.Text = "Требуемые файлы";
            // 
            // reqList
            // 
            this.reqList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reqList.FormattingEnabled = true;
            this.reqList.ItemHeight = 16;
            this.reqList.Location = new System.Drawing.Point(6, 22);
            this.reqList.Name = "reqList";
            this.reqList.Size = new System.Drawing.Size(206, 164);
            this.reqList.TabIndex = 2;
            // 
            // fileList
            // 
            this.fileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileList.FormattingEnabled = true;
            this.fileList.ItemHeight = 16;
            this.fileList.Location = new System.Drawing.Point(6, 19);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(429, 292);
            this.fileList.TabIndex = 25;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.appList);
            groupBox2.Controls.Add(appList_btns);
            groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox2.Location = new System.Drawing.Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(287, 420);
            groupBox2.TabIndex = 31;
            groupBox2.TabStop = false;
            groupBox2.Text = "Приложения";
            // 
            // appList
            // 
            this.appList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.appList.FormattingEnabled = true;
            this.appList.ItemHeight = 16;
            this.appList.Location = new System.Drawing.Point(6, 19);
            this.appList.Name = "appList";
            this.appList.Size = new System.Drawing.Size(275, 372);
            this.appList.TabIndex = 1;
            this.appList.TabStop = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appCount_label});
            statusStrip1.Location = new System.Drawing.Point(0, 437);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(982, 22);
            statusStrip1.TabIndex = 32;
            statusStrip1.Text = "statusStrip1";
            // 
            // appCount_label
            // 
            this.appCount_label.Name = "appCount_label";
            this.appCount_label.Size = new System.Drawing.Size(122, 17);
            this.appCount_label.Text = "100 apps in repository";
            // 
            // FileRepositoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(982, 459);
            this.Controls.Add(statusStrip1);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileRepositoryForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Файлы";
            appList_btns.ResumeLayout(false);
            appList_btns.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox reqList;
        private System.Windows.Forms.ToolStripButton addApp_btn;
        private System.Windows.Forms.ToolStripButton removeApp_btn;
        private System.Windows.Forms.ListBox appList;
        private System.Windows.Forms.ListBox fileList;
        private System.Windows.Forms.ToolStripButton addFile_btn;
        private System.Windows.Forms.ToolStripButton removeFile_btn;
        private System.Windows.Forms.ToolStripButton changeVersions_btn;
        private System.Windows.Forms.ToolStripButton addReq_btn;
        private System.Windows.Forms.ToolStripButton removeReq_btn;
        private System.Windows.Forms.ToolStripStatusLabel appCount_label;
    }
}