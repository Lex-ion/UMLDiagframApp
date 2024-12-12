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
		public readonly MouseButtons Button { get; }
		public readonly MouseButtonsStates ButtonState { get; }

		public readonly int PositionX { get; }
		public readonly int PositionY { get; }


   

        public  int PositionXDelta { get; set; }
		public  int PositionYDelta { get; set; }


		public readonly int Scroll { get; }

		public readonly bool RightMouseDown { get; }
		public readonly bool LeftMouseDown { get; }

		public MouseArgs(MouseButtons button,MouseArgs? oldArgs, int positionX, int positionY, int positionXDelta, int positionYDelta, int scroll, bool rightMouseDown, bool leftMouseDown)
		{
			

			Button = button;
			PositionX = positionX;
			PositionY = positionY;
			PositionXDelta = positionXDelta;
			PositionYDelta = positionYDelta;
			Scroll = scroll;
			RightMouseDown = rightMouseDown;
			LeftMouseDown = leftMouseDown;


			if ((int)button == (int)(oldArgs?.Button??Button))
			{
				if (button == MouseButtons.None)
					return;
				ButtonState = (MouseButtonsStates)((int)button+1);
			}

			else if (button==MouseButtons.None&& (oldArgs?.Button ?? Button)!=MouseButtons.None)
			{
				ButtonState= (MouseButtonsStates)((int)(oldArgs?.Button ?? Button) + 2);
			}

			else if ((oldArgs?.Button ?? Button) == MouseButtons.None)
			{
				ButtonState= (MouseButtonsStates)Button;
			}

			
		}
	}

	
}
