namespace UMLDiagframApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;

			g.FillEllipse(Brushes.Black, new(10 ,10,50,50));
			
		}
	}
}
