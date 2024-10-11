using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static AloneAIOR.AloneProcess;
using static AloneAIOR.ImageSystem;
using System.Runtime.InteropServices;

namespace AloneAIOR
{
    public partial class AloneAIO : Form
    {
        private Mutex mutex;
        public AloneAIO()
        {
            AloneProcess.InitializeSetting();
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            bool createdNew;
            mutex = new Mutex(true, "AloneAIORMutex", out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Error");
                Close();
                return;
            }
            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            mutex?.ReleaseMutex();
            mutex?.Close();
            base.OnClosed(e);
        }

        private async void AloneAIO_Load(object sender, EventArgs e)
        {
            AloneProcess.SetFormInstance(this);

            this.Size = new Size(205, 22);
            this.TopLevel = true;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            InputSystem.SetTalesRunnerKey();
            this.Size = new Size(253, 22);

        }

        private async void testing_Click(object sender, EventArgs e)
        {
            /*
                int RunPoint1 = await RunFunction.CheckRunPoint();
                MessageBox.Show(RunPoint1.ToString());
             */
            TalesState.Text = "Testing";
            EnsureProcessInitialized();
            await MapFunction.Map3();

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            System.Windows.Forms.Application.ExitThread();
        }
    }
}