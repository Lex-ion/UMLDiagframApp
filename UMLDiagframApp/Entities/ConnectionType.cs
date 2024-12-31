using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
	public enum ConnectionType
	{
		Asociation=			0x000100,
		OneWayAsociation =	0x000101,
		Agregation =		0x000200,
		Composition=		0x000300,
		Generalization=		0x000400,
	}
}
