﻿namespace UMLDiagframApp
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			pictureBox1 = new PictureBox();
			openFileDialog1 = new OpenFileDialog();
			saveFileDialog1 = new SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Dock = DockStyle.Fill;
			pictureBox1.Location = new Point(0, 0);
			pictureBox1.Margin = new Padding(3, 4, 3, 4);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(982, 703);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			pictureBox1.Paint += pictureBox1_Paint;
			pictureBox1.MouseDown += Form1_MouseDown;
			pictureBox1.MouseMove += Form1_MouseMove;
			pictureBox1.MouseUp += Form1_MouseUp;
			// 
			// openFileDialog1
			// 
			openFileDialog1.DefaultExt = "json";
			openFileDialog1.FileName = "openFileDialog1";
			openFileDialog1.Filter = "Soubory JSON|*.json";
			openFileDialog1.Title = "Vyberte Soubor";
			// 
			// saveFileDialog1
			// 
			saveFileDialog1.Filter = "Soubor JSON|*.json";
			saveFileDialog1.Title = "Vyberte adresář";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(982, 703);
			Controls.Add(pictureBox1);
			Margin = new Padding(3, 4, 3, 4);
			Name = "Form1";
			Text = "Form1";
			Scroll += Form1_Scroll;
			KeyDown += Form1_KeyDown;
			MouseDown += Form1_MouseDown;
			MouseMove += Form1_MouseMove;
			MouseUp += Form1_MouseUp;
			Resize += Form1_Resize;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private PictureBox pictureBox1;
		private OpenFileDialog openFileDialog1;
		private SaveFileDialog saveFileDialog1;
	}
}
