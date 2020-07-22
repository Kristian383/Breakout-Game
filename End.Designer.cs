namespace OTTER
{
    partial class End
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(End));
            this.lstResults = new System.Windows.Forms.ListBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnKraj = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBrojPonavljanja = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtIterative = new System.Windows.Forms.RadioButton();
            this.rbtCount = new System.Windows.Forms.RadioButton();
            this.rbtBinaryRecursion = new System.Windows.Forms.RadioButton();
            this.rbtLastO = new System.Windows.Forms.RadioButton();
            this.rbtFirstO = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lstResults
            // 
            this.lstResults.BackColor = System.Drawing.Color.Silver;
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point(160, 178);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(183, 238);
            this.lstResults.TabIndex = 0;
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.SystemColors.Control;
            this.btnRestart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRestart.BackgroundImage")));
            this.btnRestart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRestart.Location = new System.Drawing.Point(26, 28);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(118, 79);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnKraj
            // 
            this.btnKraj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKraj.BackgroundImage")));
            this.btnKraj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKraj.Location = new System.Drawing.Point(648, 357);
            this.btnKraj.Name = "btnKraj";
            this.btnKraj.Size = new System.Drawing.Size(118, 70);
            this.btnKraj.TabIndex = 3;
            this.btnKraj.UseVisualStyleBackColor = true;
            this.btnKraj.Click += new System.EventHandler(this.btnKraj_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(160, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(183, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(572, 80);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(219, 236);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(418, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 21);
            this.button1.TabIndex = 7;
            this.button1.Text = "Search ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBrojPonavljanja
            // 
            this.txtBrojPonavljanja.Location = new System.Drawing.Point(498, 138);
            this.txtBrojPonavljanja.Name = "txtBrojPonavljanja";
            this.txtBrojPonavljanja.Size = new System.Drawing.Size(68, 20);
            this.txtBrojPonavljanja.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Broj ponavljanja algoritma";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(354, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Upiši bodove nekog igrača";
            // 
            // rbtIterative
            // 
            this.rbtIterative.AutoSize = true;
            this.rbtIterative.Checked = true;
            this.rbtIterative.Location = new System.Drawing.Point(406, 311);
            this.rbtIterative.Name = "rbtIterative";
            this.rbtIterative.Size = new System.Drawing.Size(95, 17);
            this.rbtIterative.TabIndex = 21;
            this.rbtIterative.TabStop = true;
            this.rbtIterative.Text = "Binary Iterative";
            this.rbtIterative.UseVisualStyleBackColor = true;
            // 
            // rbtCount
            // 
            this.rbtCount.AutoSize = true;
            this.rbtCount.Location = new System.Drawing.Point(407, 276);
            this.rbtCount.Name = "rbtCount";
            this.rbtCount.Size = new System.Drawing.Size(85, 17);
            this.rbtCount.TabIndex = 20;
            this.rbtCount.Text = "Binary Count";
            this.rbtCount.UseVisualStyleBackColor = true;
            // 
            // rbtBinaryRecursion
            // 
            this.rbtBinaryRecursion.AutoSize = true;
            this.rbtBinaryRecursion.Location = new System.Drawing.Point(406, 244);
            this.rbtBinaryRecursion.Name = "rbtBinaryRecursion";
            this.rbtBinaryRecursion.Size = new System.Drawing.Size(102, 17);
            this.rbtBinaryRecursion.TabIndex = 19;
            this.rbtBinaryRecursion.Text = "BinaryRecursion";
            this.rbtBinaryRecursion.UseVisualStyleBackColor = true;
            // 
            // rbtLastO
            // 
            this.rbtLastO.AutoSize = true;
            this.rbtLastO.Location = new System.Drawing.Point(406, 210);
            this.rbtLastO.Name = "rbtLastO";
            this.rbtLastO.Size = new System.Drawing.Size(133, 17);
            this.rbtLastO.TabIndex = 18;
            this.rbtLastO.Text = "Last Occurence Binary";
            this.rbtLastO.UseVisualStyleBackColor = true;
            // 
            // rbtFirstO
            // 
            this.rbtFirstO.AutoSize = true;
            this.rbtFirstO.Location = new System.Drawing.Point(407, 178);
            this.rbtFirstO.Name = "rbtFirstO";
            this.rbtFirstO.Size = new System.Drawing.Size(132, 17);
            this.rbtFirstO.TabIndex = 17;
            this.rbtFirstO.Text = "First Occurence Binary";
            this.rbtFirstO.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(498, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(68, 20);
            this.textBox1.TabIndex = 16;
            // 
            // End
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtBrojPonavljanja);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtIterative);
            this.Controls.Add(this.rbtCount);
            this.Controls.Add(this.rbtBinaryRecursion);
            this.Controls.Add(this.rbtLastO);
            this.Controls.Add(this.rbtFirstO);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnKraj);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.lstResults);
            this.Name = "End";
            this.Text = "Kraj";
            this.Load += new System.EventHandler(this.Kraj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnKraj;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBrojPonavljanja;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtIterative;
        private System.Windows.Forms.RadioButton rbtCount;
        private System.Windows.Forms.RadioButton rbtBinaryRecursion;
        private System.Windows.Forms.RadioButton rbtLastO;
        private System.Windows.Forms.RadioButton rbtFirstO;
        private System.Windows.Forms.TextBox textBox1;
    }
}