namespace AriesCloud.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createDirectoryContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.upButton = new System.Windows.Forms.Button();
            this.mainListView = new System.Windows.Forms.ListView();
            this.mainContextMenuStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContextMenuStrip
            // 
            this.mainContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDirectoryContextToolStripMenuItem,
            this.downloadContextToolStripMenuItem,
            this.renameContextToolStripMenuItem,
            this.removeContextToolStripMenuItem});
            this.mainContextMenuStrip.Name = "mainContextMenuStrip";
            this.mainContextMenuStrip.Size = new System.Drawing.Size(162, 92);
            // 
            // createDirectoryContextToolStripMenuItem
            // 
            this.createDirectoryContextToolStripMenuItem.Name = "createDirectoryContextToolStripMenuItem";
            this.createDirectoryContextToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.createDirectoryContextToolStripMenuItem.Text = "Создать папку";
            this.createDirectoryContextToolStripMenuItem.Click += new System.EventHandler(this.CreateDirectoryContextToolStripMenuItemOnClick);
            // 
            // downloadContextToolStripMenuItem
            // 
            this.downloadContextToolStripMenuItem.Name = "downloadContextToolStripMenuItem";
            this.downloadContextToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.downloadContextToolStripMenuItem.Text = "Загрузить";
            this.downloadContextToolStripMenuItem.Click += new System.EventHandler(this.DownloadContextToolStripMenuItemOnClick);
            // 
            // renameContextToolStripMenuItem
            // 
            this.renameContextToolStripMenuItem.Name = "renameContextToolStripMenuItem";
            this.renameContextToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.renameContextToolStripMenuItem.Text = "Переименовать";
            this.renameContextToolStripMenuItem.Click += new System.EventHandler(this.RenameContextToolStripMenuItemOnClick);
            // 
            // removeContextToolStripMenuItem
            // 
            this.removeContextToolStripMenuItem.Name = "removeContextToolStripMenuItem";
            this.removeContextToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.removeContextToolStripMenuItem.Text = "Удалить";
            this.removeContextToolStripMenuItem.Click += new System.EventHandler(this.RemoveContextToolStripMenuItemOnClick);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(542, 26);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDirectoryToolStripMenuItem,
            this.uploadToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(51, 22);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // createDirectoryToolStripMenuItem
            // 
            this.createDirectoryToolStripMenuItem.Name = "createDirectoryToolStripMenuItem";
            this.createDirectoryToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.createDirectoryToolStripMenuItem.Text = "Создать папку";
            this.createDirectoryToolStripMenuItem.Click += new System.EventHandler(this.CreateDirectoryToolStripMenuItemOnClick);
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.uploadToolStripMenuItem.Text = "Загрузить";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.UploadToolStripMenuItemOnClick);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.downloadToolStripMenuItem.Text = "Скачать";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.DownloadToolStripMenuItemOnClick);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.renameToolStripMenuItem.Text = "Переименовать";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItemOnClick);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.updateToolStripMenuItem.Text = "Обновить";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItemOnClick);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.removeToolStripMenuItem.Text = "Удалить";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItemOnClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemOnClick);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(83, 22);
            this.settingsToolStripMenuItem.Text = "Настройки";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItemOnClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemOnClick);
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.pathTextBox);
            this.controlPanel.Controls.Add(this.upButton);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanel.Location = new System.Drawing.Point(0, 26);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(542, 34);
            this.controlPanel.TabIndex = 4;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(39, 4);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(500, 26);
            this.pathTextBox.TabIndex = 6;
            // 
            // upButton
            // 
            this.upButton.FlatAppearance.BorderSize = 0;
            this.upButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.upButton.Location = new System.Drawing.Point(3, 3);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(30, 28);
            this.upButton.TabIndex = 5;
            this.upButton.Text = "🡹";
            this.upButton.UseVisualStyleBackColor = true;
            // 
            // mainListView
            // 
            this.mainListView.ContextMenuStrip = this.mainContextMenuStrip;
            this.mainListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainListView.HideSelection = false;
            this.mainListView.Location = new System.Drawing.Point(0, 60);
            this.mainListView.Name = "mainListView";
            this.mainListView.Size = new System.Drawing.Size(542, 214);
            this.mainListView.TabIndex = 7;
            this.mainListView.UseCompatibleStateImageBehavior = false;
            this.mainListView.View = System.Windows.Forms.View.Details;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(542, 274);
            this.Controls.Add(this.mainListView);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(319, 187);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aries Cloud";
            this.mainContextMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mainContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem downloadContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.ListView mainListView;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryToolStripMenuItem;
    }
}