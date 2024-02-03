namespace PiControllerServer
{
    partial class ControlDefinerDialog
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
            this.btnClose = new Button();
            this.controlDefiner = new ControlDefiner();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Location = new Point(227, 717);
            this.btnClose.Margin = new Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(151, 38);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // controlDefiner
            // 
            this.controlDefiner.Location = new Point(17, 20);
            this.controlDefiner.Margin = new Padding(6, 8, 6, 8);
            this.controlDefiner.Name = "controlDefiner";
            this.controlDefiner.Size = new Size(361, 697);
            this.controlDefiner.TabIndex = 1;
            // 
            // ControlDefinerDialog
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new SizeF(10F, 25F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new Size(397, 769);
            this.ControlBox = false;
            this.Controls.Add(this.controlDefiner);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Margin = new Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(396, 816);
            this.Name = "ControlDefinerDialog";
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Text = "Control Definition";
            this.ResumeLayout(false);
        }

        #endregion

        private Button btnClose;
        private ControlDefiner controlDefiner;
    }
}