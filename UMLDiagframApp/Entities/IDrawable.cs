using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	interface IDrawable
	{
		public Vector<int> Position { get; set; }


		public void Draw();
	}
}
