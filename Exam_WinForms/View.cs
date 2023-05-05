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

namespace Exam_WinForms
{
    public partial class View : Form
    {
        Button[] buttons;
        static Controller controller;
        Task time;
        string text;
        static int timer = 0;
        int mistake = 0;
        static bool end = false;
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
            foreach (Button x in buttons)
                if (x.Text == e.KeyData.ToString())
                    x.BackColor = Color.LightBlue;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Button x in buttons)
                x.BackColor = Color.White;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label8.Text = (trackBar1.Value + 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label9.Text = controller.SetText(trackBar1) + "\n";
            text = controller.SetText(trackBar1);
            button1.Enabled = false;
            time = new Task(Timer);
            time.Start();
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
        public void End()
        {
            end = true;
            time.Wait();
            MessageBox.Show($"Ошибок: {mistake}\nУровень: {trackBar1.Value+1}\nВремя: {timer}", "Result");
        }
    }
}
