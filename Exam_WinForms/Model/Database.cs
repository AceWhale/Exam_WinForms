using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Exam_WinForms.Model
{
    internal class Database
    {
        Controller controller;
        static int chr = 0;
        int pos = 0, size = 0;
        string lvl_txt;
        Task ch_per_sec = new Task(ChPerSec);
        public Database(Controller cr) { controller = cr; }
        public string SetText(int a)
        {
            string temp = File.ReadAllText("text.txt");
            string[] text = temp.Split('\n');
            lvl_txt = text[a];
            size = lvl_txt.Length-1;
            return text[a];
        }
        public void StartChPerSec() => ch_per_sec.Start();
        static void ChPerSec()
        {
            while (true)
            {
                Thread.Sleep(1000);
                chr = 0;
            }
        }
        public int GetSpeed() { return chr; }
        public void SpeedAdd() => chr++;
        public void TextCheck(char a)
        {
            if (a == lvl_txt[pos])
            {
                pos++;
                controller.Correct(a);
                if (size == pos)
                    controller.End();
            }
            else
                controller.Mistake();
        }
    }
}
