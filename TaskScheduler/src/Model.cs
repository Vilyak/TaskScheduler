using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.src.DataTypes;

namespace TaskScheduler.src
{
    internal class Model
    {
        private List<WinTask> tasks;

        public List<WinTask> Tasks
        {
            get
            {
                return tasks;
            }
        }

        public Model()
        {
            tasks = new List<WinTask>();
        }

        public List<WinTask> fetchTasks()
        {
            RunningTaskCollection collection = TaskService.Instance.GetRunningTasks(true);
            List<WinTask> winTasks = new List<WinTask>();

            foreach (Microsoft.Win32.TaskScheduler.Task rt in TaskService.Instance.AllTasks)
            {
                if (rt != null)
                {
                    winTasks.Add(new WinTask(rt.Path, rt.NextRunTime));
                }
            }
            this.tasks = winTasks;
            return this.tasks;
        }
    }
}
