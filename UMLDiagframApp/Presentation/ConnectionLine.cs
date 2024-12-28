using System.Numerics;
using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
	public class ConnectionLine : AbstractSelectable
	{
		public DiagramBoxPair DiagramBoxPair { get; set; }

		ConnectionNodeList _nodes;

		public ConnectionNode? SelectedNode { get; private set; }

		private ConnectionLineSegment? _selectedSegment;
		private ConnectionNode? _selectedNode;



		private int? _selectionX;
		private int? _selectionY;
		private int _oldX;
		private int _oldY;

		double dist;

		public ConnectionLine(DiagramBox firstBox, DiagramBox secondBox) : base(0, 0, 50, 50)
		{
			DiagramBoxPair = new DiagramBoxPair(firstBox, secondBox);

			_nodes = new ConnectionNodeList();
		/*	_nodes.Add(new(0, 0));
			_nodes.Add(new(100, 120));
			_nodes.Add(new(100, 140));
			_nodes.Add(new(100, 160));
			_nodes.Add(new(100, 180));
			_nodes.Add(new(100, 200));
			_nodes.Add(new(-200, -300));*/
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
				(int, int) cords = (_nodes.First!.X, _nodes.First!.Y) + args;

				ConnectionNode? selectedNode = _nodes.First!;

				g.DrawLine(Pens.Azure, new(firstCords.Item1, firstCords.Item2), new(cords.Item1, cords.Item2));

				for (int i = 0; i < _nodes.Count; i++)
				{
					if (i + 1 >= _nodes.Count)
					{
						g.DrawLine(Pens.Azure, new(cords.Item1, cords.Item2), new(lastCords.Item1, lastCords.Item2));

						if (isSelected)
						{
							if (selectedNode == _selectedNode)
								g.FillEllipse(Brushes.LightGoldenrodYellow, new RectangleF(cords.Item1 - 5, cords.Item2 - 5, 10, 10));
							else
								g.DrawEllipse(Pens.LightGoldenrodYellow, new RectangleF(cords.Item1 - 5, cords.Item2 - 5, 10, 10));
						}

						break;
					}

					(int, int) nextCords = (selectedNode.After!.X, selectedNode.After!.Y) + args;

					g.DrawLine(Pens.Azure, new(cords.Item1, cords.Item2), new(nextCords.Item1, nextCords.Item2));
					if (isSelected)
					{
						if (selectedNode == _selectedNode)
							g.FillEllipse(Brushes.LightGoldenrodYellow, new RectangleF(cords.Item1 - 5, cords.Item2 - 5, 10, 10));
						else
							g.DrawEllipse(Pens.LightGoldenrodYellow, new RectangleF(cords.Item1 - 5, cords.Item2 - 5, 10, 10));
					}
					selectedNode = selectedNode.After;
					cords = nextCords;

				}
			}
			g.DrawString(dist.ToString(), SystemFonts.DefaultFont, Brushes.Black, new Point(0, 0));
		}

		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{
			Cursor.Current = Cursors.Hand;



			if (mArgs.ButtonState == MouseButtonsStates.LeftDown && _selectedNode is null && _selectedSegment is not null)
			{
				(int, int) cords = (mArgs.PositionX-dArgs.ViewportOffsetX, mArgs.PositionY-dArgs.ViewportOffsetY);
				var newNode = new ConnectionNode(cords.Item1, cords.Item2);
				_selectedNode = newNode;

				if (_selectedSegment?.First is not null && _selectedSegment?.Last is not null)
				{
					_nodes.AddAfter(_selectedSegment?.First, newNode);
				}
				else if (_selectedSegment?.First is null && _selectedSegment?.Last is not null)
				{
					_nodes.AddBefore(_selectedSegment?.Last,newNode);
				}
				else if(_selectedSegment?.First is not null && _selectedSegment?.Last is null)
				{
					_nodes.AddAfter(_selectedSegment?.First, newNode);
				}

			}
			else if (mArgs.ButtonState == MouseButtonsStates.LeftDown && _selectedNode is null)
			{

				(int, int) cords = (mArgs.PositionX - dArgs.ViewportOffsetX, mArgs.PositionY - dArgs.ViewportOffsetY);
				var newNode = new ConnectionNode(cords.Item1, cords.Item2);
				_selectedNode = newNode;
				_nodes.Add(newNode);
			}

			if (_selectedNode is not null && mArgs.ButtonState == MouseButtonsStates.LeftDown)
			{
				_selectionX = mArgs.PositionX;
				_selectionY = mArgs.PositionY;

				_oldX = _selectedNode.X; _oldY = _selectedNode.Y;
			}

			if (_selectedNode is not null && (mArgs.Button == MouseButtons.Left || mArgs.LeftMouseDown) && _selectionX is not null && _selectionY is not null)
			{

				int dX = X - dArgs.ViewportOffsetX;

				_selectedNode.X = _oldX + (mArgs.PositionX - (int)_selectionX) / dArgs;
				_selectedNode.Y = _oldY + (mArgs.PositionY - (int)_selectionY) / dArgs;

			}



			if (_selectedNode is not null && mArgs.ButtonState == MouseButtonsStates.LeftUp)
			{
				Vector2 v = new(_selectedNode.X, _selectedNode.Y);
				if (_selectedNode.After is not null)
					if (Vector2.Distance(v, new(_selectedNode.After.X, _selectedNode.After.Y)) <= 20)
						_nodes.RemoveAfter(_selectedNode);

				if (_selectedNode.Before is not null)
					if (Vector2.Distance(v, new(_selectedNode.Before.X, _selectedNode.Before.Y)) <= 20)
						_nodes.RemoveBefore(_selectedNode);

				if (new Rectangle(DiagramBoxPair.First.X, DiagramBoxPair.First.Y, DiagramBoxPair.First.Width, DiagramBoxPair.First.Height).Contains(_selectedNode.X, _selectedNode.Y))
					_nodes.Remove(_selectedNode);

				else if (new Rectangle(DiagramBoxPair.Second.X, DiagramBoxPair.Second.Y, DiagramBoxPair.Second.Width, DiagramBoxPair.Second.Height).Contains(_selectedNode.X, _selectedNode.Y))
					_nodes.Remove(_selectedNode);

				_selectionX = null;
				_selectionY = null;
			}

			//	throw new NotImplementedException();
		}

		public override bool IsSelected(int x, int y, DrawArgs args)
		{
			isSelected = false;
			(int, int) firstCords = (DiagramBoxPair.First.CenterX, DiagramBoxPair.First.CenterY) + args;
			(int, int) lastCords = (DiagramBoxPair.Second.CenterX, DiagramBoxPair.Second.CenterY) + args;


			Vector2 vF = new(firstCords.Item1, firstCords.Item2);
			Vector2 vS = new(lastCords.Item1, lastCords.Item2);

			_selectedSegment = null;
			_selectedNode = null;

			if (_nodes.Count <= 0)
			{

				return SegmentSelcted(firstCords, lastCords);
			}
			else
			{
				(int, int) cords = (_nodes.First!.X, _nodes.First!.Y) + args;
				var selectedNode = _nodes.First!;

				Vector2 v2 = new(x, y);
				Vector2 v1 = new(cords.Item1, cords.Item2);
				if (Vector2.Distance(v1, v2) <= 12)
				{
					_selectedNode = selectedNode;
					isSelected = true;
					return true;
				}


				if (SegmentSelcted(firstCords, cords))
				{
					_selectedSegment = new(null, selectedNode);
					return true;
				}

				for (int i = 0; i < _nodes.Count; i++)
				{
					v1 = new(cords.Item1, cords.Item2);
					if (Vector2.Distance(v1, v2) <= 12)
					{
						isSelected = true;
						_selectedNode = selectedNode;
						return true;
					}
					if (i + 1 >= _nodes.Count)
					{
						bool b = SegmentSelcted(cords, lastCords);
						if (b)
							_selectedSegment = new(selectedNode, null);
						return b;
					}

					(int, int) nextCords = (selectedNode.After!.X, selectedNode.After!.Y) + args;

					if (SegmentSelcted(cords, nextCords))
					{
						_selectedSegment = new(selectedNode, selectedNode.After);
						return true;
					}

					selectedNode = selectedNode.After;
					cords = nextCords;

				}


			}

			return false;


			bool SegmentSelcted((int, int) first, (int, int) second)
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

				Vector2 vN = (new Vector2(second.Item1, second.Item2) - new Vector2(first.Item1, first.Item2));
				vN = new Vector2(vN.Y, -vN.X);

				double c = -(first.Item1 * vN.X + first.Item2 * vN.Y);


				double numerator = Math.Abs(vN.X * x + vN.Y * y + c);
				double denominator = Math.Sqrt(vN.X * vN.X + vN.Y * vN.Y);

				dist = numerator / denominator;


				isSelected = numerator / denominator < 15;
				return isSelected;
			}
		}
	}
}
