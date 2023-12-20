using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public class BackUp
        {
            [JsonProperty]
            private string sourse_of_backup;
            [JsonProperty]
            private string destination_of_backup;
            [JsonProperty]
            private DateTime time_of_last_backup;
            [JsonProperty]
            private TimeSpan time_between_backup;
            [JsonProperty]
            private int counter_of_backup;

            public BackUp()
            {
                sourse_of_backup = null;
                destination_of_backup = null;
                time_of_last_backup = new DateTime();
                time_between_backup = new TimeSpan(0,0,0,0);
                counter_of_backup = 0;
            }

            public DateTime get_time_of_last_backup()
            {
                return time_of_last_backup;
            }

            public TimeSpan get_time_between_backup()
            {
                return time_between_backup;
            }

            public string get_sourse_of_backup()
            {
                return sourse_of_backup;
            }

            public string get_destination_of_backup()
            {
                return destination_of_backup;
            }

            public void set_time_of_last_backup(DateTime time)
            {
                time_of_last_backup = time; 
            }

            public void set_time_between_backup(TimeSpan time)
            {
                time_between_backup = time;
            }

            public void set_sourse_of_backup(string str = null)
            {
                if (!Directory.Exists(str) || !File.Exists(str)) 
                { 
                    //throw new Exception("No such a file or directory"); 
                }
  
                sourse_of_backup = str;
            }

            public void set_destination_of_backup(string str = null)
            {
                if (!Directory.Exists(str) || !File.Exists(str))
                {
                    //throw new Exception("No suci a file or directory");
                }

                destination_of_backup = str;
            }

            public void inc_counter_of_backup()
            {
                counter_of_backup++;
            }

            public void zero_counter_of_backup()
            {
                counter_of_backup = 0;
            }

            public int get_counter_of_backup()
            {
                return counter_of_backup;
            }

        }

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) // деструктор, который освобождает занимаемые ресурсы
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.notify_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(491, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Создать бэкап";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 388);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(491, 32);
            this.button3.TabIndex = 10;
            this.button3.Text = "Восстановление";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Существующие бэкапы:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 68);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(944, 305);
            this.textBox1.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 464);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(491, 32);
            this.button2.TabIndex = 14;
            this.button2.Text = "Удалить бэкап";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 502);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(491, 32);
            this.button4.TabIndex = 15;
            this.button4.Text = "Удалить все бэкапы";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 426);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(491, 32);
            this.button5.TabIndex = 16;
            this.button5.Text = "Бэкапы";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // notify_icon
            // 
            this.notify_icon.Text = "BackUpApp";
            this.notify_icon.Visible = true;
            this.notify_icon.Click += new System.EventHandler(this.notify_icon_Click);
            // 
            // worker
            // 
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 546);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Приложуха";
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Button button3;
        private Label label1;
        private TextBox textBox1;
        private List<BackUp> list_of_backups;
        private Button button2;
        private Button button4;
        private Button button5;
        private NotifyIcon notify_icon;
        private BackgroundWorker worker;
    }
}

