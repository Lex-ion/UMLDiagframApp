using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.ValidationStrategies
{
	public class NoneValidationStrategy : IValidationStrategy
	{
		public string Message { get; private set; }

		public bool Validate(string text)
		{
			return true;
		}
	}
}
