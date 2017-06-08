namespace TFREC_IR_app
{
    partial class settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(settings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.objectcheck = new System.Windows.Forms.CheckBox();
            this.ambientcheck = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Kcheck = new System.Windows.Forms.CheckBox();
            this.Fcheck = new System.Windows.Forms.CheckBox();
            this.Ccheck = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.intervalSet = new System.Windows.Forms.Button();
            this.intervalText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portSet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.objectcheck);
            this.groupBox1.Controls.Add(this.ambientcheck);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Recieved";
            // 
            // objectcheck
            // 
            this.objectcheck.AutoSize = true;
            this.objectcheck.Location = new System.Drawing.Point(150, 19);
            this.objectcheck.Name = "objectcheck";
            this.objectcheck.Size = new System.Drawing.Size(57, 17);
            this.objectcheck.TabIndex = 1;
            this.objectcheck.Text = "Object";
            this.objectcheck.UseVisualStyleBackColor = true;
            // 
            // ambientcheck
            // 
            this.ambientcheck.AutoSize = true;
            this.ambientcheck.Location = new System.Drawing.Point(34, 19);
            this.ambientcheck.Name = "ambientcheck";
            this.ambientcheck.Size = new System.Drawing.Size(64, 17);
            this.ambientcheck.TabIndex = 0;
            this.ambientcheck.Text = "Ambient";
            this.ambientcheck.UseVisualStyleBackColor = true;
            this.ambientcheck.CheckedChanged += new System.EventHandler(this.ambientcheck_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Kcheck);
            this.groupBox2.Controls.Add(this.Fcheck);
            this.groupBox2.Controls.Add(this.Ccheck);
            this.groupBox2.Location = new System.Drawing.Point(12, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(72, 143);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Units";
            // 
            // Kcheck
            // 
            this.Kcheck.AutoSize = true;
            this.Kcheck.Location = new System.Drawing.Point(6, 112);
            this.Kcheck.Name = "Kcheck";
            this.Kcheck.Size = new System.Drawing.Size(33, 17);
            this.Kcheck.TabIndex = 2;
            this.Kcheck.Text = "K";
            this.Kcheck.UseVisualStyleBackColor = true;
            this.Kcheck.CheckedChanged += new System.EventHandler(this.Kcheck_CheckedChanged);
            // 
            // Fcheck
            // 
            this.Fcheck.AutoSize = true;
            this.Fcheck.Location = new System.Drawing.Point(6, 71);
            this.Fcheck.Name = "Fcheck";
            this.Fcheck.Size = new System.Drawing.Size(32, 17);
            this.Fcheck.TabIndex = 1;
            this.Fcheck.Text = "F";
            this.Fcheck.UseVisualStyleBackColor = true;
            this.Fcheck.CheckedChanged += new System.EventHandler(this.Fcheck_CheckedChanged);
            // 
            // Ccheck
            // 
            this.Ccheck.AutoSize = true;
            this.Ccheck.Location = new System.Drawing.Point(6, 30);
            this.Ccheck.Name = "Ccheck";
            this.Ccheck.Size = new System.Drawing.Size(33, 17);
            this.Ccheck.TabIndex = 0;
            this.Ccheck.Text = "C";
            this.Ccheck.UseVisualStyleBackColor = true;
            this.Ccheck.CheckedChanged += new System.EventHandler(this.Ccheck_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.intervalSet);
            this.groupBox3.Controls.Add(this.intervalText);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.portSet);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.portText);
            this.groupBox3.Location = new System.Drawing.Point(90, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 143);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Read Settings";
            // 
            // intervalSet
            // 
            this.intervalSet.Location = new System.Drawing.Point(144, 70);
            this.intervalSet.Name = "intervalSet";
            this.intervalSet.Size = new System.Drawing.Size(32, 23);
            this.intervalSet.TabIndex = 5;
            this.intervalSet.Text = "set";
            this.intervalSet.UseVisualStyleBackColor = true;
            this.intervalSet.Click += new System.EventHandler(this.intervalSet_Click);
            // 
            // intervalText
            // 
            this.intervalText.Location = new System.Drawing.Point(76, 72);
            this.intervalText.Name = "intervalText";
            this.intervalText.Size = new System.Drawing.Size(62, 20);
            this.intervalText.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Read Interval";
            // 
            // portSet
            // 
            this.portSet.Location = new System.Drawing.Point(144, 28);
            this.portSet.Name = "portSet";
            this.portSet.Size = new System.Drawing.Size(34, 23);
            this.portSet.TabIndex = 2;
            this.portSet.Text = "set";
            this.portSet.UseVisualStyleBackColor = true;
            this.portSet.Click += new System.EventHandler(this.portSet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(38, 28);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(100, 20);
            this.portText.TabIndex = 0;
            this.portText.TextChanged += new System.EventHandler(this.portText_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(117, 229);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(198, 229);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "settings";
            this.Text = "settings";
            this.Load += new System.EventHandler(this.settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox objectcheck;
        private System.Windows.Forms.CheckBox ambientcheck;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox Kcheck;
        private System.Windows.Forms.CheckBox Fcheck;
        private System.Windows.Forms.CheckBox Ccheck;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.Button portSet;
        private System.Windows.Forms.Button intervalSet;
        private System.Windows.Forms.TextBox intervalText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}