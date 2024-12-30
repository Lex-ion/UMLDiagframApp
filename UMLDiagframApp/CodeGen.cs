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


		public CodeGen(List<DiagramBox> diagramBoxes, string path)
		{
			_diagramBoxes = diagramBoxes;
			Path = path;
		}

		public void Generate()
		{ string text = "";
			foreach (var box in _diagramBoxes)
			{
				 text += $"public class {box.Name} \n{{\n";
				foreach (var atr in box.Attributes)
				{
					text += $"{atr.Modifier.ToString().ToLower()} {atr.Type} {atr.Name};\n";
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
					text += $"{met.Modifier.ToString().ToLower()} {met.Type} {met.Name}({pars}) => throw new NotImplementedException();\n";
				} 


				text += "}\n\n";
			}
			File.WriteAllText(Path, text);
		}

	}
}
