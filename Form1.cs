using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path_from_create_backup = null;
            string path_to_create_backup = null;

            FolderBrowserDialog DirDialog = new FolderBrowserDialog(); // выбор папки
            DirDialog.Description = "Выбор директории откуда";
            DirDialog.SelectedPath = @"С:\";
            if (DirDialog.ShowDialog() == DialogResult.OK)
            {
                path_from_create_backup = DirDialog.SelectedPath;
            }

            FolderBrowserDialog DirDialog1 = new FolderBrowserDialog(); // выбор папки
            DirDialog1.Description = "Выбор директории куда";
            DirDialog1.SelectedPath = @"С:\";
            if (DirDialog1.ShowDialog() == DialogResult.OK)
            {
                path_to_create_backup = DirDialog1.SelectedPath;
            }

            CreateBackUp(path_from_create_backup, path_to_create_backup);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
