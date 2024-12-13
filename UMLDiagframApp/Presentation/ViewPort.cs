using UMLDiagframApp.Entities;

namespace UMLDiagframApp.Presentation
{
    public class ViewPort
    {
        DrawArgs _args;

        List<IDrawable> _drawables;

        List<ISelectable> _selectables;
        ISelectable? _selected;

        Action<MouseArgs>? _command;

        int x;
        int y;

        int dx;
        int dy;

        MouseArgs? mouseArgs;

        ContextMenuFactory _menuFactory;
        public ViewPort(int width, int height)
        {
            _args = new(width,height,0,0,1);
            _drawables = new List<IDrawable>();
            _selectables = new List<ISelectable>();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    _drawables.Add(new Circle(15 + i * 15, 15 + j * 15));
                }
            }

            for (int i = 0; i < 5; i++)
            {

                DiagramBox db = new DiagramBox("Test" + i, 0 + i * 10, 0 + i * 10, 100, 300);

                _drawables.Add(db);
                _selectables.Add(db);
            }

            _args.ViewportScale = 1;
            _args.ViewportSizeX = width;
            _args.ViewportSizeY = height;

            _menuFactory = new(_drawables, _selectables,_args);
        }


        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(Brushes.DimGray, new RectangleF(-1, 0, _args.ViewportSizeX + 1, _args.ViewportSizeY));

            int i = -0;
            int step = 50;
            do
            {
                g.FillPolygon(Brushes.DarkSlateGray, [new PointF((i * step + _args.ViewportOffsetX * _args % step) * 1, 0), new PointF((25 + i * step + _args.ViewportOffsetX * _args % step) * 1, 0), new PointF((25 + i * step - 200 + _args.ViewportOffsetX * _args % step) * 1, _args.ViewportSizeY), new PointF((i * step - 200 + _args.ViewportOffsetX * _args % step) * 1, _args.ViewportSizeY)]);
                i++;
            } while ((i * step - 200 + _args.ViewportOffsetX * _args % step) * 1 < _args.ViewportSizeX);

            List<IDrawable> destroyList = new List<IDrawable>();

            foreach (var d in _drawables)
            {
                if (d.Destroyed)
                {
                    destroyList.Add(d);

                 

                    continue;
                }
                d.Draw(_args, g);

			}

            foreach (var d in destroyList)
            {
				_drawables.Remove(d);

				if (d is ISelectable)
				{
					_selectables.Remove((d as ISelectable)!);
				}
			}

            g.DrawString("Offset: " + _args.ViewportOffsetX + " ; " + _args.ViewportOffsetY, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 10));
            g.DrawString("Size: " + _args.ViewportSizeX + " ; " + _args.ViewportSizeY, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 20));
            g.DrawString("Scale: " + _args.ViewportScale, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 30));
            g.DrawString("Mouse: " + x + " ; " + y, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 40));
            g.DrawString("Mouse deltas: " + dx + " ; " + dy, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 50));
            g.DrawString("Mouse state: " + mouseArgs?.Button + " ; " + mouseArgs?.ButtonState + " ; " + mouseArgs?.LeftMouseDown + " ; " + mouseArgs?.RightMouseDown, SystemFonts.DefaultFont, Brushes.Black, new PointF(10, 60));


        }


        public void MouseInput(MouseArgs args)
        {

            if (_command is null)
            {


                foreach (var s in _selectables.ToArray().Reverse())
                {
                    if (s.IsSelected(args.PositionX, args.PositionY, _args))
                    {
                        _selected = s;

                        _selectables.Remove(s);
                        _selectables.Add(s);

                        _drawables.Remove(s);
                        _drawables.Add(s);


                        break;
                    }

                    _selected = null;

                }



                if (_selected != null&&(int)args.ButtonState< 0x00200000)
                {
                    _command = (args) =>
                    {
                        _selected.MouseInput(args, _args);
                        if (args.ButtonState == MouseButtonsStates.None || args.ButtonState == MouseButtonsStates.LeftUp)
                            _command = null;
                    };



                }
				else if (args.ButtonState == MouseButtonsStates.RightUp)
				{
					var cords = (args.PositionX - _args.ViewportOffsetX, args.PositionY - _args.ViewportOffsetY);
                    ContextMenu menu = _selected is null? _menuFactory.GetViewPortMenu(cords.Item1 - 1, cords.Item2 - 1)
                        : _menuFactory.GetSelectedBoxMenu(cords.Item1 - 1, cords.Item2 - 1,_selected);

					_drawables.Add(menu);
					_selectables.Add(menu);
					_selected = menu;
				}
				else
                if (args.LeftMouseDown)
                {
                    _command = (args) =>
                    {

                        Cursor.Current = Cursors.SizeAll;
                        _args.ViewportOffsetX += (int)(args.PositionXDelta / _args.ViewportScale);
                        _args.ViewportOffsetY += (int)(args.PositionYDelta / _args.ViewportScale);
                    };


                }             
                if (args.ButtonState==MouseButtonsStates.RightHold&&args.PositionXDelta!=0&& _selected is null)
                {
                    _command = (args) =>
                    {


                        Cursor.Current = Cursors.SizeNS;
                        _args.ViewportScale -= (float)args.PositionYDelta / 50;
                        _args.ViewportScale = Math.Clamp(_args.ViewportScale, 0.1f, 2.5f);
                    };
                }
                else
                if (args.Button == MouseButtons.Middle)
                {
                    _args.ViewportOffsetX = (int)((_args.ViewportSizeX / 2 - (int)_drawables.Average(d => d.X * _args.ViewportScale)) / _args.ViewportScale);
                    _args.ViewportOffsetY = (int)((_args.ViewportSizeY / 2 - (int)_drawables.Average(d => d.Y * _args.ViewportScale)) / _args.ViewportScale);
                }

              

            }
            _command?.Invoke(args);

            if (((int)args.ButtonState & 0x0ffff) == 2)
            {
                _command = null;
                Cursor.Current = Cursors.Default;
            }


            x = args.PositionX;
            y = args.PositionY;
            dx = args.PositionXDelta;
            dy = args.PositionYDelta;

            mouseArgs = args;
        }

        public void Resize(int width, int height)
        {
            int deltaW = _args.ViewportSizeX - width;
            int deltaH = _args.ViewportSizeY - height;

            _args.ViewportSizeY = height;
            _args.ViewportSizeX = width;

            _args.ViewportOffsetY -= deltaH / 2;
            _args.ViewportOffsetX -= deltaW / 2;
        }
    }
}
