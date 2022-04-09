using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CancelationToken
{
    public partial class Form1 : Form
    {
        CancellationTokenSource tokenSource = null;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var progress = new Progress<int>(value =>
            {
                progressBar1.Value = value;
                label1.Text = $"{value}";

            });
            try
            {
                await Task.Run(() => IntiateProgressBar(100, progress, token));
                label1.Text = "Complete";
            }
            catch (OperationCanceledException ex)
            {
                label1.Text = "Cancel";
            }
            finally
            {
                tokenSource.Dispose();
            }
            
        }
       

        private void IntiateProgressBar(int count, IProgress<int> progress, CancellationToken token)
        {
            for(int i = 0; i < count; i++)
            {
                Thread.Sleep(100);
                int per = (i *100) / count;
                progress.Report(per);
                if(token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
               // progressBar1.Value = per;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tokenSource?.Cancel();
        }
    }
}
