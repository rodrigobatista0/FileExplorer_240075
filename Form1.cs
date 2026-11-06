using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileExplorer_240075
{
    public partial class Form1 : Form
    {
        string currentPath = null;
        string currentTitle = null;

        List<string> backList = new List<string>();
        List<string> forwardList = new List<string>();

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;

            ConfigureTree();
            ConfigureList();

            trwFile.AfterSelect += trwFile_AfterSelect;
            trwFile.BeforeExpand += trwFile_BeforeExpand;
            lsvExplorer.DoubleClick += lsvExplorer_DoubleClick;
            btnBack.Click += btnBack_Click;
            btnForward.Click += btnForward_Click;
            btnUp.Click += btnUp_Click;
            btnRefresh.Click += btnRefresh_Click;
            this.KeyDown += Form1_KeyDown;

            OpenFolder("");
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search This PC")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search This PC";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            menuSort.Show(btnSort, new Point(0, btnSort.Height));
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            menuView.Show(btnView, new Point(0, btnView.Height));
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            menuNew.Show(btnNew, new Point(0, btnNew.Height));
        }

        private void SetTag(string key, string path)
        {
            TreeNode[] found = trwFile.Nodes.Find(key, true);
            if (found.Length > 0)
            {
                found[0].Tag = path;
            }
        }

        private void ConfigureTree()
        {
            string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            SetTag("nodeHome", user);
            SetTag("nodeGallery", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            SetTag("nodeDesktop", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            SetTag("nodeDownloads", Path.Combine(user, "Downloads"));
            SetTag("nodeDocuments", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            SetTag("nodePictures", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            SetTag("nodeMusic", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            SetTag("nodeVideos", Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));

            TreeNode pc = trwFile.Nodes["nodeThisPC"];
            pc.Tag = "";

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady == false)
                {
                    continue;
                }

                TreeNode node = new TreeNode("💾 " + drive.Name);
                node.Tag = drive.RootDirectory.FullName;
                node.Nodes.Add("...");
                pc.Nodes.Add(node);
            }

            trwFile.Nodes["nodeQuickAcess"].Expand();
            pc.Expand();
        }

        private void trwFile_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;

            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "...")
            {
                node.Nodes.Clear();

                if (node.Tag == null || node.Tag.ToString() == "")
                {
                    return;
                }

                try
                {
                    string[] folders = Directory.GetDirectories(node.Tag.ToString());

                    foreach (string folder in folders)
                    {
                        DirectoryInfo info = new DirectoryInfo(folder);

                        if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        {
                            continue;
                        }

                        TreeNode child = new TreeNode(info.Name);
                        child.Tag = info.FullName;
                        child.Nodes.Add("...");
                        node.Nodes.Add(child);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (IOException)
                {
                }
            }
        }

        private void trwFile_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                return;
            }

            OpenFolder(e.Node.Tag.ToString());
        }

        private void ConfigureList()
        {
            lsvExplorer.View = View.Details;
            lsvExplorer.FullRowSelect = true;

            lsvExplorer.Columns.Clear();
            lsvExplorer.Columns.Add("Name", 260);
            lsvExplorer.Columns.Add("Date modified", 140);
            lsvExplorer.Columns.Add("Type", 120);
            lsvExplorer.Columns.Add("Size", 90);
        }

        private string FormatSize(long bytes)
        {
            if (bytes < 1024)
            {
                return bytes + " B";
            }
            else if (bytes < 1024 * 1024)
            {
                return (bytes / 1024) + " KB";
            }
            else if (bytes < 1024 * 1024 * 1024)
            {
                return (bytes / (1024 * 1024)) + " MB";
            }
            else
            {
                return (bytes / (1024L * 1024 * 1024)) + " GB";
            }
        }

        private bool LoadList(string path)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            try
            {
                if (path == "")
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.IsReady == false)
                        {
                            continue;
                        }

                        ListViewItem item = new ListViewItem(drive.Name);
                        item.SubItems.Add("");
                        item.SubItems.Add("Local Disk");
                        item.SubItems.Add(FormatSize(drive.TotalSize));
                        item.Tag = drive.RootDirectory.FullName;
                        items.Add(item);
                    }
                }
                else
                {
                    DirectoryInfo folder = new DirectoryInfo(path);

                    foreach (DirectoryInfo dir in folder.GetDirectories())
                    {
                        if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        {
                            continue;
                        }

                        ListViewItem item = new ListViewItem(dir.Name);
                        item.SubItems.Add(dir.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));
                        item.SubItems.Add("File folder");
                        item.SubItems.Add("");
                        item.Tag = dir.FullName;
                        items.Add(item);
                    }

                    foreach (FileInfo file in folder.GetFiles())
                    {
                        if ((file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        {
                            continue;
                        }

                        string ext = file.Extension.TrimStart('.').ToUpper();

                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add(file.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));
                        item.SubItems.Add(ext + " File");
                        item.SubItems.Add(FormatSize(file.Length));
                        item.Tag = file.FullName;
                        items.Add(item);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied.", "File Explorer");
                return false;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "File Explorer");
                return false;
            }

            lsvExplorer.Items.Clear();
            lsvExplorer.Items.AddRange(items.ToArray());
            return true;
        }

        private void ShowPath(string path)
        {
            currentPath = path;

            if (path == "")
            {
                currentTitle = "This PC";
                txtDirectory.Text = "💻  This PC  >";
            }
            else
            {
                currentTitle = Path.GetFileName(path);
                if (currentTitle == "")
                {
                    currentTitle = path;
                }
                txtDirectory.Text = path;
            }

            this.Text = currentTitle;

            btnBack.Enabled = backList.Count > 0;
            btnForward.Enabled = forwardList.Count > 0;
            btnUp.Enabled = path != "";
        }

        private void OpenFolder(string path)
        {
            if (path != "" && Directory.Exists(path) == false)
            {
                MessageBox.Show("Folder not found.", "File Explorer");
                return;
            }

            if (LoadList(path) == false)
            {
                return;
            }

            if (currentPath != null && currentPath != path)
            {
                backList.Add(currentPath);
                forwardList.Clear();
            }

            ShowPath(path);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (backList.Count == 0)
            {
                return;
            }

            string destination = backList[backList.Count - 1];

            if (LoadList(destination))
            {
                backList.RemoveAt(backList.Count - 1);
                forwardList.Add(currentPath);
                ShowPath(destination);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (forwardList.Count == 0)
            {
                return;
            }

            string destination = forwardList[forwardList.Count - 1];

            if (LoadList(destination))
            {
                forwardList.RemoveAt(forwardList.Count - 1);
                backList.Add(currentPath);
                ShowPath(destination);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (currentPath == "")
            {
                return;
            }

            DirectoryInfo parent = Directory.GetParent(currentPath);

            if (parent == null)
            {
                OpenFolder("");
            }
            else
            {
                OpenFolder(parent.FullName);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadList(currentPath);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && !(ActiveControl is TextBox))
            {
                btnBack.PerformClick();
                e.Handled = true;
            }
        }

        private void lsvExplorer_DoubleClick(object sender, EventArgs e)
        {
            if (lsvExplorer.SelectedItems.Count == 0)
            {
                return;
            }

            string path = lsvExplorer.SelectedItems[0].Tag.ToString();

            if (Directory.Exists(path))
            {
                OpenFolder(path);
            }
            else if (File.Exists(path))
            {
                OpenFile(path);
            }
        }

        private void OpenFile(string path)
        {
            try
            {
                string ext = Path.GetExtension(path).ToLower();

                if (ext == ".txt")
                {
                    FormTextEditor editor = new FormTextEditor(path);
                    editor.ShowDialog();
                    LoadList(currentPath);
                }
                else
                {
                    ProcessStartInfo info = new ProcessStartInfo(path);
                    info.UseShellExecute = true;
                    Process.Start(info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot open file");
            }
        }
    }
}