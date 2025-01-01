using UMLDiagframApp.Entities;
using UMLDiagframApp.Presentation;

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
			_vp = new ViewPort(Width,Height,saveFileDialog1,openFileDialog1);

			_vp.Center();

		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;

			_vp.Draw(g);

		}

		private void Form1_MouseUp(object sender, MouseEventArgs e)
		{
			_lastMouseArgs ??= new(e.Button, null, e.X, e.Y, 0, 0, 0, rightMouseDown, leftMouseDown);
			if (e.Button == MouseButtons.Left)
				leftMouseDown = false;
			if (e.Button == MouseButtons.Right)
				rightMouseDown = false;


			_lastMouseArgs = new(e.Button, _lastMouseArgs, e.X, e.Y, -_lastMouseArgs.Value.PositionX + e.X, -_lastMouseArgs.Value.PositionY + e.Y, 0, rightMouseDown, leftMouseDown);

			_vp.MouseInput(_lastMouseArgs.Value);

			pictureBox1.Refresh();
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			_lastMouseArgs ??= new(e.Button, null, e.X, e.Y, 0, 0, 0, rightMouseDown, leftMouseDown);

			if (e.Button == MouseButtons.Left)
				leftMouseDown = true;
			if (e.Button == MouseButtons.Right)
				rightMouseDown = true;

			_lastMouseArgs = new(e.Button, _lastMouseArgs, e.X, e.Y, -_lastMouseArgs.Value.PositionX + e.X, -_lastMouseArgs.Value.PositionY + e.Y, 0, rightMouseDown, leftMouseDown); ;

			_vp.MouseInput(_lastMouseArgs.Value);

			pictureBox1.Refresh();
		}

		private void Form1_Scroll(object sender, ScrollEventArgs e)
		{

			_lastMouseArgs ??= new(0, null, 0, 0, 0, 0, 0, rightMouseDown, leftMouseDown);
			_lastMouseArgs = new(_lastMouseArgs.Value.Button, _lastMouseArgs, _lastMouseArgs.Value.PositionX, _lastMouseArgs.Value.PositionY, 0, 0, e.OldValue - e.NewValue, rightMouseDown, leftMouseDown);
			_vp.MouseInput(_lastMouseArgs.Value);

			pictureBox1.Refresh();
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			_lastMouseArgs ??= new(e.Button, null, e.X, e.Y, 0, 0, 0, rightMouseDown, leftMouseDown);
			_lastMouseArgs = new(e.Button, _lastMouseArgs, e.X, e.Y, -_lastMouseArgs.Value.PositionX + e.X, -_lastMouseArgs.Value.PositionY + e.Y, 0, rightMouseDown, leftMouseDown);

			_vp.MouseInput(_lastMouseArgs.Value);

			pictureBox1.Refresh();
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			_vp.Resize(Width, Height);
			pictureBox1?.Refresh();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			_vp.HandleKeyInput(e);
			pictureBox1.Refresh();
		}

	
	}
}
