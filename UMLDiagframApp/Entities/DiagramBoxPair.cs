using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp.Entities
{
	public class DiagramBoxPair
	{
		public DiagramBox First {  get; set; }
		public DiagramBox Second { get; set; }

		public DiagramBoxPair(DiagramBox first, DiagramBox second)
		{
			if(first == second)
				throw new ArgumentException("Pair must contain unique boxes.");

			First = first;
			Second = second;
		}

		public override bool Equals(object? obj)
		{
			if(obj is null)
				return false;
			if(obj is not DiagramBoxPair)
				return false;
			var pair = obj as DiagramBoxPair;

			if (pair!.First == First)
			{
				return pair.Second == Second;
			}
			else if (pair.Second == First)
			{
				return pair.First == Second;
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(First.GetHashCode(), Second.GetHashCode());
		}

		public static bool operator ==(DiagramBoxPair first, DiagramBoxPair second) { return first.Equals(second); }
		public static bool operator !=(DiagramBoxPair first, DiagramBoxPair second) { return !first.Equals(second); }
	}
}
