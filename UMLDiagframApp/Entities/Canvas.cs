using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public class Canvas
	{
		private List<IDrawable> drawables;

		public Canvas()
		{
			drawables = new List<IDrawable>();
		}

		public void Draw(Graphics g)
		{

		}

	}
}
