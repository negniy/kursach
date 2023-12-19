using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            path_from_create_backup = null;
            path_to_create_backup = null;
        }
        public Form2(string path_sourse, string path_dest, TimeSpan time)
        {
            InitializeComponent();
            path_from_create_backup = path_sourse;
            path_to_create_backup = path_dest;
            time_to_create_backup = time;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<TimeSpan> var_of_time = new List<TimeSpan>();
            var_of_time.Add(new TimeSpan(0, 0, 1, 0));
            var_of_time.Add(new TimeSpan(0, 0, 5, 0));
            var_of_time.Add(new TimeSpan(0, 0, 10, 0));
            var_of_time.Add(new TimeSpan(0, 0, 30, 0));
            var_of_time.Add(new TimeSpan(0, 1, 0, 0));
            var_of_time.Add(new TimeSpan(0, 3, 0, 0));
            var_of_time.Add(new TimeSpan(0, 12, 0, 0));
            var_of_time.Add(new TimeSpan(0, 24, 0, 0));
            var_of_time.Add(new TimeSpan(3, 0, 0, 0));
            var_of_time.Add(new TimeSpan(7, 0, 0, 0));
            var_of_time.Add(new TimeSpan(30, 0, 0, 0));

            time_to_create_backup = new TimeSpan(0, 0, 0, 0);
            if (comboBox1.SelectedIndex != -1)
            {
                time_to_create_backup = var_of_time[comboBox1.SelectedIndex];
                textBox3.Text = var_of_time[comboBox1.SelectedIndex].ToString();
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // добавление элементов в выпадающий список
            comboBox1.Items.Add("1 мин");
            comboBox1.Items.Add("5 мин");
            comboBox1.Items.Add("10 мин");
            comboBox1.Items.Add("30 мин");
            comboBox1.Items.Add("1 ч");
            comboBox1.Items.Add("3 ч");
            comboBox1.Items.Add("12 ч");
            comboBox1.Items.Add("24 ч");
            comboBox1.Items.Add("3 дн");
            comboBox1.Items.Add("7 дн");
            comboBox1.Items.Add("1 мec");
            this.Controls.Add(comboBox1);

            if (path_from_create_backup != null)
            {
                textBox1.Text = path_from_create_backup;
            }
            else { textBox1.Text = "Путь не выбран"; }
            if (path_to_create_backup != null)
            {
                textBox2.Text = path_to_create_backup;
            }
            else { textBox2.Text = "Путь не выбран"; }
            if (time_to_create_backup != null)
            {
                textBox3.Text = time_to_create_backup.ToString();
            }
            else { textBox3.Text = "Время не задано"; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path_from_create_backup = null;

            FolderBrowserDialog DirDialog = new FolderBrowserDialog(); // выбор папки
            DirDialog.Description = "Выбор директории откуда";
            DirDialog.SelectedPath = @"С:\";
            if (DirDialog.ShowDialog() == DialogResult.OK)
            {
                path_from_create_backup = DirDialog.SelectedPath;
            }

            textBox1.Text = path_from_create_backup;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            path_to_create_backup = null;

            FolderBrowserDialog DirDialog = new FolderBrowserDialog(); // выбор папки
            DirDialog.Description = "Выбор директории куда";
            DirDialog.SelectedPath = @"С:\";
            if (DirDialog.ShowDialog() == DialogResult.OK)
            {
                path_to_create_backup = DirDialog.SelectedPath;
            }
            textBox2.Text = path_to_create_backup;
        }

        public TimeSpan get_time_to_create_backup()
        {
            return time_to_create_backup;
        }

        public string get_path_from_create_backup()
        {
            return path_from_create_backup;
        }

        public string get_path_to_create_backup()
        {
            return path_to_create_backup;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (path_from_create_backup == null || time_to_create_backup == null || path_to_create_backup == null)
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
                Close();
        }
    }
}
