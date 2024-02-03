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
            this.gbType.Controls.Add(this.rbTypeBlank);
            this.gbType.Controls.Add(this.rbTypeKnob);
            this.gbType.Controls.Add(this.rbTypeButton);
            this.gbType.Location = new Point(0, 53);
            this.gbType.Margin = new Padding(4, 5, 4, 5);
            this.gbType.Name = "gbType";
            this.gbType.Padding = new Padding(4, 5, 4, 5);
            this.gbType.Size = new Size(361, 162);
            this.gbType.TabIndex = 0;
            this.gbType.TabStop = false;
            this.gbType.Text = "Control type";
            // 
            // rbTypeBlank
            // 
            this.rbTypeBlank.AutoSize = true;
            this.rbTypeBlank.Checked = true;
            this.rbTypeBlank.Location = new Point(9, 37);
            this.rbTypeBlank.Margin = new Padding(4, 5, 4, 5);
            this.rbTypeBlank.Name = "rbTypeBlank";
            this.rbTypeBlank.Size = new Size(79, 29);
            this.rbTypeBlank.TabIndex = 2;
            this.rbTypeBlank.TabStop = true;
            this.rbTypeBlank.Text = "Blank";
            this.rbTypeBlank.UseVisualStyleBackColor = true;
            // 
            // rbTypeKnob
            // 
            this.rbTypeKnob.AutoSize = true;
            this.rbTypeKnob.Location = new Point(9, 120);
            this.rbTypeKnob.Margin = new Padding(4, 5, 4, 5);
            this.rbTypeKnob.Name = "rbTypeKnob";
            this.rbTypeKnob.Size = new Size(79, 29);
            this.rbTypeKnob.TabIndex = 1;
            this.rbTypeKnob.TabStop = true;
            this.rbTypeKnob.Text = "Knob";
            this.rbTypeKnob.UseVisualStyleBackColor = true;
            this.rbTypeKnob.CheckedChanged += this.rbTypeKnob_CheckedChanged;
            // 
            // rbTypeButton
            // 
            this.rbTypeButton.AutoSize = true;
            this.rbTypeButton.Location = new Point(9, 78);
            this.rbTypeButton.Margin = new Padding(4, 5, 4, 5);
            this.rbTypeButton.Name = "rbTypeButton";
            this.rbTypeButton.Size = new Size(90, 29);
            this.rbTypeButton.TabIndex = 0;
            this.rbTypeButton.TabStop = true;
            this.rbTypeButton.Text = "Button";
            this.rbTypeButton.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(4, 10);
            this.lblName.Margin = new Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(63, 25);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new Point(73, 5);
            this.txtName.Margin = new Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(283, 31);
            this.txtName.TabIndex = 2;
            // 
            // nudNote
            // 
            this.nudNote.Location = new Point(64, 653);
            this.nudNote.Margin = new Padding(4, 5, 4, 5);
            this.nudNote.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudNote.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            this.nudNote.Name = "nudNote";
            this.nudNote.Size = new Size(293, 31);
            this.nudNote.TabIndex = 3;
            this.nudNote.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new Point(4, 657);
            this.lblNote.Margin = new Padding(4, 0, 4, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(55, 25);
            this.lblNote.TabIndex = 4;
            this.lblNote.Text = "Note:";
            // 
            // lbSoftStops
            // 
            this.lbSoftStops.FormattingEnabled = true;
            this.lbSoftStops.ItemHeight = 25;
            this.lbSoftStops.Location = new Point(4, 37);
            this.lbSoftStops.Margin = new Padding(4, 5, 4, 5);
            this.lbSoftStops.Name = "lbSoftStops";
            this.lbSoftStops.Size = new Size(305, 79);
            this.lbSoftStops.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new Point(320, 128);
            this.btnAdd.Margin = new Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(37, 38);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += this.btnAdd_Click;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new Point(320, 37);
            this.btnRemove.Margin = new Padding(4, 5, 4, 5);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new Size(37, 38);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += this.btnRemove_Click;
            // 
            // nudSoftStop
            // 
            this.nudSoftStop.Location = new Point(4, 128);
            this.nudSoftStop.Margin = new Padding(4, 5, 4, 5);
            this.nudSoftStop.Name = "nudSoftStop";
            this.nudSoftStop.Size = new Size(307, 31);
            this.nudSoftStop.TabIndex = 8;
            // 
            // gbSoftStops
            // 
            this.gbSoftStops.Controls.Add(this.lbSoftStops);
            this.gbSoftStops.Controls.Add(this.nudSoftStop);
            this.gbSoftStops.Controls.Add(this.btnRemove);
            this.gbSoftStops.Controls.Add(this.btnAdd);
            this.gbSoftStops.Location = new Point(0, 225);
            this.gbSoftStops.Margin = new Padding(4, 5, 4, 5);
            this.gbSoftStops.Name = "gbSoftStops";
            this.gbSoftStops.Padding = new Padding(4, 5, 4, 5);
            this.gbSoftStops.Size = new Size(361, 178);
            this.gbSoftStops.TabIndex = 9;
            this.gbSoftStops.TabStop = false;
            this.gbSoftStops.Text = "Soft stops";
            // 
            // gbKnobSettings
            // 
            this.gbKnobSettings.Controls.Add(this.cbCentered);
            this.gbKnobSettings.Controls.Add(this.lblOverdrive);
            this.gbKnobSettings.Controls.Add(this.lblMaxValue);
            this.gbKnobSettings.Controls.Add(this.lblMinValue);
            this.gbKnobSettings.Controls.Add(this.nudOverdrive);
            this.gbKnobSettings.Controls.Add(this.nudMaxValue);
            this.gbKnobSettings.Controls.Add(this.nudMinValue);
            this.gbKnobSettings.Location = new Point(0, 413);
            this.gbKnobSettings.Margin = new Padding(4, 5, 4, 5);
            this.gbKnobSettings.Name = "gbKnobSettings";
            this.gbKnobSettings.Padding = new Padding(4, 5, 4, 5);
            this.gbKnobSettings.Size = new Size(361, 230);
            this.gbKnobSettings.TabIndex = 10;
            this.gbKnobSettings.TabStop = false;
            this.gbKnobSettings.Text = "Knob settings";
            // 
            // cbCentered
            // 
            this.cbCentered.AutoSize = true;
            this.cbCentered.Location = new Point(110, 182);
            this.cbCentered.Margin = new Padding(4, 5, 4, 5);
            this.cbCentered.Name = "cbCentered";
            this.cbCentered.Size = new Size(109, 29);
            this.cbCentered.TabIndex = 6;
            this.cbCentered.Text = "Centered";
            this.cbCentered.UseVisualStyleBackColor = true;
            // 
            // lblOverdrive
            // 
            this.lblOverdrive.AutoSize = true;
            this.lblOverdrive.Location = new Point(9, 137);
            this.lblOverdrive.Margin = new Padding(4, 0, 4, 0);
            this.lblOverdrive.Name = "lblOverdrive";
            this.lblOverdrive.Size = new Size(93, 25);
            this.lblOverdrive.TabIndex = 5;
            this.lblOverdrive.Text = "Overdrive:";
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new Point(9, 88);
            this.lblMaxValue.Margin = new Padding(4, 0, 4, 0);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new Size(95, 25);
            this.lblMaxValue.TabIndex = 4;
            this.lblMaxValue.Text = "Maximum:";
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Location = new Point(9, 40);
            this.lblMinValue.Margin = new Padding(4, 0, 4, 0);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new Size(92, 25);
            this.lblMinValue.TabIndex = 3;
            this.lblMinValue.Text = "Minimum:";
            // 
            // nudOverdrive
            // 
            this.nudOverdrive.Location = new Point(110, 133);
            this.nudOverdrive.Margin = new Padding(4, 5, 4, 5);
            this.nudOverdrive.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudOverdrive.Name = "nudOverdrive";
            this.nudOverdrive.Size = new Size(243, 31);
            this.nudOverdrive.TabIndex = 2;
            // 
            // nudMaxValue
            // 
            this.nudMaxValue.Location = new Point(110, 85);
            this.nudMaxValue.Margin = new Padding(4, 5, 4, 5);
            this.nudMaxValue.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudMaxValue.Name = "nudMaxValue";
            this.nudMaxValue.Size = new Size(243, 31);
            this.nudMaxValue.TabIndex = 1;
            // 
            // nudMinValue
            // 
            this.nudMinValue.Location = new Point(110, 37);
            this.nudMinValue.Margin = new Padding(4, 5, 4, 5);
            this.nudMinValue.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudMinValue.Name = "nudMinValue";
            this.nudMinValue.Size = new Size(243, 31);
            this.nudMinValue.TabIndex = 0;
            // 
            // ControlDefiner
            // 
            this.AutoScaleDimensions = new SizeF(10F, 25F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.gbKnobSettings);
            this.Controls.Add(this.gbSoftStops);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.nudNote);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.gbType);
            this.Margin = new Padding(4, 5, 4, 5);
            this.Name = "ControlDefiner";
            this.Size = new Size(367, 694);
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
