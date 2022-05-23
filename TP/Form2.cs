using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP
{
    public partial class Form2 : Form
    {
        public Item item { get; set; }
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            item = new Item { Name = textBox1.Text, Position = textBox2.Text, Salary = Int32.Parse(textBox3.Text), Description = textBox4.Text };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
