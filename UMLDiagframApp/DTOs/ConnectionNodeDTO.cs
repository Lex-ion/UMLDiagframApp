using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.DTOs
{
	public class ConnectionNodeDTO
	{

		public int X { get; set; }
		public int Y { get; set; }

		public ConnectionNodeDTO(int x, int y)
		{
			X = x;
			Y = y;
		}

		public ConnectionNodeDTO()
		{
			X = 0;
			Y = 0;
		}
	}
}
