using UMLDiagframApp.Entities;

namespace UMLDiagframApp
{
	public class ViewPort
	{
		DrawArgs _args;

		List<IDrawable> _drawables;

		public ViewPort(int width, int height)
		{
			_args = new();
			_drawables = new List<IDrawable>();
			_drawables.Add(new Circle(100, 300));
			_drawables.Add(new Circle(150, 350));
			_drawables.Add(new Circle(200, 250));
			_args.ViewportScale = 1;
			_args.ViewportSizeX = width;
			_args.ViewportSizeY = height;
		}

		public void Draw(Graphics g)
		{
			//g.ScaleTransform(_args.ViewportScale, _args.ViewportScale);

			_drawables.ForEach(d => d.Draw(_args, g));


			g.DrawString("Offset: " + _args.ViewportOffsetX + " ; " + _args.ViewportOffsetY, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 10));
			g.DrawString("Size: " + _args.ViewportSizeX + " ; " + _args.ViewportSizeY, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 20));
			g.DrawString("Scale: " + _args.ViewportScale, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 30));
		}


		public void MouseInput(MouseArgs args)
		{
			if (args.LeftMouseDown)
			{
				Cursor.Current = Cursors.SizeAll;
				_args.ViewportOffsetX += args.PostitionXDelta;
				_args.ViewportOffsetY += args.PostitionYDelta;
			}
			if (args.RightMouseDown)
			{
				Cursor.Current = Cursors.SizeNS;
				_args.ViewportScale -= (float)args.PostitionYDelta / 50;
				_args.ViewportScale = Math.Clamp(_args.ViewportScale, 0.1f, 2.5f);
			}

			if (args.Button == MouseButtons.Middle)
			{
				_args.ViewportOffsetX = (int)((_args.ViewportSizeX / 2 - (int)_drawables.Average(d => d.X * _args.ViewportScale)) / _args.ViewportScale);
				_args.ViewportOffsetY = (int)((_args.ViewportSizeY / 2 - (int)_drawables.Average(d => d.Y * _args.ViewportScale)) / _args.ViewportScale);
			}
		}

		public void Resize(int width, int height)
		{
			int deltaW = _args.ViewportSizeX - width;
			int deltaH = _args.ViewportSizeY - height;

			_args.ViewportSizeY = height;
			_args.ViewportSizeX = width;

			_args.ViewportOffsetY -= deltaH / 2;
			_args.ViewportOffsetX -= deltaW / 2;
		}
	}
}
