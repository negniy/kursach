using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3(List<string> list_path, bool look)
        {
            InitializeComponent();
            this.list_of_path = list_path;
            look_only = look;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            look_only = false;
            if (comboBox2.SelectedIndex != -1)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Выберите индекс");
            }   
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                selected_index = comboBox2.SelectedIndex;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // добавление элементов в выпадающий список
            for (int i = 0; i < range; i++)
            {
                comboBox2.Items.Add((i+1).ToString());
            }
            Controls.Add(comboBox2);
            selected_index = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selected_index == -1) 
            {
                MessageBox.Show("Выберите индекс");
                return;
            }
            OpenFileDialog DirDialog = new OpenFileDialog(); // просмотр папки
            DirDialog.Title = "Существующие бэкапы";
            DirDialog.InitialDirectory = list_of_path[selected_index];
            DirDialog.ShowReadOnly = true;
            DirDialog.ShowDialog();
            look_only = true;
        }
    }
}
