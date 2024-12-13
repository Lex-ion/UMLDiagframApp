using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLDiagframApp.Entities;
using UMLDiagframApp.Presentation;

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
				new("Resetovat přiblížení",ResetScale),
				new("Vycentrovat",Center),
				]);
		}

		public  ContextMenu GetSelectedBoxMenu(int x, int y, ISelectable selected)
		{
			return new ContextMenu(x, y, [
				new("Odstranit",()=>Delete(selected))
				]);
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

		private void Delete(ISelectable s)
		{
			_drawables.Remove(s);
			_selectables.Remove(s);
		}

	}
}
