using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UMLDiagframApp.Entities;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp
{
	public class JSONManipulator
	{
		private List<IDrawable> _drawables;
		private List<ISelectable> _selectables;


		public JSONManipulator(List<IDrawable> drawables, List<ISelectable> selectables)
		{
			_drawables = drawables;
			_selectables = selectables;
		}

		public void Load(string path)
		{
		}

		public void Save(string path) {


			List<DiagramBox> boxes = _selectables.Where(s => s is DiagramBox).Select(s => s as DiagramBox).ToList();
			List<ConnectionLine> lines = _selectables.Where(s => s is ConnectionLine).Select(s => s as ConnectionLine).ToList();

			ViewPortDTO dto = new ViewPortDTO(boxes,lines);

			JsonSerializerOptions options = new JsonSerializerOptions()
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true,
			};
			string json = JsonSerializer.Serialize(dto);
			File.WriteAllText(path, json);
		}


	}
}
