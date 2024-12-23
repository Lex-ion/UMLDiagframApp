using System.Numerics;
using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
	public class ConnectionLine : AbstractSelectable
	{
		public DiagramBoxPair DiagramBoxPair { get; set; }

		List<ConnectionNode> _nodes;

		public ConnectionNode? SelectedNode { get; private set; } 

		double dist;

		public ConnectionLine(DiagramBox firstBox, DiagramBox secondBox) : base(0, 0, 50, 50)
		{
			DiagramBoxPair = new DiagramBoxPair(firstBox, secondBox);
			_nodes = new List<ConnectionNode>() {};

		}

		public override void Draw(DrawArgs args, Graphics g)
		{

			(int, int) firstCords = (DiagramBoxPair.First.CenterX, DiagramBoxPair.First.CenterY) + args;
			(int, int) lastCords = (DiagramBoxPair.Second.CenterX, DiagramBoxPair.Second.CenterY) + args;

			if (_nodes.Count <= 0)
			{
				g.DrawLine(Pens.Azure, new(firstCords.Item1, firstCords.Item2), new(lastCords.Item1, lastCords.Item2));
			}
			else
			{
				(int, int) cords = (_nodes[0].X, _nodes[0].Y) + args;

				g.DrawLine(Pens.Azure, new(firstCords.Item1, firstCords.Item2), new(cords.Item1, cords.Item2));

				for (int i = 0; i < _nodes.Count; i++)
				{
					if (i + 1 >= _nodes.Count)
					{
						g.DrawLine(Pens.Azure, new(cords.Item1, cords.Item2), new(lastCords.Item1, lastCords.Item2));
						break;
					}

					(int, int) nextCords = (_nodes[i + 1].X, _nodes[i + 1].Y) + args;

					g.DrawLine(Pens.Azure, new(cords.Item1, cords.Item2), new(nextCords.Item1, nextCords.Item2));
					cords = nextCords;

				}
			}
			g.DrawString(dist.ToString(), SystemFonts.DefaultFont, Brushes.Black, new Point(0, 0));
		}

		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{
			Cursor.Current = Cursors.Hand;
			//	throw new NotImplementedException();
		}

		public override bool IsSelected(int x, int y, DrawArgs args)
		{

			(int, int) firstCords = (DiagramBoxPair.First.CenterX, DiagramBoxPair.First.CenterY) + args;
			(int, int) lastCords = (DiagramBoxPair.Second.CenterX, DiagramBoxPair.Second.CenterY) + args;


			Vector2 vF = new(firstCords.Item1, firstCords.Item2);
			Vector2 vS = new(lastCords.Item1,lastCords.Item2);



			if (_nodes.Count <= 0)
			{


				

				return SegmentSelcted(firstCords,lastCords);


			}
			else
			{


				(int, int) cords = (_nodes[0].X, _nodes[0].Y) + args;

				if (SegmentSelcted(firstCords, cords))
					return true;

				for (int i = 0; i < _nodes.Count; i++)
				{
					if (i + 1 >= _nodes.Count)
					{
						return SegmentSelcted(cords, lastCords);
					}

					(int, int) nextCords = (_nodes[i + 1].X, _nodes[i + 1].Y) + args;
					if (SegmentSelcted(cords, nextCords))
						return true;

					cords = nextCords;

				}
				

			}

			return false;


			bool SegmentSelcted((int, int) first, (int,int) second)
			{

				Rectangle bounds = new Rectangle(
					Math.Min(first.Item1, second.Item1),
					Math.Min(first.Item2, second.Item2),
					Math.Abs(first.Item1 - second.Item1),
					Math.Abs(first.Item2 - second.Item2));

				if (bounds.Width < 15)
					bounds.Width = 15;
				if (bounds.Height < 15)
					bounds.Height = 15;

				if (!bounds.Contains(x, y))
					return false;

				Vector2 vN = (new Vector2 (second.Item1,second.Item2) - new Vector2(first.Item1,first.Item2));
				vN = new Vector2(vN.Y, -vN.X);

				double c = -(first.Item1 * vN.X + first.Item2 * vN.Y);


				double numerator = Math.Abs(vN.X * x + vN.Y * y + c);
				double denominator = Math.Sqrt(vN.X * vN.X + vN.Y * vN.Y);

				dist = numerator / denominator;

				return numerator / denominator < 15;
			}
		}


		
	}
}
