using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp.ValidationStrategies
{
	public class ClassNameValidationStrategy : IValidationStrategy

	{

		List<DiagramBox> _classes;

		public string Message { get; private set; }

		public ClassNameValidationStrategy(List<DiagramBox> classes)
		{
			_classes = classes;
			Message = "";
		}

		public bool Validate(string text)
		{
			if(string.IsNullOrEmpty(text))
			{
				Message = "Název musí být uveden!";
				return false;
			}

	

			if (text[0] == '@')
			{
				if (text.Length < 2)
				{
					Message = "Neplatný název";
					return false;
				}

				if (!Char.IsLetter(text[1]) && text[1] != '_')
				{
					Message = "Název třídy musí začínat písmenem či podtržítkem!";
					return false;
				}
				text = text[1..];
			}

			if (!Char.IsLetter(text[0]) && text[0] != '_')
			{
				Message = "Název třídy musí začínat písmenem či podtržítkem!";
				return false;
			}


			foreach (DiagramBox box in _classes)
			{
				if (box.Name == text)
				{
					Message = "Třída s tímto názvem již existuje!";
					return false;
				}
			}

			text = text[1..];

			foreach (char c in text)
			{
				if (!Char.IsAsciiLetterOrDigit(c)&&c!='_')
						{
					Message = "Název třídy obsahuje neplatný znak!";
					return false;
				}
			}



			return true;
		}
	}
}
