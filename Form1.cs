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

        public Form1()
        {
            InitializeComponent();
            AddNodes();
        }
        public void AddNodes()
        {
            trwFile.Nodes.Clear();

            trwFile.Nodes.Add("🏠 Home");
            trwFile.Nodes.Add("🖼️ Gallery");

            TreeNode quickAcessNodes = trwFile.Nodes.Add("📌 Quick Access");
            quickAcessNodes.Nodes.Add("🖥️ Desktop");
            quickAcessNodes.Nodes.Add("📥 Downloads");
            quickAcessNodes.Nodes.Add("📄 Documents");
            quickAcessNodes.Nodes.Add("🖼️ Pictures");
            quickAcessNodes.Nodes.Add("🎵 Music");
            quickAcessNodes.Nodes.Add("🎬 Videos");

            quickAcessNodes.Expand();

            trwFile.Nodes.Add("💻 This PC");
        }

        public void ShowThisPC()
        {
            panelMain.Controls.Clear();

            currentPath = null;
            currenTitle = null;
            panelMain.Controls.Add(lblDeviceAndDrivers);
            panelMain.Controls.Add(panelCardDriveC);

            txtDirectory.Text = "💻  This PC  >";
        }

        public void OpenFolder(string caminho, string nomeTitulo)
        {
            panelMain.Controls.Clear();
            txtDirectory.Text = $"💻  This PC  > {nomeTitulo}";
            currentPath = caminho ;
            currenTitle = nomeTitulo ;

            Label lblTitulo = new Label
            {
                Text = nomeTitulo,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            panelMain.Controls.Add(lblTitulo);

            try
            {
                string[] subpastas = Directory.GetDirectories(caminho);
                int posTop = 60;
                int posLeft = 20;

                foreach (string s in subpastas)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(s);
                    if ((dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        continue;

                    Panel cardPasta = new Panel
                    {
                        Location = new Point(posLeft, posTop),
                        Size = new Size(110, 90),
                        BackColor = Color.White,
                        Cursor = Cursors.Hand,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    Label lblIcon = new Label
                    {
                        Text = "📁",
                        Font = new Font("Segoe UI Emoji", 24F),
                        Location = new Point(32, 10),
                        AutoSize = true
                    };

                    Label lblNome = new Label
                    {
                        Text = dirInfo.Name,
                        Font = new Font("Segoe UI", 8.5F),
                        Location = new Point(5, 58),
                        Size = new Size(100, 28),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    cardPasta.Controls.Add(lblIcon);
                    cardPasta.Controls.Add(lblNome);
                    panelMain.Controls.Add(cardPasta);

                    posLeft += 130;
                    if (posLeft > panelMain.Width - 140)
                    {
                        posLeft = 20;
                        posTop += 105;
                    }
                    
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Acesso negado a esta pasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetUniqueFolderName(string basePath, string name)
        {
            string candidate = name;
            int idx = 2;
            while (Directory.Exists(Path.Combine(basePath, candidate)))
                candidate = $"{name} ({idx++})";
            return candidate;
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
        private void stripFolder_Click(object sender, EventArgs e)
        {
            if(currentPath == null)
            {
                MessageBox.Show("You need to have a directory selected to create a folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string name = GetUniqueFolderName(currentPath, defaultName);
                Directory.CreateDirectory(Path.Combine(currentPath, name));
                OpenFolder(currentPath, currenTitle);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("The acess to this folder as reject", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void trwFile_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text.Contains("This PC"))
            {
                ShowThisPC();
            }
            else if (e.Node.Text.Contains("Documents"))
            {
                OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Documents");
            }
            else if (e.Node.Text.Contains("Downloads"))
            {
                OpenFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"), "Downloads");
            }
            else if (e.Node.Text.Contains("Desktop"))
            {
                OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Desktop");
            }
            else if (e.Node.Text.Contains("Pictures"))
            {
                OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Pictures");
            }
            else if (e.Node.Text.Contains("Music"))
            {
                OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Music");
            }
            else if (e.Node.Text.Contains("Videos"))
            {
                OpenFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Videos");
            }
        }
    }
}

