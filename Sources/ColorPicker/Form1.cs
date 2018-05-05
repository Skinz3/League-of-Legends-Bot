using LeagueBot.Windows;
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

namespace ColorViewer
{
    public partial class Form1 : Form
    {
        Thread formThread;

        public Form1()
        {
            formThread = Thread.CurrentThread;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartAnalysis();
        }
        private void StartAnalysis()
        {
            new Thread(new ThreadStart(new Action(() =>
            {
                while (formThread.ThreadState == ThreadState.Running)
                {
                    try
                    {
                        pictureBox1.Invoke(new Action(() => { pictureBox1.BackColor = Interop.GetPixelColor(Interop.GetMousePosition()); }));
                        textBox1.Invoke(new Action(() => { textBox1.Text = GetColorDescription(pictureBox1.BackColor); }));
                        textBox2.Invoke(new Action(() => { textBox2.Text = Interop.GetMousePosition().ToString(); }));
                    }
                    catch
                    {
                        Environment.Exit(0);
                    }
                }
            }))).Start();
        }
        private string GetColorDescription(Color color)
        {
            return string.Format("R:{0} G:{1} B:{2}", color.R, color.G, color.B);
        }
    }
}
