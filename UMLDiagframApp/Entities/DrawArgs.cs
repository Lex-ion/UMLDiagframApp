using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public struct DrawArgs
	{
		public int ViewportSizeX { get; set; }
		public int ViewportSizeY { get; set; }

		public int ViewportOffsetX { get; set; }
		public int ViewportOffsetY { get; set; }
		public float ViewportScale { get; set; }

		public DrawArgs(int viewportSizeX, int viewportSizeY, int viewportOffsetX, int viewportOffsetY, float viewportScale)
		{
			ViewportSizeX = viewportSizeX;
			ViewportSizeY = viewportSizeY;
			ViewportOffsetX = viewportOffsetX;
			ViewportOffsetY = viewportOffsetY;
			ViewportScale = viewportScale;
		}

		public static (int, int) operator +(DrawArgs drawArgs, Vector2 position)
			=> new((int)((position.X + drawArgs.ViewportOffsetX) * drawArgs.ViewportScale), (int)((position.Y+drawArgs.ViewportOffsetY)*drawArgs.ViewportScale));


		public static (int, int) operator +(DrawArgs drawArgs, (int,int) position)
			=> new((int)((position.Item1 + drawArgs.ViewportOffsetX) * drawArgs.ViewportScale), (int)((position.Item2 + drawArgs.ViewportOffsetY) * drawArgs.ViewportScale));

		public static (int, int) operator +((int,int) position, DrawArgs drawArgs)
			=> new((int)((position.Item1 + drawArgs.ViewportOffsetX) * drawArgs.ViewportScale), (int)((position.Item2 + drawArgs.ViewportOffsetY) * drawArgs.ViewportScale));



		public static int operator *(DrawArgs drawArgs, int i)
			=> (int)(i*drawArgs.ViewportScale);
		public static int operator *( int i, DrawArgs drawArgs)
		=> (int)(i * drawArgs.ViewportScale);

		public static int operator /(int i, DrawArgs drawArgs)
		=> (int)(i / drawArgs.ViewportScale);
	}
}
