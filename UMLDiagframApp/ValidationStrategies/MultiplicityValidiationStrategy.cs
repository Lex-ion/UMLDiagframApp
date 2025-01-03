using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UMLDiagframApp.ValidationStrategies
{
	public class MultiplicityValidiationStrategy : IValidationStrategy
	{
		public string Message { get; private set; }

		public bool Validate(string text)
		{
			Regex regex = new("^(\\d+|\\*)(\\.\\.(\\d+|\\*))?$");
			if (regex.IsMatch(text)) { return true; }
			Message = "Multiplicita není ve správném formátu.";
			return false;
		}
	}
}
