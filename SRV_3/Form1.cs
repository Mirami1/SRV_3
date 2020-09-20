using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRV_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Process cmd;


        public void Run()
        {

            if (cmd != null && !cmd.HasExited)
                cmd.Kill();
            try
            {
                cmd = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = textBox3.Text,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = true,
                        StandardOutputEncoding = Encoding.GetEncoding(866),
                    }
                };
                cmd.OutputDataReceived += new DataReceivedEventHandler(Cmd_OutputDataReceived);
                cmd.Start();
                cmd.BeginOutputReadLine();
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cmd_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
                listBox1.Invoke(new Action(() => listBox1.Items.Add(e.Data)));
        }

        public void Send()
        {
            try
            {
                var pWriter = cmd.StandardInput;

                if (pWriter.BaseStream.CanWrite)
                {
                    pWriter.WriteLine(textBox2.Text);
                }
                else
                    MessageBox.Show("Cant Write");
                pWriter.Flush();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( cmd!=null && !cmd.HasExited)
                cmd.Kill();
        }

    }
}
