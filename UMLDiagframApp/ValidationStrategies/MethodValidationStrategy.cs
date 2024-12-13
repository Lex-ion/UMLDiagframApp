namespace UMLDiagframApp.ValidationStrategies
{
	public class MethodValidationStrategy : IValidationStrategy
	{
		public string Message { get; private set; }

		public bool Validate(string text)
		{
			text = text.Trim();

			if (string.IsNullOrEmpty(text))
			{
				Message = "Nebyl zadána metoda!"
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
							flag = false; break;

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

			if (!text.Contains('(') || !text.Contains(')'))
			{
				Message = "Neplatný zápis!";
				return false;
			}

			string name = text.Split('(').First().Trim();


			if (!new ClassNameValidationStrategy([]).Validate(name))
			{
				Message = "Neplatný název metody!";
				return false;
			}

			string type = text.Split(")").Last().Split(':').Last();
			if (string.IsNullOrWhiteSpace(type))
			{
				Message = "Chybí typ!";
				return false;
			}
			var ps = text.Split('(').Last().Split(')').First().Split(',');
			foreach (var s in text.Split('(').Last().Split(')').First().Split(','))
			{
				
				if (s.Count(c => c == ':') != 1 && (s == ""&&ps.Length>1))
				{
					Message = "Neplatné zadání parametru!";
					return false;
				}
				if(s.Contains(':'))
				if(s.Split(':').Any(c=>c.Length<1))
				{
					Message = "Neplatné zadání parametru!";
					return false;
				}
			}

			return true;
		}
	}
}
