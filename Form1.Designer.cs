namespace FileExplorer_240075
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.imlFileExplorer = new System.Windows.Forms.ImageList(this.components);
            this.panelLeft = new System.Windows.Forms.Panel();
            this.trwFile = new System.Windows.Forms.TreeView();
            this.panelToolBar = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnShare = new System.Windows.Forms.Button();
            this.btnTrash = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.menuSort = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extraLargeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelToolBar.SuspendLayout();
            this.menuSort.SuspendLayout();
            this.menuView.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.txtDirectory);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnUp);
            this.panelTop.Controls.Add(this.btnForward);
            this.panelTop.Controls.Add(this.btnBack);
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(799, 42);
            this.panelTop.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(637, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(151, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Search This PC";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDirectory.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirectory.Location = new System.Drawing.Point(221, 6);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(410, 27);
            this.txtDirectory.TabIndex = 1;
            this.txtDirectory.Text = "💻  This PC  >";
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(170, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "↻";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnUp
            // 
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(117, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(47, 32);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // btnForward
            // 
            this.btnForward.Enabled = false;
            this.btnForward.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnForward.FlatAppearance.BorderSize = 0;
            this.btnForward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForward.Location = new System.Drawing.Point(60, 3);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(51, 33);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = "→";
            this.btnForward.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Enabled = false;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(51, 33);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // imlFileExplorer
            // 
            this.imlFileExplorer.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imlFileExplorer.ImageSize = new System.Drawing.Size(16, 16);
            this.imlFileExplorer.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeft.Controls.Add(this.trwFile);
            this.panelLeft.Location = new System.Drawing.Point(3, 106);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(246, 454);
            this.panelLeft.TabIndex = 2;
            // 
            // trwFile
            // 
            this.trwFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.trwFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trwFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.trwFile.ItemHeight = 30;
            this.trwFile.Location = new System.Drawing.Point(0, -3);
            this.trwFile.Name = "trwFile";
            this.trwFile.ShowLines = false;
            this.trwFile.ShowPlusMinus = false;
            this.trwFile.Size = new System.Drawing.Size(246, 493);
            this.trwFile.TabIndex = 0;
            // 
            // panelToolBar
            // 
            this.panelToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelToolBar.Controls.Add(this.btnView);
            this.panelToolBar.Controls.Add(this.btnSort);
            this.panelToolBar.Controls.Add(this.btnTrash);
            this.panelToolBar.Controls.Add(this.btnShare);
            this.panelToolBar.Controls.Add(this.btnRename);
            this.panelToolBar.Controls.Add(this.btnCopy);
            this.panelToolBar.Controls.Add(this.btnCut);
            this.panelToolBar.Controls.Add(this.btnNew);
            this.panelToolBar.Location = new System.Drawing.Point(0, 51);
            this.panelToolBar.Name = "panelToolBar";
            this.panelToolBar.Size = new System.Drawing.Size(799, 49);
            this.panelToolBar.TabIndex = 3;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(10, 6);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 40);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "➕ New";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // btnCut
            // 
            this.btnCut.FlatAppearance.BorderSize = 0;
            this.btnCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCut.Font = new System.Drawing.Font("Segoe UI Emoji", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCut.Location = new System.Drawing.Point(100, 6);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(45, 40);
            this.btnCut.TabIndex = 4;
            this.btnCut.Text = "✂️";
            this.btnCut.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI Emoji", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Location = new System.Drawing.Point(151, 6);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(45, 40);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "📋";
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // btnRename
            // 
            this.btnRename.FlatAppearance.BorderSize = 0;
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRename.Font = new System.Drawing.Font("Segoe UI Emoji", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRename.Location = new System.Drawing.Point(202, 6);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(45, 40);
            this.btnRename.TabIndex = 6;
            this.btnRename.Text = "🏷️";
            this.btnRename.UseVisualStyleBackColor = true;
            // 
            // btnShare
            // 
            this.btnShare.FlatAppearance.BorderSize = 0;
            this.btnShare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShare.Font = new System.Drawing.Font("Segoe UI Emoji", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShare.Location = new System.Drawing.Point(253, 6);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(47, 40);
            this.btnShare.TabIndex = 7;
            this.btnShare.Text = "🔗";
            this.btnShare.UseVisualStyleBackColor = true;
            // 
            // btnTrash
            // 
            this.btnTrash.FlatAppearance.BorderSize = 0;
            this.btnTrash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrash.Font = new System.Drawing.Font("Segoe UI Emoji", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrash.Location = new System.Drawing.Point(306, 6);
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Size = new System.Drawing.Size(47, 40);
            this.btnTrash.TabIndex = 8;
            this.btnTrash.Text = "🗑️";
            this.btnTrash.UseVisualStyleBackColor = true;
            // 
            // btnSort
            // 
            this.btnSort.FlatAppearance.BorderSize = 0;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSort.Location = new System.Drawing.Point(349, 6);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(84, 40);
            this.btnSort.TabIndex = 4;
            this.btnSort.Text = "⇅ Sort ˅";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnView
            // 
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Location = new System.Drawing.Point(439, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(82, 40);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "⊞ View ˅";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // menuSort
            // 
            this.menuSort.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameToolStripMenuItem,
            this.dateToolStripMenuItem,
            this.sizeToolStripMenuItem});
            this.menuSort.Name = "menuSort";
            this.menuSort.Size = new System.Drawing.Size(176, 76);
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.nameToolStripMenuItem.Text = "Name";
            // 
            // dateToolStripMenuItem
            // 
            this.dateToolStripMenuItem.Name = "dateToolStripMenuItem";
            this.dateToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.dateToolStripMenuItem.Text = "Date Modified";
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.sizeToolStripMenuItem.Text = "Size";
            // 
            // menuView
            // 
            this.menuView.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extraLargeIconsToolStripMenuItem,
            this.largeIconsToolStripMenuItem,
            this.mediumIconsToolStripMenuItem,
            this.smallIconsToolStripMenuItem,
            this.listToolStripMenuItem,
            this.detailsToolStripMenuItem,
            this.tilesToolStripMenuItem,
            this.contentToolStripMenuItem});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(188, 212);
            // 
            // extraLargeIconsToolStripMenuItem
            // 
            this.extraLargeIconsToolStripMenuItem.Name = "extraLargeIconsToolStripMenuItem";
            this.extraLargeIconsToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.extraLargeIconsToolStripMenuItem.Text = "Extra large icons";
            // 
            // largeIconsToolStripMenuItem
            // 
            this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
            this.largeIconsToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.largeIconsToolStripMenuItem.Text = "Large icons";
            // 
            // mediumIconsToolStripMenuItem
            // 
            this.mediumIconsToolStripMenuItem.Name = "mediumIconsToolStripMenuItem";
            this.mediumIconsToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.mediumIconsToolStripMenuItem.Text = "Medium icons";
            // 
            // smallIconsToolStripMenuItem
            // 
            this.smallIconsToolStripMenuItem.Name = "smallIconsToolStripMenuItem";
            this.smallIconsToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.smallIconsToolStripMenuItem.Text = "Small icons";
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.listToolStripMenuItem.Text = "List";
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Checked = true;
            this.detailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.detailsToolStripMenuItem.Text = "Details";
            // 
            // tilesToolStripMenuItem
            // 
            this.tilesToolStripMenuItem.Name = "tilesToolStripMenuItem";
            this.tilesToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.tilesToolStripMenuItem.Text = "Tiles";
            // 
            // contentToolStripMenuItem
            // 
            this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            this.contentToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.contentToolStripMenuItem.Text = "Content";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 562);
            this.Controls.Add(this.panelToolBar);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "This PC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelToolBar.ResumeLayout(false);
            this.menuSort.ResumeLayout(false);
            this.menuView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.ImageList imlFileExplorer;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.TreeView trwFile;
        private System.Windows.Forms.Panel panelToolBar;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ContextMenuStrip menuSort;
        private System.Windows.Forms.ToolStripMenuItem nameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuView;
        private System.Windows.Forms.ToolStripMenuItem extraLargeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
    }
}

