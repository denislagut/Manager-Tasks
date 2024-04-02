using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Task_manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            num_task.Add(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите задачу", "Задача не найдена", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (DateTime.Today <= monthCalendar1.SelectionRange.Start) state = "В процессе";
                else state = "Просрочено";
                dataGridView1.Rows.Add(num_task[num_task.Count - 1], textBox1.Text, state, monthCalendar1.SelectionRange.Start.ToShortDateString());
                num_task.Add(num_task[num_task.Count - 1] + 1);
                if (state == "В процессе")
                {
                    dataGridView1[2, dataGridView1.Rows.Count - 2].Style.BackColor = Color.SkyBlue;
                }
                else
                {
                    dataGridView1[2, dataGridView1.Rows.Count - 2].Style.BackColor = Color.LightCoral;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1[2, i].Value != "Выполнено")
                {
                    if (DateTime.Today <= DateTime.Parse(dataGridView1[3, i].Value.ToString()))
                    {

                        dataGridView1[2, i].Value = "В процессе";
                        dataGridView1[2, i].Style.BackColor = Color.SkyBlue;
                    }
                    else
                    {
                        dataGridView1[2, i].Value = "Просрочено";
                        dataGridView1[2, i].Style.BackColor = Color.LightCoral;
                    }
                }
                else dataGridView1[2, i].Style.BackColor = Color.PaleGreen;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                int number, index = -1;
                bool task_found = false;
                bool check = int.TryParse(textBox2.Text, out number);
                if (check && (number >= 0 && number <= dataGridView1.Rows.Count))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Column4"].Value != null && row.Cells["Column4"].Value.ToString() == number.ToString())
                        {
                            for (int i = 0; i < num_task.Count; i++)
                            {
                                if (index != -1)
                                {
                                    dataGridView1[0, num_task[i] - 1].Value = num_task[i] - 1;
                                    num_task[i]--;
                                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = "";
                                }
                                if (num_task[i] == number) index = i;
                            }
                            num_task.RemoveAt(index);
                            dataGridView1.Rows.Remove(row);
                            task_found = true;
                            break;
                        }
                    }
                    if (!task_found) MessageBox.Show("Задача не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Неверный номер задачи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                int number;
                bool task_found = false;
                bool check = int.TryParse(textBox2.Text, out number);
                if (check && (number >= 0 && number <= dataGridView1.Rows.Count))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Column3"].Value != null && row.Cells["Column4"].Value.ToString() == number.ToString())
                        {
                            row.Cells["Column3"].Value = "Выполнено";
                            task_found = true;
                            break;
                        }
                    }
                    if (!task_found) MessageBox.Show("Задача не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Неверный номер задачи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
