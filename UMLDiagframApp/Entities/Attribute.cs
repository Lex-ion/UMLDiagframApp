using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public class Attribute
	{
		public ModifiersEnum Modifier { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }

		public Attribute(ModifiersEnum modifier, string type, string name)
		{
			Modifier = modifier;
			Type = type;
			Name = name;
		}

		public override bool Equals(object? obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string? ToString()
		{
			string m = "";

			switch (Modifier)
			{
				case ModifiersEnum.Public:
					m += "+";
					break;
				case ModifiersEnum.Private:
					m += "-";
					break;
				case ModifiersEnum.Protected:
					m += "#";
					break;
				case ModifiersEnum.Internal:
					m += "~";
					break;
				default:
					break;
			}
			m = m + Name +":"+ Type;
			m = m.Replace(" ", "");
			return m;
		}
	}
}
