namespace UMLDiagframApp.Entities
{
	public class DiagramBox : AbstractSelectable
	{
		public string Name { get; set; }


		public DiagramBox(string name, int x, int y, int width, int height) : base(x, y, width, height)
		{
			Name = name;
		}



		public override void Draw(DrawArgs args, Graphics g)
		{
			g.FillRectangle(isSelected ? Brushes.CadetBlue : Brushes.Aqua, new((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + args.ViewportOffsetY) * args.ViewportScale)
				, (int)((width) * args.ViewportScale), (int)((height) * args.ViewportScale)));


			g.FillRectangle(Brushes.DarkCyan, new((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + args.ViewportOffsetY) * args.ViewportScale)
				, (int)((width) * args.ViewportScale), (int)((30) * args.ViewportScale)));

			Font f = new(FontFamily.GenericMonospace, 15 * args.ViewportScale, FontStyle.Regular);

			g.DrawString(Name, f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + args.ViewportOffsetY) * args.ViewportScale)
				, (int)((width) * args.ViewportScale), (int)(( 30) * args.ViewportScale)),new StringFormat() { Alignment=StringAlignment.Center, LineAlignment = StringAlignment.Center});
		}

		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{
			(int, int) p = (X, Y) + dArgs;
			(int, int) d = (X + width, Y + 30) + dArgs;

			if (mArgs.PositionX >= p.Item1 && mArgs.PositionX <= d.Item1 &&
				mArgs.PositionY >= p.Item2 && mArgs.PositionY <= d.Item2
				)
			{
				Cursor.Current = Cursors.Hand;
				if (mArgs.Button == MouseButtons.Left || mArgs.LeftMouseDown)
				{

					X += mArgs.PositionXDelta/dArgs;
					Y += mArgs.PositionYDelta/dArgs;
				}

			}

		}
	}
}
