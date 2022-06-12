
namespace SCKRM.Installer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.projectFolderSelect = new System.Windows.Forms.Button();
            this.projectDownload = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progress = new System.Windows.Forms.Label();
            this.progressPercentage = new System.Windows.Forms.Label();
            this.selectedProjectLabel = new System.Windows.Forms.Label();
            this.detectedVersionLabel = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.newestVersionLabel = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.TextBox();
            this.selectProjectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.install = new System.Windows.Forms.Button();
            this.downloadedVersion = new System.Windows.Forms.Label();
            this.language = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // projectFolderSelect
            // 
            this.projectFolderSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.projectFolderSelect.Location = new System.Drawing.Point(438, 200);
            this.projectFolderSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectFolderSelect.Name = "projectFolderSelect";
            this.projectFolderSelect.Size = new System.Drawing.Size(106, 22);
            this.projectFolderSelect.TabIndex = 0;
            this.projectFolderSelect.Text = "Choose a project";
            this.projectFolderSelect.UseVisualStyleBackColor = true;
            this.projectFolderSelect.Click += new System.EventHandler(this.ProjectFolderSelect);
            // 
            // projectDownload
            // 
            this.projectDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.projectDownload.Location = new System.Drawing.Point(438, 260);
            this.projectDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectDownload.Name = "projectDownload";
            this.projectDownload.Size = new System.Drawing.Size(106, 22);
            this.projectDownload.TabIndex = 1;
            this.projectDownload.Text = "Download";
            this.projectDownload.UseVisualStyleBackColor = true;
            this.projectDownload.Click += new System.EventHandler(this.ProjectDownload);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 290);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(374, 22);
            this.progressBar.TabIndex = 2;
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.AutoEllipsis = true;
            this.progress.Location = new System.Drawing.Point(9, 260);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(423, 22);
            this.progress.TabIndex = 3;
            this.progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressPercentage
            // 
            this.progressPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressPercentage.Location = new System.Drawing.Point(392, 290);
            this.progressPercentage.Name = "progressPercentage";
            this.progressPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressPercentage.Size = new System.Drawing.Size(38, 22);
            this.progressPercentage.TabIndex = 4;
            this.progressPercentage.Text = "0%";
            this.progressPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // selectedProjectLabel
            // 
            this.selectedProjectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedProjectLabel.AutoEllipsis = true;
            this.selectedProjectLabel.Location = new System.Drawing.Point(9, 9);
            this.selectedProjectLabel.Name = "selectedProjectLabel";
            this.selectedProjectLabel.Size = new System.Drawing.Size(421, 18);
            this.selectedProjectLabel.TabIndex = 5;
            this.selectedProjectLabel.Text = "Selected project - none";
            // 
            // detectedVersionLabel
            // 
            this.detectedVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detectedVersionLabel.AutoEllipsis = true;
            this.detectedVersionLabel.Location = new System.Drawing.Point(9, 27);
            this.detectedVersionLabel.Name = "detectedVersionLabel";
            this.detectedVersionLabel.Size = new System.Drawing.Size(421, 18);
            this.detectedVersionLabel.TabIndex = 6;
            this.detectedVersionLabel.Text = "Detected version - none";
            // 
            // refresh
            // 
            this.refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh.Location = new System.Drawing.Point(438, 230);
            this.refresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(106, 22);
            this.refresh.TabIndex = 0;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.AllRefresh);
            // 
            // newestVersionLabel
            // 
            this.newestVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newestVersionLabel.AutoEllipsis = true;
            this.newestVersionLabel.Location = new System.Drawing.Point(9, 55);
            this.newestVersionLabel.Name = "newestVersionLabel";
            this.newestVersionLabel.Size = new System.Drawing.Size(421, 18);
            this.newestVersionLabel.TabIndex = 6;
            this.newestVersionLabel.Text = "Newest version - none";
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log.Location = new System.Drawing.Point(12, 108);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(414, 144);
            this.log.TabIndex = 7;
            // 
            // install
            // 
            this.install.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.install.Location = new System.Drawing.Point(438, 290);
            this.install.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.install.Name = "install";
            this.install.Size = new System.Drawing.Size(106, 22);
            this.install.TabIndex = 0;
            this.install.Text = "Install";
            this.install.UseVisualStyleBackColor = true;
            this.install.Click += new System.EventHandler(this.ProjectInstall);
            // 
            // downloadedVersion
            // 
            this.downloadedVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadedVersion.AutoEllipsis = true;
            this.downloadedVersion.Location = new System.Drawing.Point(9, 73);
            this.downloadedVersion.Name = "downloadedVersion";
            this.downloadedVersion.Size = new System.Drawing.Size(421, 18);
            this.downloadedVersion.TabIndex = 6;
            this.downloadedVersion.Text = "Downloaded version - none";
            // 
            // language
            // 
            this.language.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.language.FormattingEnabled = true;
            this.language.Items.AddRange(new object[] {
            "English (US)",
            "한국어 (대한민국)"});
            this.language.Location = new System.Drawing.Point(438, 12);
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(106, 23);
            this.language.TabIndex = 8;
            this.language.SelectedIndexChanged += new System.EventHandler(this.LanguageSelect);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 325);
            this.Controls.Add(this.language);
            this.Controls.Add(this.log);
            this.Controls.Add(this.progressPercentage);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.projectDownload);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.install);
            this.Controls.Add(this.projectFolderSelect);
            this.Controls.Add(this.downloadedVersion);
            this.Controls.Add(this.newestVersionLabel);
            this.Controls.Add(this.detectedVersionLabel);
            this.Controls.Add(this.selectedProjectLabel);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(350, 208);
            this.Name = "MainForm";
            this.Text = "SC KRM Installer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button projectFolderSelect;
        private System.Windows.Forms.Button projectDownload;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progress;
        private System.Windows.Forms.Label progressPercentage;
        private System.Windows.Forms.Label selectedProjectLabel;
        private System.Windows.Forms.Label detectedVersionLabel;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Label newestVersionLabel;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.FolderBrowserDialog selectProjectFolderDialog;
        private System.Windows.Forms.Button install;
        private System.Windows.Forms.Label downloadedVersion;
        private System.Windows.Forms.ComboBox language;
    }
}

