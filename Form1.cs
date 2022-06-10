using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_3_2
{
    public partial class Form1 : Form
    {
        public TextBox[] alltextboxs;
        public TextBox[] needtextboxs;
        public TextBox[] resources;
        public TextBox[] max;
        public Form1()
        {
            InitializeComponent();
            alltextboxs = new TextBox[3 * 5]{allocationBox00, allocationBox01, allocationBox02,
                allocationBox10, allocationBox11, allocationBox12,
                allocationBox20, allocationBox21, allocationBox22,
                allocationBox30, allocationBox31, allocationBox32,
                allocationBox40, allocationBox41, allocationBox42};
            
            needtextboxs = new TextBox[3 * 5]{needBox00, needBox01, needBox02,
                needBox10, needBox11, needBox12,
                needBox20, needBox21, needBox22,
                needBox30, needBox31, needBox32,
                needBox40, needBox41, needBox42};
            resources = new TextBox[3] { resource1, resource2, resource3 };
            max = new TextBox[3 * 5]
            {
                textBox15, textBox14, textBox13, textBox12,
                textBox11, textBox10, textBox9, textBox8, textBox7,
                textBox6, textBox5, textBox4, textBox3, textBox2, textBox1
            };
        }

        public static int[] bank_al (int[,] allocation, int[,] need, int[] work)
        {
            int process_num = allocation.GetLength(0);
            int resource_num = allocation.GetLength(1);

            int[] ans = new int[process_num];
            int ans_point = 0;

            bool[] finish = new bool[process_num];
            for (int i = 0; i < process_num; i++) finish[i] = false;

            int finish_count = 0;
            while (finish_count < process_num)
            {
                int i;
                bool isChange = false;
                for (i = 0; i < process_num; i++)
                {
                    if (finish[i] == false)
                    {
                        bool flag = true;
                        for (int j = 0; j < resource_num; j++) if (need[i,j] > work[j]) flag = false;
                        if (flag)
                        {
                            for (int j = 0; j < resource_num; j++) work[j] = work[j] + allocation[i,j];
                            finish[i] = true;
                            isChange = true;
                            finish_count++;
                            ans[ans_point++] = i;
                        }
                    }
                }
                if (finish_count < process_num && (!isChange)) break;
            }
            int[] aa = new int[2] { -1, -1 };
            if (finish_count < process_num) return aa;
            return ans;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            new FileOpen().readFile(alltextboxs);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FileOpen().readFile(needtextboxs);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!(FileOpen.digitjdg(resources[i].Text)))
                {
                    MessageBox.Show("资源" + (i + 1).ToString() + "输入有误");
                    return;
                }
            }


            int[] resource = new int[3];
            for (int i = 0; i < 3; i++) resource[i] = int.Parse(resources[i].Text);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!FileOpen.digitjdg(alltextboxs[i * 3 + j].Text))
                    {
                        MessageBox.Show("allocation:"+(i + 1).ToString() + "行" + (j + 1).ToString() + "列数字错误！");
                        return;
                    }
                    if (!FileOpen.digitjdg(needtextboxs[i * 3 + j].Text))
                    {
                        MessageBox.Show("need:"+(i + 1).ToString() + "行" + (j + 1).ToString() + "列数字错误！");
                        return;
                    }
                }
            }

            int[,] allmetrix = new int[5, 3];
            int[,] needmetrix = new int[5, 3];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    allmetrix[i, j] = int.Parse(alltextboxs[i * 3 + j].Text);
                    needmetrix[i, j] = int.Parse(needtextboxs[i * 3 + j].Text);
                }
            }
            int[] ans = new int[5];
            ans = bank_al(allmetrix, needmetrix, resource);

            if (ans[0] == -1)
            {
                MessageBox.Show("系统不安全！");
            }
            else
            {
                string ansans = "{";
                for (int i = 0; i < 4; i++) ansans += "P"+ans[i].ToString() + "->";
                ansans += "P" + ans[4].ToString() + "}";
                MessageBox.Show("系统处于安全状态！安全序列为：" + ansans);
            }
 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.unlocknow.Enabled = true;
            this.request.Enabled = true;
            this.locknow.Enabled = false;
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button9.Enabled = false;
            button8.Enabled = false;
            textBox18.Enabled = false;
            textBox17.Enabled = false;
            textBox16.Enabled = false;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    alltextboxs[i * 3 + j].Enabled = false;
                    needtextboxs[i * 3 + j].Enabled = false;
                    max[i * 3 + j].Enabled = false;
                }
            }
            for (int i = 0; i < 3; i++) resources[i].Enabled = false;

            
            this.Invalidate();
        }

        private void unlocknow_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    alltextboxs[i * 3 + j].Enabled = true;
                    needtextboxs[i * 3 + j].Enabled = true;
                    max[i * 3 + j].Enabled = true;
                    
                }
            }
            for (int i = 0; i < 3; i++) resources[i].Enabled = true;

            unlocknow.Enabled = false;
            request.Enabled = false;
            locknow.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;

            button9.Enabled = true;
            button8.Enabled = true;
            textBox18.Enabled = true;
            textBox17.Enabled = true;
            textBox16.Enabled = true;
        }

        private void request_Click(object sender, EventArgs e)
        {
            int[,] allocation = new int[5, 3];
            int[,] need = new int[5, 3];
            int[] avaliable = new int[3];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    allocation[i, j] = int.Parse(alltextboxs[i * 3 + j].Text);
                    need[i, j] = int.Parse(needtextboxs[i * 3 + j].Text);
                }
            }
            for (int i = 0; i < 3; i++) avaliable[i] = int.Parse(resources[i].Text);

            Form2 form2 = new Form2(avaliable, need, allocation, this);
            form2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            count_max();
        }

        public void count_max()
        {
            for (int i = 0; i < 15; i++)
            {
                max[i].Text = (int.Parse(alltextboxs[i].Text) + int.Parse(needtextboxs[i].Text)).ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                alltextboxs[i].Text = (int.Parse(max[i].Text) - int.Parse(needtextboxs[i].Text)).ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                needtextboxs[i].Text = (int.Parse(max[i].Text) - int.Parse(alltextboxs[i].Text)).ToString();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            new FileOpen().readFile(max);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 计算最大资源
            int suma = 0, sumb = 0, sumc = 0;
            for (int i = 0; i < 5; i++)
            {
                suma += int.Parse(alltextboxs[i * 3].Text);
                sumb += int.Parse(alltextboxs[i * 3 + 1].Text);
                sumc += int.Parse(alltextboxs[i * 3 + 2].Text);
            }


            textBox18.Text = (suma + int.Parse(resource1.Text)).ToString();
            textBox17.Text = (sumb + int.Parse(resource2.Text)).ToString();
            textBox16.Text = (sumc + int.Parse(resource3.Text)).ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // 计算当前资源
            int suma = 0, sumb = 0, sumc = 0;
            for (int i = 0; i < 5; i++)
            {
                suma += int.Parse(alltextboxs[i * 3].Text);
                sumb += int.Parse(alltextboxs[i * 3 + 1].Text);
                sumc += int.Parse(alltextboxs[i * 3 + 2].Text);
            }


            resource1.Text = (-1 * suma + int.Parse(textBox18.Text)).ToString();
            resource2.Text = (-1 * sumb + int.Parse(textBox17.Text)).ToString();
            resource3.Text = (-1 * sumc + int.Parse(textBox16.Text)).ToString();
        }
    }

    class FileOpen
    {
        private string selectPath()
        {
            string path = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Files (*.txt)|*.txt"//如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            };

            //var result = openFileDialog.ShowDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }

            return path;
        }


        public static bool digitjdg(string x)
        {
            if (x.Length == 0) return false;
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            bool IsDigit = rx.IsMatch(x);
            return IsDigit;//是数字返回true,不是返回false
        }

        public bool errorDetect(string[] s)
        {
            if (s.Length != 3) return false;
            foreach (string str in s)
            {
                if (!digitjdg(str)) return false;
            }
            return true;
        }
        public void readFile(TextBox[] textBoxes)
        {
            string path = selectPath();
            if (path == String.Empty)
            {
                MessageBox.Show("读取失败！");
                return;
            }
            StreamReader sr = new StreamReader(path, Encoding.Default);
            int count = 0;
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] s = line.Split('\t');

                if (!errorDetect(s))
                {
                    MessageBox.Show("读取错误！请检查后输入。");
                }

                for (int i = 0; i < 3; i++)
                    textBoxes[count * 3 + i].Text = s[i];
                count++;
                if (count > 5) break;
                
            }
            MessageBox.Show("读取成功！");
        }
    }


}
