using Exam_WinForms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_WinForms
{
    internal class Controller
    {
        Database db;
        View view;
        public Controller(View view) { this.view = view; db = new Database(this); }
        public string SetText(TrackBar a) => db.SetText(a.Value);
        public void StartThreadChPerSec() => db.StartChPerSec();
        public void ChAdd() => db.SpeedAdd();
        public int GetSpeed() => db.GetSpeed();
        public void Mistake() => view.Mistake();
        public void Correct(char a) => view.Correct(a);
        public void TapCheck(char a) => db.TextCheck(a);
        public void End() => view.End();
    }
}
