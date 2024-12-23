using UMLDiagframApp.Entities;
using UMLDiagframApp.Presentation;
using UMLDiagframApp.ValidationStrategies;

namespace UMLDiagframApp
{
	public class ContextMenuFactory
	{
		List<IDrawable> _drawables;

		List<ISelectable> _selectables;

		DrawArgs _args;

		public ContextMenuFactory(List<IDrawable> drawables, List<ISelectable> selectables, DrawArgs drawArgs)
		{
			_drawables = drawables;
			_selectables = selectables;
			_args = drawArgs;
		}

		public ContextMenu GetViewPortMenu(int x, int y)
		{
			return new ContextMenu(x, y, [
				new("Přidat",()=>CreateNew(x,y)),
				new("Resetovat přiblížení",ResetScale),
				new("Vycentrovat",Center),
				]);
		}

		public ContextMenu GetSelectedBoxMenu(int x, int y, ISelectable selected)
		{
			ContextMenuCommand[] baseCommands = [       new("Odstranit",()=>Delete(selected))
				];
			ContextMenuCommand[] boxCommands = [
				new("Přejmenovat",()=>Rename(selected as DiagramBox)),
				new("Přidat atribut",()=>AddAttribute(selected as DiagramBox)),
				new("Přidat metodu",()=>AddMethod(selected as DiagramBox)),
				new("Vytvořit propojení",()=>CreateConnection(selected as DiagramBox,_selectables.Where(s=> s is DiagramBox).Select(d=>d as DiagramBox).ToList<DiagramBox>())),

			];

			List<ContextMenuCommand> cmds = new();

			baseCommands.ToList().ForEach(c => cmds.Add(c));

			if (selected is DiagramBox)
			{
				boxCommands.ToList().ForEach(cmds.Add);
				DiagramBox box = (DiagramBox)selected;
				if (box.SelectedIndex > -1 && box.SelectedIndex != box.Attributes.Count)
				{

					cmds.Add(new("Upravit výběr", box.UpdateItem));
					cmds.Add(new("Odebrat vyběr", box.RemoveItem));
				}
			}

			return new ContextMenu(x, y, cmds);
		}


		private void ResetScale()
		{
			_args.ViewportScale = 1;
		}

		private void Center()
		{
			_args.ViewportOffsetX = (int)((_args.ViewportSizeX / 2 - (int)_drawables.Average(d => d.X * _args.ViewportScale)) / _args.ViewportScale);
			_args.ViewportOffsetY = (int)((_args.ViewportSizeY / 2 - (int)_drawables.Average(d => d.Y * _args.ViewportScale)) / _args.ViewportScale);
		}

		private void CreateNew(int x, int y)
		{

			TextInputForm t = new("", new ClassNameValidationStrategy(_selectables.Where(s => s is DiagramBox).Select(s => s as DiagramBox).ToList()!));
			t.ShowDialog();

			if (t.DialogResult == DialogResult.Abort)
			{
				return;
			}
			DiagramBox d = new(t.Value, x / _args, y / _args, 0, 300);
			_drawables.Add(d);
			_selectables.Add(d);
		}

		private void Delete(ISelectable s)
		{
			_drawables.Remove(s);
			_selectables.Remove(s);
		}

		private void Rename(DiagramBox box)
		{
			TextInputForm t = new(box.Name, new ClassNameValidationStrategy(_selectables.Where(s => s is DiagramBox).Select(s => s as DiagramBox).ToList()!));
			t.ShowDialog();

			if (t.DialogResult == DialogResult.OK)
			{
				box.Name = t.Value;
				box.Changed = true;
			}

		}

		private void AddMethod(DiagramBox box)
		{
			TextInputForm t = new("", new MethodValidationStrategy());
			t.ShowDialog();

			if (t.DialogResult == DialogResult.Abort)
			{
				return;
			}

			ModifiersEnum modifiers;
			string s;
			s = t.Value[1..];
			switch (t.Value[0])
			{
				case '+':
					modifiers = ModifiersEnum.Public;
					
					break;

				case '#':
					modifiers = ModifiersEnum.Protected; break;

				case '~':
					modifiers = ModifiersEnum.Internal; break;

				case '-':
					modifiers = ModifiersEnum.Private; break;
				default:
					s = t.Value.Split(' ').Last();
					switch (t.Value.Split(' ').First().ToLower())
					{
						default:
							modifiers = ModifiersEnum.Public; break;

						case "public":
							modifiers = ModifiersEnum.Public; break;
						case "private":
							modifiers = ModifiersEnum.Private; break;
						case "protected":
							modifiers = ModifiersEnum.Protected; break;
						case "internal":
							modifiers = ModifiersEnum.Internal; break;
					}
					break;
			}

			string type = s.Split(')').Last().Split(':').Last();
			string name = s.Split('(').First();

			string ps = s.Split('(').Last().Split(')').First();




			Entities.Method a = new(modifiers,type, name, ps);

			box.Methods.Add(a);
			box.Changed = true;

		}

		private void AddAttribute(DiagramBox box)
		{
			TextInputForm t = new("", new AttributeValidationStrategy());
			t.ShowDialog();

			if (t.DialogResult == DialogResult.Abort)
			{
				return;
			}

			ModifiersEnum modifiers;

			string s;
			s = t.Value[1..];
			switch (t.Value[0])
			{
				case '+':
					modifiers = ModifiersEnum.Public;

					break;

				case '#':
					modifiers = ModifiersEnum.Protected; break;

				case '~':
					modifiers = ModifiersEnum.Internal; break;

				case '-':
					modifiers = ModifiersEnum.Private; break;
				default:
					s = t.Value.Split(' ').Last();
					switch (t.Value.Split(' ').First().ToLower())
					{
						default:
							modifiers = ModifiersEnum.Public; break;

						case "public":
							modifiers = ModifiersEnum.Public; break;
						case "private":
							modifiers = ModifiersEnum.Private; break;
						case "protected":
							modifiers = ModifiersEnum.Protected; break;
						case "internal":
							modifiers = ModifiersEnum.Internal; break;
					}
					break;
			}

			string name = s.Split(":").First();
			string type = s.Split(":").Last();

			box.Attributes.Add(new Entities.Attribute(modifiers,type,name));
			box.Changed=true;
		}

		private void CreateConnection(DiagramBox box, List<DiagramBox> boxes)
		{
			TextInputForm t = new TextInputForm("", new NoneValidationStrategy());
			t.ShowDialog();
			if (t.DialogResult == DialogResult.Abort)
				return;
			string b = t.Value;

			var con = new ConnectionLine(box, boxes.First(bs => bs.Name == b));

			if(_selectables.Where(s=>s is ConnectionLine).Select(s=>s as  ConnectionLine).Any(s=>s.DiagramBoxPair==con.DiagramBoxPair))
			{
				return;
			}

			_drawables.Add(con);
			_selectables.Add(con);
		}

	}
}
