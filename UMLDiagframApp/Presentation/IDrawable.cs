using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
    public interface IDrawable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Destroyed {  get;  }
        public void Draw(DrawArgs args, Graphics g);

        public void Destroy();
    }
}
