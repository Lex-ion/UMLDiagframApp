using System.Text.Json;
using UMLDiagframApp.DTOs;
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
			string json = File.ReadAllText(path);
			ViewPortDTO viewPortDTO = JsonSerializer.Deserialize<ViewPortDTO>(json, new JsonSerializerOptions() {});
			_drawables.Clear();
			_selectables.Clear();


			foreach (var line in viewPortDTO.ConnectionLines)
			{
				var box1 = viewPortDTO.DiagramBoxes.FirstOrDefault(b => b.Name == line.DiagramBoxPairDTO.First);
				var box2 = viewPortDTO.DiagramBoxes.FirstOrDefault(b => b.Name == line.DiagramBoxPairDTO.Second);
				if (box1 is null || box2 is null)
					continue;
				DiagramBoxPair pair = new(box1, box2);

				var realLine = new ConnectionLine(line, pair);

				_drawables.Add(realLine);
				_selectables.Add(realLine);

			}


			foreach (var box in viewPortDTO.DiagramBoxes)
			{
				_drawables.Add(box);
				_selectables.Add(box);
			}
		}

		public void Save(string path)
		{


			List<DiagramBox> boxes = _selectables.Where(s => s is DiagramBox).Select(s => s as DiagramBox).ToList();
			List<ConnectionLineDTO> lines = _selectables.Where(s => s is ConnectionLine).Select(s => s as ConnectionLine).Select(s => new ConnectionLineDTO(s)).ToList();

			ViewPortDTO dto = new ViewPortDTO(boxes, lines);

			JsonSerializerOptions options = new JsonSerializerOptions()
			{/*
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true,*/
			};
			string json = JsonSerializer.Serialize(dto);
			/*
			using var jDoc = JsonDocument.Parse(json);
			json= JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });
			*/

			File.WriteAllText(path, json);
		}


	}
}
