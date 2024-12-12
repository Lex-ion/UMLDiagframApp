using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
    public abstract class AbstractSelectable : ISelectable
    {
        public virtual int X { get => _x; set => _x = value; }
        public virtual int Y { get => _y; set => _y = value; }

        protected int width;
        protected int height;

        protected bool isSelected;

        private int _x;
        private int _y;

        protected AbstractSelectable(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            this.width = width;
            this.height = height;
        }

        public abstract void Draw(DrawArgs args, Graphics g);

        public bool IsSelected(int x, int y, DrawArgs args)
        {

            isSelected =
                x >= (X + args.ViewportOffsetX) * args.ViewportScale && x <= (X + width + args.ViewportOffsetX) * args.ViewportScale &&
                y >= (Y + args.ViewportOffsetY) * args.ViewportScale && y <= (Y + height + args.ViewportOffsetY) * args.ViewportScale;
            return isSelected;
        }

        public abstract void MouseInput(MouseArgs mArgs, DrawArgs dArgs);
    }
}
