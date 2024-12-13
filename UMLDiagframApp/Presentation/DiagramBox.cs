using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
	public class DiagramBox : AbstractSelectable
	{
		public string Name { get; set; }


		private int? _selectionX;
		private int? _selectionY;
		private int _oldX;
		private int _oldY;
		bool focused;



		public DiagramBox(string name, int x, int y, int width, int height) : base(x, y, width, height)
		{
			Name = name;
			_selectionX = null;
			_selectionY = null;
		}



		public override void Draw(DrawArgs args, Graphics g)
		{
			Font f = new(FontFamily.GenericMonospace, 15 * args.ViewportScale, FontStyle.Regular);
			Font m = new(FontFamily.GenericMonospace, 15 , FontStyle.Regular);

			g.ResetTransform();
			g.ScaleTransform(args.ViewportScale, args.ViewportScale);
			width = (int)Math.Ceiling( g.MeasureString(Name+new string('x',5),m).Width);
			g.ScaleTransform(1, 1);
			g.ResetTransform();
			Color c = Color.FromArgb(64, 0, 0, 0);

			g.FillRectangle(new SolidBrush(c), new((int)((X + 5 + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + 9 + args.ViewportOffsetY) * args.ViewportScale)
				, (int)(width * args.ViewportScale), (int)(height * args.ViewportScale)));

			g.FillRectangle(isSelected ? Brushes.CadetBlue : Brushes.Aqua, new((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + args.ViewportOffsetY) * args.ViewportScale)
				, (int)(width * args.ViewportScale), (int)(height * args.ViewportScale)));

			g.FillRectangle(Brushes.DarkCyan, new((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + args.ViewportOffsetY) * args.ViewportScale)
				, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)));

			g.DrawRectangle(Pens.DarkBlue, new((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + args.ViewportOffsetY) * args.ViewportScale)
			, (int)(width * args.ViewportScale), (int)(height * args.ViewportScale)));


			g.DrawString(Name, f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + args.ViewportOffsetY) * args.ViewportScale)
				, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

			g.DrawString("X:" + X, f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + 30 + args.ViewportOffsetY) * args.ViewportScale)
				, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			g.DrawString("Y:" + Y, f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + 60 + args.ViewportOffsetY) * args.ViewportScale)
				, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
		}

		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{

			Cursor.Current = Cursors.Default;
			(int, int) p = (X, Y) + dArgs;
			(int, int) d = (X + width, Y + 30) + dArgs;

			if (mArgs.PositionX >= p.Item1 && mArgs.PositionX <= d.Item1 &&
				mArgs.PositionY >= p.Item2 && mArgs.PositionY <= d.Item2
				|| focused
				)
			{
				Cursor.Current = Cursors.Hand;

				if (mArgs.ButtonState == MouseButtonsStates.LeftDown)
				{
					_selectionX = mArgs.PositionX;
					_selectionY = mArgs.PositionY;
					focused = true;
					_oldX = X; _oldY = Y;
				}

				if ((mArgs.Button == MouseButtons.Left || mArgs.LeftMouseDown) && _selectionX is not null && _selectionY is not null)
				{

					int dX = X - dArgs.ViewportOffsetX;

					X = _oldX + (mArgs.PositionX - (int)_selectionX) / dArgs;
					Y = _oldY + (mArgs.PositionY - (int)_selectionY) / dArgs;
				}

			}

			if (mArgs.ButtonState == MouseButtonsStates.LeftUp)
			{
				focused = false;

				_selectionX = null;
				_selectionY = null;

			}

		}

		
	}

}
