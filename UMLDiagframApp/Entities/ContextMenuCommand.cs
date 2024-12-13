using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public class ContextMenuCommand
	{
		public string Name { get; }
		public Action Command { get; }

		public ContextMenuCommand(string name, Action command)
		{
			Name = name;
			Command = command;
		}
	}
}
