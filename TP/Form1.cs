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
        //Ебать, это лист челов, тут всё хранить будем, ёпта
        private List<Item> items = new List<Item>();
        /// <summary>
        /// ЕБАТЬ ЭТО ЖЕ КОНСТРУКТОР ЕБАНОЙ ФОРМЫ, ЧТО ТУТ БЛЯТЬ НАПИСАТЬ-ТО?
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Ахуенный метод кнопки добавления нового чела
        /// P.S КНОПОЧКА НЕ НАЖАЛАСЬ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            //ЕБАШИМ АДОВЫЙ ЭКЗЕМПЛФОРМОЧКИ
            Form2 form = new Form2();
            //запускаем диалоговую формочку, ну а хули
            form.ShowDialog();
            //Спрашиваем, как там с челом то?
            if (form.DialogResult == DialogResult.OK)
            {
                //Засовываем в жопу листа новую игру
                items.Add(form.item);
                //Ебашим загрузку данных на всякие датагриды
                LoadData();
            }
        }
        //Ультимативный метод разъёба данных
        private void LoadData()
        {
            //Ебаный рефреш не работает, так что сначала обнуляем датасорс
            dataGridView1.DataSource = null;
            //Вставляем наши бармалеи в датагрид
            dataGridView1.DataSource = items;
            //сейм для комбобокса
            comboBox1.DataSource = null;
            comboBox1.DataSource = items;
            //отображение на комбо боксе
            comboBox1.DisplayMember = "Position";
            //значение в комбо боксе
            comboBox1.ValueMember = "Position";
        }
        //Ну тут изи метод для чтения из файла
        private void button2_Click(object sender, EventArgs e)
        {
            loadFromFile("test_save.txt");
            MessageBox.Show("Загрузка из файла прошла успешно!");
        }
        //Ну а хули, пишем в файл жсон
        private void button3_Click(object sender, EventArgs e)
        {
            saveToFile("test_save.txt");
            MessageBox.Show("Сохранение прошло успешно!");
        }
        //ЕБАНЫЙ ЛИНКЬЮ ПОШЁЛ, роль указана пользователем, так что выводим только то, что указано
        private void button4_Click(object sender, EventArgs e)
        {
            //выбираем, чё там пользователь выбрал
            var selected = from item in items
                           where item.Position == comboBox1.SelectedValue.ToString()
                           select item;
            //Не забываем из этого говна сделать нужный нам Лист
            List<Item> test = selected.ToList();
            //Вставляем в датагрид
            dataGridView2.DataSource = test;
        }
        //Пошёл нахуй этот линкью, тут уже чисто сгруппировать и показать кто чаще всего встречается
        private void button5_Click(object sender, EventArgs e)
        {
            //Группируем, выбираем, создаём списочек
            var res = items
                .GroupBy(x => x.Position)
                .Select(x => new
                {
                    Name = x.Key,
                    Count = x.Count()
                });
            //Пишем в текстбокс кто там самый модный из жанров
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
