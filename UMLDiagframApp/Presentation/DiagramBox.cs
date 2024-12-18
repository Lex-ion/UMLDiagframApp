using System;
using UMLDiagframApp.Entities;
using UMLDiagframApp.ValidationStrategies;

namespace UMLDiagframApp.Presentation
{
	public class DiagramBox : AbstractSelectable
	{
		public string Name { get; set; }
		public bool Changed { get; set; }

		private int? _selectionX;
		private int? _selectionY;
		private int _oldX;
		private int _oldY;
		bool focused;

		public int SelectedIndex { get; private set; } = -1;

		float lastScale;

		public List<Entities.Attribute> Attributes { get; }
		public List<Entities.Method> Methods { get; }


		public DiagramBox(string name, int x, int y, int width, int height) : base(x, y, width, height)
		{
			Name = name;
			_selectionX = null;
			_selectionY = null;

			Methods = new List<Entities.Method>();
			Attributes = new List<Entities.Attribute>();
		}



		public override void Draw(DrawArgs args, Graphics g)
		{
			Font f = new(FontFamily.GenericMonospace, 15 * args.ViewportScale, FontStyle.Regular);
			Font m = new(FontFamily.GenericMonospace, 15, FontStyle.Regular);

			if (lastScale != args.ViewportScale || Changed)
			{

				g.ResetTransform();
				g.ScaleTransform(args.ViewportScale, args.ViewportScale);
				width = (int)Math.Ceiling(g.MeasureString(Name + new string('x', 5), m).Width);

				foreach (var method in Methods)
				{
					int newWidth = (int)Math.Ceiling(g.MeasureString(method.ToString() + new string('x', 5), m).Width);
					width = newWidth > width ? newWidth : width;
				}

				foreach (var attribute in Attributes)
				{
					int newWidth = (int)Math.Ceiling(g.MeasureString(attribute.ToString() + new string('x', 5), m).Width);
					width = newWidth > width ? newWidth : width;
				}

				height =(int)( (Attributes.Count+Methods.Count+1)*30+30);

				g.ScaleTransform(1, 1);
				g.ResetTransform();

				Changed = false;
				lastScale = args.ViewportScale;
			}

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

			//	g.DrawString("X:" + X, f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + 30 + args.ViewportOffsetY) * args.ViewportScale)
			//		, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			//	g.DrawString("Y:" + Y, f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + 60 + args.ViewportOffsetY) * args.ViewportScale)
			//		, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			
			
			if (SelectedIndex > -1&&isSelected&&SelectedIndex!=Attributes.Count)
			{
				g.FillRectangle(Brushes.DarkSlateBlue, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + 30+30*SelectedIndex + args.ViewportOffsetY) * args.ViewportScale)
					, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)));
			}

			int i = 0;


			foreach (var a in Attributes)
			{
				g.DrawString(a.ToString(), f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y + 30 + i * 30 + args.ViewportOffsetY) * args.ViewportScale)
		, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
				i++;
			}

			int x = (int)((X +1+ args.ViewportOffsetX) * args.ViewportScale);
			int y = (int)((Y + 30 + i * 30+15 + args.ViewportOffsetY) * args.ViewportScale);
			g.DrawLine(Pens.DarkViolet, new(x, y), new((int)((X -1+ width + args.ViewportOffsetX) * args.ViewportScale), y));

			foreach (var method in Methods)
			{
				g.DrawString(method.ToString(), f, Brushes.Black, new RectangleF((int)((X + args.ViewportOffsetX) * args.ViewportScale), (int)((Y +30+ 30 + i * 30 + args.ViewportOffsetY) * args.ViewportScale)
			, (int)(width * args.ViewportScale), (int)(30 * args.ViewportScale)), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
				i++;
			}


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


			if(isSelected)
			{
				int mouseY = mArgs.PositionY-p.Item2;
				if (mouseY >= 30*dArgs.ViewportScale)
				{
					SelectedIndex =(int)( (mouseY - 30*dArgs.ViewportScale) /( 30*dArgs.ViewportScale));
				}
				else
					SelectedIndex = -1;
				if (SelectedIndex >= Methods.Count + Attributes.Count + 1)
					SelectedIndex = -1;
			}

		}

		public void UpdateItem()
		{
			if (SelectedIndex == -1||SelectedIndex==Attributes.Count)
				return;
			bool isAttribute=SelectedIndex<Attributes.Count;

			IValidationStrategy strategy = isAttribute ? new AttributeValidationStrategy() : new MethodValidationStrategy();
			if(isAttribute)
			{
				Entities.Attribute attribute = Attributes[SelectedIndex];
				TextInputForm t = new TextInputForm(attribute.ToString()!, strategy);
				t.ShowDialog();
				if (t.DialogResult == DialogResult.Abort)
					return;
				Attributes[SelectedIndex] =Entities. Attribute.CreateFromString(t.Value);

			}
			else
			{
				int i = SelectedIndex - Attributes.Count-1;
				Method method = Methods[i];

				TextInputForm t = new TextInputForm(method.ToString()!, strategy);
				t.ShowDialog();
				if (t.DialogResult == DialogResult.Abort)
					return;
				Methods[i]=Method.CreateFromString(t.Value);
			}
			Changed = true;
		}

		public void RemoveItem()
		{
			if (SelectedIndex == -1 || SelectedIndex == Attributes.Count)
				return;

			if (SelectedIndex < Attributes.Count)
			{
				Attributes.RemoveAt(SelectedIndex);

			}
			else
			{
				int i = SelectedIndex - Attributes.Count - 1;
				Methods.RemoveAt(i); ;
			}

			Changed = true;

			SelectedIndex = -1;
		}


	}

}
