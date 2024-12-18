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

		public static Attribute CreateFromString(string value)
		{
			ModifiersEnum modifiers;
			string t = value;
			string s;
			s = t[1..];
			switch (t[0])
			{
				case '+':
					modifiers = ModifiersEnum.Public;

					break;

				case '#':
					modifiers = ModifiersEnum.Protected; break;

				case '~':
					modifiers = ModifiersEnum.Internal; break;

				case '-':
					modifiers = ModifiersEnum.Private; break;
				default:
					s = t.Split(' ').Last();
					switch (t.Split(' ').First().ToLower())
					{
						default:
							modifiers = ModifiersEnum.Public; break;

						case "public":
							modifiers = ModifiersEnum.Public; break;
						case "private":
							modifiers = ModifiersEnum.Private; break;
						case "protected":
							modifiers = ModifiersEnum.Protected; break;
						case "internal":
							modifiers = ModifiersEnum.Internal; break;
					}
					break;
			}

			string name = s.Split(":").First();
			string type = s.Split(":").Last();

			return new Entities.Attribute(modifiers, type, name);

		}
	}
}
