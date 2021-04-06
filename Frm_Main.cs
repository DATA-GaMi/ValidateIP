using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;
using System.Text;

namespace ValidateIP
{
    public partial class Frm_Main : Form
    {
        Process p;
        public Frm_Main()
        {
            InitializeComponent();
        }


        private void btn_Validate_Click(object sender, EventArgs e)
        {
            string Input = textBox1.Text;

            p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.WorkingDirectory = "";
            //p.StartInfo.
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = false;

            p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            //启动程序
            p.Start();
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(Input);

            p.StandardInput.AutoFlush = true;
            p.BeginOutputReadLine();
        }
        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                StringBuilder sb = new StringBuilder(this.OutBox.Text);
                this.OutBox.Text = sb.AppendLine(outLine.Data).ToString();
                this.OutBox.SelectionStart = this.OutBox.Text.Length;
                this.OutBox.ScrollToCaret();
            }
        }
        //窗体关闭事件
        private void MakeTrainingSetForm_ClipSampleGeneration_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (p != null)
                p.WaitForExit(6000);
                //p.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "当前系统时间" +" " + DateTime.Now.ToLongTimeString();
        }
    }
}