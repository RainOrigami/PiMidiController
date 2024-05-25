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
            this.rbTypeMacro = new RadioButton();
            this.rbTypeBlank = new RadioButton();
            this.rbTypeSound = new RadioButton();
            this.rbTypeKnob = new RadioButton();
            this.rbTypeButton = new RadioButton();
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.nudNote = new NumericUpDown();
            this.lblNote = new Label();
            this.lbSoftStops = new ListBox();
            this.btnAddSoftStop = new Button();
            this.btnRemoveSoftStop = new Button();
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
            this.gbSoundSettings = new GroupBox();
            this.cbToggleSound = new CheckBox();
            this.btnBrowseSound = new Button();
            this.txtSoundPath = new TextBox();
            this.lblSoundPath = new Label();
            this.gbNoteSettings = new GroupBox();
            this.gbMacroSettings = new GroupBox();
            this.btnDownMacro = new Button();
            this.btnUpMacro = new Button();
            this.btnAddMacro = new Button();
            this.btnRemoveMacro = new Button();
            this.lbMacros = new ListBox();
            this.gbType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudNote).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudSoftStop).BeginInit();
            this.gbSoftStops.SuspendLayout();
            this.gbKnobSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.nudOverdrive).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMaxValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudMinValue).BeginInit();
            this.gbSoundSettings.SuspendLayout();
            this.gbNoteSettings.SuspendLayout();
            this.gbMacroSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.rbTypeMacro);
            this.gbType.Controls.Add(this.rbTypeBlank);
            this.gbType.Controls.Add(this.rbTypeSound);
            this.gbType.Controls.Add(this.rbTypeKnob);
            this.gbType.Controls.Add(this.rbTypeButton);
            this.gbType.Location = new Point(0, 32);
            this.gbType.Name = "gbType";
            this.gbType.Size = new Size(253, 152);
            this.gbType.TabIndex = 0;
            this.gbType.TabStop = false;
            this.gbType.Text = "Control type";
            // 
            // rbTypeMacro
            // 
            this.rbTypeMacro.AutoSize = true;
            this.rbTypeMacro.Location = new Point(6, 122);
            this.rbTypeMacro.Name = "rbTypeMacro";
            this.rbTypeMacro.Size = new Size(59, 19);
            this.rbTypeMacro.TabIndex = 3;
            this.rbTypeMacro.TabStop = true;
            this.rbTypeMacro.Text = "Macro";
            this.rbTypeMacro.UseVisualStyleBackColor = true;
            this.rbTypeMacro.CheckedChanged += this.rbType_CheckedChanged;
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
            this.rbTypeBlank.CheckedChanged += this.rbType_CheckedChanged;
            // 
            // rbTypeSound
            // 
            this.rbTypeSound.AutoSize = true;
            this.rbTypeSound.Location = new Point(6, 97);
            this.rbTypeSound.Name = "rbTypeSound";
            this.rbTypeSound.Size = new Size(59, 19);
            this.rbTypeSound.TabIndex = 1;
            this.rbTypeSound.TabStop = true;
            this.rbTypeSound.Text = "Sound";
            this.rbTypeSound.UseVisualStyleBackColor = true;
            this.rbTypeSound.CheckedChanged += this.rbType_CheckedChanged;
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
            this.rbTypeKnob.CheckedChanged += this.rbType_CheckedChanged;
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
            this.rbTypeButton.CheckedChanged += this.rbType_CheckedChanged;
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
            this.txtName.Location = new Point(51, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(202, 23);
            this.txtName.TabIndex = 2;
            // 
            // nudNote
            // 
            this.nudNote.Location = new Point(45, 22);
            this.nudNote.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudNote.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            this.nudNote.Name = "nudNote";
            this.nudNote.Size = new Size(205, 23);
            this.nudNote.TabIndex = 3;
            this.nudNote.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new Point(3, 24);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new Size(36, 15);
            this.lblNote.TabIndex = 4;
            this.lblNote.Text = "Note:";
            // 
            // lbSoftStops
            // 
            this.lbSoftStops.FormattingEnabled = true;
            this.lbSoftStops.ItemHeight = 15;
            this.lbSoftStops.Location = new Point(3, 22);
            this.lbSoftStops.Name = "lbSoftStops";
            this.lbSoftStops.Size = new Size(215, 49);
            this.lbSoftStops.TabIndex = 5;
            // 
            // btnAddSoftStop
            // 
            this.btnAddSoftStop.Location = new Point(224, 77);
            this.btnAddSoftStop.Name = "btnAddSoftStop";
            this.btnAddSoftStop.Size = new Size(26, 23);
            this.btnAddSoftStop.TabIndex = 6;
            this.btnAddSoftStop.Text = "+";
            this.btnAddSoftStop.UseVisualStyleBackColor = true;
            this.btnAddSoftStop.Click += this.btnAdd_Click;
            // 
            // btnRemoveSoftStop
            // 
            this.btnRemoveSoftStop.Location = new Point(224, 22);
            this.btnRemoveSoftStop.Name = "btnRemoveSoftStop";
            this.btnRemoveSoftStop.Size = new Size(26, 23);
            this.btnRemoveSoftStop.TabIndex = 7;
            this.btnRemoveSoftStop.Text = "-";
            this.btnRemoveSoftStop.UseVisualStyleBackColor = true;
            this.btnRemoveSoftStop.Click += this.btnRemove_Click;
            // 
            // nudSoftStop
            // 
            this.nudSoftStop.Location = new Point(3, 77);
            this.nudSoftStop.Name = "nudSoftStop";
            this.nudSoftStop.Size = new Size(215, 23);
            this.nudSoftStop.TabIndex = 8;
            // 
            // gbSoftStops
            // 
            this.gbSoftStops.Controls.Add(this.lbSoftStops);
            this.gbSoftStops.Controls.Add(this.nudSoftStop);
            this.gbSoftStops.Controls.Add(this.btnRemoveSoftStop);
            this.gbSoftStops.Controls.Add(this.btnAddSoftStop);
            this.gbSoftStops.Enabled = false;
            this.gbSoftStops.Location = new Point(0, 190);
            this.gbSoftStops.Name = "gbSoftStops";
            this.gbSoftStops.Size = new Size(253, 107);
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
            this.gbKnobSettings.Enabled = false;
            this.gbKnobSettings.Location = new Point(0, 303);
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
            this.nudOverdrive.Location = new Point(77, 80);
            this.nudOverdrive.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudOverdrive.Name = "nudOverdrive";
            this.nudOverdrive.Size = new Size(170, 23);
            this.nudOverdrive.TabIndex = 2;
            // 
            // nudMaxValue
            // 
            this.nudMaxValue.Location = new Point(77, 51);
            this.nudMaxValue.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudMaxValue.Name = "nudMaxValue";
            this.nudMaxValue.Size = new Size(170, 23);
            this.nudMaxValue.TabIndex = 1;
            // 
            // nudMinValue
            // 
            this.nudMinValue.Location = new Point(77, 22);
            this.nudMinValue.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            this.nudMinValue.Name = "nudMinValue";
            this.nudMinValue.Size = new Size(170, 23);
            this.nudMinValue.TabIndex = 0;
            // 
            // gbSoundSettings
            // 
            this.gbSoundSettings.Controls.Add(this.cbToggleSound);
            this.gbSoundSettings.Controls.Add(this.btnBrowseSound);
            this.gbSoundSettings.Controls.Add(this.txtSoundPath);
            this.gbSoundSettings.Controls.Add(this.lblSoundPath);
            this.gbSoundSettings.Enabled = false;
            this.gbSoundSettings.Location = new Point(0, 447);
            this.gbSoundSettings.Name = "gbSoundSettings";
            this.gbSoundSettings.Size = new Size(256, 73);
            this.gbSoundSettings.TabIndex = 11;
            this.gbSoundSettings.TabStop = false;
            this.gbSoundSettings.Text = "Sound settings";
            // 
            // cbToggleSound
            // 
            this.cbToggleSound.AutoSize = true;
            this.cbToggleSound.Location = new Point(40, 46);
            this.cbToggleSound.Name = "cbToggleSound";
            this.cbToggleSound.Size = new Size(61, 19);
            this.cbToggleSound.TabIndex = 3;
            this.cbToggleSound.Text = "Toggle";
            this.cbToggleSound.UseVisualStyleBackColor = true;
            // 
            // btnBrowseSound
            // 
            this.btnBrowseSound.Location = new Point(225, 16);
            this.btnBrowseSound.Name = "btnBrowseSound";
            this.btnBrowseSound.Size = new Size(28, 23);
            this.btnBrowseSound.TabIndex = 2;
            this.btnBrowseSound.Text = "...";
            this.btnBrowseSound.UseVisualStyleBackColor = true;
            this.btnBrowseSound.Click += this.btnBrowseSound_Click;
            // 
            // txtSoundPath
            // 
            this.txtSoundPath.Location = new Point(40, 17);
            this.txtSoundPath.Name = "txtSoundPath";
            this.txtSoundPath.Size = new Size(181, 23);
            this.txtSoundPath.TabIndex = 1;
            // 
            // lblSoundPath
            // 
            this.lblSoundPath.AutoSize = true;
            this.lblSoundPath.Location = new Point(3, 20);
            this.lblSoundPath.Name = "lblSoundPath";
            this.lblSoundPath.Size = new Size(34, 15);
            this.lblSoundPath.TabIndex = 0;
            this.lblSoundPath.Text = "Path:";
            // 
            // gbNoteSettings
            // 
            this.gbNoteSettings.Controls.Add(this.nudNote);
            this.gbNoteSettings.Controls.Add(this.lblNote);
            this.gbNoteSettings.Enabled = false;
            this.gbNoteSettings.Location = new Point(0, 787);
            this.gbNoteSettings.Name = "gbNoteSettings";
            this.gbNoteSettings.Size = new Size(256, 53);
            this.gbNoteSettings.TabIndex = 12;
            this.gbNoteSettings.TabStop = false;
            this.gbNoteSettings.Text = "Note";
            // 
            // gbMacroSettings
            // 
            this.gbMacroSettings.Controls.Add(this.btnDownMacro);
            this.gbMacroSettings.Controls.Add(this.btnUpMacro);
            this.gbMacroSettings.Controls.Add(this.btnAddMacro);
            this.gbMacroSettings.Controls.Add(this.btnRemoveMacro);
            this.gbMacroSettings.Controls.Add(this.lbMacros);
            this.gbMacroSettings.Enabled = false;
            this.gbMacroSettings.Location = new Point(0, 526);
            this.gbMacroSettings.Name = "gbMacroSettings";
            this.gbMacroSettings.Size = new Size(256, 255);
            this.gbMacroSettings.TabIndex = 13;
            this.gbMacroSettings.TabStop = false;
            this.gbMacroSettings.Text = "Macro";
            // 
            // btnDownMacro
            // 
            this.btnDownMacro.Location = new Point(224, 109);
            this.btnDownMacro.Name = "btnDownMacro";
            this.btnDownMacro.Size = new Size(26, 23);
            this.btnDownMacro.TabIndex = 4;
            this.btnDownMacro.Text = "▼";
            this.btnDownMacro.UseVisualStyleBackColor = true;
            this.btnDownMacro.Click += this.btnDownMacro_Click;
            // 
            // btnUpMacro
            // 
            this.btnUpMacro.Location = new Point(224, 80);
            this.btnUpMacro.Name = "btnUpMacro";
            this.btnUpMacro.Size = new Size(26, 23);
            this.btnUpMacro.TabIndex = 3;
            this.btnUpMacro.Text = "▲";
            this.btnUpMacro.UseVisualStyleBackColor = true;
            this.btnUpMacro.Click += this.btnUpMacro_Click;
            // 
            // btnAddMacro
            // 
            this.btnAddMacro.Location = new Point(224, 22);
            this.btnAddMacro.Name = "btnAddMacro";
            this.btnAddMacro.Size = new Size(26, 23);
            this.btnAddMacro.TabIndex = 2;
            this.btnAddMacro.Text = "+";
            this.btnAddMacro.UseVisualStyleBackColor = true;
            this.btnAddMacro.Click += this.btnAddMacro_Click;
            // 
            // btnRemoveMacro
            // 
            this.btnRemoveMacro.Location = new Point(224, 51);
            this.btnRemoveMacro.Name = "btnRemoveMacro";
            this.btnRemoveMacro.Size = new Size(26, 23);
            this.btnRemoveMacro.TabIndex = 1;
            this.btnRemoveMacro.Text = "-";
            this.btnRemoveMacro.UseVisualStyleBackColor = true;
            this.btnRemoveMacro.Click += this.btnRemoveMacro_Click;
            // 
            // lbMacros
            // 
            this.lbMacros.FormattingEnabled = true;
            this.lbMacros.ItemHeight = 15;
            this.lbMacros.Location = new Point(3, 22);
            this.lbMacros.Name = "lbMacros";
            this.lbMacros.Size = new Size(218, 229);
            this.lbMacros.TabIndex = 0;
            this.lbMacros.MouseDoubleClick += this.lbMacros_MouseDoubleClick;
            // 
            // ControlDefiner
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.gbMacroSettings);
            this.Controls.Add(this.gbKnobSettings);
            this.Controls.Add(this.gbSoftStops);
            this.Controls.Add(this.gbNoteSettings);
            this.Controls.Add(this.gbSoundSettings);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.gbType);
            this.Name = "ControlDefiner";
            this.Size = new Size(259, 842);
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
            this.gbSoundSettings.ResumeLayout(false);
            this.gbSoundSettings.PerformLayout();
            this.gbNoteSettings.ResumeLayout(false);
            this.gbNoteSettings.PerformLayout();
            this.gbMacroSettings.ResumeLayout(false);
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
        private Button btnAddSoftStop;
        private Button btnRemoveSoftStop;
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
        private RadioButton rbTypeSound;
        private GroupBox gbSoundSettings;
        private Button btnBrowseSound;
        private TextBox txtSoundPath;
        private Label lblSoundPath;
        private GroupBox gbNoteSettings;
        private GroupBox gbMacroSettings;
        private RadioButton rbTypeMacro;
        private ListBox lbMacros;
        private Button btnDownMacro;
        private Button btnUpMacro;
        private Button btnAddMacro;
        private Button btnRemoveMacro;
        private CheckBox cbToggleSound;
    }
}
