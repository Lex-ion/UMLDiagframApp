namespace UMLDiagframApp
{
	partial class ProgressForm
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
			progressBar1 = new ProgressBar();
			label1 = new Label();
			SuspendLayout();
			// 
			// progressBar1
			// 
			progressBar1.BackColor = Color.Blue;
			progressBar1.ForeColor = Color.FromArgb(0, 192, 192);
			progressBar1.Location = new Point(12, 12);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new Size(776, 23);
			progressBar1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
			label1.Location = new Point(12, 47);
			label1.Name = "label1";
			label1.Size = new Size(63, 19);
			label1.TabIndex = 1;
			label1.Text = "Status";
			// 
			// ProgressForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.DarkCyan;
			ClientSize = new Size(800, 75);
			Controls.Add(label1);
			Controls.Add(progressBar1);
			FormBorderStyle = FormBorderStyle.None;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ProgressForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterParent;
			Text = "ProgressForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ProgressBar progressBar1;
		private Label label1;
	}
}