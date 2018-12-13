using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;

namespace SDCM_check
{
    public partial class FindNg : Form
    {
        private readonly List<string> listOK;
        private readonly List<string> listNG;
        
        System.Media.SoundPlayer snd = new System.Media.SoundPlayer(Properties.Resources.Computer_Error_Alert_SoundBible_com_783113881);


        public FindNg(List<string> listOK, List<string> listNG)
        {
            InitializeComponent();
            this.listOK = listOK;
            this.listNG = listNG;

            checkedListBoxNG.DrawMode = DrawMode.OwnerDrawFixed;
            checkedListBoxOK.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void FindNg_Load(object sender, EventArgs e)
        {
            checkedListBoxNG.Items.AddRange(listNG.ToArray());
            checkedListBoxOK.Items.AddRange(listOK.ToArray());

            labelNgCount.Text = GetCheckedItems(checkedListBoxNG).ToString() + @"/" + checkedListBoxNG.Items.Count;
            labelOkCount.Text = GetCheckedItems(checkedListBoxOK).ToString() + @"/" + checkedListBoxOK.Items.Count;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            snd.Play();
        }

        private Int32 GetCheckedItems(CheckedListBox listBox)
        {
            return listBox.CheckedItems.Count;
        }

        private void checkedListBoxOK_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
        }

        private void checkedListBoxNG_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
            snd.Play();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                string serial = textBox1.Text;
                bool found = false;

                for (int i=0;i< checkedListBoxNG.Items.Count;i++)
                {
                    if (checkedListBoxNG.Items[i].ToString() == serial)
                    {
                        checkedListBoxNG.SetItemCheckState(i, CheckState.Checked);
                        found = true;
                        pnlToBlink = panelNg;
                        timer1.Enabled = true;
                        break;
                    }
                }

                if (!found)
                {
                    for (int i = 0; i < checkedListBoxOK.Items.Count; i++)
                    {
                        if (checkedListBoxOK.Items[i].ToString() == serial)
                        {
                            checkedListBoxOK.SetItemCheckState(i, CheckState.Checked);
                            
                            pnlToBlink = panelOk;
                            timer1.Enabled = true;
                            break;
                        }
                    }
                }
                labelOkCount.Text = GetCheckedItems(checkedListBoxOK).ToString() + @"/" + checkedListBoxOK.Items.Count;
                labelNgCount.Text = GetCheckedItems(checkedListBoxNG).ToString() + @"/" + checkedListBoxNG.Items.Count;
                textBox1.SelectAll();
            }

            
        }
        

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        Panel pnlToBlink = null;
        Stopwatch stoper = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            Color blinkColor;
            if (pnlToBlink == panelNg)
            {
                blinkColor = Color.Red;
            }
            else
            {
                blinkColor = Color.Lime;
            }

            if (pnlToBlink != null)
            {
                if (stoper.IsRunning)
                {
                    if (stoper.Elapsed.Seconds > 1)
                    {
                        stoper.Stop();
                        timer1.Enabled = false;
                        pnlToBlink.BackColor = blinkColor;
                    }
                }
                else
                {
                    stoper.Reset();
                    stoper.Start();
                }

                if (pnlToBlink.BackColor == Color.White)
                {
                    pnlToBlink.BackColor = blinkColor;
                }
                else
                {
                    pnlToBlink.BackColor = Color.White;
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }
    }
}
