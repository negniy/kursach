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

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if(list_of_backups == null) list_of_backups = DeserializeBackUpInfo();
                if (list_of_backups != null)
                {
                    foreach (BackUp bu in list_of_backups)
                    {
                        var last_modified = File.GetLastWriteTime(bu.get_sourse_of_backup());
                        var variance = last_modified.Subtract(bu.get_time_of_last_backup());
                        var result = TimeSpan.Compare(variance, bu.get_time_between_backup());
                        if (result >= 0)
                        {
                            RefreshBackUp(bu);
                        }
                    }
                }
                SerializeBackUpInfo();
                // System.Threading.Thread.Sleep(1000);
                if (worker.CancellationPending) return;
            }

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("рк не обновляются");
        }

        private void notify_icon_Click(object Sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            // Activate the form.
            this.Activate();
        }

        private void Form1_Deactivate(object sender, EventArgs e) 
        {
            if (WindowState == FormWindowState.Minimized) // если свернуто
            {
                ShowInTaskbar = false;
                notify_icon.Visible = true;
            }
            // worker.RunWorkerAsync();
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

            ShowBackUpInfo();
        }

        private void Form1_Load(object sender, EventArgs e) // загрузка формы
        {
            worker.CancelAsync();
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

        private void button5_Click(object sender, EventArgs e) // изменение бэкпапа
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
    }
}
