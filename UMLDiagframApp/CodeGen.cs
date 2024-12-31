using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Presentation;

namespace UMLDiagframApp
{
	public class CodeGen
	{
		public string Path;

		private List<DiagramBox> _diagramBoxes;
		private List<ConnectionLine> _connectionLines;

		public CodeGen(string path, List<DiagramBox> diagramBoxes, List<ConnectionLine> connectionLines)
		{
			Path = path;
			_diagramBoxes = diagramBoxes;
			_connectionLines = connectionLines;
		}

		public void Generate()
		{ string text = "";
			foreach (var box in _diagramBoxes)
			{
				 text += $"public partial class {box.Name} \n{{\n";
				foreach (var atr in box.Attributes)
				{
					text += $"\t{atr.Modifier.ToString().ToLower()} {atr.Type} {atr.Name};\n";
				}
				foreach (var met in box.Methods) 
				{
					string pars="";
					bool first = true;
					foreach (var item in met.Params.Split(','))
					{
						var d = item.Split(':');
						if (string.IsNullOrWhiteSpace(d[0]))
							continue;
						if (!first)
							pars += ",";

						pars += d[1]+" ";
						pars += d[0];

						first = false;
					}
					text += $"\t{met.Modifier.ToString().ToLower()} {met.Type} {met.Name}({pars}) => throw new NotImplementedException();\n";
				} 


				text += "}\n\n";
			}

			foreach(var line in _connectionLines)
			{
				if(line .ConnectionType == Entities.ConnectionType.Generalization)
				{
					text += $"public partial class {line.DiagramBoxPair.Second.Name}:{line.DiagramBoxPair.First.Name} {{}}\n\n";
				}
				else if(line.ConnectionType == Entities.ConnectionType.OneWayAsociation)
				{
					text += $"public partial class {line.DiagramBoxPair.Second.Name}\n{{\n\tpublic {line.DiagramBoxPair.First.Name} {line.DiagramBoxPair.First.Name} {{get;set;}} \n}}\n\n";
				}
				else
				{
					text += $"public partial class {line.DiagramBoxPair.First.Name}\n{{\n\tpublic {line.DiagramBoxPair.Second.Name} {line.DiagramBoxPair.Second.Name} {{get;set;}} \n}}\n\n";
					text += $"public partial class {line.DiagramBoxPair.Second.Name}\n{{\n\tpublic {line.DiagramBoxPair.First.Name} {line.DiagramBoxPair.First.Name} {{get;set;}} \n}}\n\n";

				}
			}

			File.WriteAllText(Path, text);
		}

	}
}
