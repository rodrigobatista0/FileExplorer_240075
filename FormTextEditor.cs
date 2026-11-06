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
    public partial class FormTextEditor : Form
    {
        string filePath;
        bool changed = false;
        public FormTextEditor(string path)
        {
            InitializeComponent();
            
        }
    }
}
