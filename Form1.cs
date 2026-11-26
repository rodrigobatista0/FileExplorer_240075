using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VB = Microsoft.VisualBasic.FileIO;

namespace FileExplorer_240075
{
    public partial class Form1 : Form
    {
        #region Campos e construtor

        string currentPath = null;
        string currentTitle = null;
        private const string ThisPcText = "💻  This PC  >";

        // Histórico de navegação
        List<string> backList = new List<string>();
        List<string> forwardList = new List<string>();

        // Área de transferência (Cut / Copy)
        List<string> clipPaths = new List<string>();
        bool clipIsCut = false;

        public Form1()
        {
            InitializeComponent();
            lsvExplorer.View = View.Tile;
            ConfigureTree();
            ConfigureList();
            OpenFolder("");
        }

        #endregion

        #region Navegação central (OpenFolder / ShowPath)

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

        #endregion

        #region Botões Back / Forward / Up / Refresh

        private void btnBack_Click(object sender, EventArgs e)
        {
            GoHistory(backList, forwardList);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            GoHistory(forwardList, backList);
        }

        private void GoHistory(List<string> from, List<string> to)
        {
            if (from.Count == 0)
            {
                return;
            }

            string destination = from[from.Count - 1];

            if (destination != "" && Directory.Exists(destination) == false)
            {
                from.RemoveAt(from.Count - 1);
                MessageBox.Show("Folder not found.", "File Explorer");
                ShowPath(currentPath);
                return;
            }

            if (LoadList(destination))
            {
                from.RemoveAt(from.Count - 1);
                to.Add(currentPath);
                ShowPath(destination);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (currentPath == "")
            {
                return;
            }

            string parent = Path.GetDirectoryName(currentPath.TrimEnd(Path.DirectorySeparatorChar));

            OpenFolder(parent ?? "");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string path = currentPath;

            while (path != "" && Directory.Exists(path) == false)
            {
                string parent = Path.GetDirectoryName(path.TrimEnd(Path.DirectorySeparatorChar));
                path = parent ?? "";
            }

            if (path != currentPath)
            {
                OpenFolder(path);
            }
            else
            {
                LoadList(path);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && !(ActiveControl is TextBox))
            {
                btnBack.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnRefresh.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.Up)
            {
                btnUp.PerformClick();
                e.Handled = true;
            }
            else if (e.Control && !(ActiveControl is TextBox))
            {
                if (e.KeyCode == Keys.C)
                {
                    FillClipboard(false);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.X)
                {
                    FillClipboard(true);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.V)
                {
                    PasteItems();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.F2 && !(ActiveControl is TextBox))
            {
                StartRename();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete && !(ActiveControl is TextBox))
            {
                DeleteSelected();
                e.Handled = true;
            }
        }

        #endregion

        #region Barra de endereços

        private void txtDirectory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                NavigateTO(txtDirectory.Text);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                ShowPath(currentPath);
                lsvExplorer.Focus();
            }
        }

        private void txtDirectory_Enter(object sender, EventArgs e)
        {
            if (currentPath == "")
            {
                txtDirectory.Clear();
            }
            else
            {
                txtDirectory.SelectAll();
            }
        }

        private void txtDirectory_Leave(object sender, EventArgs e)
        {
            ShowPath(currentPath);
        }

        private void NavigateTO(string input)
        {
            input = input.Trim().Trim('"');

            if (input == "" || input == ThisPcText.Trim() || input.Equals("This PC", StringComparison.OrdinalIgnoreCase))
            {
                OpenFolder("");
                return;
            }

            string fullpath;
            try
            {
                fullpath = Path.GetFullPath(input);
            }
            catch (Exception)
            {
                MessageBox.Show("O caminho contém carateres inválidos.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowPath(currentPath);
                return;
            }

            if (File.Exists(fullpath))
            {
                OpenFile(fullpath);
                ShowPath(currentPath);
                return;
            }

            OpenFolder(fullpath);
            lsvExplorer.Focus();
        }

        #endregion

        #region Caixa de pesquisa

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
        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
            {
                return;
            }
            e.SuppressKeyPress = true;

            string text = txtSearch.Text;
            string folder = currentPath;

            if(text == "" || text == "Search This PC")
            {
                return;
            }
            this.Text = "Searching...";

            try
            {
                List<ListViewItem> results = await Task.Run(() => SearchAll(folder, text));

                if (folder != currentPath)
                {
                    return;
                }

                lsvExplorer.Items.Clear();
                lsvExplorer.Items.AddRange(results.ToArray());
                this.Text = "Search results (" + results.Count + ")";
            }
            catch (Exception)
            {
                MessageBox.Show("Nome inválido para pesquisa.", "Search");
                this.Text = currentTitle;
            }
        }
        private List<ListViewItem> SearchAll(string folder, string text)
        {
            List<ListViewItem> results = new List<ListViewItem>();

            if (folder == "")
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        results.AddRange(SearchFolder(drive.RootDirectory.FullName, text));
                    }
                }
            }
            else
            {
                results.AddRange(SearchFolder(folder, text));
            }

            return results;
        }

        private List<ListViewItem> SearchFolder(string folder, string text)
        {
            List<ListViewItem> results = new List<ListViewItem>();

            try
            {
                DirectoryInfo dir = new DirectoryInfo(folder);

                foreach (FileSystemInfo info in dir.EnumerateFileSystemInfos("*" + text + "*"))
                {
                    if ((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue;
                    }

                    ListViewItem item = new ListViewItem(info.Name);
                    item.SubItems.Add(info.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));

                    FileInfo file = info as FileInfo;
                    if (file == null)
                    {
                        item.SubItems.Add("File folder");
                        item.SubItems.Add("");
                    }
                    else
                    {
                        item.SubItems.Add(file.Extension.TrimStart('.').ToUpper() + " File");
                        item.SubItems.Add(FormatSize(file.Length));
                    }

                    item.Tag = info.FullName;
                    results.Add(item);
                }

                foreach (DirectoryInfo sub in dir.EnumerateDirectories())
                {
                    if ((sub.Attributes & (FileAttributes.Hidden | FileAttributes.ReparsePoint)) != 0)
                    {
                        continue;
                    }

                    results.AddRange(SearchFolder(sub.FullName, text));
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (IOException)
            {
            }

            return results;
        }
        #endregion

        #region Árvore de pastas (TreeView)

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
            if (txtDirectory.Text == "💻  This PC  >")
            {
                lsvExplorer.View = View.Tile;
            }

            if (txtDirectory.Text != "💻  This PC  >")
            {
                lsvExplorer.View = View.Details;
            }
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

        #endregion

        #region Lista de ficheiros (ListView)

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

        #endregion

        #region Abrir pastas e ficheiros (duplo clique / Enter)

        private void lsvExplorer_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedItem();
        }

        private void lsvExplorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                OpenSelectedItem();
            }
        }

        private void OpenSelectedItem()
        {
            if (lsvExplorer.SelectedItems.Count == 0)
            {
                return;
            }

            object tag = lsvExplorer.SelectedItems[0].Tag;
            if (tag == null)
            {
                return;
            }

            string path = tag.ToString();

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

        #endregion

        #region Barra de ferramentas (menus Sort / View / New)

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

        #endregion

        #region Cut / Copy / Paste

        private void FillClipboard(bool cut)
        {
            if (currentPath == "" || lsvExplorer.SelectedItems.Count == 0)
            {
                return;
            }

            clipPaths.Clear();

            foreach (ListViewItem item in lsvExplorer.SelectedItems)
            {
                clipPaths.Add(item.Tag.ToString());
            }
            clipIsCut = cut;
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            FillClipboard(true);
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            FillClipboard(false);
        }

        private void PasteItems()
        {
            if (currentPath == "" || clipPaths.Count == 0)
            {
                return;
            }

            List<string> errors = new List<string>();

            foreach (string source in clipPaths)
            {
                string name = Path.GetFileName(source.TrimEnd(Path.DirectorySeparatorChar));

                try
                {
                    bool isDir = Directory.Exists(source);

                    if (isDir == false && File.Exists(source) == false)
                    {
                        errors.Add(name + ": já não existe.");
                        continue;
                    }

                    if (clipIsCut && SameFolder(Path.GetDirectoryName(source), currentPath))
                    {
                        continue;
                    }

                    if (isDir && IsSameOrInside(currentPath, source))
                    {
                        errors.Add(name + ": não podes colar uma pasta dentro dela própria.");
                        continue;
                    }

                    string dest = GetUniquePath(currentPath, name, isDir);

                    if (clipIsCut)
                    {
                        if (isDir)
                        {
                            MoveDirectory(source, dest);
                        }
                        else
                        {
                            File.Move(source, dest);
                        }
                    }
                    else
                    {
                        if (isDir)
                        {
                            CopyDirectory(source, dest);
                        }
                        else
                        {
                            File.Copy(source, dest);
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    errors.Add(name + ": acesso negado.");
                }
                catch (IOException ex)
                {
                    errors.Add(name + ": " + ex.Message);
                }
            }

            if (clipIsCut)
            {
                clipPaths.Clear();
            }

            LoadList(currentPath);

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Paste");
            }
        }

        private string GetUniquePath(string folder, string name, bool isDir)
        {
            string dest = Path.Combine(folder, name);

            if (File.Exists(dest) == false && Directory.Exists(dest) == false)
            {
                return dest;
            }

            string baseName = isDir ? name : Path.GetFileNameWithoutExtension(name);
            string ext = isDir ? "" : Path.GetExtension(name);
            int n = 1;

            do
            {
                string suffix = n == 1 ? " - Copy" : " - Copy (" + n + ")";
                dest = Path.Combine(folder, baseName + suffix + ext);
                n++;
            }
            while (File.Exists(dest) || Directory.Exists(dest));

            return dest;
        }

        private void CopyDirectory(string source, string dest)
        {
            Directory.CreateDirectory(dest);

            foreach (string file in Directory.GetFiles(source))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }

            foreach (string sub in Directory.GetDirectories(source))
            {
                CopyDirectory(sub, Path.Combine(dest, Path.GetFileName(sub)));
            }
        }

        private void MoveDirectory(string source, string dest)
        {
            if (string.Equals(Path.GetPathRoot(source), Path.GetPathRoot(dest), StringComparison.OrdinalIgnoreCase))
            {
                Directory.Move(source, dest);
            }
            else
            {
                CopyDirectory(source, dest);
                Directory.Delete(source, true);
            }
        }

        private bool SameFolder(string a, string b)
        {
            return string.Equals(a.TrimEnd('\\'), b.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
        }

        private bool IsSameOrInside(string folder, string parent)
        {
            string a = folder.TrimEnd('\\') + "\\";
            string b = parent.TrimEnd('\\') + "\\";
            return a.StartsWith(b, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Rename
        private void btnRename_Click(object sender, EventArgs e)
        {
            StartRename();
        }
        private void StartRename()
        {
            if (currentPath == "" || lsvExplorer.SelectedItems.Count == 0)
            {
                return;
            }

            lsvExplorer.SelectedItems[0].BeginEdit();  
        }
        private void lsvExplorer_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (currentPath == "")
            {
                e.CancelEdit = true;
            }
        }
        private void lsvExplorer_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            e.CancelEdit = true;

            if (e.Label == null)
            {
                return;
            }

            ListViewItem item = lsvExplorer.Items[e.Item];
            string oldPath = item.Tag.ToString();
            string newName = e.Label.Trim();

            if (newName == "")
            {
                MessageBox.Show("O nome não pode estar vazio.", "Rename");
                return;
            }

            if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Um nome não pode conter nenhum destes carateres:\n\\ / : * ? \" < > |", "Rename");
                return;
            }

            string newPath = Path.Combine(Path.GetDirectoryName(oldPath), newName);

            if (newPath == oldPath)
            {
                return;
            }

            bool onlyCase = string.Equals(oldPath, newPath, StringComparison.OrdinalIgnoreCase);

            if (onlyCase == false && (File.Exists(newPath) || Directory.Exists(newPath)))
            {
                MessageBox.Show("Já existe um ficheiro ou pasta com esse nome.", "Rename");
                return;
            }

            try
            {
                bool isDir = Directory.Exists(oldPath);

                if (onlyCase)
                {
                    string temp = Path.Combine(Path.GetDirectoryName(oldPath), Guid.NewGuid().ToString());
                    MovePath(oldPath, temp, isDir);
                    MovePath(temp, newPath, isDir);
                }
                else
                {
                    MovePath(oldPath, newPath, isDir);
                }

                item.Text = newName;
                item.Tag = newPath;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Não tens permissão para renomear este item.", "Rename");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Rename");
            }
        }
        private void MovePath(string from, string to, bool isDir)
        {
            if (isDir)
            {
                Directory.Move(from, to);
            }
            else
            {
                File.Move(from, to);
            }
        }

        #endregion

        #region Delete
        private void btnTrash_Click(object sender, EventArgs e)
        {
            DeleteSelected();

        }

        private void DeleteSelected()
        {
            if (currentPath == "" || lsvExplorer.SelectedItems.Count == 0)
            {
                return;
            }

            string question;
            if (lsvExplorer.SelectedItems.Count == 1)
            {
                question = "Mover \"" + lsvExplorer.SelectedItems[0].Text + "\" para a Reciclagem?";
            }
            else
            {
                question = "Mover " + lsvExplorer.SelectedItems.Count + " itens para a Reciclagem?";
            }

            DialogResult answer = MessageBox.Show(question, "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            List<ListViewItem> deleted = new List<ListViewItem>();
            List<string> errors = new List<string>();

            foreach (ListViewItem item in lsvExplorer.SelectedItems)
            {
                string path = item.Tag.ToString();

                try
                {
                    if (Directory.Exists(path))
                    {
                        VB.FileSystem.DeleteDirectory(path, VB.UIOption.OnlyErrorDialogs, VB.RecycleOption.SendToRecycleBin);
                    }
                    else if (File.Exists(path))
                    {
                        VB.FileSystem.DeleteFile(path, VB.UIOption.OnlyErrorDialogs, VB.RecycleOption.SendToRecycleBin);
                    }

                    deleted.Add(item);                 
                }
                catch (UnauthorizedAccessException)
                {
                    errors.Add(item.Text + ": acesso negado.");
                }
                catch (OperationCanceledException)
                {
                   
                }
                catch (IOException ex)
                {
                    errors.Add(item.Text + ": " + ex.Message);
                }
            }

            foreach (ListViewItem item in deleted)
            {
                lsvExplorer.Items.Remove(item);
            }

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Delete");
            }
        }

        #endregion


    }
}