using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace 韌體解胞器
{
    public partial class Form1 : Form
    {
        bool enable = false;
        public Form1()
        {
            InitializeComponent();
        }
        string pr = "";
        private void print(string input)
        {
            
            System.Diagnostics.Debug.Print(input);
        }
        int version = 0;
        int new_blocks = 0;
        System.IO.TextReader trans_list;
        System.IO.FileStream output_img;
        private void button3_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text != "" & System.IO.File.Exists(textBox1.Text + @"\system.new.dat") & System.IO.File.Exists(textBox1.Text + @"\system.transfer.list")))
            {
                textBox3.Text = "錯誤:請確認您已經選擇目錄，且目錄內有system.transfer.list與system.new.dat檔案。";
            }
            else
            {
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
                backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
                button1.Enabled = false;
                button3.Visible = false;
                button4.Visible = true;
                enable = true;
                textBox3.Text = "開始轉換";
                commands.Clear();
            }
                
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!enable)
            {
                
            }
            else
            {
                
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBar1.Value = e.ProgressPercentage;
            label4.Text = pr;
            textBox3.Text = "目前進度:" + e.ProgressPercentage.ToString() + " %" + System.Environment.NewLine;
            if (e.ProgressPercentage >= 98)
            {

                
                {
                    textBox3.Text = "完成輸出，輸出位置:" + textBox1.Text + @"\system.img.ext4";
                    button1.Enabled = true;
                    progressBar1.Value = 0;
                    button3.Visible = true;
                    button4.Visible = false;
                    
                    print(enable.ToString());
                }
            }
            else
            {
                if (!enable)
                {
                 print(enable.ToString());
                textBox3.Text = "使用者手動停止了這個操作。";
                button1.Enabled = true;
                progressBar1.Value = 0;
                button3.Visible = true;
                button4.Visible = false;
                System.IO.File.Delete(textBox1.Text + @"\system.img.ext4");
                }
                
            }
        }
        private void Start()
        {
            
        }
        List<object[]> commands = new List<object[]>();
        private void parse_transfer_list_file()
        {
            trans_list = new System.IO.StreamReader(textBox1.Text + @"\system.transfer.list");
            version = Convert.ToInt32(trans_list.ReadLine());
            new_blocks = Convert.ToInt32(trans_list.ReadLine());
            if (version >= 2)
            {
                trans_list.ReadLine();
                trans_list.ReadLine();
            }
            
            for(int i = 4; i <= 6;i++)
            {
                string[] line = System.Text.RegularExpressions.Regex.Split(trans_list.ReadLine()," ");
                string cmd = line[0];
                if (cmd == "erase" | cmd == "new" | cmd == "zero")
                {
                    object[] temp = new object[2] { cmd, rangeset(line[1]) };
                    commands.Add(temp);
                }
                
            }
            trans_list.Close();
        }
        private byte[] Read(System.IO.FileStream input,int count)
        {
            List<byte> tmp = new List<byte>();
            for(int i = 1; i <= count; i++)
            {
                tmp.Add((byte)input.ReadByte());
            }
            return tmp.ToArray();
        }
        private List<int[]> rangeset(string src)
        {
            string[] src_set = System.Text.RegularExpressions.Regex.Split(src, ",");
            List<int> num_set = new List<int>();
            List<int[]> Return = new List<int[]>();
            foreach(string item in src_set)
            {
                num_set.Add(Convert.ToInt32(item));
            }
            if(num_set.Count != num_set[0] + 1)
            {
                MessageBox.Show("Error.");
            }
            foreach (int i in Range(1, num_set.Count, 2))
            {
                int[] temp = new int[2] { num_set[i], num_set[i + 1] };
                Return.Add(temp);
            }
            return Return;

        }
        private int[] Range(int start,int stop,int step)
        {
            int nowint = start;
            List<int> uu = new List<int>();
            uu.Add(start);
            for(int i =0; ; i++)
            {
                nowint += step;
                if (nowint == stop)
                {
                    break;
                }
                
                uu.Add(nowint);
            }
            return uu.ToArray();
        }
       private int Max(List<int> input)
        {
            int max = 0;
            foreach(int i in input)
            {
                if(max < i)
                {
                    max = i;
                }
            }
            return max;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] p = Range(0, 10, 2);
            System.Diagnostics.Debug.WriteLine(Range(0, 10, 2));
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (textBox1.Text != "" & System.IO.File.Exists(textBox1.Text + @"\system.new.dat") & System.IO.File.Exists(textBox1.Text + @"\system.transfer.list"))
            {
                parse_transfer_list_file();
                string z = "韌體:Android";
                switch (version)
                {
                    case 1:
                        print(z + "5.0");
                       pr= (z + "5.0");
                        break;
                    case 2:
                        print(z + "5.1");
                        pr= (z + "5.1");
                        break;
                    case 3:
                        print(z + "6.0");
                        pr= (z + "6.0");
                        break;
                    case 4:
                        print(z + "7.0");
                        pr= (z + "7.0");
                        break;
                }

                if (System.IO.File.Exists(textBox1.Text + @"\system.img.ext4"))
                {
                    System.IO.File.Delete(textBox1.Text + @"\system.img.ext4");
                }
                output_img = new System.IO.FileStream(textBox1.Text + @"\system.img.ext4", System.IO.FileMode.Create);
                System.IO.FileStream new_data_file = new System.IO.FileStream(textBox1.Text + @"\system.new.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                List<int[]> all_block_sets = new List<int[]>();
                foreach (object[] command in commands)
                {
                    object[] tmp = command;
                    List<int[]> temp = (List<int[]>)tmp[1];
                    foreach (int[] i in temp)
                    {
                        all_block_sets.Add(i);
                    }

                }
                List<int> tmp1 = new List<int>();
                foreach (int[] pair in all_block_sets)
                {
                    tmp1.Add(pair[1]);
                }
                int max_file_size = Max(tmp1) * 4096;
                //取得檔案大小
                long maxsize = 0;
                {
                    foreach(object[] command in commands)
                    {
                        string n = (string)command[0];
                        if (n == "new")
                        {
                            List<int[]> aa = (List<int[]>)command[1];
                            int[] ll = aa[aa.Count - 1];
                            long begin = ll[0];
                            long end = ll[1];
                            long block_count = end - begin;
                            maxsize = (begin + block_count) * 4096;

                        }
                    }
                    
                }
                System.Diagnostics.Debug.Print(maxsize.ToString());

                foreach (object[] command in commands)
                {
                    string n = (string)command[0];
                    if (n == "new")
                    {
                        
                        List<int[]> tmpp = (List<int[]>)command[1];
                            foreach (int[] block in tmpp)
                            {
                            if (enable == true) { 
                                long begin = block[0];
                                long end = block[1];
                                long block_count = end - begin;
                                long BLOCK_SIZE = 4096;
                                print(String.Format("Copying {0} blocks into position {1}...", block_count, begin));
                                //try
                                //{
                                //output_img.Seek(begin * 4096, System.IO.SeekOrigin.Begin);
                                output_img.Position = begin * BLOCK_SIZE;
                                // }
                                //catch(Exception ex)
                                //{
                                //print("發生錯誤:" + ex.Message + "，在複製" + String.Format("位置{1}的{0} 個區塊時所發生的。", block_count, begin));
                                //}




                                while (block_count > 0)
                                {

                                    for (int i = 1; i <= 4096; i++)
                                    {
                                        output_img.WriteByte((byte)new_data_file.ReadByte());

                                    }
                                    // output_img.Write(Read(new_data_file,4096),0,1);

                                    block_count -= 1;



                                }
                                Double process = Convert.ToDouble((begin + block_count) * 4096) / Convert.ToDouble(maxsize) * Convert.ToDouble(100);
                                backgroundWorker1.ReportProgress(Convert.ToInt32(process));
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                output_img.Close();
                new_data_file.Close();
                print("輸出ext4檔案位置:" + textBox1.Text + @"\system.img.ext4");
                
            }
            else
            {
                print("錯誤:請確認您已經選擇目錄，且目錄內有system.transfer.list與system.new.dat檔案。");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            enable = false;
        }
    }
}
