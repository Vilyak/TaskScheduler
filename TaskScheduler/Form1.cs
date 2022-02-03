using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskScheduler.src;

namespace TaskScheduler
{
    public partial class Form1 : Form
    {
        private Controller controller;
        public Form1()
        {
            InitializeComponent();
            this.button1.Enabled = this.textBox1.Text.Length > 0;
            this.button2.Enabled = this.listBox1.SelectedIndex != -1;
            this.controller = new Controller(this.textBox1, this.comboBox1, this.dateTimePicker1, this.listBox1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = this.textBox1.Text.Length > 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void selectProcessBtn_Click(object sender, EventArgs e)
        {
            this.controller.importProcessDialogHandler();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.controller.submitBtnHandler();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.controller.removeTaskHandler();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = this.listBox1.SelectedIndex != -1;
        }
    }
}
