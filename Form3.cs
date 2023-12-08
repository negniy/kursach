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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
    }
}
