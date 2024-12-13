namespace UMLDiagframApp
{
	partial class TextInputForm
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
			components = new System.ComponentModel.Container();
			textBox1 = new TextBox();
			button1 = new Button();
			button2 = new Button();
			errorProvider1 = new ErrorProvider(components);
			((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.BackColor = Color.Teal;
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Location = new Point(12, 12);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(249, 16);
			textBox1.TabIndex = 0;
			textBox1.KeyUp += textBox1_KeyUp;
			// 
			// button1
			// 
			button1.BackColor = Color.CadetBlue;
			button1.FlatAppearance.BorderColor = Color.MidnightBlue;
			button1.FlatStyle = FlatStyle.Flat;
			button1.Location = new Point(12, 38);
			button1.Name = "button1";
			button1.Size = new Size(168, 23);
			button1.TabIndex = 1;
			button1.Text = "Potvrdit";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.BackColor = Color.CadetBlue;
			button2.FlatAppearance.BorderColor = Color.MidnightBlue;
			button2.FlatStyle = FlatStyle.Flat;
			button2.Location = new Point(186, 38);
			button2.Name = "button2";
			button2.Size = new Size(75, 23);
			button2.TabIndex = 2;
			button2.Text = "Zrušit";
			button2.UseVisualStyleBackColor = false;
			button2.Click += button2_Click;
			// 
			// errorProvider1
			// 
			errorProvider1.BlinkRate = 500;
			errorProvider1.ContainerControl = this;
			// 
			// TextInputForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(0, 192, 192);
			ClientSize = new Size(273, 73);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(textBox1);
			FormBorderStyle = FormBorderStyle.None;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "TextInputForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterParent;
			Text = "TextInputForm";
			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
		private Button button1;
		private Button button2;
		private ErrorProvider errorProvider1;
	}
}