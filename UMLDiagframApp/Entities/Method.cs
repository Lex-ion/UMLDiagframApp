using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public class Method
	{
		public ModifiersEnum Modifier { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		
		public string Params { get; set; }

		public Method(ModifiersEnum modifier, string type, string text, string @params)
		{
			Modifier = modifier;
			Type = type;
			Name = text;
			Params = @params;
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
			m = m + Name + "(" + Params + "):"+Type;
			m = m.Replace(" ", "");
			return m;
		}
	}
}
