using System.Diagnostics;
namespace recheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFolderBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "reWASD.exe";
            openFileDialog.Filter = "reWASD.exe|reWASD.exe";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = openFileDialog.FileName;
            }
        }


        // Token: 0x06000029 RID: 41 RVA: 0x000021F8 File Offset: 0x000003F8
        private Process StartProcess(string path)
        {
            Process process = Process.Start(path);
            process.EnableRaisingEvents = true;
            return process;
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00002DF8 File Offset: 0x00000FF8
        private void KillProcesses()
        {
            (from x in Process.GetProcesses()
             where x.ProcessName.StartsWith("reWASD", StringComparison.OrdinalIgnoreCase)
             select x).ToList<Process>().ForEach(delegate (Process x)
             {
                 x.Kill();
             });
            (from x in Process.GetProcesses()
             where x.ProcessName.StartsWith("aae_", StringComparison.OrdinalIgnoreCase)
             select x).ToList<Process>().ForEach(delegate (Process x)
             {
                 x.Kill();
             });
        }

        // Token: 0x0600002B RID: 43 RVA: 0x00002EAC File Offset: 0x000010AC
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {


            string directoryName = ""; 
            directoryName = Path.GetDirectoryName(pathBox.Text);
            if (!orig_files)
            {
                if (isChecked)
                {
                    DeleteFiles(directoryName);
                    KillProcesses();
                    RenameFiles(directoryName);
                    return;
                }
                KillProcesses();
                RenameFiles(directoryName);
            }
        }
        public void CreateSymlinks(string path)
        {
            if (orig_files)
            {
                try
                {
                    File.CreateSymbolicLink(path + origUI, path + newUI);
                    File.CreateSymbolicLink(path + origEng, path + newEng);
                    File.CreateSymbolicLink(path + origServ, path + newServ);
                    orig_files = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void DeleteFiles(string path)
        {
            try
            {
                File.Delete(path + origUI);
                File.Delete(path + origEng);
                File.Delete(path + origServ);
            }
            catch
            {
            }
        }
        

		// Token: 0x0600000A RID: 10 RVA: 0x000020A5 File Offset: 0x000002A5

		// Token: 0x04000004 RID: 4
		
	
        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0)
            {
                return;
            }
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += delegate
            {
                timer1.Enabled = false;
                timer1.Stop();
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00002AE4 File Offset: 0x00000CE4
        public void RenameFiles(string path)
        {
            if (orig_files)
            {
                try
                {
                    File.Move(path + origUI, path + newUI);
                    File.Move(path + origEng, path + newEng);
                    File.Move(path + origServ, path + newServ);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            try
            {
                File.Move(path + newUI, path + origUI);
                File.Move(path + newEng, path + origEng);
                File.Move(path + newServ, path + origServ);
                orig_files = true;
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private string origUI = "/reWASD.exe";
       
        // Token: 0x0400000D RID: 13
        private string newUI = "/aae_ui.exe";

        // Token: 0x0400000E RID: 14
        private string origEng = "/reWASDEngine.exe";

        // Token: 0x0400000F RID: 15
        private string newEng = "/aae_eng.exe";

        // Token: 0x04000010 RID: 16
        private string origServ = "/reWASDService.exe";

        // Token: 0x04000011 RID: 17
        private string newServ = "/aae_serv.exe";

        // Token: 0x04000012 RID: 18
        private Process reWASD;

        // Token: 0x04000013 RID: 19
        private bool orig_files = true;

        // Token: 0x04000014 RID: 20
        private bool isChecked;

        // Token: 0x04000015 RID: 21
        public const int WM_NCLBUTTONDOWN = 161;

        // Token: 0x04000016 RID: 22
        public const int HT_CAPTION = 2;

        private void runrbtn_Click(object sender, EventArgs e)
        {
            string text = pathBox.Text;
            string directoryName = Path.GetDirectoryName(text);
            if (!orig_files)
            {
                MessageBox.Show("AA Enabler is already running, please restart the application");
                return;
            }
            if (!text.Contains("reWASD.exe"))
            {
                MessageBox.Show("reWASD.exe not found");
                return;
            }
            if (!isChecked)
            {

                RenameFiles(directoryName);
                CreateSymlinks(directoryName);
                reWASD = StartProcess(text);
                wait(3000);
                DeleteFiles(directoryName);
                return;
            }

            RenameFiles(directoryName);
            CreateSymlinks(directoryName);
            wait(2000);
        }
    }

}
