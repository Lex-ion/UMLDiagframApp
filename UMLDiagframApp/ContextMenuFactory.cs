﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public ContextMenuFactory(List<IDrawable> drawables, List<ISelectable> selectables, DrawArgs drawArgs	)
		{
			_drawables = drawables;
			_selectables = selectables;
			_args = drawArgs;
		}

		public  ContextMenu GetViewPortMenu(int x, int y)
		{
			return new ContextMenu(x, y, [
				new("Přidat",()=>CreateNew(x,y)),
				new("Resetovat přiblížení",ResetScale),
				new("Vycentrovat",Center),
				]);
		}

		public  ContextMenu GetSelectedBoxMenu(int x, int y, ISelectable selected)
		{
			ContextMenuCommand[] baseCommands = [       new("Odstranit",()=>Delete(selected))
				];
			ContextMenuCommand[] boxCommands = [       new("Přejmenovat",()=>Rename(selected as DiagramBox))
			];

			List<ContextMenuCommand> cmds=new();

			baseCommands.ToList().ForEach(c=>cmds.Add(c));

			if (selected is DiagramBox)
				boxCommands.ToList().ForEach(cmds.Add);

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

			TextInputForm t = new("", new ClassNameValidationStrategy(_selectables.Where(s=>s is DiagramBox).Select(s=>s as DiagramBox).ToList()!));
			t.ShowDialog();

			if (t.DialogResult == DialogResult.Abort)
			{
				return;
			}
			DiagramBox d = new(t.Value, x/_args,y/_args, 0, 300);
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
			TextInputForm t = new(box.Name,new ClassNameValidationStrategy(_selectables.Where(s => s is DiagramBox).Select(s => s as DiagramBox).ToList()!));
			t.ShowDialog();

			if(t.DialogResult == DialogResult.OK)
			{
				box.Name = t.Value;
			}
		}

	}
}
