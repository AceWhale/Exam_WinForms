using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_WinForms
{
    public partial class Result : Form
    {
        Controller controller;
        public Result(Controller a)
        {
            InitializeComponent();
            controller = a;
        }
        public void ShowMenu()
        {
            Show();
            string temp = controller.GetResult();
            string[] res = temp.Split('\n');
            foreach (string s in res)
                listBox1.Items.Add(s);
        }

        private void очиститьРезультатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Вы действительно хотите очистить историю?", "Результаты", MessageBoxButtons.YesNo);
            if (check == DialogResult.Yes)
            {
                File.WriteAllText("result.txt", "");
                listBox1.Items.Clear();
                string temp = controller.GetResult();
                string[] res = temp.Split('\n');
                foreach (string s in res)
                    listBox1.Items.Add(s);
            }
        }
    }
}
