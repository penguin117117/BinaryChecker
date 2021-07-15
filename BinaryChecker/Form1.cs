using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BinaryChecker
{
    
    public partial class Form1 : Form
    {
        private static string filepath01 = "";
        private static string filepath02 = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = FileSelect.FileOpen();
            if (path == "") return;
            filepath01 = path;
            toolStripStatusLabel2.Text = Path.GetFileName(path);
        }

        private void 比較ファイルを開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = FileSelect.FileOpen();
            if (path == "") return;
            filepath02 = path;
            toolStripStatusLabel5.Text = Path.GetFileName(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] file1 , file2;
            if (filepath01 == "" || filepath02 == "")return;
            

            file1 = File.ReadAllBytes(filepath01);
            file2 = File.ReadAllBytes(filepath02);

            int bincount1 = 0;
            int bincount2 = 0;
            try {
                bincount1 = file1.Count();
                bincount2 = file2.Count();
            } catch (Exception exc) {
                MessageBox.Show("ファイルサイズが大きすぎます"+"\n\r"+exc.Message,"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
            

            if (bincount1 != bincount2) {
                textBox1.Text = "バイナリのサイズが違います" + Environment.NewLine;
                if (bincount1 > bincount2) {
                    BinChecker(bincount2, file1, file2);
                    return;
                }
                BinChecker(bincount1, file1, file2);
                return;
            }
            textBox1.Text = "";
            BinChecker(bincount1,file1,file2);
            if (textBox1.Text == "") textBox1.Text= "全てのバイナリが一致しました" + Environment.NewLine;

        }

        public void BinChecker(int bincount , byte[] bit1 , byte[] bit2) {
            textBox1.Visible = false;
            button1.Enabled = false;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = bincount;
            toolStripProgressBar1.Value = 0;
            for (int i = 0; i < bincount; i++)
            {
                toolStripProgressBar1.Value++;
                if (bit1[i] == bit2[i]) continue;
                textBox1.AppendText("開いたファイル "+bit1[i].ToString("X2") + "  比較対象ファイル " + bit2[i].ToString("X2") + "  位置 " + i.ToString("X8") + Environment.NewLine);
                
                Application.DoEvents();
            }
            System.Threading.Thread.Sleep(10);
            textBox1.Visible = true;
            button1.Enabled = true;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            var filecount = fileName.Count();
            Console.WriteLine(fileName[0]);
            Console.WriteLine(fileName[1]);
            if (filecount == 2) {
                if (File.Exists(fileName[0]) == false) return;
                filepath01 = fileName[0];
                toolStripStatusLabel2.Text = Path.GetFileName(fileName[0]);
                if (File.Exists(fileName[1])==false) return;
                filepath02 = fileName[1];
                toolStripStatusLabel5.Text = Path.GetFileName(fileName[1]);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }
    }
}
