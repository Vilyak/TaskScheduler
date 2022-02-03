using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskScheduler.src.DataTypes;

namespace TaskScheduler.src
{
    internal class View
    {
        private TextBox pathBox;
        private ComboBox comboBox;
        private ListBox taskList;

        public View(TextBox pathBox, ComboBox comboBox, ListBox taskList)
        {
            this.pathBox = pathBox;
            this.comboBox = comboBox;
            this.taskList = taskList;
        }

        public void updateTasks(List<WinTask> tasks)
        {

            List<String> tasksToRender = tasks.ConvertAll<String>(task => task.path + "|" + task.dateTime.ToShortTimeString());
            this.taskList.Items.Clear();
            tasksToRender.ForEach(task => this.taskList.Items.Add(task));
        }
    }
}
