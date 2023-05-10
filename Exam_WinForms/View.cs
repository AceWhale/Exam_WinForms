using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Exam_WinForms
{
    public partial class View : Form
    {
        Button[] buttons;
        static Controller controller;
        List<Task> time = new List<Task>();
        string text;
        static int timer = 0;
        int mistake = 0, max_speed = 0;
        static bool end = false, mode = true;
        static Label label_time = null, label_speed = null;
        public View()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            label_time = label4;
            label_speed = label2;
            buttons = new Button[] { button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12,
            button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24, button25,
            button26, button27, button28};
            foreach (Button x in buttons)
                x.BackColor = Color.White;
            controller = new Controller(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (button1.Enabled == false)
            {
                controller.ChAdd();
                if(e.KeyData.ToString() == "Space")
                    controller.TapCheck(' ');
                else
                    controller.TapCheck(Convert.ToChar(e.KeyValue + 32));
            }
            label_speed.Text = controller.GetSpeed().ToString() + " ch/sec";
            if (controller.GetSpeed() > max_speed)
            {
                max_speed = controller.GetSpeed();
                label12.Text = max_speed.ToString() + " ch/sec";
            }
            foreach (Button x in buttons)
                if (x.Text == e.KeyData.ToString())
                    x.BackColor = Color.LightBlue;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (mode)
                foreach (Button x in buttons)
                {
                    x.BackColor = Color.White;
                    x.ForeColor = Color.Black;
                }
            else
                foreach (Button x in buttons)
                {
                    x.BackColor = Color.Gray;
                    x.ForeColor = Color.White;
                }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label8.Text = (trackBar1.Value + 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button29.Enabled = false;
            label10.Text = "";
            mistake = 0;
            timer = 0;
            max_speed = 0;
            end = false;
            label9.Text = controller.SetText(trackBar1) + "\n";
            label10.Text = "";
            text = controller.SetText(trackBar1);
            button1.Enabled = false;
            time.Add(new Task(Timer));
            time[time.Count - 1].Start();
            controller.StartThreadChPerSec();
        }
        private static void Timer()
        {
            while (!end)
            {
                Thread.Sleep(1000);
                timer++;
                label_time.Text = timer.ToString() + " sec";
            }
        }
        public void Mistake()
        {
            mistake++;
            label6.Text = mistake.ToString();
        }
        public void Correct(char a) => label10.Text += a;

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void View_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.ShowBalloonTip(3000, "Тренажер", "Таймер остановился, после открытия таймер снова запуститься", ToolTipIcon.Info);
                notifyIcon1.Visible = true;
                end = true;
                if(time.Count !=0)
                    time[time.Count - 1].Wait();
                Hide();
            }
        }
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            end = false;
            if (time.Count != 0)
            {
                time.Add(new Task(Timer));
                time[time.Count - 1].Start();
            }
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (mode)
            {
                BackColor = Color.Black;
                foreach (Button x in buttons)
                {
                    x.BackColor = Color.Gray;
                    x.ForeColor = Color.White;
                }
                foreach (Control item in this.Controls)
                {
                    if (item is Label)
                        item.ForeColor = Color.White;
                    if (item is Button)
                    {
                        item.BackColor = Color.Black;
                        item.ForeColor = Color.White;
                    }
                    if (item is TextBox)
                        item.BackColor = Color.Black;
                }
                label9.BackColor = Color.Black;
                label10.BackColor = Color.Black;
                button29.BackgroundImage = Properties.Resources.free_icon_sun_146182;
                mode = false;
            }
            else
            {
                BackColor = Color.WhiteSmoke;
                foreach (Button x in buttons)
                {
                    x.BackColor = Color.White;
                    x.ForeColor = Color.Black;
                }
                foreach (Control item in this.Controls)
                {
                    if (item is Label)
                        item.ForeColor = Color.Black;
                    if (item is Button)
                    {
                        item.BackColor = Color.White;
                        item.ForeColor = Color.Black;
                    }
                    if (item is TextBox)
                        item.BackColor = Color.White;
                }
                label9.BackColor = Color.White;
                label10.BackColor = Color.White;
                button29.BackgroundImage = Properties.Resources._37857;
                mode = true;
            }
        }

        private void результатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.ResultView();
        }

        public void End()
        {
            button1.Enabled = true;
            button29.Enabled = true;
            end = true;
            time[time.Count - 1].Wait();
            controller.AddResult(mistake, trackBar1.Value, timer, max_speed);
            MessageBox.Show($"Ошибок: {mistake}\nСкорость: {max_speed} сим\\с\nВремя: {timer} сек", $"Уровень: {trackBar1.Value + 1}");
        }
    }
}
