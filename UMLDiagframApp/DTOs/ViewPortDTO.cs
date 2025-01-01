using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp.DTOs
{
    public class ViewPortDTO
    {
        public List<DiagramBox> DiagramBoxes { get; set; }
        public List<ConnectionLineDTO> ConnectionLines { get; set; }
        public ViewPortDTO() {
        DiagramBoxes = new List<DiagramBox>();
            ConnectionLines = new List<ConnectionLineDTO>();
        }

        public ViewPortDTO(List<DiagramBox> diagramBoxes, List<ConnectionLineDTO> connectionLines)
        {
            DiagramBoxes = diagramBoxes;
            ConnectionLines = connectionLines;
        }
    }
}
