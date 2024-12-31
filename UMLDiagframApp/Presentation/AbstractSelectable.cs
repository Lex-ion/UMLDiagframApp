using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
    public abstract class AbstractSelectable : ISelectable
    {
        public virtual int X { get => _x; set => _x = value; }
        public virtual int Y { get => _y; set => _y = value; }
		[JsonIgnore]
		public bool Destroyed { get; private set; }

		public int Width { get; protected set; }
        public int Height { get; protected set; }

		protected bool isSelected;

        private int _x;
        private int _y;

        protected AbstractSelectable(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            this.Width = width;
            this.Height = height;
        }

        public abstract void Draw(DrawArgs args, Graphics g);

        public virtual bool IsSelected(int x, int y, DrawArgs args)
        {           

            isSelected =
                x >= (X + args.ViewportOffsetX) * args.ViewportScale && x <= (X + Width + args.ViewportOffsetX) * args.ViewportScale &&
                y >= (Y + args.ViewportOffsetY) * args.ViewportScale && y <= (Y + Height + args.ViewportOffsetY) * args.ViewportScale;
            return isSelected;
        }

        public abstract void MouseInput(MouseArgs mArgs, DrawArgs dArgs);

		public virtual void Destroy()
		{
            Destroyed = true;
		}

	}
}
