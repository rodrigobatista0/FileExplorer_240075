using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FileExplorer_240075
{
    public partial class Form1 : Form
    {
        string defaultName = "New Folder";
        string currentPath = null;
        string currenTitle = null;
        List<string> backList = new List<string>();
        List<string> forwardList = new List<string>();

        public Form1()
        {
            InitializeComponent();
            ConfigureTree();
            ConfigurarListView();
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

                        // não mostra pastas escondidas
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

        private void ConfigurarListView()
        {
            lsvExplorer.View = View.Details;
            lsvExplorer.FullRowSelect = true;
            lsvExplorer.Columns.Clear();
            lsvExplorer.Columns.Add("Name", 260);
            lsvExplorer.Columns.Add("Date modified", 140);
            lsvExplorer.Columns.Add("Type", 120);
            lsvExplorer.Columns.Add("Size", 90, HorizontalAlignment.Right);
            lsvExplorer.SmallImageList = imlFileExplorer;
        }
    }
}

