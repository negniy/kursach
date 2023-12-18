using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4(WindowsFormsApp1.Form1.BackUp bu)
        {
            InitializeComponent();
            back = bu;
            list_of_rk = null;
            list_of_rk = Directory.GetDirectories(bu.get_destination_of_backup(), "BackUp_*");
            Console.WriteLine(list_of_rk);
        }

        private void ShowRKInfo() // просмотр информации о Рк
        {
            textBox1.Text = " ";
            int counter = 1;
            if (list_of_rk == null) return;
            foreach (string rk in list_of_rk)
            {
                textBox1.Text += " " + counter.ToString() + ") " + "Название РК: " + rk + " Дата и время создания: " + Directory.GetCreationTime(rk).ToString()+ "\r\n";
                counter++;
            }
        }

        public void CopyFolder(string sourse, string dest)
        {
            string[] files = Directory.GetFiles(sourse);
            foreach (string file in files)
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)), true);
            }
            string[] folders = Directory.GetDirectories(sourse);
            foreach (string folder in folders)
            {
                CopyFolder(folder, Path.Combine(dest, Path.GetFileName(folder)));
            }
        }

        private void Regain() // восстановление
        {
            CopyFolder(list_of_rk[selected_index], back.get_sourse_of_backup());
        }

        private void DeleteRK() // удаление выбранной копии
        {
            int counter = 0;
            foreach (string rk in list_of_rk)
            {
                if(counter == selected_index) Directory.Delete(rk, true); // удаляет каталог с подкатологами и файлами
                counter++;
            }
            for (int i = selected_index; i < list_of_rk.Length - 1; i++)
            {
                list_of_rk[i] = list_of_rk[i + 1];
            }
            Array.Resize(ref list_of_rk, list_of_rk.Length - 1);
        }

        private void button3_Click(object sender, EventArgs e) // восстановление
        {
            if (selected_index == -1)
            {
                MessageBox.Show("Выберите индекс");
                return;
            }

            Regain();
            MessageBox.Show("Восстановление успешно завершено");
            ShowRKInfo();
        }

        private void button2_Click(object sender, EventArgs e) // удаление выбранной копии
        {
            if(selected_index == -1)
            {
                MessageBox.Show("Выберите индекс");
                return;
            }

            DeleteRK();
            MessageBox.Show("Удаление выбранной копии успешно завершено");
            ShowRKInfo();
        }

        private void button4_Click(object sender, EventArgs e) // удаление всех копий
        {
            if (list_of_rk.Length == 0) return;
            foreach (string rk in list_of_rk)
            {
                Directory.Delete(rk, true); // удаляет каталог с подкатологами и файлами
            }
            list_of_rk = null;
            back.zero_counter_of_backup();
            MessageBox.Show("Удаление всех копий успешно завершено");
            ShowRKInfo();
        }

        public WindowsFormsApp1.Form1.BackUp GetBackUp()
        {
            return back;
        }

        private void Form4_Load(object sender, EventArgs e) // загрузка формы
        {
            ShowRKInfo();

            for (int i = 0; i < list_of_rk.Length; i++)
            {
                comboBox1.Items.Add((i + 1).ToString());
            }
            Controls.Add(comboBox1);
            selected_index = -1;
        }

        private void button1_Click(object sender, EventArgs e) // открыть файловый диалог
        {
            OpenFileDialog DirDialog = new OpenFileDialog(); // просмотр папки
            DirDialog.Title = "Существующие бэкапы";
            DirDialog.InitialDirectory = back.get_destination_of_backup();
            DirDialog.ShowReadOnly = true;
            DirDialog.ShowDialog();
            ShowRKInfo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                selected_index = comboBox1.SelectedIndex;
            }
        }
    }
}
