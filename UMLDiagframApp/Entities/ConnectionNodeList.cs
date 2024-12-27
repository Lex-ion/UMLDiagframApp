namespace UMLDiagframApp.Entities
{
	public class ConnectionNodeList
	{
		public int Count { get; private set; }

		public ConnectionNode? First { get; private set; }
		public ConnectionNode? Last { get; private set; }

		public void AddAfter(ConnectionNode node, ConnectionNode newNode)
		{
			var previousAfter = node.After;

			node.After = newNode;
			newNode.Before = node;
			newNode.After = previousAfter;
			if (previousAfter != null)
				previousAfter.Before = newNode;

			Count++;
		}
		public void AddBefore(ConnectionNode node, ConnectionNode newNode)
		{
			var previousBefore = node.Before;
			node.Before = newNode;
			newNode.After = node;
			newNode.Before = previousBefore;
			if (previousBefore != null)
				previousBefore.After = newNode;
			Count++;

		}

		public void Remove(ConnectionNode? node)
		{
			if (node == null)
				return;

			Count--;

			if (node.Before != null)
				node.Before.After = node.After;
			if (node.After != null)
				node.After.Before = node.Before;
		}

		public void RemoveBefore(ConnectionNode node)
		{
			Remove(node.Before);
		}

		public void RemoveAfter(ConnectionNode node)
		{
			Remove(node.After);
		}

		public void Add(ConnectionNode node)
		{
			if (Last == null)
			{
				Count++;
				Last = node;
				First = node;
			}
			else { 
			AddAfter(Last, node);
			}
		}
	}
}
