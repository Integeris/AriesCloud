namespace AriesCloud.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.scramblerTabPage = new System.Windows.Forms.TabPage();
            this.generateKeyButton = new System.Windows.Forms.Button();
            this.changeKeyButton = new System.Windows.Forms.Button();
            this.keyPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.accountTabPage = new System.Windows.Forms.TabPage();
            this.changePasswordButton = new System.Windows.Forms.Button();
            this.confirmTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.scramblerTabPage.SuspendLayout();
            this.accountTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.scramblerTabPage);
            this.mainTabControl.Controls.Add(this.accountTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(514, 148);
            this.mainTabControl.TabIndex = 0;
            // 
            // scramblerTabPage
            // 
            this.scramblerTabPage.Controls.Add(this.generateKeyButton);
            this.scramblerTabPage.Controls.Add(this.changeKeyButton);
            this.scramblerTabPage.Controls.Add(this.keyPathTextBox);
            this.scramblerTabPage.Controls.Add(this.label1);
            this.scramblerTabPage.Location = new System.Drawing.Point(4, 31);
            this.scramblerTabPage.Name = "scramblerTabPage";
            this.scramblerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.scramblerTabPage.Size = new System.Drawing.Size(506, 113);
            this.scramblerTabPage.TabIndex = 0;
            this.scramblerTabPage.Text = "Шифрование";
            this.scramblerTabPage.UseVisualStyleBackColor = true;
            // 
            // generateKeyButton
            // 
            this.generateKeyButton.Location = new System.Drawing.Point(314, 38);
            this.generateKeyButton.Name = "generateKeyButton";
            this.generateKeyButton.Size = new System.Drawing.Size(185, 31);
            this.generateKeyButton.TabIndex = 6;
            this.generateKeyButton.Text = "Сгенерировать новый";
            this.generateKeyButton.UseVisualStyleBackColor = true;
            this.generateKeyButton.Click += new System.EventHandler(this.GenerateKeyButtonOnClick);
            // 
            // changeKeyButton
            // 
            this.changeKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeKeyButton.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changeKeyButton.Location = new System.Drawing.Point(470, 6);
            this.changeKeyButton.Name = "changeKeyButton";
            this.changeKeyButton.Size = new System.Drawing.Size(29, 26);
            this.changeKeyButton.TabIndex = 5;
            this.changeKeyButton.Text = "...";
            this.changeKeyButton.UseVisualStyleBackColor = true;
            this.changeKeyButton.Click += new System.EventHandler(this.ChangeKeyButtonOnClick);
            // 
            // keyPathTextBox
            // 
            this.keyPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyPathTextBox.Location = new System.Drawing.Point(127, 6);
            this.keyPathTextBox.Name = "keyPathTextBox";
            this.keyPathTextBox.Size = new System.Drawing.Size(337, 26);
            this.keyPathTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Путь к ключу:";
            // 
            // accountTabPage
            // 
            this.accountTabPage.Controls.Add(this.changePasswordButton);
            this.accountTabPage.Controls.Add(this.confirmTextBox);
            this.accountTabPage.Controls.Add(this.label3);
            this.accountTabPage.Controls.Add(this.newPasswordTextBox);
            this.accountTabPage.Controls.Add(this.label2);
            this.accountTabPage.Location = new System.Drawing.Point(4, 31);
            this.accountTabPage.Name = "accountTabPage";
            this.accountTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.accountTabPage.Size = new System.Drawing.Size(506, 113);
            this.accountTabPage.TabIndex = 1;
            this.accountTabPage.Text = "Аккаунт";
            this.accountTabPage.UseVisualStyleBackColor = true;
            // 
            // changePasswordButton
            // 
            this.changePasswordButton.Location = new System.Drawing.Point(123, 75);
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.Size = new System.Drawing.Size(185, 31);
            this.changePasswordButton.TabIndex = 7;
            this.changePasswordButton.Text = "Сменить";
            this.changePasswordButton.UseVisualStyleBackColor = true;
            this.changePasswordButton.Click += new System.EventHandler(this.ChangePasswordButtonOnClick);
            // 
            // confirmTextBox
            // 
            this.confirmTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmTextBox.Location = new System.Drawing.Point(203, 38);
            this.confirmTextBox.Name = "confirmTextBox";
            this.confirmTextBox.Size = new System.Drawing.Size(295, 26);
            this.confirmTextBox.TabIndex = 5;
            this.confirmTextBox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Подтверждение пароля:";
            // 
            // newPasswordTextBox
            // 
            this.newPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newPasswordTextBox.Location = new System.Drawing.Point(203, 6);
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.Size = new System.Drawing.Size(295, 26);
            this.newPasswordTextBox.TabIndex = 3;
            this.newPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Новый пароль:";
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(177)))), ((int)(((byte)(196)))));
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Location = new System.Drawing.Point(318, 106);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(184, 31);
            this.applyButton.TabIndex = 9;
            this.applyButton.Text = "Применить";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.ApplyButtonOnClick);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 148);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.mainTabControl);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.mainTabControl.ResumeLayout(false);
            this.scramblerTabPage.ResumeLayout(false);
            this.scramblerTabPage.PerformLayout();
            this.accountTabPage.ResumeLayout(false);
            this.accountTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage scramblerTabPage;
        private System.Windows.Forms.TabPage accountTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyPathTextBox;
        private System.Windows.Forms.Button changeKeyButton;
        private System.Windows.Forms.TextBox confirmTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button generateKeyButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button changePasswordButton;
    }
}