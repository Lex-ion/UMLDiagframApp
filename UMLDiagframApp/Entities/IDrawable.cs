using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public interface IDrawable
	{
		public int X { get; set; }
		public int Y { get; set; }

		public void Draw(DrawArgs args, Graphics g);
	}
}
