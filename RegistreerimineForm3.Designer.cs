namespace Andmebass_TARpv23
{
    partial class RegistreerimineForm3
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
            this.nimi = new System.Windows.Forms.Label();
            this.parool = new System.Windows.Forms.Label();
            this.rolli = new System.Windows.Forms.Label();
            this.Nimi_txt = new System.Windows.Forms.TextBox();
            this.parool_txt = new System.Windows.Forms.TextBox();
            this.Rolli_comboBox = new System.Windows.Forms.ComboBox();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nimi
            // 
            this.nimi.AutoSize = true;
            this.nimi.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nimi.Location = new System.Drawing.Point(21, 35);
            this.nimi.Name = "nimi";
            this.nimi.Size = new System.Drawing.Size(61, 26);
            this.nimi.TabIndex = 2;
            this.nimi.Text = "Nimi";
            // 
            // parool
            // 
            this.parool.AutoSize = true;
            this.parool.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parool.Location = new System.Drawing.Point(21, 91);
            this.parool.Name = "parool";
            this.parool.Size = new System.Drawing.Size(79, 26);
            this.parool.TabIndex = 3;
            this.parool.Text = "Parool";
            // 
            // rolli
            // 
            this.rolli.AutoSize = true;
            this.rolli.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rolli.Location = new System.Drawing.Point(21, 145);
            this.rolli.Name = "rolli";
            this.rolli.Size = new System.Drawing.Size(58, 26);
            this.rolli.TabIndex = 4;
            this.rolli.Text = "Rolli";
            // 
            // Nimi_txt
            // 
            this.Nimi_txt.Location = new System.Drawing.Point(106, 41);
            this.Nimi_txt.Name = "Nimi_txt";
            this.Nimi_txt.Size = new System.Drawing.Size(119, 20);
            this.Nimi_txt.TabIndex = 5;
            // 
            // parool_txt
            // 
            this.parool_txt.Location = new System.Drawing.Point(106, 98);
            this.parool_txt.Name = "parool_txt";
            this.parool_txt.Size = new System.Drawing.Size(119, 20);
            this.parool_txt.TabIndex = 6;
            // 
            // Rolli_comboBox
            // 
            this.Rolli_comboBox.FormattingEnabled = true;
            this.Rolli_comboBox.Location = new System.Drawing.Point(106, 145);
            this.Rolli_comboBox.Name = "Rolli_comboBox";
            this.Rolli_comboBox.Size = new System.Drawing.Size(121, 21);
            this.Rolli_comboBox.TabIndex = 7;
            // 
            // RegisterButton
            // 
            this.RegisterButton.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterButton.Location = new System.Drawing.Point(26, 214);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(149, 40);
            this.RegisterButton.TabIndex = 12;
            this.RegisterButton.Text = "Registreerimine";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click_1);
            // 
            // RegistreerimineForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 299);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.Rolli_comboBox);
            this.Controls.Add(this.parool_txt);
            this.Controls.Add(this.Nimi_txt);
            this.Controls.Add(this.rolli);
            this.Controls.Add(this.parool);
            this.Controls.Add(this.nimi);
            this.Name = "RegistreerimineForm3";
            this.Text = "RegistreerimineForm3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nimi;
        private System.Windows.Forms.Label parool;
        private System.Windows.Forms.Label rolli;
        private System.Windows.Forms.TextBox Nimi_txt;
        private System.Windows.Forms.TextBox parool_txt;
        private System.Windows.Forms.ComboBox Rolli_comboBox;
        private System.Windows.Forms.Button RegisterButton;
    }
}