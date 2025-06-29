namespace WindowsFormsApp1
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
            this.button2 = new System.Windows.Forms.Button();
            this.StartMainLoopButton = new System.Windows.Forms.Button();
            this.ZbieractwoLv2Checkbox = new System.Windows.Forms.CheckBox();
            this.StartDriverButton = new System.Windows.Forms.Button();
            this.checkBoxFarmByRequest = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(483, 242);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 67);
            this.button2.TabIndex = 1;
            this.button2.Text = "Get Exchaneg Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // StartMainLoopButton
            // 
            this.StartMainLoopButton.BackColor = System.Drawing.Color.LightSalmon;
            this.StartMainLoopButton.Enabled = false;
            this.StartMainLoopButton.Location = new System.Drawing.Point(334, 242);
            this.StartMainLoopButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartMainLoopButton.Name = "StartMainLoopButton";
            this.StartMainLoopButton.Size = new System.Drawing.Size(145, 82);
            this.StartMainLoopButton.TabIndex = 5;
            this.StartMainLoopButton.Text = "Start Main Loop";
            this.StartMainLoopButton.UseVisualStyleBackColor = false;
            this.StartMainLoopButton.Click += new System.EventHandler(this.StartMainLoop_Click);
            // 
            // ZbieractwoLv2Checkbox
            // 
            this.ZbieractwoLv2Checkbox.AutoSize = true;
            this.ZbieractwoLv2Checkbox.Location = new System.Drawing.Point(50, 242);
            this.ZbieractwoLv2Checkbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ZbieractwoLv2Checkbox.Name = "ZbieractwoLv2Checkbox";
            this.ZbieractwoLv2Checkbox.Size = new System.Drawing.Size(100, 17);
            this.ZbieractwoLv2Checkbox.TabIndex = 6;
            this.ZbieractwoLv2Checkbox.Text = "Zbieractwo Lv2";
            this.ZbieractwoLv2Checkbox.UseVisualStyleBackColor = true;
            // 
            // StartDriverButton
            // 
            this.StartDriverButton.BackColor = System.Drawing.Color.Gold;
            this.StartDriverButton.Location = new System.Drawing.Point(182, 238);
            this.StartDriverButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartDriverButton.Name = "StartDriverButton";
            this.StartDriverButton.Size = new System.Drawing.Size(127, 82);
            this.StartDriverButton.TabIndex = 9;
            this.StartDriverButton.Text = "Start Driver";
            this.StartDriverButton.UseVisualStyleBackColor = false;
            this.StartDriverButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBoxFarmByRequest
            // 
            this.checkBoxFarmByRequest.AutoSize = true;
            this.checkBoxFarmByRequest.Checked = true;
            this.checkBoxFarmByRequest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFarmByRequest.Location = new System.Drawing.Point(50, 303);
            this.checkBoxFarmByRequest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxFarmByRequest.Name = "checkBoxFarmByRequest";
            this.checkBoxFarmByRequest.Size = new System.Drawing.Size(111, 17);
            this.checkBoxFarmByRequest.TabIndex = 11;
            this.checkBoxFarmByRequest.Text = "Farm by Requests";
            this.checkBoxFarmByRequest.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 67);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxFarmByRequest);
            this.Controls.Add(this.StartDriverButton);
            this.Controls.Add(this.ZbieractwoLv2Checkbox);
            this.Controls.Add(this.StartMainLoopButton);
            this.Controls.Add(this.button2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button StartMainLoopButton;
        private System.Windows.Forms.CheckBox ZbieractwoLv2Checkbox;
        private System.Windows.Forms.Button StartDriverButton;
        private System.Windows.Forms.CheckBox checkBoxFarmByRequest;
        private System.Windows.Forms.Button button1;
    }
}

