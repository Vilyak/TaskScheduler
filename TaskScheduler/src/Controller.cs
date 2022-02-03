using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using TaskScheduler.src.DataTypes;

namespace TaskScheduler.src
{
    internal class Controller
    {
        private Model model;
        private View view;
        private TextBox pathBox;
        private DateTimePicker dateTimePicker;
        private ComboBox comboBox;
        private ListBox taskList;

        public Controller(TextBox pathBox, ComboBox comboBox, DateTimePicker dateTimePicker, ListBox taskList)
        {
            this.pathBox = pathBox;
            this.dateTimePicker = dateTimePicker;
            this.comboBox = comboBox;
            this.taskList = taskList;
            this.comboBox.SelectedIndex = 0;
            view = new View(pathBox, comboBox, taskList);
            model = new Model();
            fetchAllTasks();
        }

        public void importProcessDialogHandler()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String path = dialog.FileName;
                this.pathBox.Text = path;
            }
        }

        public void submitBtnHandler()
        {

            try
            {
                String path = this.pathBox.Text;
                DateTime dateTime = this.dateTimePicker.Value;
                QuickTriggerType repeatType = QuickTriggerType.Idle;
                try
                {
                    repeatType = (QuickTriggerType)Enum.Parse(typeof(QuickTriggerType), this.comboBox.Text);
                }
                catch { }
                Trigger trigger = getTriggerByType(repeatType, dateTime);

                TaskDefinition td = TaskService.Instance.NewTask();
                td.RegistrationInfo.Description = "Proccess";
                td.Principal.LogonType = TaskLogonType.InteractiveToken;
                td.Triggers.Add(trigger);

                td.Actions.Add(new ExecAction(path));

                TaskService.Instance.RootFolder.RegisterTaskDefinition(path.Split('\\').Last(), td);
                fetchAllTasks();
                MessageBox.Show("Задача " + path.Split('\\').Last() + " успешно добавлена!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void fetchAllTasks()
        {
            List<WinTask> tasks = this.model.fetchTasks();
            this.view.updateTasks(tasks);
        }

        public void removeTaskHandler()
        {
            try
            {
                if (taskList.SelectedIndex != -1)
                {
                    String taskName = taskList.Items[taskList.SelectedIndex].ToString().Split('|').First();
                    TaskService.Instance.RootFolder.DeleteTask(taskName);
                    fetchAllTasks();
                    MessageBox.Show("Задача успешно удалена!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private Trigger getTriggerByType(QuickTriggerType type, DateTime dateTime)
        {
            switch (type)
            {
                case QuickTriggerType.Daily:
                    return new DailyTrigger() { StartBoundary = dateTime, Enabled = false };
                case QuickTriggerType.Weekly:
                    return new WeeklyTrigger() { StartBoundary = dateTime, Enabled = false };
                case QuickTriggerType.Monthly:
                    return new MonthlyTrigger() { StartBoundary = dateTime, Enabled = false };
                default:
                    return new WeeklyTrigger
                    {
                        StartBoundary = dateTime,
                        DaysOfWeek = DaysOfTheWeek.AllDays
                    };
            }
        }
    }
}
