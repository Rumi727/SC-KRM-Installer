using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;
using SCKRM.Compress;
using SCKRM.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCKRM.Installer
{
    public partial class MainForm : Form
    {
        const string projectOwner = "SimsimhanChobo";
        const string projectName = "SC-KRM";

        public MainForm()
        {
            InitializeComponent();

            client = new GitHubClient(new ProductHeaderValue("SC-KRM-Installer-App"));

            try
            {
                string languagePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "language");
                if (File.Exists(languagePath))
                {
                    byte[] languageByte = File.ReadAllBytes(languagePath);
                    if (languageByte == null || languageByte.Length <= 0)
                        language.SelectedIndex = 0;
                    else
                        language.SelectedIndex = languageByte[0];
                }
                else
                    language.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                Program.Exception(e);
                language.SelectedIndex = 0;
            }

            System.Windows.Forms.Application.ApplicationExit += ApplicationExit;
            AllRefresh();
        }

        void ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllBytes(Path.Combine(System.Windows.Forms.Application.StartupPath, "language"), new byte[] { (byte)language.SelectedIndex });
            }
            catch (Exception ex)
            {
                Program.Exception(ex);
            }
        }

        GitHubClient client;
        Release projectRelease;



        string _selectProjectPath;
        public string selectProjectPath
        {
            get => _selectProjectPath;
            set
            {
                _selectProjectPath = value;
                selectedProjectLabel.Text = LanguageManager.LanguageLoad("selectedProject") + value;
            }
        }

        string _newestVersion;
        public string newestVersion
        {
            get => _newestVersion;
            set
            {
                _newestVersion = value;
                newestVersionLabel.Text = LanguageManager.LanguageLoad("newestVersion") + value;
            }
        }



        bool logLine = false;
        void Log(string value)
        {
            if (logLine)
                log.AppendText(Environment.NewLine + value);
            else
                log.AppendText(value);

            logLine = true;
        }

        string GetProjectFolderPath() => Path.Combine(System.Windows.Forms.Application.StartupPath, projectOwner + "-" + projectName);
        string GetProjectZipFilePath() => Path.Combine(System.Windows.Forms.Application.StartupPath, projectOwner + "-" + projectName + ".zip");
        string DownloadedProjectFolderPath()
        {
            string projectFolder = GetProjectFolderPath();
            if (!Directory.Exists(projectFolder))
            {
                MessageBox.Show(LanguageManager.LanguageLoad("downloadedNoProject"), LanguageManager.LanguageLoad("noProject"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }

            string[] paths = Directory.GetDirectories(projectFolder);
            if (paths == null || paths.Length != 1)
            {
                MessageBox.Show(LanguageManager.LanguageLoad("downloadDamagedProject"), LanguageManager.LanguageLoad("damagedProject"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }

            return paths[0];
        }



        void ProjectFolderSelect(object sender, EventArgs e)
        {
            if (!projectDownload.Enabled)
                return;
            else if (!refresh.Enabled)
                return;
            else if (!install.Enabled)
                return;

            DialogResult dialogResult = selectProjectFolderDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                selectProjectPath = selectProjectFolderDialog.SelectedPath;
                AllRefresh();
            }
        }

        async void AllRefresh()
        {
            if (!projectDownload.Enabled)
                return;
            else if (!refresh.Enabled)
                return;
            else if (!install.Enabled)
                return;

            language.Enabled = false;
            refresh.Enabled = false;
            projectDownload.Enabled = false;
            install.Enabled = false;
            projectFolderSelect.Enabled = false;

            projectDownload.Text = LanguageManager.LanguageLoad("download");
            install.Text = LanguageManager.LanguageLoad("install");
            refresh.Text = LanguageManager.LanguageLoad("refresh");
            projectFolderSelect.Text = LanguageManager.LanguageLoad("chooseAProject");

            selectProjectPath = selectProjectFolderDialog.SelectedPath;

            string version = GetVersion(DownloadedProjectFolderPath());
            downloadedVersion.Text = LanguageManager.LanguageLoad("downloadedVersion") + version;

            version = GetVersion(selectProjectFolderDialog.SelectedPath);
            detectedVersionLabel.Text = LanguageManager.LanguageLoad("detectedVersion") + version;

            try
            {
                newestVersion = LanguageManager.LanguageLoad("none");
                projectRelease = await client.Repository.Release.GetLatest(projectOwner, projectName);
                newestVersion = projectRelease.TagName;
            }
            catch (NotFoundException e)
            {
                Program.Exception(e);
            }
            catch (ApiException e)
            {
                Program.Exception(e);
            }

            language.Enabled = true;
            refresh.Enabled = true;
            projectDownload.Enabled = true;
            install.Enabled = true;
            projectFolderSelect.Enabled = true;
        }

        void AllRefresh(object sender, EventArgs e) => AllRefresh();



        #region Project Download
        async void ProjectDownload(object sender, EventArgs e)
        {
            if (!projectDownload.Enabled)
                return;
            else if (!install.Enabled)
                return;
            else if (!refresh.Enabled)
                return;

            language.Enabled = false;
            projectDownload.Enabled = false;
            install.Enabled = false;
            refresh.Enabled = false;
            projectFolderSelect.Enabled = false;

            downloadedVersion.Text = LanguageManager.LanguageLoad("downloadedVersion") + LanguageManager.LanguageLoad("none");

            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.UserAgent, projectName + "-Installer-App");
                webClient.DownloadProgressChanged += DownloadProgressChanged;
                webClient.DownloadFileCompleted += Completed;

                try
                {
                    webClient.DownloadFileAsync(new Uri(projectRelease.ZipballUrl), GetProjectZipFilePath());
                }
                catch (WebException ex)
                {
                    Program.Exception(ex);
                }

                bool isDownloadComplted = false;
                int textAni = 0;
                while (!isDownloadComplted)
                {

                    if (textAni == 0)
                        progress.Text = LanguageManager.LanguageLoad("download") + ".";
                    else if (textAni == 1)
                        progress.Text = LanguageManager.LanguageLoad("download") + "..";
                    else if (textAni == 2)
                    {
                        progress.Text = LanguageManager.LanguageLoad("download") + "...";
                        textAni = -1;
                    }
                    
                    textAni++;
                    await Task.Delay(500);
                }

                void Completed(object sender, AsyncCompletedEventArgs e) => isDownloadComplted = true;



                AsyncTask asyncTask = new AsyncTask();
                bool isDecompressEnd = false;

                if (Directory.Exists(GetProjectFolderPath()))
                    Directory.Delete(GetProjectFolderPath(), true);

                Decompress();
                async void Decompress()
                {
                    await CompressFileManager.DecompressZipFile(GetProjectZipFilePath(), GetProjectFolderPath(), "", asyncTask);
                    isDecompressEnd = true;
                }

                textAni = 0;
                Text();
                async void Text()
                {
                    while (!isDecompressEnd)
                    {
                        if (textAni == 0)
                            progress.Text = LanguageManager.LanguageLoad("decompressing") + ".";
                        else if (textAni == 1)
                            progress.Text = LanguageManager.LanguageLoad("decompressing") + "..";
                        else if (textAni == 2)
                        {
                            progress.Text = LanguageManager.LanguageLoad("decompressing") + "...";
                            textAni = -1;
                        }

                        textAni++;
                        await Task.Delay(500);
                    }
                }

                while (!isDecompressEnd)
                {
                    progressBar.Value = (int)asyncTask.progress;
                    progressBar.Maximum = (int)asyncTask.maxProgress;

                    progressPercentage.Text = (int)(asyncTask.progress / asyncTask.maxProgress * 100) + "%";
                    
                    await Task.Delay(1);
                }

                try
                {
                    File.Delete(GetProjectZipFilePath());
                }
                catch (DirectoryNotFoundException ex)
                {
                    Program.Exception(ex);
                }
                catch (PathTooLongException ex)
                {
                    Program.Exception(ex);
                }
                catch (IOException ex)
                {
                    Program.Exception(ex);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Program.Exception(ex);
                }

                progress.Text = LanguageManager.LanguageLoad("downloadComplete");
            }

            progressBar.Value = 0;
            progressPercentage.Text = "0%";

            language.Enabled = true;
            refresh.Enabled = true;
            install.Enabled = true;
            projectDownload.Enabled = true;
            projectFolderSelect.Enabled = true;

            AllRefresh();
        }

        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Maximum = 100;

            progressBar.Value = e.ProgressPercentage;
            progressPercentage.Text = e.ProgressPercentage + "%";
        }
        #endregion



        void ProjectInstall(object sender, EventArgs e)
        {
            if (!install.Enabled)
                return;
            else if (!refresh.Enabled)
                return;
            else if (!projectDownload.Enabled)
                return;

            string downloadedProjectFolderPath = DownloadedProjectFolderPath();
            if (string.IsNullOrEmpty(downloadedProjectFolderPath))
                return;
            else if (!Directory.Exists(selectProjectPath))
            {
                MessageBox.Show(LanguageManager.LanguageLoad("noProjectSelected"), LanguageManager.LanguageLoad("noProject"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string downloadedVersion = GetVersion(downloadedProjectFolderPath);
            if (downloadedVersion == LanguageManager.LanguageLoad("none"))
            {
                MessageBox.Show(LanguageManager.LanguageLoad("downloadedProjectNoVersionFile"), LanguageManager.LanguageLoad("noVersionFile"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string downloadedSCKRMPath = Path.Combine(downloadedProjectFolderPath, "Assets/SC KRM");
            string downloadedStreamingAssetsPath = Path.Combine(downloadedProjectFolderPath, "Assets/StreamingAssets");

            if (!Directory.Exists(downloadedSCKRMPath) || !Directory.Exists(downloadedStreamingAssetsPath) || !File.Exists(downloadedSCKRMPath + ".meta") || !File.Exists(downloadedStreamingAssetsPath + ".meta"))
            {
                MessageBox.Show(LanguageManager.LanguageLoad("downloadDamagedProject"), LanguageManager.LanguageLoad("damagedProject"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(LanguageManager.LanguageLoad("installWarning"), LanguageManager.LanguageLoad("warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            bool projectSettingOverwirte = MessageBox.Show(LanguageManager.LanguageLoad("overwriteProjectSettings"), LanguageManager.LanguageLoad("overwrite"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;



            language.Enabled = false;
            install.Enabled = false;
            refresh.Enabled = false;
            projectDownload.Enabled = false;
            projectFolderSelect.Enabled = false;



            string selectedSCKRMPath = Path.Combine(selectProjectPath, "Assets/SC KRM");
            string selectedStreamingAssetsPath = Path.Combine(selectProjectPath, "Assets/StreamingAssets");


            if (!Directory.Exists(selectedSCKRMPath))
                Directory.CreateDirectory(selectedSCKRMPath);
            if (!Directory.Exists(selectedStreamingAssetsPath))
                Directory.CreateDirectory(selectedStreamingAssetsPath);


            Log(LanguageManager.LanguageLoad("backup"));
            string backupPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "backup");
            if (Directory.Exists(backupPath))
                Directory.Delete(backupPath, true);

            DirectoryTool.Copy(selectedSCKRMPath, Path.Combine(backupPath, "SC KRM"));
            DirectoryTool.Copy(selectedStreamingAssetsPath, Path.Combine(backupPath, "StreamingAssets"));



            Log(LanguageManager.LanguageLoad("sckrmVersionCopy"));
            File.Copy(Path.Combine(downloadedProjectFolderPath, "SC-KRM-Version"), Path.Combine(selectProjectPath, "SC-KRM-Version"), true);



            Log(LanguageManager.LanguageLoad("sckrmDelete"));
            Directory.Delete(selectedSCKRMPath, true);

            Log(LanguageManager.LanguageLoad("sckrmCopy"));
            DirectoryTool.Copy(downloadedSCKRMPath, selectedSCKRMPath);

            Log(LanguageManager.LanguageLoad("packCopy"));

            {
                string[] paths = Directory.GetFiles(downloadedStreamingAssetsPath, "pack.*");
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    string name = Path.GetFileName(path);
                    string selectedPath = Path.Combine(selectedStreamingAssetsPath, name);
                    
                    if (!File.Exists(selectedPath))
                        File.Copy(path, selectedPath);
                }
            }

            Log(LanguageManager.LanguageLoad("assetsCopy"));

            if (Directory.Exists(Path.Combine(downloadedStreamingAssetsPath, "assets")))
                DirectoryTool.Copy(Path.Combine(downloadedStreamingAssetsPath, "assets"), Path.Combine(selectedStreamingAssetsPath, "assets"));

            File.Copy(downloadedSCKRMPath + ".meta", selectedSCKRMPath + ".meta", true);
            File.Copy(downloadedStreamingAssetsPath + ".meta", selectedStreamingAssetsPath + ".meta", true);

            File.Copy(Path.Combine(downloadedStreamingAssetsPath, "assets") + ".meta", Path.Combine(selectedStreamingAssetsPath, "assets") + ".meta", true);
            File.Copy(Path.Combine(downloadedStreamingAssetsPath, "projectSettings") + ".meta", Path.Combine(selectedStreamingAssetsPath, "projectSettings") + ".meta", true);



            Dictionary<string, JObject> selectedProjectSetting = new Dictionary<string, JObject>();
            {
                string projectSettingsPath = Path.Combine(selectedStreamingAssetsPath, "projectSettings");
                if (Directory.Exists(projectSettingsPath))
                {
                    Log(LanguageManager.LanguageLoad("selectedProjectSettingDeserialize"));

                    string[] paths = Directory.GetFiles(projectSettingsPath, "*.json");
                    for (int i = 0; i < paths.Length; i++)
                    {
                        string path = paths[i];
                        string name = Path.GetFileName(path);
                        Log(name);

                        try
                        {
                            selectedProjectSetting.Add(name, JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path)));
                        }
                        catch (Exception ex)
                        {
                            Program.Exception(ex);
                        }
                    }
                }
            }



            if (Directory.Exists(Path.Combine(downloadedStreamingAssetsPath, "projectSettings")))
            {
                Log(LanguageManager.LanguageLoad("projectSettingsJsonMerge"));

                string selectedProjectSettingsPath = Path.Combine(selectedStreamingAssetsPath, "projectSettings");
                if (!Directory.Exists(selectedProjectSettingsPath))
                    Directory.CreateDirectory(selectedProjectSettingsPath);

                string[] paths = Directory.GetFiles(Path.Combine(downloadedStreamingAssetsPath, "projectSettings"), "*.json");
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    string name = Path.GetFileName(path);
                    Log(name);

                    try
                    {
                        JObject jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path));
                        if (selectedProjectSetting.ContainsKey(name))
                        {
                            JObject selectedJObject = selectedProjectSetting[name];
                            if (projectSettingOverwirte && selectedJObject != null)
                            {
                                selectedJObject.Merge(jObject, new JsonMergeSettings() { MergeArrayHandling = MergeArrayHandling.Union });
                                jObject = selectedJObject;
                            }
                            else if (jObject != null)
                                jObject.Merge(selectedJObject, new JsonMergeSettings() { MergeArrayHandling = MergeArrayHandling.Replace });
                        }

                        File.WriteAllText(Path.Combine(selectedStreamingAssetsPath, "projectSettings", name), jObject.ToString());
                    }
                    catch (Exception ex)
                    {
                        Program.Exception(ex);
                    }
                }
            }



            Log(LanguageManager.LanguageLoad("installationFinished"));



            language.Enabled = true;
            install.Enabled = true;
            refresh.Enabled = true;
            projectDownload.Enabled = true;
            projectFolderSelect.Enabled = true;

            AllRefresh();
        }

        string GetVersion(string path)
        {
            string versionFilePath = Path.Combine(path, "SC-KRM-Version");
            if (!File.Exists(versionFilePath))
                return LanguageManager.LanguageLoad("none");

            return File.ReadAllText(versionFilePath);
        }

        void LanguageSelect()
        {
            if (!install.Enabled)
                return;
            else if (!refresh.Enabled)
                return;
            else if (!projectDownload.Enabled)
                return;

            if (language.SelectedIndex == 1)
                LanguageManager.currentLanguage = LanguageManager.Language.ko_kr;
            else
                LanguageManager.currentLanguage = LanguageManager.Language.en_us;

            AllRefresh();
        }

        void LanguageSelect(object sender, EventArgs e) => LanguageSelect();
    }
}
