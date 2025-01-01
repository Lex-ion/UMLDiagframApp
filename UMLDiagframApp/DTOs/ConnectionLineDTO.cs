using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UMLDiagframApp.Entities;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp.DTOs
{
	public class ConnectionLineDTO
	{
		public List<ConnectionNodeDTO> Nodes { get; set; }


		public DiagramBoxPairDTO DiagramBoxPairDTO { get; set; }

		public string FirstMultiplicity { get; set; }
		public string SecondMultiplicity { get; set; }
		public ConnectionType ConnectionType { get; set; }


		public  int X { get ; set ; }
		public  int Y { get; set; }
		

		public int Width { get;  set; }
		public int Height { get;  set; }

		public ConnectionLineDTO(ConnectionLine line) 
		{
			Nodes = new();

			DiagramBoxPairDTO = new(line.DiagramBoxPair.First.Name,line.DiagramBoxPair.Second.Name);

			FirstMultiplicity = line.FirstMultiplicity;
			SecondMultiplicity = line.SecondMultiplicity;
			ConnectionType = line.ConnectionType;

			X = line.X;
			Y= line.Y;
			Width = line.Width;
			Height = line.Height;

			if (line.Nodes.First == null)
				return;

			var selectedNode = line.Nodes.First;
			do
			{
				Nodes.Add(new(selectedNode.X, selectedNode.Y));
				selectedNode = selectedNode.After;
			} while (selectedNode is not null);
		}

		public ConnectionLineDTO()
		{
			Nodes = new();
			FirstMultiplicity = "1";
			SecondMultiplicity = "1";
			ConnectionType = ConnectionType.Asociation;
			DiagramBoxPairDTO = new();
		}
	}
}
