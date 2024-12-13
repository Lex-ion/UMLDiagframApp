using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.ValidationStrategies
{
	public interface IValidationStrategy
	{
		public string Message { get; }
		public bool Validate(string text);
	}
}
