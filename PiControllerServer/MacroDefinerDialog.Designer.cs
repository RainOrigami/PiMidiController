namespace PiControllerServer
{
    partial class MacroDefinerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbMacroType = new GroupBox();
            this.rbDelay = new RadioButton();
            this.rbKey = new RadioButton();
            this.gbKey = new GroupBox();
            this.cbAvailableKeys = new ComboBox();
            this.gbDelay = new GroupBox();
            this.lblMs = new Label();
            this.nudDelay = new NumericUpDown();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.gbMacroType.SuspendLayout();
            this.gbKey.SuspendLayout();
            this.gbDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudDelay).BeginInit();
            this.SuspendLayout();
            // 
            // gbMacroType
            // 
            this.gbMacroType.Controls.Add(this.rbDelay);
            this.gbMacroType.Controls.Add(this.rbKey);
            this.gbMacroType.Location = new Point(12, 12);
            this.gbMacroType.Name = "gbMacroType";
            this.gbMacroType.Size = new Size(195, 72);
            this.gbMacroType.TabIndex = 0;
            this.gbMacroType.TabStop = false;
            this.gbMacroType.Text = "Macro type";
            // 
            // rbDelay
            // 
            this.rbDelay.AutoSize = true;
            this.rbDelay.Location = new Point(6, 47);
            this.rbDelay.Name = "rbDelay";
            this.rbDelay.Size = new Size(54, 19);
            this.rbDelay.TabIndex = 1;
            this.rbDelay.Text = "Delay";
            this.rbDelay.UseVisualStyleBackColor = true;
            this.rbDelay.CheckedChanged += this.rbTypes_CheckedChanged;
            // 
            // rbKey
            // 
            this.rbKey.AutoSize = true;
            this.rbKey.Checked = true;
            this.rbKey.Location = new Point(6, 22);
            this.rbKey.Name = "rbKey";
            this.rbKey.Size = new Size(79, 19);
            this.rbKey.TabIndex = 0;
            this.rbKey.TabStop = true;
            this.rbKey.Text = "Key stroke";
            this.rbKey.UseVisualStyleBackColor = true;
            this.rbKey.CheckedChanged += this.rbTypes_CheckedChanged;
            // 
            // gbKey
            // 
            this.gbKey.Controls.Add(this.cbAvailableKeys);
            this.gbKey.Location = new Point(12, 90);
            this.gbKey.Name = "gbKey";
            this.gbKey.Size = new Size(195, 55);
            this.gbKey.TabIndex = 1;
            this.gbKey.TabStop = false;
            this.gbKey.Text = "Key stoke";
            // 
            // cbAvailableKeys
            // 
            this.cbAvailableKeys.FormattingEnabled = true;
            this.cbAvailableKeys.Location = new Point(7, 22);
            this.cbAvailableKeys.Name = "cbAvailableKeys";
            this.cbAvailableKeys.Size = new Size(182, 23);
            this.cbAvailableKeys.TabIndex = 1;
            // 
            // gbDelay
            // 
            this.gbDelay.Controls.Add(this.lblMs);
            this.gbDelay.Controls.Add(this.nudDelay);
            this.gbDelay.Enabled = false;
            this.gbDelay.Location = new Point(12, 151);
            this.gbDelay.Name = "gbDelay";
            this.gbDelay.Size = new Size(195, 52);
            this.gbDelay.TabIndex = 2;
            this.gbDelay.TabStop = false;
            this.gbDelay.Text = "Delay";
            // 
            // lblMs
            // 
            this.lblMs.AutoSize = true;
            this.lblMs.Location = new Point(166, 24);
            this.lblMs.Name = "lblMs";
            this.lblMs.Size = new Size(23, 15);
            this.lblMs.TabIndex = 1;
            this.lblMs.Text = "ms";
            // 
            // nudDelay
            // 
            this.nudDelay.Location = new Point(6, 22);
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new Size(154, 23);
            this.nudDelay.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new Point(51, 209);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += this.btnOk_Click;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(132, 209);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += this.btnCancel_Click;
            // 
            // MacroDefinerDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(210, 240);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbDelay);
            this.Controls.Add(this.gbKey);
            this.Controls.Add(this.gbMacroType);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MacroDefinerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Define Macro";
            this.gbMacroType.ResumeLayout(false);
            this.gbMacroType.PerformLayout();
            this.gbKey.ResumeLayout(false);
            this.gbDelay.ResumeLayout(false);
            this.gbDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudDelay).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private GroupBox gbMacroType;
        private RadioButton rbDelay;
        private RadioButton rbKey;
        private GroupBox gbKey;
        private ComboBox cbAvailableKeys;
        private GroupBox gbDelay;
        private Label lblMs;
        private NumericUpDown nudDelay;
        private Button btnOk;
        private Button btnCancel;
    }
}