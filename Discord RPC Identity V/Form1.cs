using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Discord_RPC_Identity_V
{
    public partial class Form1 : Form
    {
        private bool showConsole = false;
        private MainProcess mainProc;
        public Form1()
        {
            AllocConsole();
            TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            Console.SetOut(writer);
            Debug.Info("UI", "Initialize component");
            InitializeComponent();
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            ShowWindow(GetConsoleWindow(), SW_HIDE);
            Debug.Info("UI", "Done initialize component");
            Debug.Info("UI", "Initialize main process");
            MainProcess process = new MainProcess();
            process.Initialize();
            mainProc = process;
            if (GetStartup("dwrgRPC") == null)
            {
                autoStartupToolStripMenuItem.Checked = false;
            }
            else
            {
                autoStartupToolStripMenuItem.Checked = true;
            }
            Debug.Info("UI", "Done initialize main process");
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private const UInt32 StdOutputHandle = 0xFFFFFFF5;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.ContextMenuStrip.Show(new Point(Cursor.Position.X + 1, Cursor.Position.Y));
        }
        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.ContextMenuStrip.Show(new Point(Cursor.Position.X + 1, Cursor.Position.Y));
        }
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            if (notifyIcon1.ContextMenuStrip != null)
            {
                notifyIcon1.ContextMenuStrip.Visible = false;
                notifyIcon1.ContextMenuStrip.Hide();
            }
            mainProc.getRPC().client.Dispose();
            Application.Exit();
        }
        public static object? GetStartup(string appName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                return key.GetValue(appName);
            }
        }
        public static void AddStartup(string appName, string path)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(appName, "\"" + path + "\"");
            }
        }
        public static void RemoveStartup(string appName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(appName, false);
            }
        }
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            debugConsoleToolStripMenuItem.Checked = showConsole;
        }

        private void debugConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showConsole == false)
            {
                ShowWindow(GetConsoleWindow(), SW_SHOW);
                showConsole = true;
            }
            else
            {
                ShowWindow(GetConsoleWindow(), SW_HIDE);
                showConsole = false;
            }
            debugConsoleToolStripMenuItem.Checked = showConsole;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resetDetectProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainProc.ResetCounter();
        }

        private void autoStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetStartup("dwrgRPC") == null)
            {
                AddStartup("dwrgRPC", Process.GetCurrentProcess().MainModule.FileName);
                autoStartupToolStripMenuItem.Checked = true;
            }
            else
            {
                RemoveStartup("dwrgRPC");
                autoStartupToolStripMenuItem.Checked = false;
            }
        }
    }
}
