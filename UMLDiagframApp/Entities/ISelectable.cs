using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagframApp.Entities
{
    public interface ISelectable : IDrawable
    {    
        public bool IsSelected(int x,int y, DrawArgs args);
        public void MouseInput(MouseArgs mArgs,DrawArgs dArgs);
    }
}
