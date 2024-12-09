using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public class Circle : IDrawable
	{
		public int X { get => _x; set =>_x=value; }

		private int _x;


		public int Y { get => _y; set => _y=value; }
		private int _y;

		public Circle(int x, int y)
		{
			_x = x;
			_y = y;
		}

		public void Draw(DrawArgs args, Graphics g)
		{
			g.FillEllipse(Brushes.Black,( X + args.ViewportOffsetX)*args.ViewportScale,( Y + args.ViewportOffsetY) * args.ViewportScale, 15*args.ViewportScale , 15 *args.ViewportScale);
		}
	}
}
