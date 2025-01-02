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

		private SaveFileDialog _saveFileDialog;
		private OpenFileDialog _openFileDialog;

		private JSONManipulator _jsonManipulator;

		public ViewPort(int width, int height, SaveFileDialog saveFileDialog, OpenFileDialog openFileDialog)
		{
			_saveFileDialog = saveFileDialog;
			_openFileDialog = openFileDialog;

			_args = new(width, height, 0, 0, 1);
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

			_menuFactory = new(_drawables, _selectables, _args);
			_jsonManipulator = new(_drawables, _selectables);
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

						OnTop(s);
						if (s is ConnectionLine cl)
						{
							OnTop(cl.DiagramBoxPair.First);
							OnTop(cl.DiagramBoxPair.Second);

						}


						break;

						void OnTop(ISelectable selectable)
						{
							_selectables.Remove(selectable);
							_selectables.Add(selectable);

							_drawables.Remove(selectable);
							_drawables.Add(selectable);
						}
					}

					_selected = null;

				}



				if (_selected != null && (int)args.ButtonState < 0x00200000)
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
					ContextMenu menu = _selected is null ? _menuFactory.GetViewPortMenu(cords.Item1 - 1, cords.Item2 - 1)
						: _menuFactory.GetSelectedMenu(cords.Item1 - 1, cords.Item2 - 1, _selected);

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
				if (args.ButtonState == MouseButtonsStates.RightHold && args.PositionXDelta != 0 && _selected is null)
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
					Center();
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

		public void Center()
		{
			_args.ViewportOffsetX = (int)((_args.ViewportSizeX / 2 - (int)_drawables.Average(d => d.X * _args.ViewportScale)) / _args.ViewportScale);
			_args.ViewportOffsetY = (int)((_args.ViewportSizeY / 2 - (int)_drawables.Average(d => d.Y * _args.ViewportScale)) / _args.ViewportScale);

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

		public void HandleKeyInput(KeyEventArgs keyEvent)
		{
			//system input handle ctrl+s and etc...
			if (keyEvent.KeyCode == Keys.E&&keyEvent.Control)
			{
				_saveFileDialog.Filter = "Obrázek PNG|*.png";
				var result = _saveFileDialog.ShowDialog();
				if (result != DialogResult.OK)
					return;

				ExportToPng(_saveFileDialog.FileName);
			}
			else
			if (keyEvent.KeyCode == Keys.F && keyEvent.Control)
			{
				_saveFileDialog.Filter = "Soubor C#|*.cs";
				var result = _saveFileDialog.ShowDialog();
				if (result != DialogResult.OK)
					return;

				CodeGen gen = new(_saveFileDialog.FileName, _selectables.Where(s => s is DiagramBox).Select(s => (DiagramBox)s).ToList(), _selectables.Where(s => s is ConnectionLine).Select(s => (ConnectionLine)s).ToList());
				gen.Generate();
			}
			else
			if (keyEvent.KeyCode == Keys.S && keyEvent.Control)
			{
				Save();
			}
			else
			if (keyEvent.KeyCode == Keys.O && keyEvent.Control)
			{
				if(!SaveBeforeSomething())
					return;
				var result = _openFileDialog.ShowDialog();
				if (result != DialogResult.OK)
					return;

				_jsonManipulator.Load(_openFileDialog.FileName);
				Center();
			}
			else
			if (keyEvent.Control && keyEvent.KeyCode == Keys.N)
			{
				if (!SaveBeforeSomething())
					return;
				_drawables.Clear();
				_selectables.Clear();
			}
			else
			if (keyEvent.KeyCode == Keys.A && keyEvent.Shift)
			{
				_menuFactory.CreateNew(-_args.ViewportOffsetX + _args.ViewportSizeX / 2, -_args.ViewportOffsetY + _args.ViewportSizeY / 2);//add scale
			}
			else
			if(_selected is not null)
			{

				if (keyEvent.KeyCode == Keys.X && keyEvent.Shift)
				{
					_menuFactory.Delete(_selected);
					_selected= null;
				}

				if (_selected is DiagramBox db)
				{
				}
			}
		}

		public bool SaveBeforeSomething()
		{
			var result = MessageBox.Show("Chcete rozpracováný soubour uložit?", "Pozor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

			if (result == DialogResult.Cancel)
				return false;

			if (result == DialogResult.Yes)
				if (!Save())
					return false;

			return true;

		}

		public bool Save()
		{

			_saveFileDialog.Filter = "Soubor JSON|*.json";
			var result = _saveFileDialog.ShowDialog();
			if (result != DialogResult.OK)
				return false;

			_jsonManipulator.Save(_saveFileDialog.FileName);
			return true;
		}


		public void ExportToPng(string path)
		{
			ProgressForm progressForm = new(0, _drawables.Count * 2, 0);



			if (_drawables.Count <= 0)
			{
				return;
			}

			new Thread(() =>
			{

				int progress = 0;


				Point upperCorner = new(_drawables[0].X, _drawables[0].Y);
				Point lowerCorner = new(_drawables[0].X + _drawables[0].Width, _drawables[0].Y + _drawables[0].Height);

				foreach (var drawable in _drawables[1..])
				{
					progressForm.UpdateProgress(progress, "Výpočet velikosti");
					if (drawable.X < upperCorner.X)
						upperCorner.X = drawable.X;
					if (drawable.Y < upperCorner.Y)
						upperCorner.Y = drawable.Y;

					if (drawable.X + drawable.Width > lowerCorner.X)
						lowerCorner.X = drawable.X + drawable.Width;
					if (drawable.Y + drawable.Height > lowerCorner.Y)
						lowerCorner.Y = drawable.Y + drawable.Height;
					progress++;
				}
				progressForm.UpdateProgress(progress, "Vytváření bitmapy");
				int pad = 50;

				using Bitmap bitmap = new Bitmap(lowerCorner.X - upperCorner.X + pad, lowerCorner.Y - upperCorner.Y + pad);


				DrawArgs a = new(bitmap.Width, bitmap.Height, -upperCorner.X + pad / 2, -upperCorner.Y + pad / 2, 1);

				using Graphics g = Graphics.FromImage(bitmap);
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

				g.FillRectangle(Brushes.DarkGray, new(0, 0, bitmap.Width, bitmap.Height));
				foreach (var drawable in _drawables)
				{
					progressForm.UpdateProgress(progress, "Vykreslování");
					drawable.Draw(a, g);
					progress++;
				}
				g.Save();
				bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
				g.Dispose();
				bitmap.Dispose();
				progressForm.Finished();

			}).Start();

			progressForm.ShowDialog();
		}
	}
}
