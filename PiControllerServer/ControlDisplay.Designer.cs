namespace PiControllerServer
{
    partial class ControlDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.lblNameValue = new Label();
            this.lblType = new Label();
            this.lblNote = new Label();
            this.lblTypeValue = new Label();
            this.lblNoteValue = new Label();
            this.btnEdit = new Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(42, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // lblNameValue
            // 
            this.lblNameValue.AutoSize = true;
            this.lblNameValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblNameValue.Location = new Point(48, 0);
            this.lblNameValue.Name = "lblNameValue";
            this.lblNameValue.Size = new Size(12, 15);
            this.lblNameValue.TabIndex = 1;
            this.lblNameValue.Text = "-";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new Point(0, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(34, 15);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new Point(0, 30);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(36, 15);
            this.lblNote.TabIndex = 3;
            this.lblNote.Text = "Note:";
            // 
            // lblTypeValue
            // 
            this.lblTypeValue.AutoSize = true;
            this.lblTypeValue.Location = new Point(48, 15);
            this.lblTypeValue.Name = "lblTypeValue";
            this.lblTypeValue.Size = new Size(12, 15);
            this.lblTypeValue.TabIndex = 4;
            this.lblTypeValue.Text = "-";
            // 
            // lblNoteValue
            // 
            this.lblNoteValue.AutoSize = true;
            this.lblNoteValue.Location = new Point(48, 30);
            this.lblNoteValue.Name = "lblNoteValue";
            this.lblNoteValue.Size = new Size(12, 15);
            this.lblNoteValue.TabIndex = 5;
            this.lblNoteValue.Text = "-";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnEdit.Location = new Point(0, 48);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(144, 23);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += this.btnEdit_Click;
            // 
            // ControlDisplay
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblNoteValue);
            this.Controls.Add(this.lblTypeValue);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblNameValue);
            this.Controls.Add(this.lblName);
            this.Name = "ControlDisplay";
            this.Size = new Size(146, 74);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblNameValue;
        private Label lblType;
        private Label lblNote;
        private Label lblTypeValue;
        private Label lblNoteValue;
        private Button btnEdit;
    }
}
