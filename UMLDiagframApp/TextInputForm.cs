using System.ComponentModel;

namespace UMLDiagframApp
{
	public partial class TextInputForm : Form
	{
		public string Value { get; set; }
		public TextInputForm(string defaultText)
		{
			InitializeComponent();
			Value = "";
			textBox1.Text = defaultText;

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Submit();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Abort();
		}

		private void textBox1_Validating(object sender, CancelEventArgs e)
		{
			bool valid = !string.IsNullOrWhiteSpace(textBox1.Text);
			errorProvider1.SetError(textBox1, valid ? null : "Toto pole je povinné!");
			e.Cancel = !valid;
		}

		private bool IsValid()
		{

			bool valid = !string.IsNullOrWhiteSpace(textBox1.Text);
			errorProvider1.SetError(textBox1, valid ? null : "Toto pole je povinné!");
			return valid;
		}

		private void Submit()
		{
			if (!IsValid())
				return;

			DialogResult = DialogResult.OK;

			Value = textBox1.Text;

			Close();
		}

		private void Abort()
		{
			DialogResult = DialogResult.Abort;
			Close();

		}

		private void textBox1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Submit();

			}
			else if (e.KeyCode == Keys.Escape)
			{
				Abort() ;
			}
		}
	}
}
