namespace PiControllerServer
{
    partial class ControlDefiner
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
            this.gbType = new GroupBox();
            this.rbTypeBlank = new RadioButton();
            this.rbTypeKnob = new RadioButton();
            this.rbTypeButton = new RadioButton();
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.nudNote = new NumericUpDown();
            this.lblNote = new Label();
            this.lbSoftStops = new ListBox();
            this.btnAdd = new Button();
            this.btnRemove = new Button();
            this.nudSoftStop = new NumericUpDown();
            this.gbSoftStops = new GroupBox();
            this.gbKnobSettings = new GroupBox();
            this.cbCentered = new CheckBox();
            this.lblOverdrive = new Label();
            this.lblMaxValue = new Label();
            this.lblMinValue = new Label();
            this.nudOverdrive = new NumericUpDown();
            this.nudMaxValue = new NumericUpDown();
            this.nudMinValue = new NumericUpDown();
            this.gbType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudNote).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudSoftStop).BeginInit();
            this.gbSoftStops.SuspendLayout();
            this.gbKnobSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudOverdrive).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMaxValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMinValue).BeginInit();
            this.SuspendLayout();
            // 
            // gbType
            // 
            this.gbType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.gbType.Controls.Add(this.rbTypeBlank);
            this.gbType.Controls.Add(this.rbTypeKnob);
            this.gbType.Controls.Add(this.rbTypeButton);
            this.gbType.Location = new Point(0, 32);
            this.gbType.Name = "gbType";
            this.gbType.Size = new Size(253, 97);
            this.gbType.TabIndex = 0;
            this.gbType.TabStop = false;
            this.gbType.Text = "Control type";
            // 
            // rbTypeBlank
            // 
            this.rbTypeBlank.AutoSize = true;
            this.rbTypeBlank.Checked = true;
            this.rbTypeBlank.Location = new Point(6, 22);
            this.rbTypeBlank.Name = "rbTypeBlank";
            this.rbTypeBlank.Size = new Size(54, 19);
            this.rbTypeBlank.TabIndex = 2;
            this.rbTypeBlank.TabStop = true;
            this.rbTypeBlank.Text = "Blank";
            this.rbTypeBlank.UseVisualStyleBackColor = true;
            // 
            // rbTypeKnob
            // 
            this.rbTypeKnob.AutoSize = true;
            this.rbTypeKnob.Location = new Point(6, 72);
            this.rbTypeKnob.Name = "rbTypeKnob";
            this.rbTypeKnob.Size = new Size(53, 19);
            this.rbTypeKnob.TabIndex = 1;
            this.rbTypeKnob.TabStop = true;
            this.rbTypeKnob.Text = "Knob";
            this.rbTypeKnob.UseVisualStyleBackColor = true;
            this.rbTypeKnob.CheckedChanged += this.rbTypeKnob_CheckedChanged;
            // 
            // rbTypeButton
            // 
            this.rbTypeButton.AutoSize = true;
            this.rbTypeButton.Location = new Point(6, 47);
            this.rbTypeButton.Name = "rbTypeButton";
            this.rbTypeButton.Size = new Size(61, 19);
            this.rbTypeButton.TabIndex = 0;
            this.rbTypeButton.TabStop = true;
            this.rbTypeButton.Text = "Button";
            this.rbTypeButton.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(3, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(42, 15);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtName.Location = new Point(51, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(199, 23);
            this.txtName.TabIndex = 2;
            // 
            // nudNote
            // 
            this.nudNote.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.nudNote.Location = new Point(45, 392);
            this.nudNote.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudNote.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            this.nudNote.Name = "nudNote";
            this.nudNote.Size = new Size(205, 23);
            this.nudNote.TabIndex = 3;
            this.nudNote.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // lblNote
            // 
            this.lblNote.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new Point(3, 394);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(36, 15);
            this.lblNote.TabIndex = 4;
            this.lblNote.Text = "Note:";
            // 
            // lbSoftStops
            // 
            this.lbSoftStops.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lbSoftStops.FormattingEnabled = true;
            this.lbSoftStops.ItemHeight = 15;
            this.lbSoftStops.Location = new Point(3, 22);
            this.lbSoftStops.Name = "lbSoftStops";
            this.lbSoftStops.Size = new Size(215, 49);
            this.lbSoftStops.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnAdd.Location = new Point(224, 77);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(26, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += this.btnAdd_Click;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnRemove.Location = new Point(224, 22);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new Size(26, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += this.btnRemove_Click;
            // 
            // nudSoftStop
            // 
            this.nudSoftStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.nudSoftStop.Location = new Point(3, 77);
            this.nudSoftStop.Name = "nudSoftStop";
            this.nudSoftStop.Size = new Size(215, 23);
            this.nudSoftStop.TabIndex = 8;
            // 
            // gbSoftStops
            // 
            this.gbSoftStops.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.gbSoftStops.Controls.Add(this.lbSoftStops);
            this.gbSoftStops.Controls.Add(this.nudSoftStop);
            this.gbSoftStops.Controls.Add(this.btnRemove);
            this.gbSoftStops.Controls.Add(this.btnAdd);
            this.gbSoftStops.Location = new Point(0, 135);
            this.gbSoftStops.Name = "gbSoftStops";
            this.gbSoftStops.Size = new Size(253, 107);
            this.gbSoftStops.TabIndex = 9;
            this.gbSoftStops.TabStop = false;
            this.gbSoftStops.Text = "Soft stops";
            // 
            // gbKnobSettings
            // 
            this.gbKnobSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.gbKnobSettings.Controls.Add(this.cbCentered);
            this.gbKnobSettings.Controls.Add(this.lblOverdrive);
            this.gbKnobSettings.Controls.Add(this.lblMaxValue);
            this.gbKnobSettings.Controls.Add(this.lblMinValue);
            this.gbKnobSettings.Controls.Add(this.nudOverdrive);
            this.gbKnobSettings.Controls.Add(this.nudMaxValue);
            this.gbKnobSettings.Controls.Add(this.nudMinValue);
            this.gbKnobSettings.Location = new Point(0, 248);
            this.gbKnobSettings.Name = "gbKnobSettings";
            this.gbKnobSettings.Size = new Size(253, 138);
            this.gbKnobSettings.TabIndex = 10;
            this.gbKnobSettings.TabStop = false;
            this.gbKnobSettings.Text = "Knob settings";
            // 
            // cbCentered
            // 
            this.cbCentered.AutoSize = true;
            this.cbCentered.Location = new Point(77, 109);
            this.cbCentered.Name = "cbCentered";
            this.cbCentered.Size = new Size(74, 19);
            this.cbCentered.TabIndex = 6;
            this.cbCentered.Text = "Centered";
            this.cbCentered.UseVisualStyleBackColor = true;
            // 
            // lblOverdrive
            // 
            this.lblOverdrive.AutoSize = true;
            this.lblOverdrive.Location = new Point(6, 82);
            this.lblOverdrive.Name = "lblOverdrive";
            this.lblOverdrive.Size = new Size(61, 15);
            this.lblOverdrive.TabIndex = 5;
            this.lblOverdrive.Text = "Overdrive:";
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new Point(6, 53);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new Size(65, 15);
            this.lblMaxValue.TabIndex = 4;
            this.lblMaxValue.Text = "Maximum:";
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Location = new Point(6, 24);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new Size(63, 15);
            this.lblMinValue.TabIndex = 3;
            this.lblMinValue.Text = "Minimum:";
            // 
            // nudOverdrive
            // 
            this.nudOverdrive.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.nudOverdrive.Location = new Point(77, 80);
            this.nudOverdrive.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudOverdrive.Name = "nudOverdrive";
            this.nudOverdrive.Size = new Size(170, 23);
            this.nudOverdrive.TabIndex = 2;
            // 
            // nudMaxValue
            // 
            this.nudMaxValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.nudMaxValue.Location = new Point(77, 51);
            this.nudMaxValue.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudMaxValue.Name = "nudMaxValue";
            this.nudMaxValue.Size = new Size(170, 23);
            this.nudMaxValue.TabIndex = 1;
            // 
            // nudMinValue
            // 
            this.nudMinValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.nudMinValue.Location = new Point(77, 22);
            this.nudMinValue.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudMinValue.Name = "nudMinValue";
            this.nudMinValue.Size = new Size(170, 23);
            this.nudMinValue.TabIndex = 0;
            // 
            // ControlDefiner
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.gbKnobSettings);
            this.Controls.Add(this.gbSoftStops);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.nudNote);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.gbType);
            this.Name = "ControlDefiner";
            this.Size = new Size(253, 418);
            this.gbType.ResumeLayout(false);
            this.gbType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudNote).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudSoftStop).EndInit();
            this.gbSoftStops.ResumeLayout(false);
            this.gbKnobSettings.ResumeLayout(false);
            this.gbKnobSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudOverdrive).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMaxValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMinValue).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private GroupBox gbType;
        private RadioButton rbTypeKnob;
        private RadioButton rbTypeButton;
        private Label lblName;
        private TextBox txtName;
        private NumericUpDown nudNote;
        private Label lblNote;
        private RadioButton rbTypeBlank;
        private ListBox lbSoftStops;
        private Button btnAdd;
        private Button btnRemove;
        private NumericUpDown nudSoftStop;
        private GroupBox gbSoftStops;
        private GroupBox gbKnobSettings;
        private Label lblOverdrive;
        private Label lblMaxValue;
        private Label lblMinValue;
        private NumericUpDown nudOverdrive;
        private NumericUpDown nudMaxValue;
        private NumericUpDown nudMinValue;
        private CheckBox cbCentered;
    }
}
