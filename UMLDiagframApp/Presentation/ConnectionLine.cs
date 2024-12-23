using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
	public class ConnectionLine : AbstractSelectable
	{
		public DiagramBoxPair DiagramBoxPair { get; set; }

		List<ConnectionLine> _nodes;



		public ConnectionLine(DiagramBox firstBox, DiagramBox secondBox) : base(0, 0, 50, 50)
		{
			DiagramBoxPair = new DiagramBoxPair(firstBox, secondBox);
			_nodes = new List<ConnectionLine>();
		}

		public override void Draw(DrawArgs args, Graphics g)
		{

			(int, int) firstCords = (DiagramBoxPair.First.X, DiagramBoxPair.First.Y) + args;
			(int, int) lastCords = (DiagramBoxPair.Second.X, DiagramBoxPair.Second.Y) + args;

			if (_nodes.Count <= 0)
			{
				g.DrawLine(Pens.Azure, new(firstCords.Item1, firstCords.Item2), new(lastCords.Item1, lastCords.Item2));
			}
			else
			{


				for (int i = 0; i < _nodes.Count; i++)
				{

				}
			}
		}

		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{
			throw new NotImplementedException();
		}
	}
}
