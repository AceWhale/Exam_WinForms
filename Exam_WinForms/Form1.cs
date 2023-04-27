using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_WinForms
{
    public partial class Form1 : Form
    {
        Button[] buttons;
        public Form1()
        {
            InitializeComponent();
            buttons = new Button[] { button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12,
            button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24, button25,
            button26, button27, button28};
            foreach (Button x in buttons)
                x.BackColor = Color.White;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
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
    }
}
