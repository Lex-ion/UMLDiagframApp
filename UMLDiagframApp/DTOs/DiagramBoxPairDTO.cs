using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.DTOs
{
	public class DiagramBoxPairDTO
	{
		public string First {  get; set; }
		public string Second { get; set; }

		public DiagramBoxPairDTO(string first, string second)
		{
			First = first;
			Second = second;
		}

		public DiagramBoxPairDTO()
		{
			First = "";
			Second = "";
		}
	}
}
