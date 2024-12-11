using UMLDiagframApp.Entities;

namespace UMLDiagframApp
{
	public class ViewPort
	{
		DrawArgs _args;

		List<IDrawable> _drawables;

		List<ISelectable> _selectables;
		ISelectable? _selected;

		int x;
		int y;

		int dx;
		int dy;
		public ViewPort(int width, int height)
		{
			_args = new();
			_drawables = new List<IDrawable>();
			_selectables = new List<ISelectable>();
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 15; j++)
				{
					_drawables.Add(new Circle(15 + i * 15, 15 + j * 15));
				}
			}

			DiagramBox db = new DiagramBox("Test", 0, 0, 100, 300);

			_drawables.Add(db);
			_selectables.Add(db);

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
			g.DrawString("Mouse: " + x + " ; " + y, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 40));
			g.DrawString("Mouse deltas: " + dx + " ; " + dy, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 50));
		}


		public void MouseInput(MouseArgs args)
		{
			foreach (var s in _selectables.ToArray().Reverse())
			{
				if (s.IsSelected(args.PositionX, args.PositionY, _args))
				{
					_selected = s;
					continue;
				}

				_selected = null;

			}



			if (_selected != null)
			{
				_selected.MouseInput(args, _args);
			}
			else
			if (args.LeftMouseDown)
			{
				Cursor.Current = Cursors.SizeAll;
				_args.ViewportOffsetX += (int)(args.PositionXDelta / _args.ViewportScale);
				_args.ViewportOffsetY += (int)(args.PositionYDelta / _args.ViewportScale);


			}
			else
			if (args.RightMouseDown)
			{
				Cursor.Current = Cursors.SizeNS;
				_args.ViewportScale -= (float)args.PositionYDelta / 50;
				_args.ViewportScale = Math.Clamp(_args.ViewportScale, 0.1f, 2.5f);
			}
			else
			if (args.Button == MouseButtons.Middle)
			{
				_args.ViewportOffsetX = (int)((_args.ViewportSizeX / 2 - (int)_drawables.Average(d => d.X * _args.ViewportScale)) / _args.ViewportScale);
				_args.ViewportOffsetY = (int)((_args.ViewportSizeY / 2 - (int)_drawables.Average(d => d.Y * _args.ViewportScale)) / _args.ViewportScale);
			}
			else
			if (args.Button == MouseButtons.Right && !args.RightMouseDown)
			{
				_drawables.Add(new Circle((int)((args.PositionX) / _args.ViewportScale) - _args.ViewportOffsetX, (int)((args.PositionY) / _args.ViewportScale) - _args.ViewportOffsetY));
			}

		

			x = args.PositionX;
			y = args.PositionY;
			dx = args.PositionXDelta;
			dy = args.PositionYDelta;
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
