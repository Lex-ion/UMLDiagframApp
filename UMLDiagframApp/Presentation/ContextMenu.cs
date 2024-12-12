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

		public ContextMenu(int x, int y, int width, int height, List<ContextMenuCommand> commands) : base(x, y, width, height)
		{
			_commands = commands;

		}

		public override void Draw(DrawArgs args, Graphics g)
		{
			Rectangle r = new Rectangle(X+args.ViewportOffsetX,Y+args.ViewportOffsetY,width,height);
			g.FillRectangle(Brushes.LightCyan, r);
			g.DrawRectangle(Pens.DarkSlateBlue, r);

			g.DrawString("Lorem",new (FontFamily.GenericMonospace,15),Brushes.Black,new PointF(X+args.ViewportOffsetX,Y+args.ViewportOffsetY));

		}

		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{
		}

		public override bool IsSelected(int x, int y, DrawArgs args)
		{
			 isSelected= x >= (X + args.ViewportOffsetX)&& x <= (X + width + args.ViewportOffsetX) &&
				y >= (Y + args.ViewportOffsetY)  && y <= (Y + height + args.ViewportOffsetY) ;

			if (!isSelected)
			{
				Destroy();
			}

			return isSelected;
		}
	}
}
