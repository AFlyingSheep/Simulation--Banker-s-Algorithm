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

    public partial class Form3 : Form
    {
        TextBox[] alltextboxs;
        TextBox[] needtextboxs;

        TextBox[] alltextboxs_new;
        TextBox[] needtextboxs_new;

        int[,] allocation;
        int[,] allocation_new;
        int[,] need;
        int[,] need_new;
        int[] resource;
        int[] resource_new;
        int[] request;
        int index;
        Form1 form;
        Form2 form2;
        string xulie;
        public Form3(Form1 form1,Form2 form2, int[,] allocation, int[,] allcation_new, 
            int[,] need, int[,] need_new, int[] resource, int[] resource_new, 
            int[] request, int index, string xulie)
        {
            this.xulie = xulie;
            form = form1;
            this.form2 = form2;
            this.allocation = allocation;
            this.allocation_new = allcation_new;
            this.need = need;
            this.need_new = need_new;
            this.resource = resource;
            this.resource_new = resource_new;
            this.request = request;
            this.index = index;

            InitializeComponent();
            label33.Text = xulie;
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

            alltextboxs_new = new TextBox[3 * 5]{nallocationBox00, nallocationBox01, nallocationBox02,
                nallocationBox10, nallocationBox11, nallocationBox12,
                nallocationBox20, nallocationBox21, nallocationBox22,
                nallocationBox30, nallocationBox31, nallocationBox32,
                nallocationBox40, nallocationBox41, nallocationBox42};

            needtextboxs_new = new TextBox[3 * 5]{nneedBox00, nneedBox01, nneedBox02,
                nneedBox10, nneedBox11, nneedBox12,
                nneedBox20, nneedBox21, nneedBox22,
                nneedBox30, nneedBox31, nneedBox32,
                nneedBox40, nneedBox41, nneedBox42};

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    alltextboxs[i * 3 + j].Text = allocation[i, j].ToString();
                    alltextboxs_new[i * 3 + j].Text = allocation_new[i, j].ToString();
                    if (allocation[i, j] != allocation_new[i, j])
                    {
                        alltextboxs_new[i * 3 + j].BackColor = Color.Yellow;
                    }

                    needtextboxs[i * 3 + j].Text = need[i, j].ToString();
                    needtextboxs_new[i * 3 + j].Text = need_new[i, j].ToString();
                    if (need[i, j] != need_new[i, j])
                    {
                        needtextboxs_new[i * 3 + j].BackColor = Color.Yellow;
                    }
                }
            }
            string source = "";
            string new_source = "";
            for (int i = 0; i < 3; i++) 
            {
                source += " " + ((char)((int)'A' + i)) + ":" + resource[i].ToString();
                new_source += " " + ((char)((int)'A' + i)) + ":" + resource_new[i].ToString();
            }
            textBox1.Text = source;
            textBox2.Text = new_source;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    form.alltextboxs[i * 3 + j].Text = alltextboxs_new[i * 3 + j].Text;
                    form.needtextboxs[i * 3 + j].Text = needtextboxs_new[i * 3 + j].Text;       
                }
            }
            for (int i = 0; i < 3; i++)
            {
                form.resources[i].Text = resource_new[i].ToString();
            }
            form.count_max();
            form2.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < 3; j++)
            {
                resource_new[j] = resource[j];
                allocation_new[index, j] = allocation[index, j];
                need_new[index, j] = need[index, j];
            }
            this.Close();
        }
    }
}
