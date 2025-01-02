using System.Numerics;
using UMLDiagframApp.DTOs;
using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
	public class ConnectionLine : AbstractSelectable
	{
		public DiagramBoxPair DiagramBoxPair { get; set; }

		public string FirstMultiplicity { get; set; }
		public string SecondMultiplicity { get; set; }
		public ConnectionType ConnectionType { get; set; }

		private ConnectionLineSegment? _selectedSegment;
		private ConnectionNode? _selectedNode;

		public ConnectionNodeList Nodes { get; set; }


		private int? _selectionX;
		private int? _selectionY;
		private int _oldX;
		private int _oldY;

		double dist;

		private PointF? _oldFirstIntersection;
		private PointF? _oldSecondtIntersection;

		public ConnectionLine(DiagramBox firstBox, DiagramBox secondBox) : base(firstBox.X, firstBox.Y, 0, 0)
		{
			DiagramBoxPair = new DiagramBoxPair(firstBox, secondBox);

			Nodes = new ConnectionNodeList();

			FirstMultiplicity = "1";
			SecondMultiplicity = "1";

			ConnectionType = ConnectionType.Asociation;
		}

		public ConnectionLine(ConnectionLineDTO dto, DiagramBoxPair pair) : base(dto.X, dto.Y, dto.Width, dto.Height)
		{
			Nodes = new ConnectionNodeList();
			DiagramBoxPair = pair;
			FirstMultiplicity = dto.FirstMultiplicity;
			SecondMultiplicity = dto.SecondMultiplicity;
			ConnectionType = dto.ConnectionType;
			foreach (var node in dto.Nodes)
			{
				Nodes.Add(new(node.X, node.Y));
			}

		}

		public override void Draw(DrawArgs args, Graphics g)
		{


			(int, int) firstCords = (DiagramBoxPair.First.CenterX, DiagramBoxPair.First.CenterY) + args;
			(int, int) lastCords = (DiagramBoxPair.Second.CenterX, DiagramBoxPair.Second.CenterY) + args;

			Font f = new(FontFamily.GenericMonospace, 15 * args.ViewportScale, FontStyle.Regular);
			if (Nodes.Count <= 0)
			{
				g.DrawLine(Pens.Azure, new(firstCords.Item1, firstCords.Item2), new(lastCords.Item1, lastCords.Item2));
				var intersection = GetLineRectangleIntersection(firstCords, lastCords, args * (DiagramBoxPair.First.X + args.ViewportOffsetX), args * (DiagramBoxPair.First.Y + args.ViewportOffsetY), (DiagramBoxPair.First.X + DiagramBoxPair.First.Width + args.ViewportOffsetX) * args, (DiagramBoxPair.First.Height + DiagramBoxPair.First.Y + args.ViewportOffsetY) * args);
				if (intersection.HasValue)
					_oldFirstIntersection = intersection.Value;

				if (_oldFirstIntersection.HasValue)
				{

					Vector2 v = new(_oldFirstIntersection.Value.X - firstCords.Item1, _oldFirstIntersection.Value.Y - firstCords.Item2);
					v = Vector2.Normalize(v);
					var u = new Vector2(v.Y, -v.X);

					Point left = new((int)(_oldFirstIntersection.Value.X + v.X * 25 * args + -u.X * 15 * args), (int)(_oldFirstIntersection.Value.Y + v.Y * 25 * args + -u.Y * 15 * args));
					Point right = new((int)(_oldFirstIntersection.Value.X + v.X * 25 * args + u.X * 15 * args), (int)(_oldFirstIntersection.Value.Y + v.Y * 25 * args + u.Y * 15 * args));
					switch (ConnectionType)
					{
						case ConnectionType.Asociation:

							break;
						case ConnectionType.OneWayAsociation:
							g.DrawLine(Pens.Azure, _oldFirstIntersection.Value, left);
							g.DrawLine(Pens.Azure, _oldFirstIntersection.Value, right);
							break;
						case ConnectionType.Agregation:
							g.DrawPolygon(Pens.Azure, [_oldFirstIntersection.Value, right, new(_oldFirstIntersection.Value.X + v.X * 50 * args, _oldFirstIntersection.Value.Y + v.Y * 50 * args), left]);
							break;
						case ConnectionType.Composition:
							g.FillPolygon(Brushes.Azure, [_oldFirstIntersection.Value, right, new(_oldFirstIntersection.Value.X + v.X * 50 * args, _oldFirstIntersection.Value.Y + v.Y * 50 * args), left]);
							break;
						case ConnectionType.Generalization:
							g.DrawPolygon(Pens.Azure, [_oldFirstIntersection.Value, right, left]);
							break;
					}

					if (ConnectionType != ConnectionType.Generalization)
						g.DrawString(FirstMultiplicity, f, Brushes.Black, _oldFirstIntersection.Value.X + v.X * 50, _oldFirstIntersection.Value.Y + v.Y * 50);

				}
				var intersection2 = GetLineRectangleIntersection(firstCords, lastCords, args * (DiagramBoxPair.Second.X + args.ViewportOffsetX), args * (DiagramBoxPair.Second.Y + args.ViewportOffsetY), (DiagramBoxPair.Second.X + DiagramBoxPair.Second.Width + args.ViewportOffsetX) * args, (DiagramBoxPair.Second.Height + DiagramBoxPair.Second.Y + args.ViewportOffsetY) * args);

				if (intersection2.HasValue)
					_oldSecondtIntersection = intersection2.Value;
				if (_oldSecondtIntersection.HasValue)
				{
					Vector2 v = new(_oldSecondtIntersection.Value.X - lastCords.Item1, _oldSecondtIntersection.Value.Y - lastCords.Item2);
					v = Vector2.Normalize(v);

					if (ConnectionType != ConnectionType.Generalization)
						g.DrawString(SecondMultiplicity, f, Brushes.Black, _oldSecondtIntersection.Value.X + v.X * 50 * args, _oldSecondtIntersection.Value.Y + v.Y * 50 * args);
				}
			}
			else
			{
				(int, int) cords = (Nodes.First!.X, Nodes.First!.Y) + args;

				ConnectionNode? selectedNode = Nodes.First!;

				g.DrawLine(Pens.Azure, new(firstCords.Item1, firstCords.Item2), new(cords.Item1, cords.Item2));

				var intersection = GetLineRectangleIntersection(firstCords, cords, args * (DiagramBoxPair.First.X + args.ViewportOffsetX), args * (DiagramBoxPair.First.Y + args.ViewportOffsetY), (DiagramBoxPair.First.X + DiagramBoxPair.First.Width + args.ViewportOffsetX) * args, (DiagramBoxPair.First.Height + DiagramBoxPair.First.Y + args.ViewportOffsetY) * args);
				if (intersection.HasValue)
					_oldFirstIntersection = intersection.Value;

				if (_oldFirstIntersection.HasValue)
				{

					Vector2 v = new(_oldFirstIntersection.Value.X - firstCords.Item1, _oldFirstIntersection.Value.Y - firstCords.Item2);
					v = Vector2.Normalize(v);

					var u = new Vector2(v.Y, -v.X);



					Point left = new((int)(_oldFirstIntersection.Value.X + v.X * 25 * args + -u.X * 15 * args), (int)(_oldFirstIntersection.Value.Y + v.Y * 25 * args + -u.Y * 15 * args));
					Point right = new((int)(_oldFirstIntersection.Value.X + v.X * 25 * args + u.X * 15 * args), (int)(_oldFirstIntersection.Value.Y + v.Y * 25 * args + u.Y * 15 * args));
					switch (ConnectionType)
					{
						case ConnectionType.Asociation:

							break;
						case ConnectionType.OneWayAsociation:
							g.DrawLine(Pens.Azure, _oldFirstIntersection.Value, left);
							g.DrawLine(Pens.Azure, _oldFirstIntersection.Value, right);
							break;
						case ConnectionType.Agregation:
							g.DrawPolygon(Pens.Azure, [_oldFirstIntersection.Value, right, new(_oldFirstIntersection.Value.X + v.X * 50 * args, _oldFirstIntersection.Value.Y + v.Y * 50 * args), left]);
							break;
						case ConnectionType.Composition:
							g.FillPolygon(Brushes.Azure, [_oldFirstIntersection.Value, right, new(_oldFirstIntersection.Value.X + v.X * 50 * args, _oldFirstIntersection.Value.Y + v.Y * 50 * args), left]);
							break;
						case ConnectionType.Generalization:
							g.DrawPolygon(Pens.Azure, [_oldFirstIntersection.Value, right, left]);
							break;
					}

					if (ConnectionType != ConnectionType.Generalization)
						g.DrawString(FirstMultiplicity, f, Brushes.Black, _oldFirstIntersection.Value.X + v.X * 50 * args, _oldFirstIntersection.Value.Y + v.Y * 50 * args);


				}


				for (int i = 0; i < Nodes.Count; i++)
				{
					if (i + 1 >= Nodes.Count)
					{
						g.DrawLine(Pens.Azure, new(cords.Item1, cords.Item2), new(lastCords.Item1, lastCords.Item2));

						if (isSelected)
						{
							if (selectedNode == _selectedNode)
								g.FillEllipse(Brushes.LightGoldenrodYellow, new RectangleF(cords.Item1 - 5, cords.Item2 - 5, 10, 10));
							else
								g.DrawEllipse(Pens.LightGoldenrodYellow, new RectangleF(cords.Item1 - 5, cords.Item2 - 5, 10, 10));
						}


						var intersection2 = GetLineRectangleIntersection(lastCords, cords, args * (DiagramBoxPair.Second.X + args.ViewportOffsetX), args * (DiagramBoxPair.Second.Y + args.ViewportOffsetY), (DiagramBoxPair.Second.X + DiagramBoxPair.Second.Width + args.ViewportOffsetX) * args, (DiagramBoxPair.Second.Height + DiagramBoxPair.Second.Y + args.ViewportOffsetY) * args);

						if (intersection2.HasValue)
							_oldSecondtIntersection = intersection2.Value;
						if (_oldSecondtIntersection.HasValue)
						{
							Vector2 v = new(_oldSecondtIntersection.Value.X - lastCords.Item1, _oldSecondtIntersection.Value.Y - lastCords.Item2);
							v = Vector2.Normalize(v);

							if (ConnectionType != ConnectionType.Generalization)
								g.DrawString(SecondMultiplicity, f, Brushes.Black, _oldSecondtIntersection.Value.X + v.X * 50, _oldSecondtIntersection.Value.Y + v.Y * 50);
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
			//	g.DrawString(dist.ToString(), SystemFonts.DefaultFont, Brushes.Black, new Point(0, 0));
		}


		public override void MouseInput(MouseArgs mArgs, DrawArgs dArgs)
		{
			Cursor.Current = Cursors.Hand;


			(int, int) cords = ((mArgs.PositionX / dArgs - dArgs.ViewportOffsetX), (mArgs.PositionY / dArgs - dArgs.ViewportOffsetY));

			if (mArgs.ButtonState == MouseButtonsStates.LeftDown && _selectedNode is null && _selectedSegment is not null)
			{
				var newNode = new ConnectionNode(cords.Item1, cords.Item2);
				_selectedNode = newNode;

				if (_selectedSegment?.First is not null && _selectedSegment?.Last is not null)
				{
					Nodes.AddAfter(_selectedSegment.Value.First, newNode);
				}
				else if (_selectedSegment?.First is null && _selectedSegment?.Last is not null)
				{
					Nodes.AddBefore(_selectedSegment.Value.Last, newNode);
				}
				else if (_selectedSegment?.First is not null && _selectedSegment?.Last is null)
				{
					Nodes.AddAfter(_selectedSegment!.Value.First, newNode);
				}

			}
			else if (mArgs.ButtonState == MouseButtonsStates.LeftDown && _selectedNode is null)
			{

				var newNode = new ConnectionNode(cords.Item1, cords.Item2);
				_selectedNode = newNode;
				Nodes.Add(newNode);
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
						Nodes.RemoveAfter(_selectedNode);

				if (_selectedNode.Before is not null)
					if (Vector2.Distance(v, new(_selectedNode.Before.X, _selectedNode.Before.Y)) <= 20)
						Nodes.RemoveBefore(_selectedNode);

				if (new Rectangle(DiagramBoxPair.First.X, DiagramBoxPair.First.Y, DiagramBoxPair.First.Width, DiagramBoxPair.First.Height).Contains(_selectedNode.X, _selectedNode.Y))
					Nodes.Remove(_selectedNode);

				else if (new Rectangle(DiagramBoxPair.Second.X, DiagramBoxPair.Second.Y, DiagramBoxPair.Second.Width, DiagramBoxPair.Second.Height).Contains(_selectedNode.X, _selectedNode.Y))
					Nodes.Remove(_selectedNode);

				_selectionX = null;
				_selectionY = null;

				var currNode = Nodes.First;
				int minX = Nodes.First?.X ?? 0;
				int minY = Nodes.First?.Y ?? 0;
				int maxX = Nodes.First?.X ?? 0;
				int maxY = Nodes.First?.Y ?? 0;
				while (currNode!.After != null)
				{
					currNode = currNode.After;
					if (currNode.X < minX) minX = currNode.X;
					if (currNode.Y < minY) minY = currNode.Y;
					if (currNode.X > maxX) maxX = currNode.X;
					if (currNode.Y > maxY) maxY = currNode.Y;
				}
				X = minX;
				Y = minY;
				Width = Math.Abs(maxX - minX);
				Height = Math.Abs(maxY - minY);
			}



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

			if (Nodes.Count <= 0)
			{

				return SegmentSelcted(firstCords, lastCords);
			}
			else
			{
				(int, int) cords = (Nodes.First!.X, Nodes.First!.Y) + args;
				var selectedNode = Nodes.First!;

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

				for (int i = 0; i < Nodes.Count; i++)
				{
					v1 = new(cords.Item1, cords.Item2);
					if (Vector2.Distance(v1, v2) <= 12)
					{
						isSelected = true;
						_selectedNode = selectedNode;
						return true;
					}
					if (i + 1 >= Nodes.Count)
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


		// Funkce pro nalezení průsečíků čáry s okraji obdélníka
		public static PointF? GetLineRectangleIntersection((int, int) start, (int, int) end, int x1, int y1, int x2, int y2)
		{
			// Parametry čáry
			var lineStart = new PointF(start.Item1, start.Item2);
			var lineEnd = new PointF(end.Item1, end.Item2);

			// Okraje obdélníka (levý, pravý, horní, dolní okraj)
			var edges = new[]
			{
			new { A = new PointF(x1, y1), B = new PointF(x1, y2) }, // Levý okraj
            new { A = new PointF(x1, y1), B = new PointF(x2, y1) }, // Dolní okraj
            new { A = new PointF(x2, y1), B = new PointF(x2, y2) }, // Pravý okraj
            new { A = new PointF(x1, y2), B = new PointF(x2, y2) }  // Horní okraj
        };

			// Procházení každého okraje a hledání průsečíku
			foreach (var edge in edges)
			{
				var intersection = GetLineIntersection(lineStart, lineEnd, edge.A, edge.B);
				if (intersection.HasValue)
				{
					// Zkontrolujeme, zda průsečík leží na segmentu (mezi těmito body)
					if (IsPointOnSegment(intersection.Value, edge.A, edge.B) && IsPointOnSegment(intersection.Value, lineStart, lineEnd))
					{
						return intersection.Value;
					}
				}
			}
			return null; // Žádný průsečík
		}

		// Funkce pro výpočet průsečíku dvou čar (segmentů)
		public static PointF? GetLineIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
		{
			float x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
			float x3 = p3.X, y3 = p3.Y, x4 = p4.X, y4 = p4.Y;

			// Výpočty podle determinantů pro rovnici přímek
			float denom = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
			if (denom == 0) return null; // Čáry jsou rovnoběžné

			float intersectX = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denom;
			float intersectY = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denom;

			return new PointF(intersectX, intersectY);
		}

		// Funkce pro kontrolu, zda bod leží na segmentu mezi dvěma body
		public static bool IsPointOnSegment(PointF point, PointF segStart, PointF segEnd)
		{
			// Zkontrolujeme, zda bod leží mezi začátkem a koncem segmentu
			const float epsilon = 1e-6f; // Tolerance pro přesnost výpočtu

			bool withinX = point.X >= Math.Min(segStart.X, segEnd.X) - epsilon && point.X <= Math.Max(segStart.X, segEnd.X) + epsilon;
			bool withinY = point.Y >= Math.Min(segStart.Y, segEnd.Y) - epsilon && point.Y <= Math.Max(segStart.Y, segEnd.Y) + epsilon;

			return withinX && withinY;
		}
	}
}
