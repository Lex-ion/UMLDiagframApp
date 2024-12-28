using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp.ValidationStrategies
{
	public class ConnectionValidationStrategy : IValidationStrategy
	{
		private string _startName;
		private List<ISelectable> _selectables;

		public string Message { get;private set; }

		public ConnectionValidationStrategy(string startName, List<ISelectable> selectables)
		{
			Message = "";
			_startName = startName;
			_selectables = selectables;
		}

		public bool Validate(string text)
		{
			if (text == _startName)
			{
				Message = "Nelze propojit sama sebe se sebou!";
				return false;
			}

			if(_selectables.Where(s=>s is DiagramBox).Select(s=>s as DiagramBox).FirstOrDefault(s=>s!.Name==text) is null)
			{
				Message = "Třída nebyla nalezena.";
				return false;
			}


			return true;
		}
	}
}
