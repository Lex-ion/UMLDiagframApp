using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace UMLDiagframApp
{
	public partial class ProgressForm : Form
	{
		public ProgressForm(int minimum,int maximum, int value)
		{
			InitializeComponent();

			progressBar1.Minimum = minimum; progressBar1.Maximum = maximum; progressBar1.Value = value;
		}


		public void UpdateProgress(int value, string text)
		{
			

			if (progressBar1.InvokeRequired)
			{
				Action u = delegate { UpdateProgress(value,text); };
				progressBar1.Invoke(u);
			}
			else
			{
				if (value > progressBar1.Maximum)
				{

					Close();
				return;
				}
				progressBar1.Value=value;
				label1.Text=text;
			}
		}

		public void Finished()
		{

			if (progressBar1.InvokeRequired)
			{
				Action u = delegate { Finished(); };
				progressBar1.Invoke(u);
			}
			else
			{
				label1.Text = "Hotovo";
				progressBar1.Value=progressBar1.Maximum;
				Close();
			}
		}
	}
}
