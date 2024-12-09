using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public struct MouseArgs
	{
		public readonly MouseButtons Button { get;  }

		public readonly int PostitionX { get; }
		public readonly int PostitionY { get; }

		public readonly int PostitionXDelta { get; }
		public readonly int PostitionYDelta { get; }


		public readonly int Scroll { get; }

		public readonly bool RightMouseDown { get; }
		public readonly bool LeftMouseDown { get; }

		public MouseArgs(MouseButtons button, int postitionX, int postitionY, int postitionXDelta, int postitionYDelta, int scroll, bool rightMouseDown, bool leftMouseDown)
		{
			Button = button;
			PostitionX = postitionX;
			PostitionY = postitionY;
			PostitionXDelta = postitionXDelta;
			PostitionYDelta = postitionYDelta;
			Scroll = scroll;
			RightMouseDown = rightMouseDown;
			LeftMouseDown = leftMouseDown;
		}
	}

	
}
