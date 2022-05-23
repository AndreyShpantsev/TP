using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace TP
{
    public partial class Form1 : Form
    {
        private List<Item> items = new List<Item>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                items.Add(form.item);
                LoadData();
            }
        }
        private void LoadData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = items;
            comboBox1.DataSource = null;
            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "Position";
            comboBox1.ValueMember = "Position";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            loadFromFile("test_save.txt");
            MessageBox.Show("Загрузка из файла прошла успешно!");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            saveToFile("test_save.txt");
            MessageBox.Show("Сохранение прошло успешно!");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var selected = from item in items
                           where item.Position == comboBox1.SelectedValue.ToString()
                           select item;
            List<Item> test = selected.ToList();
            dataGridView2.DataSource = test;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var res = items
                .GroupBy(x => x.Position)
                .Select(x => new
                {
                    Name = x.Key,
                    Count = x.Count()
                });
            textBox1.Text = res.Single(x => x.Count == res.Max(y => y.Count)).Count.ToString();
        }
        public void saveToFile(string path)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Item>));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, items);
            }
        }

        public void loadFromFile(string path)
        {
            dataGridView1.Columns.Clear();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
            using (Stream fStream = new FileStream(path, FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(fStream))
                {
                    var buffItems = (List<Item>)serializer.Deserialize(reader);
                    if (buffItems != null)
                    {
                        items = buffItems;
                        dataGridView1.DataSource = items;
                    }
                    else
                    {
                        Console.WriteLine("Ошибочка");
                    }
                }
            }
        }
    }
}
