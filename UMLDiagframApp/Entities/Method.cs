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

		public static Method CreateFromString(string value)
		{
			ModifiersEnum modifiers;
			string s;
			string t = value;
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

			string type = s.Split(')').Last().Split(':').Last();
			string name = s.Split('(').First();

			string ps = s.Split('(').Last().Split(')').First();




			Entities.Method a = new(modifiers, type, name, ps);
			return a;
		}
	}
}
