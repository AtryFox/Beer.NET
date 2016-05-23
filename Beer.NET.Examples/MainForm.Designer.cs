namespace DerAtrox.BeerNET.Examples {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.optDBeer = new System.Windows.Forms.RadioButton();
            this.optSBeer = new System.Windows.Forms.RadioButton();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.optSDBeer = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optBeerEx = new System.Windows.Forms.RadioButton();
            this.optBeer = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mode";
            // 
            // optDBeer
            // 
            this.optDBeer.AutoSize = true;
            this.optDBeer.Location = new System.Drawing.Point(62, 104);
            this.optDBeer.Name = "optDBeer";
            this.optDBeer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optDBeer.Size = new System.Drawing.Size(101, 17);
            this.optDBeer.TabIndex = 3;
            this.optDBeer.Text = "Deserialize Beer";
            this.optDBeer.UseVisualStyleBackColor = true;
            // 
            // optSBeer
            // 
            this.optSBeer.AutoSize = true;
            this.optSBeer.Checked = true;
            this.optSBeer.Location = new System.Drawing.Point(62, 81);
            this.optSBeer.Name = "optSBeer";
            this.optSBeer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optSBeer.Size = new System.Drawing.Size(89, 17);
            this.optSBeer.TabIndex = 2;
            this.optSBeer.TabStop = true;
            this.optSBeer.Text = "Serialize Beer";
            this.optSBeer.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(55, 13);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(239, 20);
            this.txtInput.TabIndex = 1;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(173, 83);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(121, 76);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 183);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(282, 176);
            this.txtOutput.TabIndex = 6;
            // 
            // optSDBeer
            // 
            this.optSDBeer.AutoSize = true;
            this.optSDBeer.Location = new System.Drawing.Point(62, 127);
            this.optSDBeer.Name = "optSDBeer";
            this.optSDBeer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optSDBeer.Size = new System.Drawing.Size(101, 30);
            this.optSDBeer.TabIndex = 4;
            this.optSDBeer.Text = "Serialize and\r\nDeserialize Beer";
            this.optSDBeer.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Encoder";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optBeerEx);
            this.panel1.Controls.Add(this.optBeer);
            this.panel1.Location = new System.Drawing.Point(55, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 38);
            this.panel1.TabIndex = 8;
            // 
            // optBeerEx
            // 
            this.optBeerEx.AutoSize = true;
            this.optBeerEx.Location = new System.Drawing.Point(60, 10);
            this.optBeerEx.Name = "optBeerEx";
            this.optBeerEx.Size = new System.Drawing.Size(59, 17);
            this.optBeerEx.TabIndex = 1;
            this.optBeerEx.Text = "BeerEx";
            this.optBeerEx.UseVisualStyleBackColor = true;
            this.optBeerEx.CheckedChanged += new System.EventHandler(this.EncoderSelection_CheckedChanged);
            // 
            // optBeer
            // 
            this.optBeer.AutoSize = true;
            this.optBeer.Checked = true;
            this.optBeer.Location = new System.Drawing.Point(7, 10);
            this.optBeer.Name = "optBeer";
            this.optBeer.Size = new System.Drawing.Size(47, 17);
            this.optBeer.TabIndex = 0;
            this.optBeer.TabStop = true;
            this.optBeer.Text = "Beer";
            this.optBeer.UseVisualStyleBackColor = true;
            this.optBeer.CheckedChanged += new System.EventHandler(this.EncoderSelection_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 374);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.optSDBeer);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.optSBeer);
            this.Controls.Add(this.optDBeer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Beer.NET.Examples";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton optDBeer;
		private System.Windows.Forms.RadioButton optSBeer;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.RadioButton optSDBeer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton optBeerEx;
        private System.Windows.Forms.RadioButton optBeer;
    }
}

