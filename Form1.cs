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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateBackUpInfo() // вывод информации о бэкапах
        {
            textBox1.Text = " ";
            int counter = 1;
            foreach (BackUp bu in list_of_backups)
            {
                textBox1.Text += " "+counter.ToString()+") "+"Источник данных: " + bu.get_sourse_of_backup() + " Расположение бэкапа: " + bu.get_destination_of_backup() + " Последнее копирование: " + bu.get_time_of_last_backup()+"\r\n";
                counter++;
            }
             
        }

        private void button1_Click(object sender, EventArgs e) // создание бэкпапа
        {
            Form2 f = new Form2(); // создаем новое окно
            f.ShowDialog(); // ждём выполнения
            BackUp bu = new BackUp();
            bu.set_time_between_backup(f.get_time_to_create_backup());
            bu.set_time_of_last_backup(DateTime.Now);
            bu.set_sourse_of_backup(f.get_path_from_create_backup());
            bu.set_destination_of_backup(f.get_path_to_create_backup());

            CreateBackUp(bu);

            UpdateBackUpInfo();
        }

        private void Form1_Load(object sender, EventArgs e) // загрузка формы
        {
            list_of_backups = new List<BackUp>();
        }

        private void button2_Click(object sender, EventArgs e) // удаление бэкапа
        {
            if (list_of_backups.Count == 0) return;
            Form3 f = new Form3();
            f.set_range(list_of_backups.Count);
            f.ShowDialog();
            // удалить папку
            list_of_backups.RemoveAt(f.get_selected_index());
            UpdateBackUpInfo();
        }

        private void button4_Click(object sender, EventArgs e) // удаление всех бэкапов
        {
            // удалить папки
            list_of_backups.Clear();
            UpdateBackUpInfo();
        }

        private void button5_Click(object sender, EventArgs e) // изменение бэкпапа
        {
            if (list_of_backups.Count == 0) return;
            Form3 f = new Form3();
            f.set_range(list_of_backups.Count);
            f.ShowDialog();
            // инменить
            UpdateBackUpInfo();
        }
    }
}
