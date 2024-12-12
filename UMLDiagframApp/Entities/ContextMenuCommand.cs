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
		public int Group {  get; }
		public Action Command { get; }

		public ContextMenuCommand(string name, int group, Action command)
		{
			Name = name;
			Group = group;
			Command = command;
		}
	}
}
