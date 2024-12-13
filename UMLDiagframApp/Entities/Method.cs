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
	}
}
