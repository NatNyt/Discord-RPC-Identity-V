namespace Discord_RPC_Identity_V
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            exitToolStripMenuItem = new ToolStripMenuItem();
            resetDetectProcessToolStripMenuItem = new ToolStripMenuItem();
            autoStartupToolStripMenuItem = new ToolStripMenuItem();
            debugConsoleToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Identity V Discord RPC";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseClick += NotifyIcon1_MouseClick;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { exitToolStripMenuItem, debugConsoleToolStripMenuItem, exitToolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(156, 70);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resetDetectProcessToolStripMenuItem, autoStartupToolStripMenuItem });
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(155, 22);
            exitToolStripMenuItem.Text = "Utils";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // resetDetectProcessToolStripMenuItem
            // 
            resetDetectProcessToolStripMenuItem.Name = "resetDetectProcessToolStripMenuItem";
            resetDetectProcessToolStripMenuItem.Size = new Size(181, 22);
            resetDetectProcessToolStripMenuItem.Text = "Reset detect process";
            resetDetectProcessToolStripMenuItem.Click += resetDetectProcessToolStripMenuItem_Click;
            // 
            // autoStartupToolStripMenuItem
            // 
            autoStartupToolStripMenuItem.Name = "autoStartupToolStripMenuItem";
            autoStartupToolStripMenuItem.Size = new Size(181, 22);
            autoStartupToolStripMenuItem.Text = "Auto Startup";
            autoStartupToolStripMenuItem.Click += autoStartupToolStripMenuItem_Click;
            // 
            // debugConsoleToolStripMenuItem
            // 
            debugConsoleToolStripMenuItem.Checked = true;
            debugConsoleToolStripMenuItem.CheckState = CheckState.Checked;
            debugConsoleToolStripMenuItem.Name = "debugConsoleToolStripMenuItem";
            debugConsoleToolStripMenuItem.Size = new Size(155, 22);
            debugConsoleToolStripMenuItem.Text = "Debug Console";
            debugConsoleToolStripMenuItem.Click += debugConsoleToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(155, 22);
            exitToolStripMenuItem1.Text = "Exit";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "Form1";
            Text = "Form1";
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem debugConsoleToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem resetDetectProcessToolStripMenuItem;
        private ToolStripMenuItem autoStartupToolStripMenuItem;
    }
}
