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

namespace Textor
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private string curFileName;
        private string selectedText;

        private void create_Click(object sender, EventArgs e)
        {
            curFileName = null;
            MainRTB.Clear();
            WorkRTB.Clear();
            this.Text = "Textor";
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            fillDialogProps(openFileDialog);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    curFileName = openFileDialog.FileName;
                    MainRTB.Text = System.IO.File.ReadAllText(curFileName); 
                    this.Text = this.Text + " " + curFileName; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (curFileName != null)
            {
                System.IO.File.WriteAllText(curFileName, MainRTB.Text);
            }
            else
            {
                SaveAs_Click(sender, e);
            }
        }

        private void SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            fillDialogProps(saveFileDialog);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    curFileName = saveFileDialog.FileName;
                    Save_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write to file. Original error: " + ex.Message);
                }
            }
        }

        private void Work1_Click(object sender, EventArgs e)
        {
            WorkRTB.Clear();
            string[] arr = MainRTB.Text.Split('\n');
            List<string> list = new List<string>();
            int len = 0;
            foreach (string str in arr) {
                if (str.Length > len) {
                    len = str.Length;
                }
            }
            foreach (string str in arr)
            {
                if (str.Length == len) list.Add(str);
            }
            foreach (string str in list)
            {
                str.Trim();
                List<string> l = new List<string>(str.Split(' '));
                l.Sort();
                foreach (string word in l)
                {
                    WorkRTB.AppendText(word + " ");
                }
                WorkRTB.Text.TrimEnd();
                WorkRTB.AppendText("\n");
            }
        }

        private void fillDialogProps(FileDialog dialog)
        {
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            selectedText = MainRTB.SelectedText;
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            Copy_Click(sender, e);
            MainRTB.SelectedText = "";
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            MainRTB.SelectedText = selectedText;
        }
    }
}
