using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UMLDiagframApp.ValidationStrategies
{
	public class AttributeValidationStrategy : IValidationStrategy
	{
		public string Message {  get; set; }

		public bool Validate(string text)
		{
			text = text.Trim();

			if (string.IsNullOrEmpty(text))
			{
				Message = "Nebyl zadán atribut!"
					;
				return false;
			}

			bool flag = false;
			switch (text[0])
			{
				case '+':
					flag = true; text = text[1..]; break;

				case '#':
					flag = true; text = text[1..]; break;

				case '~':
					flag = true; text = text[1..]; break;

				case '-':
					flag = true; text = text[1..]; break;
				default:
					switch (text.Split(' ').First().ToLower())
					{
						default:
							flag = false;
							text = text[text.IndexOf(' ')..]; break;

						case "public":
							flag = true; text = text[(text.IndexOf(' ') + 1)..]; break;
						case "private":
							flag = true; text = text[(text.IndexOf(' ') + 1)..]; break;
						case "protected":
							flag = true; text = text[(text.IndexOf(' ') + 1)..]; break;
						case "internal":
							flag = true; text = text[(text.IndexOf(' ') + 1)..]; break;
					}
					break;
			}
			if (!flag)
			{
				Message = "Neplatný modifikátor přístupu!";
				return false;
			}


			if (text.Count(c=>c==':') != 1)
			{
				Message = "Neplatný zápis";
				return false;
			}



			if (!new ClassNameValidationStrategy([]).Validate(text.Split(":").First()))
			{
				Message = "Neplatný název!";
				return false;
			}

			return true;
		}
	}
}
