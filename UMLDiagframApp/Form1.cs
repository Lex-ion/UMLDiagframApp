using UMLDiagframApp.Entities;

namespace UMLDiagframApp
{
	public partial class Form1 : Form
	{
		ViewPort _vp;
		bool rightMouseDown;
		bool leftMouseDown;
		MouseArgs? _lastMouseArgs;

		public Form1()
		{
			InitializeComponent();



			_vp = new ViewPort(pictureBox1.Width, pictureBox1.Height);
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;

			_vp.Draw(g);

		}

		private void Form1_MouseUp(object sender, MouseEventArgs e)
		{
			_lastMouseArgs ??= new(e.Button, e.X, e.Y, 0, 0, 0, rightMouseDown, leftMouseDown);
			if (e.Button == MouseButtons.Left)
				leftMouseDown = false;
			if (e.Button == MouseButtons.Right)
				rightMouseDown = false;


			_lastMouseArgs = new(e.Button, e.X, e.Y, -_lastMouseArgs.Value.PostitionX + e.X, -_lastMouseArgs.Value.PostitionY + e.Y, 0, rightMouseDown, leftMouseDown);

			_vp.MouseInput(_lastMouseArgs.Value);
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			_lastMouseArgs ??= new(e.Button, e.X, e.Y, 0, 0, 0, rightMouseDown, leftMouseDown);

			if (e.Button == MouseButtons.Left)
				leftMouseDown = true;
			if (e.Button == MouseButtons.Right)
				rightMouseDown = true;

			_lastMouseArgs = new(e.Button, e.X, e.Y, -_lastMouseArgs.Value.PostitionX + e.X, -_lastMouseArgs.Value.PostitionY + e.Y, 0, rightMouseDown, leftMouseDown);

			_vp.MouseInput(_lastMouseArgs.Value);

			pictureBox1.Refresh();
		}

		private void Form1_Scroll(object sender, ScrollEventArgs e)
		{

			_lastMouseArgs ??= new(0, 0, 0, 0, 0, 0, rightMouseDown, leftMouseDown);
			_lastMouseArgs = new(_lastMouseArgs.Value.Button, _lastMouseArgs.Value.PostitionX, _lastMouseArgs.Value.PostitionY, 0, 0, e.OldValue - e.NewValue, rightMouseDown, leftMouseDown);
			_vp.MouseInput(_lastMouseArgs.Value);

			pictureBox1.Refresh();
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			_lastMouseArgs ??= new(e.Button, e.X, e.Y, 0, 0, 0, rightMouseDown, leftMouseDown);
			_lastMouseArgs = new(e.Button, e.X, e.Y, -_lastMouseArgs.Value.PostitionX + e.X, -_lastMouseArgs.Value.PostitionY + e.Y, 0, rightMouseDown, leftMouseDown);

			_vp.MouseInput(_lastMouseArgs.Value);

			pictureBox1.Refresh();
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			_vp.Resize(Width, Height);
			pictureBox1?.Refresh();
		}
	}
}
