using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using WordCount.Interfaces;

namespace WordCount.Kiwi
{
    public partial class Form1 : Form
    {
        FastColoredTextBoxNS.FastColoredTextBox txtBox;
        string FilePath { get; set; }

        public Form1()
        {
            InitializeComponent();
            txtBox = new FastColoredTextBox();
            txtBox.Dock = DockStyle.Fill;
            //txtBox.AllowDrop = true;
            //this.panel1.AllowDrop = true;
            this.panel1.Controls.Add(txtBox);
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "文本文件|*.txt|html|*.html|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                txtBox.Text = await File.OpenText(fName).ReadToEndAsync();
                txtBox.Tag = fName;
            }
        }
        /// <summary>
        /// 词频统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ProcessFile((string)txtBox.Tag);
        }
        /// <summary>
        /// 结果导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
           File.AppendAllText("D:\\WordKiwi.csv",txtBox.Text,Encoding.UTF8);
        }
        private void ProcessFile(string fileToCount)
        {
            IFileReader fileReader = new FileReader(fileToCount);
            var rootTrieNode = new TrieNode(null, ' ');
            IWordCounter wordCounter = new WordCounter(fileReader, rootTrieNode);
            wordCounter.StartCount();
            var totalDictionary = wordCounter.GetWordCountDictionary().OrderByDescending(a => a.Value);
            StringBuilder sb=new StringBuilder();
            string[] content= File.ReadAllLines("D:\\dict.csv");
            Dictionary<string, string> d = new Dictionary<string, string>();
            for (int i = 0; i < content.Length; i++)
            {
                var str = content[i].Split(new[] {','});
                d.Add(str[0],str[1]);
            }
            foreach (var word in totalDictionary)
            {
                //Console.WriteLine("{0}: {1}", word.Value, word.Key);
                if (d.ContainsKey(word.Key))
                {
                    sb.AppendLine(word.Value+","+ d.FirstOrDefault(a => a.Key == word.Key).ToString().Replace("[","").Replace("]",""));
                }
            }
            txtBox.ImeMode=ImeMode.On;
            txtBox.Text = sb.ToString();
        }
        
    }
}
