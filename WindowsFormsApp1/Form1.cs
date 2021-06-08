using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool is_gm;
        private string gm_background = "Images/gif2-optimized.gif";
        private string non_gm_background = "Images/image3.jpg";
        private string transparent_background = "Images/Back_Ground.png";
        string kanji_path = "Images/Kanji/";
        private int timer_deneme;
        private int chosen_kanji_index;
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        private void Custom_Timer(CheckBox check, ProgressBar progressBar, Func<bool> called_method)
        {
            int time_before = (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
            int time_after;
            while(check.Checked)
            {
                time_after = (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
                int val = time_after - time_before + progressBar.Value;
                if (val > progressBar.Maximum)
                    val = progressBar.Maximum;
                if (val < 0)
                    val = 0;
                progressBar.Value = val;
                if (val > 1)
                    progressBar.Value -= 1;
                    progressBar.Value += 1;
                if (progressBar.Value >= progressBar.Maximum)
                {
                    progressBar.Value = 0;
                    called_method();
                }
                time_before = time_after;
            }
            progressBar.Value = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Make_Transparent_Items();
            comboBox1.SelectedIndex = 2;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            label1.Text = "00000";
            Kanji_PBox_no_wp.Image = (Bitmap)Image.FromFile(kanji_path + 0.ToString().PadLeft(4, '0') + ".png");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Sayi_uret();
            progressBar1.Value = 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Saat_uret();
            progressBar2.Value = 1;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Gun_uret();
            progressBar3.Value = 1;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Tarih_uret();
            progressBar4.Value = 1;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Kanji_uret();
            progressBar5.Value = 1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            is_gm = !is_gm;
            var pictureboxes = new List<TabPage> { Tab_Page_Kanji, Tab_Page_Tarih, Tab_Page_Gunler };
            if (is_gm)
            {
                Form1_gm();
                Form2_gm();
                foreach(TabPage item in pictureboxes)
                    Form_gm_generel(item);
                this.BackColor = Color.Black;
            }
            else
            {
                Form1_non_gm();
                Form2_non_gm();
                foreach (TabPage item in pictureboxes)
                    Form_non_gm_generel(item);
                this.BackColor = Color.White;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                progressBar1.Maximum = int.Parse(comboBox2.SelectedItem.ToString().Remove(comboBox2.SelectedItem.ToString().Length - 2)) * 1000;
                Thread tr = new Thread(()=>Custom_Timer((CheckBox)sender, progressBar1, Sayi_uret));
                tr.Start();
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                progressBar2.Maximum = int.Parse(comboBox3.SelectedItem.ToString().Remove(comboBox3.SelectedItem.ToString().Length - 2)) * 1000;
                Thread tr = new Thread(() => Custom_Timer((CheckBox)sender, progressBar2, Saat_uret));
                tr.Start();
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                progressBar3.Maximum = int.Parse(comboBox4.SelectedItem.ToString().Remove(comboBox4.SelectedItem.ToString().Length - 2)) * 1000;
                Thread tr = new Thread(() => Custom_Timer((CheckBox)sender, progressBar3, Gun_uret));
                tr.Start();
            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                progressBar4.Maximum = int.Parse(comboBox5.SelectedItem.ToString().Remove(comboBox5.SelectedItem.ToString().Length - 2)) * 1000;
                Thread tr = new Thread(() => Custom_Timer((CheckBox)sender, progressBar4, Tarih_uret));
                tr.Start();
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                progressBar5.Maximum = int.Parse(comboBox6.SelectedItem.ToString().Remove(comboBox6.SelectedItem.ToString().Length - 2)) * 1000;
                Thread tr = new Thread(() => Custom_Timer((CheckBox)sender, progressBar5, Kanji_uret));
                tr.Start();
            }
        }
        private bool Sayi_uret()
        {
            Random rn = new Random();
            int a = 0;
            int b = 0;
            string last = "";
            string tmp = "";
            if(int.Parse(comboBox1.SelectedItem.ToString()) > 9)
            {
                a = rn.Next((int)Math.Pow(10, 9) / 10, (int)Math.Pow(10, 9));
                b = rn.Next((int)Math.Pow(10, int.Parse(comboBox1.SelectedItem.ToString()) - 9) / 10, (int)Math.Pow(10, int.Parse(comboBox1.SelectedItem.ToString()) - 9));
                last = a.ToString() + b.ToString();
            }
            else
            {
                a = rn.Next((int)Math.Pow(10, int.Parse(comboBox1.SelectedItem.ToString())) / 10, (int)Math.Pow(10, int.Parse(comboBox1.SelectedItem.ToString())));
                last = a.ToString();
            }
            for (int i = 0; i < last.Length; i++)
            {
                if(tmp != "" && (i - (last.Length % 4)) % 4 == 0)
                {
                    tmp += ',';
                }
                tmp += last[i];
            }
            label1.Text = is_gm ? tmp : last;
            return true;
        }
        private bool Saat_uret()
        {
            Random rn = new Random();
            string sb = "";
            sb += rn.Next(0, 24).ToString().PadLeft(2, '0');
            sb += " : ";
            sb += rn.Next(0, 60).ToString().PadLeft(2, '0');
            label2.Text = sb;
            return true;
        }
        private bool Gun_uret()
        {
            Random rn = new Random();
            int secilen_gun = rn.Next(0, 5);
            var labels = new List<Label> { label_2_gun_once, label_dun, label_bugun, label_yarın, label_2_gun_sonra };
            var gunler = new List<string> { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };
            for (int i = 0; i < 5; i++)
            {
                if (i == secilen_gun)
                    labels[i].Text = gunler[rn.Next(0, 7)];
                else
                    labels[i].Text = "?";
            }
            return true;
        }
        private bool Tarih_uret()
        {
            Random rn = new Random();
            var aylar = new List<string> { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            var uzun_aylar = new List<int> { 0, 2, 4, 6, 7, 9, 11 };
            int secilen_ay = rn.Next(0, 12);
            int secilen_gun = 0;
            if (secilen_ay == 1)
            {
                secilen_gun = rn.Next(1, 29);
            }
            else if (uzun_aylar.Contains(secilen_ay))
            {
                secilen_gun = rn.Next(1, 32);
            }
            else
            {
                secilen_gun = rn.Next(1, 31);
            }
            label6.Text = secilen_gun + " - " + aylar[secilen_ay];
            return true;
        }
        private bool Kanji_uret()
        {
            int previous_index = chosen_kanji_index;
            
            Random r = new Random();
            while (chosen_kanji_index == previous_index)
                chosen_kanji_index = r.Next((int)Kanji_Left_Number.Value, (int)Kanji_Right_Number.Value);
            Bitmap img = (Bitmap)Image.FromFile(kanji_path + chosen_kanji_index.ToString().PadLeft(4, '0') + ".png");
            Kanji_PBox_no_wp.Image = img;
            return true;
        }
        private void Form1_gm()
        {
            label1.ForeColor = Color.FromArgb(1, 1, 255);
            label3.ForeColor = Color.FromArgb(1, 78, 143);
            checkBox1.ForeColor = Color.FromArgb(1, 78, 143);
            button1.ForeColor = Color.FromArgb(1, 78, 143);
            pictureBox1.Image = Image.FromFile(gm_background);
            Tab_Page_Sayilar.BackgroundImage = Image.FromFile(gm_background);
            comboBox1.Items.Add("7");
            comboBox1.Items.Add("8");
            comboBox1.Items.Add("9");
            comboBox1.Items.Add("10");
            comboBox1.Items.Add("11");
            comboBox1.Items.Add("12");
            comboBox2.Items.Add("01sn");
            comboBox2.Items.Add("02sn");
            comboBox2.Items.Add("03sn");
            comboBox2.Items.Add("04sn");
            comboBox2.Items.Add("05sn");
        }
        private void Form1_non_gm()
        {
            label1.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            checkBox1.ForeColor = Color.Black;
            button1.ForeColor = Color.Black;
            if(label1.Text.Contains(","))
                label1.Text = label1.Text.Remove(label1.Text.IndexOf(','),1);
            if (int.Parse(comboBox1.SelectedItem.ToString()) > 6)
            {
                comboBox1.SelectedIndex = 3;
            }
            comboBox1.Items.Remove("7");
            comboBox1.Items.Remove("8");
            comboBox1.Items.Remove("9");
            comboBox1.Items.Remove("10");
            comboBox1.Items.Remove("11");
            comboBox1.Items.Remove("12");
            comboBox2.Items.Remove("01sn");
            comboBox2.Items.Remove("02sn");
            comboBox2.Items.Remove("03sn");
            comboBox2.Items.Remove("04sn");
            comboBox2.Items.Remove("05sn");
            if (comboBox2.SelectedIndex < 5)
                comboBox2.SelectedIndex = 2;
            pictureBox1.Image = Image.FromFile(non_gm_background);
            Tab_Page_Sayilar.BackgroundImage = Image.FromFile(non_gm_background);
        }
        private void Form2_gm()
        {
            label2.ForeColor = Color.FromArgb(1, 1, 255);
            checkBox2.ForeColor = Color.FromArgb(1, 78, 143);
            button2.ForeColor = Color.FromArgb(1, 78, 143);
            Tab_Page_Saatler.BackgroundImage = Image.FromFile(gm_background);
            pictureBox2.Image = Image.FromFile(gm_background);
            comboBox3.Items.Add("01sn");
            comboBox3.Items.Add("02sn");
            comboBox3.Items.Add("03sn");
            comboBox3.Items.Add("04sn");
            comboBox3.Items.Add("05sn");
        }
        private void Form2_non_gm()
        {
            label2.ForeColor = Color.Black;
            checkBox2.ForeColor = Color.Black;
            button2.ForeColor = Color.Black;
            comboBox3.Items.Remove("01sn");
            comboBox3.Items.Remove("02sn");
            comboBox3.Items.Remove("03sn");
            comboBox3.Items.Remove("04sn");
            comboBox3.Items.Remove("05sn");
            if (comboBox3.SelectedIndex < 5)
                comboBox3.SelectedIndex = 2;
            Tab_Page_Saatler.BackgroundImage = Image.FromFile(non_gm_background);
            pictureBox2.Image = Image.FromFile(non_gm_background);
        }
        private void Form_gm_generel(TabPage pb)
        {
            foreach (var item in pb.Controls)
            {
                if (item is Label)
                    ((Label)item).ForeColor = Color.FromArgb(1, 1, 255);
                else if (item is CheckBox)
                    ((CheckBox)item).ForeColor = Color.FromArgb(1, 78, 143);
                else if (item is Button)
                    ((Button)item).ForeColor = Color.FromArgb(1, 78, 143);
                else if (item is ComboBox)
                {
                    ((ComboBox)item).Items.Add("01sn");
                    ((ComboBox)item).Items.Add("02sn");
                    ((ComboBox)item).Items.Add("03sn");
                    ((ComboBox)item).Items.Add("04sn");
                    ((ComboBox)item).Items.Add("05sn");
                }
                else if (item is PictureBox)
                {
                    if(!((PictureBox)item).Name.Contains("no_wp"))
                    ((PictureBox)item).Image = Image.FromFile(gm_background);

                }
            }
            pb.BackgroundImage = Image.FromFile(gm_background);
        }
        private void Form_non_gm_generel(TabPage pb)
        {
            foreach (var item in pb.Controls)
            {
                if (item.GetType() == typeof(Label))
                    ((Label)item).ForeColor = Color.Black;
                else if (item.GetType() == typeof(CheckBox))
                    ((CheckBox)item).ForeColor = Color.Black;
                else if (item.GetType() == typeof(Button))
                    ((Button)item).ForeColor = Color.Black;
                else if (item.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)item).Items.Remove("01sn");
                    ((ComboBox)item).Items.Remove("02sn");
                    ((ComboBox)item).Items.Remove("03sn");
                    ((ComboBox)item).Items.Remove("04sn");
                    ((ComboBox)item).Items.Remove("05sn");
                    if (((ComboBox)item).SelectedIndex < 5)
                        ((ComboBox)item).SelectedIndex = 2;
                }
                else if (item is PictureBox)
                {
                    if (!((PictureBox)item).Name.Contains("no_wp"))
                        ((PictureBox)item).Image = Image.FromFile(non_gm_background);

                }
            }
            pb.BackgroundImage = Image.FromFile(non_gm_background);
        }
        private void Make_Transparent_Items()
        {
            var labels = new List<Label> { label1, label2, label3, label11, label12, label13, label14, label15, label6 };
            foreach(var lbl in labels)
            {
                lbl.BackColor = Color.Transparent;
                lbl.BackgroundImage = Image.FromFile(transparent_background);
                lbl.BackgroundImageLayout = ImageLayout.Stretch;
            }
            var checkboxes = new List<CheckBox> { checkBox1, checkBox2 };
            foreach (var chc in checkboxes)
            {
                chc.BackColor = Color.Transparent;
                chc.BackgroundImage = Image.FromFile(transparent_background);
                chc.BackgroundImageLayout = ImageLayout.Stretch;
            }
            var buttons = new List<Button> { button1, button2, button4, button5 };
            foreach (var btn in buttons)
            {
                btn.BackColor = Color.Transparent;
                btn.BackgroundImage = Image.FromFile(transparent_background);
                btn.BackgroundImageLayout = ImageLayout.Stretch;
            }
            var pictureboxes = new List<PictureBox> { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox_Kanji };
            foreach (var pcb in pictureboxes)
            {
                pcb.Image = Image.FromFile(non_gm_background);
            }
            tabControl1.BackColor = Color.Transparent;
            Tab_Page_Sayilar.BackgroundImage = Image.FromFile(non_gm_background);
            Tab_Page_Saatler.BackgroundImage = Image.FromFile(non_gm_background);
            Tab_Page_Gunler.BackgroundImage = Image.FromFile(non_gm_background);
            Tab_Page_Tarih.BackgroundImage = Image.FromFile(non_gm_background);
            Tab_Page_Kanji.BackgroundImage = Image.FromFile(non_gm_background);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            object a = new object();
            EventArgs b = new EventArgs();
            button1_Click(a, b);
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Kanji_Right_Number.Minimum = Kanji_Left_Number.Value + 10;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkboxes = new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            foreach(CheckBox item in checkboxes)
            {
                item.Checked = false;
            }
        }
    }
}
