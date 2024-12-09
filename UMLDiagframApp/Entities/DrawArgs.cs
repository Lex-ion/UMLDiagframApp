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
	}
}
