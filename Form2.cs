using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_3_2
{
    public partial class Form2 : Form
    {
        TextBox[] quest_res;
        int[] Availiable;
        int[,] Need;
        int[,] Allocation;
        int[] Availiable_old;
        int[,] Need_old;
        int[,] Allocation_old;
        Form1 form1;

        public Form2(int[] Availiable, int[,] Need, int[,] Allocation, Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            quest_res = new TextBox[3] { textBox1, textBox2, textBox3 };
            this.Availiable = (int[])Availiable.Clone();
            this.Need = (int[,])Need.Clone();
            this.Allocation = (int[,])Allocation.Clone();

            this.Availiable_old = (int[])Availiable.Clone();
            this.Need_old = (int[,])Need.Clone();
            this.Allocation_old = (int[,])Allocation.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] quest_num = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (!FileOpen.digitjdg(quest_res[i].Text))
                {
                    char num = (char)((int)'A' + i);
                    MessageBox.Show("资源" + num + "输入错误！");
                    return;
                }
                quest_num[i] = int.Parse(quest_res[i].Text);
            }

            for (int i = 0; i < 3; i++)
            {
                if (quest_num[i] > Availiable[i])
                {
                    MessageBox.Show("无足够资源，需要等待！");
                    return;
                }
            }
            int index = comboBox1.SelectedIndex;
            for (int j = 0; j < 3; j++)
            {
                if (quest_num[j] > Need[index,j])
                {
                    MessageBox.Show("需要资源已经超过声称的最大值！请检查！");
                    return;
                }
            }

            
            for (int j = 0; j < 3; j++)
            {
                Availiable[j] = Availiable[j] - quest_num[j];
                Allocation[index, j] = Allocation[index, j] + quest_num[j];
                Need[index, j] = Need[index, j] - quest_num[j];
            }
            int[] pro = Form1.bank_al((int[,])Allocation.Clone(), (int[,])Need.Clone(), (int[])Availiable.Clone());
            if (pro[0] == -1)
            {
                MessageBox.Show("没有通过安全性检测，进程需等待！");
                reverse(Availiable_old, Availiable, Need_old, Need, Allocation_old, Allocation);
                return;
            }
            else
            {
                string str = "安全序列为：{ ";
                for (int i = 0; i < 4; i++)
                {
                    str += "P" + pro[i].ToString() + "->";
                }
                str += "P" + pro[4].ToString() + " }";

                Form3 form = new Form3(form1,this, Allocation_old, Allocation, Need_old, Need, 
                    Availiable_old, Availiable, quest_num, index, str);
                form.Visible = true;
            }

        }
        public void reverse(int[] old1, int[] new1, int[,] old2, int[,] new2, int[,]old3, int[,]new3)
        {
            for (int i = 0; i < old1.Length; i++) new1[i] = old1[i];
            for (int i = 0; i < old2.GetLength(0); i++) 
                for (int j = 0; j < old2.GetLength(1); j++)
                {
                    new2[i, j] = old2[i, j];
                    new3[i, j] = old3[i, j];
                }
        }

    }


}
