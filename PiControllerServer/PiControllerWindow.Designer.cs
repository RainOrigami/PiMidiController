namespace PiControllerServer
{
    partial class PiControllerWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PiControllerWindow));
            this.contextMenuStrip = new ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.notifyIcon = new NotifyIcon(this.components);
            this.scDefiners = new SplitContainer();
            this.btnDown = new Button();
            this.btnUp = new Button();
            this.btnSave = new Button();
            this.lbTabs = new ListBox();
            this.txtTabName = new TextBox();
            this.btnRemoveTab = new Button();
            this.btnAddTab = new Button();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.scDefiners).BeginInit();
            this.scDefiners.Panel1.SuspendLayout();
            this.scDefiners.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new ToolStripItem[] { this.showToolStripMenuItem, this.exitToolStripMenuItem });
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new Size(106, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new Size(105, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += this.showToolStripMenuItem_Click_1;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new Size(105, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            this.notifyIcon.Text = "Pi Midi Controller";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += this.notifyIcon_MouseDoubleClick;
            // 
            // scDefiners
            // 
            this.scDefiners.Dock = DockStyle.Fill;
            this.scDefiners.FixedPanel = FixedPanel.Panel1;
            this.scDefiners.Location = new Point(0, 0);
            this.scDefiners.Name = "scDefiners";
            // 
            // scDefiners.Panel1
            // 
            this.scDefiners.Panel1.Controls.Add(this.btnDown);
            this.scDefiners.Panel1.Controls.Add(this.btnUp);
            this.scDefiners.Panel1.Controls.Add(this.btnSave);
            this.scDefiners.Panel1.Controls.Add(this.lbTabs);
            this.scDefiners.Panel1.Controls.Add(this.txtTabName);
            this.scDefiners.Panel1.Controls.Add(this.btnRemoveTab);
            this.scDefiners.Panel1.Controls.Add(this.btnAddTab);
            this.scDefiners.Size = new Size(1082, 248);
            this.scDefiners.SplitterDistance = 319;
            this.scDefiners.TabIndex = 1;
            // 
            // btnDown
            // 
            this.btnDown.Location = new Point(241, 207);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new Size(75, 23);
            this.btnDown.TabIndex = 6;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += this.btnDown_Click;
            // 
            // btnUp
            // 
            this.btnUp.Location = new Point(160, 207);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new Size(75, 23);
            this.btnUp.TabIndex = 5;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += this.btnUp_Click;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnSave.Location = new Point(3, 215);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(75, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += this.btnSave_Click;
            // 
            // lbTabs
            // 
            this.lbTabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lbTabs.FormattingEnabled = true;
            this.lbTabs.ItemHeight = 15;
            this.lbTabs.Location = new Point(3, 32);
            this.lbTabs.Name = "lbTabs";
            this.lbTabs.Size = new Size(313, 169);
            this.lbTabs.TabIndex = 3;
            this.lbTabs.SelectedIndexChanged += this.lbTabs_SelectedIndexChanged;
            // 
            // txtTabName
            // 
            this.txtTabName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtTabName.Location = new Point(165, 4);
            this.txtTabName.Name = "txtTabName";
            this.txtTabName.Size = new Size(151, 23);
            this.txtTabName.TabIndex = 2;
            this.txtTabName.KeyUp += this.txtTabName_KeyUp;
            // 
            // btnRemoveTab
            // 
            this.btnRemoveTab.Location = new Point(84, 3);
            this.btnRemoveTab.Name = "btnRemoveTab";
            this.btnRemoveTab.Size = new Size(75, 23);
            this.btnRemoveTab.TabIndex = 1;
            this.btnRemoveTab.Text = "Remove";
            this.btnRemoveTab.UseVisualStyleBackColor = true;
            this.btnRemoveTab.Click += this.btnRemoveTab_Click;
            // 
            // btnAddTab
            // 
            this.btnAddTab.Location = new Point(3, 3);
            this.btnAddTab.Name = "btnAddTab";
            this.btnAddTab.Size = new Size(75, 23);
            this.btnAddTab.TabIndex = 0;
            this.btnAddTab.Text = "Add";
            this.btnAddTab.UseVisualStyleBackColor = true;
            this.btnAddTab.Click += this.btnAddTab_Click;
            // 
            // PiControllerWindow
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1082, 248);
            this.Controls.Add(this.scDefiners);
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.MinimumSize = new Size(1098, 287);
            this.Name = "PiControllerWindow";
            this.Text = "Pi Midi Controller";
            this.FormClosing += this.PiControllerWindow_FormClosing;
            this.contextMenuStrip.ResumeLayout(false);
            this.scDefiners.Panel1.ResumeLayout(false);
            this.scDefiners.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.scDefiners).EndInit();
            this.scDefiners.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip;
        private NotifyIcon notifyIcon;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private SplitContainer scDefiners;
        private ListBox lbTabs;
        private TextBox txtTabName;
        private Button btnRemoveTab;
        private Button btnAddTab;
        private Button btnSave;
        private Button btnDown;
        private Button btnUp;
    }
}