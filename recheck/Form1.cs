using System.Diagnostics;
using System.Security.Principal;
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
            static bool IsAdministrator()
            {
                return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                          .IsInRole(WindowsBuiltInRole.Administrator);
            }
            if (!IsAdministrator())
            {
                MessageBox.Show("Please run this program as Administrator!");
                Close();

            }

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


       
        private Process StartProcess(string path)
        {
            Process process = Process.Start(path);
            process.EnableRaisingEvents = true;
            return process;
        }

      
        private void KillProcesses()
        {
            (from x in Process.GetProcesses()
             where x.ProcessName.StartsWith("reWASD", StringComparison.OrdinalIgnoreCase)
             select x).ToList<Process>().ForEach(delegate (Process x)
             {
                 x.Kill();
             });
            (from x in Process.GetProcesses()
             where x.ProcessName.StartsWith("rre_", StringComparison.OrdinalIgnoreCase)
             select x).ToList<Process>().ForEach(delegate (Process x)
             {
                 x.Kill();
             });
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
          
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
            else
            {
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
        }

        private void runrbtn_Click(object sender, EventArgs e)
        {
            string text = pathBox.Text;
            string directoryName = Path.GetDirectoryName(text);
            if (!orig_files)
            {
                MessageBox.Show("Rechecker is already running, please restart the application");
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string directoryName = Path.GetDirectoryName(pathBox.Text);
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
                MessageBox.Show("Cleaning Temp Data.");
            }
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private string origUI = "/reWASD.exe";

        private string newUI = "/rre_" + RandomString(15) + ".exe";

        private string origEng = "/reWASDEngine.exe";

        private string newEng = "/rre_" + RandomString(15) + ".exe";

        private string origServ = "/reWASDService.exe";

        private string newServ = "/rre_" + RandomString(15) + ".exe";

        private Process reWASD;

        private bool orig_files = true;

        private bool isChecked;

        public const int WM_NCLBUTTONDOWN = 161;

        public const int HT_CAPTION = 2;

      
    }

}
