using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
	public class ContextMenu : AbstractSelectable
	{
		private List<ContextMenuCommand> _commands;
		private Font _font;

		int itemHeight;

		int selectedIndex;

		public ContextMenu(int x, int y,  List<ContextMenuCommand> commands) : base(x, y, 0, 0)
		{ 
			_font = new(FontFamily.GenericMonospace, 15);
			_commands = commands;

			itemHeight = TextRenderer.MeasureText("0", _font).Height;

			Height = itemHeight*commands.Count;

			foreach (ContextMenuCommand command in _commands)
			{
				int w =	TextRenderer.MeasureText(command.Name,_font).Width;
				if (w > base.Width) {
				base.Width = w;
				}
			}
		}

		public override void Draw(DrawArgs args, Graphics g)
		{
			Rectangle r = new Rectangle(X+args.ViewportOffsetX,Y+args.ViewportOffsetY,Width,Height);
			g.FillRectangle(Brushes.LightCyan, r);
			g.DrawRectangle(Pens.DarkSlateBlue, r);

			
			int i = 0;
			foreach (var c in _commands)
			{
				if (i == selectedIndex)
				{
					g.FillRectangle(Brushes.WhiteSmoke, new(X+args.ViewportOffsetX, Y + args.ViewportOffsetY + i * itemHeight,Width,itemHeight));
				}

				g.DrawString(c.Name, _font, Brushes.Black, new PointF(X + args.ViewportOffsetX, Y + args.ViewportOffsetY+i*itemHeight));
				i++;
			}
		}

		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{
			selectedIndex = (mArgs.PositionY - Y-dArgs.ViewportOffsetY) / itemHeight;

			if (mArgs.ButtonState == MouseButtonsStates.LeftUp)
			{
				if (selectedIndex >= 0 && selectedIndex < _commands.Count)
				{
				_commands[selectedIndex].Command.Invoke();

				Destroy();
				}

			}
		}

		public override bool IsSelected(int x, int y, DrawArgs args)
		{
			 isSelected= x >= (X + args.ViewportOffsetX)&& x <= (X + Width + args.ViewportOffsetX) &&
				y >= (Y + args.ViewportOffsetY)  && y <= (Y + Height + args.ViewportOffsetY) ;

			if (!isSelected)
			{
				Destroy();
			}

			return isSelected;
		}


	}
}
