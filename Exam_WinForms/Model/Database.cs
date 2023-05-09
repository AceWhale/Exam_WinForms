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
        static bool end = false;
        List<Task> ch_per_sec = new List<Task>();
        public Database(Controller cr) { controller = cr; }
        public string SetText(int a)
        {
            pos = 0; size = 0; chr = 0; end = false;
            string temp = File.ReadAllText("text.txt");
            string[] text = temp.Split('\n');
            lvl_txt = text[a];
            size = lvl_txt.Length-1;
            return text[a];
        }
        public void StartChPerSec()
        {
            ch_per_sec.Add(new Task(ChPerSec));
            ch_per_sec[ch_per_sec.Count - 1].Start();
        }
        static void ChPerSec()
        {
            while (!end)
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
                {
                    end = true;
                    ch_per_sec[ch_per_sec.Count - 1].Wait();
                    controller.End();
                }
            }
            else
                controller.Mistake();
        }
        public void AddResult(int mistake, int level, int timer, int speed)
        {
            string date = DateTime.Now.ToString();
            string res = $"Дата: {date}\nУровень: {level+1}\nОшибок: {mistake}\nСкорость: {speed} сим\\с\nВремя: {timer} сек\n\n";
            File.AppendAllText("result.txt", res);
        }
        public string GetResult()
        {
            return File.ReadAllText("result.txt");
        }
    }
}
