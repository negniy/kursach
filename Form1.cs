using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Minimized; // свернуть окно
            ShowInTaskbar = false; // скрыть из таскбара
            notify_icon.Icon = new Icon("BUA.ico");
            worker.RunWorkerAsync(); // запускаем фоном задачу
        }

        void worker_DoWork(object sender, DoWorkEventArgs e) // асинхронный процесс
        {
            while (true)
            {
                if(list_of_backups == null) list_of_backups = DeserializeBackUpInfo();
                if (list_of_backups != null)
                {
                    foreach (BackUp bu in list_of_backups)
                    {
                        var now = DateTime.Now;
                        var variance = now.Subtract(bu.get_time_of_last_backup());
                        var result = TimeSpan.Compare(variance, bu.get_time_between_backup());
                        if (result >= 0)
                        {
                            RefreshBackUp(bu);
                        }
                    }
                }
                SerializeBackUpInfo();
                System.Threading.Thread.Sleep(1000*60);
            }

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) // заканчивание асинхронного процесса
        {
            MessageBox.Show("рк не обновляются");

        }

        private void notify_icon_Click(object Sender, EventArgs e) // клик на иконку в трее
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            // Activate the form.
            this.Activate();
            ShowBackUpInfo();
        }

        private void Form1_Deactivate(object sender, EventArgs e) // сворачивание формы
        {
            if (WindowState == FormWindowState.Minimized) // если свернуто
            {
                ShowInTaskbar = false;
                notify_icon.Visible = true;
            }
        }

        private void SerializeBackUpInfo() // сериализует данные бэкапов в json
        {
            string path_to_app_dir = "C:\\BackUp_Application";
            if (!Directory.Exists(path_to_app_dir))
            {
                Directory.CreateDirectory(path_to_app_dir);
            }
            string file_name = Path.Combine(path_to_app_dir, "BackUps.json");
            var backup_data = JsonConvert.SerializeObject(list_of_backups, Formatting.Indented);
            File.WriteAllText(file_name, backup_data);
        }

        private List<BackUp> DeserializeBackUpInfo() // десериализует данные бэкапов из json
        {
            string path_to_app_dir = "C:\\BackUp_Application";
            string file_name = Path.Combine(path_to_app_dir, "BackUps.json");
            if (!Directory.Exists(path_to_app_dir) || !File.Exists(file_name))
            {
                return null;
            }
            var json_data = File.ReadAllText(file_name);
            List<BackUp> backup_data = JsonConvert.DeserializeObject<List<BackUp>>(json_data);
            return backup_data;
        }

        private void ShowBackUpInfo() // вывод информации о бэкапах
        {
            textBox1.Text = " ";
            int counter = 1;
            if (list_of_backups == null) return;
            foreach (BackUp bu in list_of_backups)
            {
                textBox1.Text += " " + counter.ToString() + ") " + "Источник данных: " + bu.get_sourse_of_backup() + " Расположение бэкапа: " + bu.get_destination_of_backup() + " Последнее копирование: " + bu.get_time_of_last_backup() + "\r\n";
                counter++;
            }
        }

        private void CopyFolder(string sourse, string dest)
        {
            string[] files = Directory.GetFiles(sourse);
            foreach (string file in files)
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)), true);
            }
            string[] folders = Directory.GetDirectories(sourse);
            foreach (string folder in folders)
            {
                Directory.CreateDirectory(Path.Combine(dest, Path.GetFileName(folder)));
                CopyFolder(folder, Path.Combine(dest, Path.GetFileName(folder)));
            }
        }

        private void CreateBackUp(BackUp bu)
        {
            list_of_backups.Add(bu);
            int index = list_of_backups.IndexOf(bu);
            string path_to_folder = bu.get_destination_of_backup() + "\\BackUp_" + index.ToString() + "_0";
            Directory.CreateDirectory(path_to_folder);
            list_of_backups[index].inc_counter_of_backup();
            CopyFolder(bu.get_sourse_of_backup(), path_to_folder);

            MessageBox.Show("РК завершено");
            SerializeBackUpInfo();

        }

        private void UpdateBackUp(BackUp bu)
        {
            int index = list_of_backups.IndexOf(bu);
            string path_to_folder = bu.get_destination_of_backup() + "\\BackUp_" + bu.get_counter_of_backup().ToString() + "_" + index.ToString();
            Directory.CreateDirectory(path_to_folder);
            list_of_backups[index].inc_counter_of_backup();
            CopyFolder(bu.get_sourse_of_backup(), path_to_folder);
            list_of_backups[index].set_time_of_last_backup(DateTime.Now);
            SerializeBackUpInfo();
        }

        private void RefreshBackUp(BackUp bu)
        {
            int index = list_of_backups.IndexOf(bu);
            if (index == -1) return;

            string path_to_folder = bu.get_destination_of_backup() + "\\BackUp_" + index.ToString() + "_" + bu.get_counter_of_backup();
            Directory.CreateDirectory(path_to_folder);
            list_of_backups[index].inc_counter_of_backup();
            CopyFolder(bu.get_sourse_of_backup(), path_to_folder);
            list_of_backups[index].set_time_of_last_backup(DateTime.Now);
            SerializeBackUpInfo();
        }

        private void button1_Click(object sender, EventArgs e) // создание бэкпапа
        {
            Form2 f = new Form2(); // создаем новое окно
            f.ShowDialog(); // ждём выполнения
            BackUp bu = new BackUp();
            if (f.get_path_from_create_backup() == null || f.get_time_to_create_backup() == null || f.get_path_to_create_backup() == null) return;
            bu.set_time_between_backup(f.get_time_to_create_backup());
            bu.set_time_of_last_backup(DateTime.Now);
            bu.set_sourse_of_backup(f.get_path_from_create_backup());
            bu.set_destination_of_backup(f.get_path_to_create_backup());

            CreateBackUp(bu);

            ShowBackUpInfo();
        }

        private void Form1_Load(object sender, EventArgs e) // загрузка формы
        {
            if (DeserializeBackUpInfo() == null) 
            { 
                list_of_backups = new List<BackUp>();
            }
            else
            {
                list_of_backups = DeserializeBackUpInfo();
            }
            ShowBackUpInfo();
        }

        private void button2_Click(object sender, EventArgs e) // удаление бэкапа
        {
            if (list_of_backups.Count == 0) return;
            List<string> list_path = new List<string>();
            foreach (BackUp b in list_of_backups)
            {
                list_path.Add(b.get_destination_of_backup());
            }

            Form3 f = new Form3(list_path, true);
            f.set_range(list_of_backups.Count);
            f.ShowDialog();
            // удалить папку
            list_of_backups.RemoveAt(f.get_selected_index());
            SerializeBackUpInfo();
            ShowBackUpInfo();
        }

        private void button4_Click(object sender, EventArgs e) // удаление всех бэкапов
        {
            // удалить папки????
            list_of_backups.Clear();
            SerializeBackUpInfo();
            ShowBackUpInfo();
        }

        private void button5_Click(object sender, EventArgs e) // изменение/просмотр бэкпапа
        {
            if (list_of_backups.Count == 0) return;

            List<string> list_path = new List<string>();
            foreach (BackUp b in list_of_backups)
            {
                list_path.Add(b.get_destination_of_backup());
            }

            Form3 f = new Form3(list_path, false);
            f.set_range(list_of_backups.Count);
            f.ShowDialog();
            if (f.get_look_only() == true) return;
            BackUp bu = list_of_backups[f.get_selected_index()];
            Form2 f2 = new Form2(bu.get_sourse_of_backup(), bu.get_destination_of_backup(), bu.get_time_between_backup());
            f2.ShowDialog();
            bu.set_time_between_backup(f2.get_time_to_create_backup());
            bu.set_time_of_last_backup(DateTime.Now);
            bu.set_sourse_of_backup(f2.get_path_from_create_backup());
            bu.set_destination_of_backup(f2.get_path_to_create_backup());
            list_of_backups[f.get_selected_index()] = bu;
            if (worker.IsBusy) worker.CancelAsync();
            UpdateBackUp(bu);

            ShowBackUpInfo();
        }

        private void button3_Click(object sender, EventArgs e) // восстановление
        {
            if (list_of_backups.Count == 0) return;

            List<string> list_path = new List<string>();
            foreach (BackUp b in list_of_backups)
            {
                list_path.Add(b.get_destination_of_backup());
            }

            Form3 f = new Form3(list_path, false);
            f.set_range(list_of_backups.Count);
            f.ShowDialog();
            if (f.get_look_only() == true) return;
            BackUp bu = list_of_backups[f.get_selected_index()];
            Form4 f1 = new Form4(bu, f.get_selected_index());
            f1.ShowDialog();
            list_of_backups[f.get_selected_index()] = f1.GetBackUp();
            SerializeBackUpInfo();
            ShowBackUpInfo();
        }
    }
}
