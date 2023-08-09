
namespace WindowsFormsApp2
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.nud_fan_working = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReadConf = new System.Windows.Forms.Button();
            this.buttonWriteConf = new System.Windows.Forms.Button();
            this.nud_stopHeater = new System.Windows.Forms.NumericUpDown();
            this.nud_fan_idle = new System.Windows.Forms.NumericUpDown();
            this.nud_delay = new System.Windows.Forms.NumericUpDown();
            this.nud_startHeater = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonPortOpen = new System.Windows.Forms.Button();
            this.buttonPortClose = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonReadFromFile = new System.Windows.Forms.Button();
            this.buttonWriteToFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fan_working)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_stopHeater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fan_idle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startHeater)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.ReadTimeout = 3000;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // nud_fan_working
            // 
            this.nud_fan_working.Location = new System.Drawing.Point(548, 382);
            this.nud_fan_working.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nud_fan_working.Name = "nud_fan_working";
            this.nud_fan_working.Size = new System.Drawing.Size(120, 22);
            this.nud_fan_working.TabIndex = 0;
            this.nud_fan_working.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Serial Port";
            // 
            // buttonReadConf
            // 
            this.buttonReadConf.Location = new System.Drawing.Point(94, 183);
            this.buttonReadConf.Name = "buttonReadConf";
            this.buttonReadConf.Size = new System.Drawing.Size(279, 56);
            this.buttonReadConf.TabIndex = 5;
            this.buttonReadConf.Text = "Read Configuration from Device";
            this.buttonReadConf.UseVisualStyleBackColor = true;
            this.buttonReadConf.Click += new System.EventHandler(this.buttonReadConf_Click);
            // 
            // buttonWriteConf
            // 
            this.buttonWriteConf.Location = new System.Drawing.Point(396, 183);
            this.buttonWriteConf.Name = "buttonWriteConf";
            this.buttonWriteConf.Size = new System.Drawing.Size(279, 56);
            this.buttonWriteConf.TabIndex = 6;
            this.buttonWriteConf.Text = "Write Configuration to Device";
            this.buttonWriteConf.UseVisualStyleBackColor = true;
            this.buttonWriteConf.Click += new System.EventHandler(this.buttonWriteConf_Click_1);
            // 
            // nud_stopHeater
            // 
            this.nud_stopHeater.Location = new System.Drawing.Point(548, 304);
            this.nud_stopHeater.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nud_stopHeater.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_stopHeater.Name = "nud_stopHeater";
            this.nud_stopHeater.Size = new System.Drawing.Size(120, 22);
            this.nud_stopHeater.TabIndex = 7;
            this.nud_stopHeater.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // nud_fan_idle
            // 
            this.nud_fan_idle.Location = new System.Drawing.Point(548, 342);
            this.nud_fan_idle.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nud_fan_idle.Name = "nud_fan_idle";
            this.nud_fan_idle.Size = new System.Drawing.Size(120, 22);
            this.nud_fan_idle.TabIndex = 8;
            this.nud_fan_idle.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // nud_delay
            // 
            this.nud_delay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nud_delay.Location = new System.Drawing.Point(548, 420);
            this.nud_delay.Maximum = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.nud_delay.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_delay.Name = "nud_delay";
            this.nud_delay.Size = new System.Drawing.Size(120, 22);
            this.nud_delay.TabIndex = 9;
            this.nud_delay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // nud_startHeater
            // 
            this.nud_startHeater.Location = new System.Drawing.Point(548, 267);
            this.nud_startHeater.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nud_startHeater.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_startHeater.Name = "nud_startHeater";
            this.nud_startHeater.Size = new System.Drawing.Size(120, 22);
            this.nud_startHeater.TabIndex = 10;
            this.nud_startHeater.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Temperature to start heater (oC)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 306);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Temperature to stop heater (oC)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 344);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Fan speed on idle (oC)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(91, 382);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(223, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Fan speed on heater working (oC)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 420);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(272, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Delay from fan o to heater working (msec)";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(196, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 16;
            // 
            // buttonPortOpen
            // 
            this.buttonPortOpen.Location = new System.Drawing.Point(377, 16);
            this.buttonPortOpen.Name = "buttonPortOpen";
            this.buttonPortOpen.Size = new System.Drawing.Size(140, 65);
            this.buttonPortOpen.TabIndex = 17;
            this.buttonPortOpen.Text = "Open";
            this.buttonPortOpen.UseVisualStyleBackColor = true;
            this.buttonPortOpen.Click += new System.EventHandler(this.buttonPortOpen_Click);
            // 
            // buttonPortClose
            // 
            this.buttonPortClose.Location = new System.Drawing.Point(605, 12);
            this.buttonPortClose.Name = "buttonPortClose";
            this.buttonPortClose.Size = new System.Drawing.Size(140, 66);
            this.buttonPortClose.TabIndex = 18;
            this.buttonPortClose.Text = "Close";
            this.buttonPortClose.UseVisualStyleBackColor = true;
            this.buttonPortClose.Click += new System.EventHandler(this.buttonPortClose_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonReadFromFile
            // 
            this.buttonReadFromFile.Location = new System.Drawing.Point(94, 108);
            this.buttonReadFromFile.Name = "buttonReadFromFile";
            this.buttonReadFromFile.Size = new System.Drawing.Size(269, 42);
            this.buttonReadFromFile.TabIndex = 19;
            this.buttonReadFromFile.Text = "Read settings from file";
            this.buttonReadFromFile.UseVisualStyleBackColor = true;
            this.buttonReadFromFile.Click += new System.EventHandler(this.buttonReadFromFile_Click);
            // 
            // buttonWriteToFile
            // 
            this.buttonWriteToFile.Location = new System.Drawing.Point(399, 108);
            this.buttonWriteToFile.Name = "buttonWriteToFile";
            this.buttonWriteToFile.Size = new System.Drawing.Size(269, 42);
            this.buttonWriteToFile.TabIndex = 20;
            this.buttonWriteToFile.Text = "Write settings to file";
            this.buttonWriteToFile.UseVisualStyleBackColor = true;
            this.buttonWriteToFile.Click += new System.EventHandler(this.buttonWriteToFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 477);
            this.Controls.Add(this.buttonWriteToFile);
            this.Controls.Add(this.buttonReadFromFile);
            this.Controls.Add(this.buttonPortClose);
            this.Controls.Add(this.buttonPortOpen);
            this.Controls.Add(this.buttonReadConf);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nud_startHeater);
            this.Controls.Add(this.nud_delay);
            this.Controls.Add(this.nud_fan_idle);
            this.Controls.Add(this.nud_stopHeater);
            this.Controls.Add(this.buttonWriteConf);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nud_fan_working);
            this.Name = "Form1";
            this.Text = "Heater Configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_fan_working)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_stopHeater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fan_idle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startHeater)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.NumericUpDown nud_fan_working;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReadConf;
        private System.Windows.Forms.Button buttonWriteConf;
        private System.Windows.Forms.NumericUpDown nud_stopHeater;
        private System.Windows.Forms.NumericUpDown nud_fan_idle;
        private System.Windows.Forms.NumericUpDown nud_delay;
        private System.Windows.Forms.NumericUpDown nud_startHeater;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonPortOpen;
        private System.Windows.Forms.Button buttonPortClose;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonReadFromFile;
        private System.Windows.Forms.Button buttonWriteToFile;
    }
}

