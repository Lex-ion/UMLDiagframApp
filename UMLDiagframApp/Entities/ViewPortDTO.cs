using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp.Entities
{
	public class ViewPortDTO
	{
		public List<DiagramBox> DiagramBoxes { get; set; }
		public List<ConnectionLine> ConnectionLines { get; set; }
		public ViewPortDTO() { }

		public ViewPortDTO(List<DiagramBox> diagramBoxes, List<ConnectionLine> connectionLines)
		{
			DiagramBoxes = diagramBoxes;
			ConnectionLines = connectionLines;
		}
	}
}
