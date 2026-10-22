using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer_240075
{
    public partial class Form1 : Form
    {
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

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if(txtSearch.Text == "Search This PC")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtSearch.Text))
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
    }
}
