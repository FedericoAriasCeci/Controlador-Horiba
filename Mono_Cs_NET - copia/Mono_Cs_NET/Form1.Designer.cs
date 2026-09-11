namespace MonoSimple
{
    partial class Form1
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
            this.labelInitStatus = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupboxWlCtrl = new System.Windows.Forms.GroupBox();
            this.comboboxGrating = new System.Windows.Forms.ComboBox();
            this.labelGrating = new System.Windows.Forms.Label();
            this.textboxPosition = new System.Windows.Forms.TextBox();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.buttonInitialize = new System.Windows.Forms.Button();
            this.labelMonosDevs = new System.Windows.Forms.Label();
            this.comboboxMonos = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.Frame_Slits = new System.Windows.Forms.GroupBox();
            this.Frame_Slit_Exit = new System.Windows.Forms.GroupBox();
            this.Label_Slit_Exit_Units = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Text_Slit_Side_Exit = new System.Windows.Forms.TextBox();
            this.Label_Slit_Front_Exit = new System.Windows.Forms.Label();
            this.Text_Slit_Front_Exit = new System.Windows.Forms.TextBox();
            this.Frame_Slit_Entrance = new System.Windows.Forms.GroupBox();
            this.Label_Slit_Entrance_Units = new System.Windows.Forms.Label();
            this.Label_Slit_Side_Entrance = new System.Windows.Forms.Label();
            this.Text_Slit_Side_Entrance = new System.Windows.Forms.TextBox();
            this.Label_Slit_Front_Entrance = new System.Windows.Forms.Label();
            this.Text_Slit_Front_Entrance = new System.Windows.Forms.TextBox();
            this.Frame_Mirrors = new System.Windows.Forms.GroupBox();
            this.Frame_Mirror_Exit = new System.Windows.Forms.GroupBox();
            this.Radio_Exit_Lateral = new System.Windows.Forms.RadioButton();
            this.Radio_Exit_Axial = new System.Windows.Forms.RadioButton();
            this.Frame_Mirror_Entrance = new System.Windows.Forms.GroupBox();
            this.Radio_Entrance_Lateral = new System.Windows.Forms.RadioButton();
            this.Radio_Entrance_Axial = new System.Windows.Forms.RadioButton();
            this.groupboxWlCtrl.SuspendLayout();
            this.Frame_Slits.SuspendLayout();
            this.Frame_Slit_Exit.SuspendLayout();
            this.Frame_Slit_Entrance.SuspendLayout();
            this.Frame_Mirrors.SuspendLayout();
            this.Frame_Mirror_Exit.SuspendLayout();
            this.Frame_Mirror_Entrance.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInitStatus
            // 
            this.labelInitStatus.Location = new System.Drawing.Point(245, 14);
            this.labelInitStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInitStatus.Name = "labelInitStatus";
            this.labelInitStatus.Size = new System.Drawing.Size(87, 28);
            this.labelInitStatus.TabIndex = 27;
            this.labelInitStatus.Text = "Uninitialized";
            this.labelInitStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(37, 28);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(91, 17);
            this.labelPosition.TabIndex = 0;
            this.labelPosition.Text = "Position (nm)";
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(193, 14);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(53, 28);
            this.labelStatus.TabIndex = 26;
            this.labelStatus.Text = "Status:";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupboxWlCtrl
            // 
            this.groupboxWlCtrl.Controls.Add(this.comboboxGrating);
            this.groupboxWlCtrl.Controls.Add(this.labelGrating);
            this.groupboxWlCtrl.Controls.Add(this.textboxPosition);
            this.groupboxWlCtrl.Controls.Add(this.labelPosition);
            this.groupboxWlCtrl.Enabled = false;
            this.groupboxWlCtrl.Location = new System.Drawing.Point(11, 90);
            this.groupboxWlCtrl.Margin = new System.Windows.Forms.Padding(4);
            this.groupboxWlCtrl.Name = "groupboxWlCtrl";
            this.groupboxWlCtrl.Padding = new System.Windows.Forms.Padding(4);
            this.groupboxWlCtrl.Size = new System.Drawing.Size(321, 96);
            this.groupboxWlCtrl.TabIndex = 25;
            this.groupboxWlCtrl.TabStop = false;
            this.groupboxWlCtrl.Text = "Wavelength Control";
            // 
            // comboboxGrating
            // 
            this.comboboxGrating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxGrating.FormattingEnabled = true;
            this.comboboxGrating.Location = new System.Drawing.Point(190, 49);
            this.comboboxGrating.Margin = new System.Windows.Forms.Padding(4);
            this.comboboxGrating.Name = "comboboxGrating";
            this.comboboxGrating.Size = new System.Drawing.Size(96, 24);
            this.comboboxGrating.TabIndex = 3;
            this.comboboxGrating.SelectedIndexChanged += new System.EventHandler(this.comboboxGrating_SelectedIndexChanged);
            // 
            // labelGrating
            // 
            this.labelGrating.AutoSize = true;
            this.labelGrating.Location = new System.Drawing.Point(190, 28);
            this.labelGrating.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGrating.Name = "labelGrating";
            this.labelGrating.Size = new System.Drawing.Size(55, 17);
            this.labelGrating.TabIndex = 2;
            this.labelGrating.Text = "Grating";
            // 
            // textboxPosition
            // 
            this.textboxPosition.Location = new System.Drawing.Point(37, 52);
            this.textboxPosition.Margin = new System.Windows.Forms.Padding(4);
            this.textboxPosition.Name = "textboxPosition";
            this.textboxPosition.Size = new System.Drawing.Size(99, 22);
            this.textboxPosition.TabIndex = 1;
            this.textboxPosition.TextChanged += new System.EventHandler(this.textboxPosition_TextChanged);
            this.textboxPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textboxPosition_KeyDown);
            this.textboxPosition.Leave += new System.EventHandler(this.textboxPosition_Leave);
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(356, 32);
            this.tbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbStatus.Size = new System.Drawing.Size(368, 154);
            this.tbStatus.TabIndex = 24;
            // 
            // buttonInitialize
            // 
            this.buttonInitialize.Location = new System.Drawing.Point(204, 43);
            this.buttonInitialize.Margin = new System.Windows.Forms.Padding(4);
            this.buttonInitialize.Name = "buttonInitialize";
            this.buttonInitialize.Size = new System.Drawing.Size(100, 28);
            this.buttonInitialize.TabIndex = 23;
            this.buttonInitialize.Text = "Initialize";
            this.buttonInitialize.UseVisualStyleBackColor = true;
            this.buttonInitialize.Click += new System.EventHandler(this.buttonInitialize_Click);
            // 
            // labelMonosDevs
            // 
            this.labelMonosDevs.Location = new System.Drawing.Point(20, 14);
            this.labelMonosDevs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMonosDevs.Name = "labelMonosDevs";
            this.labelMonosDevs.Size = new System.Drawing.Size(133, 28);
            this.labelMonosDevs.TabIndex = 22;
            this.labelMonosDevs.Text = "Select Mono";
            this.labelMonosDevs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboboxMonos
            // 
            this.comboboxMonos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxMonos.FormattingEnabled = true;
            this.comboboxMonos.Location = new System.Drawing.Point(11, 46);
            this.comboboxMonos.Margin = new System.Windows.Forms.Padding(4);
            this.comboboxMonos.Name = "comboboxMonos";
            this.comboboxMonos.Size = new System.Drawing.Size(160, 24);
            this.comboboxMonos.TabIndex = 21;
            this.comboboxMonos.SelectedIndexChanged += new System.EventHandler(this.comboboxMonos_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(324, 489);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 28);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // Frame_Slits
            // 
            this.Frame_Slits.Controls.Add(this.Frame_Slit_Exit);
            this.Frame_Slits.Controls.Add(this.Frame_Slit_Entrance);
            this.Frame_Slits.Location = new System.Drawing.Point(14, 209);
            this.Frame_Slits.Name = "Frame_Slits";
            this.Frame_Slits.Size = new System.Drawing.Size(364, 262);
            this.Frame_Slits.TabIndex = 28;
            this.Frame_Slits.TabStop = false;
            this.Frame_Slits.Text = "Slits";
            // 
            // Frame_Slit_Exit
            // 
            this.Frame_Slit_Exit.Controls.Add(this.Label_Slit_Exit_Units);
            this.Frame_Slit_Exit.Controls.Add(this.label2);
            this.Frame_Slit_Exit.Controls.Add(this.Text_Slit_Side_Exit);
            this.Frame_Slit_Exit.Controls.Add(this.Label_Slit_Front_Exit);
            this.Frame_Slit_Exit.Controls.Add(this.Text_Slit_Front_Exit);
            this.Frame_Slit_Exit.Location = new System.Drawing.Point(20, 146);
            this.Frame_Slit_Exit.Name = "Frame_Slit_Exit";
            this.Frame_Slit_Exit.Size = new System.Drawing.Size(325, 99);
            this.Frame_Slit_Exit.TabIndex = 1;
            this.Frame_Slit_Exit.TabStop = false;
            this.Frame_Slit_Exit.Text = "Exit";
            // 
            // Label_Slit_Exit_Units
            // 
            this.Label_Slit_Exit_Units.AutoSize = true;
            this.Label_Slit_Exit_Units.Location = new System.Drawing.Point(221, 54);
            this.Label_Slit_Exit_Units.Name = "Label_Slit_Exit_Units";
            this.Label_Slit_Exit_Units.Size = new System.Drawing.Size(29, 17);
            this.Label_Slit_Exit_Units.TabIndex = 4;
            this.Label_Slit_Exit_Units.Text = "arb";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Side";
            // 
            // Text_Slit_Side_Exit
            // 
            this.Text_Slit_Side_Exit.Location = new System.Drawing.Point(125, 52);
            this.Text_Slit_Side_Exit.Name = "Text_Slit_Side_Exit";
            this.Text_Slit_Side_Exit.Size = new System.Drawing.Size(90, 22);
            this.Text_Slit_Side_Exit.TabIndex = 2;
            this.Text_Slit_Side_Exit.TextChanged += new System.EventHandler(this.Text_Slit_Side_Exit_TextChanged);
            this.Text_Slit_Side_Exit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_Slit_Side_Exit_KeyDown);
            this.Text_Slit_Side_Exit.Leave += new System.EventHandler(this.Text_Slit_Side_Exit_Leave);
            // 
            // Label_Slit_Front_Exit
            // 
            this.Label_Slit_Front_Exit.AutoSize = true;
            this.Label_Slit_Front_Exit.Location = new System.Drawing.Point(9, 33);
            this.Label_Slit_Front_Exit.Name = "Label_Slit_Front_Exit";
            this.Label_Slit_Front_Exit.Size = new System.Drawing.Size(41, 17);
            this.Label_Slit_Front_Exit.TabIndex = 1;
            this.Label_Slit_Front_Exit.Text = "Front";
            // 
            // Text_Slit_Front_Exit
            // 
            this.Text_Slit_Front_Exit.Location = new System.Drawing.Point(12, 53);
            this.Text_Slit_Front_Exit.Name = "Text_Slit_Front_Exit";
            this.Text_Slit_Front_Exit.Size = new System.Drawing.Size(88, 22);
            this.Text_Slit_Front_Exit.TabIndex = 0;
            this.Text_Slit_Front_Exit.TextChanged += new System.EventHandler(this.Text_Slit_Front_Exit_TextChanged);
            this.Text_Slit_Front_Exit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_Slit_Front_Exit_KeyDown);
            this.Text_Slit_Front_Exit.Leave += new System.EventHandler(this.Text_Slit_Front_Exit_Leave);
            // 
            // Frame_Slit_Entrance
            // 
            this.Frame_Slit_Entrance.Controls.Add(this.Label_Slit_Entrance_Units);
            this.Frame_Slit_Entrance.Controls.Add(this.Label_Slit_Side_Entrance);
            this.Frame_Slit_Entrance.Controls.Add(this.Text_Slit_Side_Entrance);
            this.Frame_Slit_Entrance.Controls.Add(this.Label_Slit_Front_Entrance);
            this.Frame_Slit_Entrance.Controls.Add(this.Text_Slit_Front_Entrance);
            this.Frame_Slit_Entrance.Location = new System.Drawing.Point(20, 34);
            this.Frame_Slit_Entrance.Name = "Frame_Slit_Entrance";
            this.Frame_Slit_Entrance.Size = new System.Drawing.Size(325, 99);
            this.Frame_Slit_Entrance.TabIndex = 0;
            this.Frame_Slit_Entrance.TabStop = false;
            this.Frame_Slit_Entrance.Text = "Entrance";
            // 
            // Label_Slit_Entrance_Units
            // 
            this.Label_Slit_Entrance_Units.AutoSize = true;
            this.Label_Slit_Entrance_Units.Location = new System.Drawing.Point(221, 54);
            this.Label_Slit_Entrance_Units.Name = "Label_Slit_Entrance_Units";
            this.Label_Slit_Entrance_Units.Size = new System.Drawing.Size(29, 17);
            this.Label_Slit_Entrance_Units.TabIndex = 4;
            this.Label_Slit_Entrance_Units.Text = "arb";
            // 
            // Label_Slit_Side_Entrance
            // 
            this.Label_Slit_Side_Entrance.AutoSize = true;
            this.Label_Slit_Side_Entrance.Location = new System.Drawing.Point(122, 32);
            this.Label_Slit_Side_Entrance.Name = "Label_Slit_Side_Entrance";
            this.Label_Slit_Side_Entrance.Size = new System.Drawing.Size(36, 17);
            this.Label_Slit_Side_Entrance.TabIndex = 3;
            this.Label_Slit_Side_Entrance.Text = "Side";
            // 
            // Text_Slit_Side_Entrance
            // 
            this.Text_Slit_Side_Entrance.Location = new System.Drawing.Point(125, 52);
            this.Text_Slit_Side_Entrance.Name = "Text_Slit_Side_Entrance";
            this.Text_Slit_Side_Entrance.Size = new System.Drawing.Size(90, 22);
            this.Text_Slit_Side_Entrance.TabIndex = 2;
            this.Text_Slit_Side_Entrance.TextChanged += new System.EventHandler(this.Text_Slit_Side_Entrance_TextChanged);
            this.Text_Slit_Side_Entrance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_Slit_Side_Entrance_KeyDown);
            this.Text_Slit_Side_Entrance.Leave += new System.EventHandler(this.Text_Slit_Side_Entrance_Leave);
            // 
            // Label_Slit_Front_Entrance
            // 
            this.Label_Slit_Front_Entrance.AutoSize = true;
            this.Label_Slit_Front_Entrance.Location = new System.Drawing.Point(9, 33);
            this.Label_Slit_Front_Entrance.Name = "Label_Slit_Front_Entrance";
            this.Label_Slit_Front_Entrance.Size = new System.Drawing.Size(41, 17);
            this.Label_Slit_Front_Entrance.TabIndex = 1;
            this.Label_Slit_Front_Entrance.Text = "Front";
            // 
            // Text_Slit_Front_Entrance
            // 
            this.Text_Slit_Front_Entrance.Location = new System.Drawing.Point(12, 53);
            this.Text_Slit_Front_Entrance.Name = "Text_Slit_Front_Entrance";
            this.Text_Slit_Front_Entrance.Size = new System.Drawing.Size(88, 22);
            this.Text_Slit_Front_Entrance.TabIndex = 0;
            this.Text_Slit_Front_Entrance.TextChanged += new System.EventHandler(this.Text_Slit_Front_Entrance_TextChanged);
            this.Text_Slit_Front_Entrance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_Slit_Front_Entrance_KeyDown);
            this.Text_Slit_Front_Entrance.Leave += new System.EventHandler(this.Text_Slit_Front_Entrance_Leave);
            // 
            // Frame_Mirrors
            // 
            this.Frame_Mirrors.Controls.Add(this.Frame_Mirror_Exit);
            this.Frame_Mirrors.Controls.Add(this.Frame_Mirror_Entrance);
            this.Frame_Mirrors.Location = new System.Drawing.Point(403, 209);
            this.Frame_Mirrors.Name = "Frame_Mirrors";
            this.Frame_Mirrors.Size = new System.Drawing.Size(321, 262);
            this.Frame_Mirrors.TabIndex = 29;
            this.Frame_Mirrors.TabStop = false;
            this.Frame_Mirrors.Text = "Mirrors";
            // 
            // Frame_Mirror_Exit
            // 
            this.Frame_Mirror_Exit.Controls.Add(this.Radio_Exit_Lateral);
            this.Frame_Mirror_Exit.Controls.Add(this.Radio_Exit_Axial);
            this.Frame_Mirror_Exit.Location = new System.Drawing.Point(18, 146);
            this.Frame_Mirror_Exit.Name = "Frame_Mirror_Exit";
            this.Frame_Mirror_Exit.Size = new System.Drawing.Size(279, 99);
            this.Frame_Mirror_Exit.TabIndex = 31;
            this.Frame_Mirror_Exit.TabStop = false;
            this.Frame_Mirror_Exit.Text = "Exit";
            // 
            // Radio_Exit_Lateral
            // 
            this.Radio_Exit_Lateral.AutoSize = true;
            this.Radio_Exit_Lateral.Location = new System.Drawing.Point(155, 50);
            this.Radio_Exit_Lateral.Name = "Radio_Exit_Lateral";
            this.Radio_Exit_Lateral.Size = new System.Drawing.Size(73, 21);
            this.Radio_Exit_Lateral.TabIndex = 1;
            this.Radio_Exit_Lateral.TabStop = true;
            this.Radio_Exit_Lateral.Text = "Lateral";
            this.Radio_Exit_Lateral.UseVisualStyleBackColor = true;
            this.Radio_Exit_Lateral.Click += new System.EventHandler(this.Radio_Exit_Lateral_Click);
            // 
            // Radio_Exit_Axial
            // 
            this.Radio_Exit_Axial.AutoSize = true;
            this.Radio_Exit_Axial.Location = new System.Drawing.Point(27, 50);
            this.Radio_Exit_Axial.Name = "Radio_Exit_Axial";
            this.Radio_Exit_Axial.Size = new System.Drawing.Size(58, 21);
            this.Radio_Exit_Axial.TabIndex = 0;
            this.Radio_Exit_Axial.TabStop = true;
            this.Radio_Exit_Axial.Text = "Axial";
            this.Radio_Exit_Axial.UseVisualStyleBackColor = true;
            this.Radio_Exit_Axial.Click += new System.EventHandler(this.Radio_Exit_Axial_Click);
            // 
            // Frame_Mirror_Entrance
            // 
            this.Frame_Mirror_Entrance.Controls.Add(this.Radio_Entrance_Lateral);
            this.Frame_Mirror_Entrance.Controls.Add(this.Radio_Entrance_Axial);
            this.Frame_Mirror_Entrance.Location = new System.Drawing.Point(18, 34);
            this.Frame_Mirror_Entrance.Name = "Frame_Mirror_Entrance";
            this.Frame_Mirror_Entrance.Size = new System.Drawing.Size(279, 99);
            this.Frame_Mirror_Entrance.TabIndex = 30;
            this.Frame_Mirror_Entrance.TabStop = false;
            this.Frame_Mirror_Entrance.Text = "Entrance";
            // 
            // Radio_Entrance_Lateral
            // 
            this.Radio_Entrance_Lateral.AutoSize = true;
            this.Radio_Entrance_Lateral.Location = new System.Drawing.Point(155, 50);
            this.Radio_Entrance_Lateral.Name = "Radio_Entrance_Lateral";
            this.Radio_Entrance_Lateral.Size = new System.Drawing.Size(73, 21);
            this.Radio_Entrance_Lateral.TabIndex = 1;
            this.Radio_Entrance_Lateral.TabStop = true;
            this.Radio_Entrance_Lateral.Text = "Lateral";
            this.Radio_Entrance_Lateral.UseVisualStyleBackColor = true;
            this.Radio_Entrance_Lateral.Click += new System.EventHandler(this.Radio_Entrance_Lateral_Click);
            // 
            // Radio_Entrance_Axial
            // 
            this.Radio_Entrance_Axial.AutoSize = true;
            this.Radio_Entrance_Axial.Location = new System.Drawing.Point(27, 50);
            this.Radio_Entrance_Axial.Name = "Radio_Entrance_Axial";
            this.Radio_Entrance_Axial.Size = new System.Drawing.Size(58, 21);
            this.Radio_Entrance_Axial.TabIndex = 0;
            this.Radio_Entrance_Axial.TabStop = true;
            this.Radio_Entrance_Axial.Text = "Axial";
            this.Radio_Entrance_Axial.UseVisualStyleBackColor = true;
            this.Radio_Entrance_Axial.Click += new System.EventHandler(this.Radio_Entrance_Axial_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 533);
            this.Controls.Add(this.Frame_Mirrors);
            this.Controls.Add(this.Frame_Slits);
            this.Controls.Add(this.labelInitStatus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.groupboxWlCtrl);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.buttonInitialize);
            this.Controls.Add(this.labelMonosDevs);
            this.Controls.Add(this.comboboxMonos);
            this.Controls.Add(this.buttonOK);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Mono (.NET C#)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupboxWlCtrl.ResumeLayout(false);
            this.groupboxWlCtrl.PerformLayout();
            this.Frame_Slits.ResumeLayout(false);
            this.Frame_Slit_Exit.ResumeLayout(false);
            this.Frame_Slit_Exit.PerformLayout();
            this.Frame_Slit_Entrance.ResumeLayout(false);
            this.Frame_Slit_Entrance.PerformLayout();
            this.Frame_Mirrors.ResumeLayout(false);
            this.Frame_Mirror_Exit.ResumeLayout(false);
            this.Frame_Mirror_Exit.PerformLayout();
            this.Frame_Mirror_Entrance.ResumeLayout(false);
            this.Frame_Mirror_Entrance.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label labelInitStatus;
        internal System.Windows.Forms.Label labelPosition;
        internal System.Windows.Forms.Label labelStatus;
        internal System.Windows.Forms.GroupBox groupboxWlCtrl;
        internal System.Windows.Forms.ComboBox comboboxGrating;
        internal System.Windows.Forms.Label labelGrating;
        internal System.Windows.Forms.TextBox textboxPosition;
        internal System.Windows.Forms.TextBox tbStatus;
        internal System.Windows.Forms.Button buttonInitialize;
        internal System.Windows.Forms.Label labelMonosDevs;
        internal System.Windows.Forms.ComboBox comboboxMonos;
        public System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox Frame_Slits;
        private System.Windows.Forms.GroupBox Frame_Slit_Exit;
        private System.Windows.Forms.Label Label_Slit_Exit_Units;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Text_Slit_Side_Exit;
        private System.Windows.Forms.Label Label_Slit_Front_Exit;
        private System.Windows.Forms.TextBox Text_Slit_Front_Exit;
        private System.Windows.Forms.GroupBox Frame_Slit_Entrance;
        private System.Windows.Forms.Label Label_Slit_Entrance_Units;
        private System.Windows.Forms.Label Label_Slit_Side_Entrance;
        private System.Windows.Forms.TextBox Text_Slit_Side_Entrance;
        private System.Windows.Forms.Label Label_Slit_Front_Entrance;
        private System.Windows.Forms.TextBox Text_Slit_Front_Entrance;
        private System.Windows.Forms.GroupBox Frame_Mirrors;
        private System.Windows.Forms.GroupBox Frame_Mirror_Exit;
        private System.Windows.Forms.RadioButton Radio_Exit_Lateral;
        private System.Windows.Forms.RadioButton Radio_Exit_Axial;
        private System.Windows.Forms.GroupBox Frame_Mirror_Entrance;
        private System.Windows.Forms.RadioButton Radio_Entrance_Lateral;
        private System.Windows.Forms.RadioButton Radio_Entrance_Axial;
    }
}

