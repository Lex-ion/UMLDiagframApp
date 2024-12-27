using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public struct ConnectionLineSegment
	{
		public ConnectionNode? First;
		public ConnectionNode? Last;

		public ConnectionLineSegment(ConnectionNode? first, ConnectionNode? last)
		{
			First = first;
			Last = last;
		}
	}
}
