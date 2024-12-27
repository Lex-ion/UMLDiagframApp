namespace UMLDiagframApp.Entities
{
	public class ConnectionNode
	{
		public int X { get; set; }
		public int Y { get; set; }

		public ConnectionNode? Before { get; set; }
		public ConnectionNode? After { get; set; }

		public ConnectionNode(int x, int y)
		{
			X = x;
			Y = y;
		}

		public ConnectionNode(int x, int y, ConnectionNode? before, ConnectionNode? after) : this(x, y)
		{
			Before = before;
			After = after;
		}

		
	}
}
