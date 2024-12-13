using System.ComponentModel;
using UMLDiagframApp.ValidationStrategies;

namespace UMLDiagframApp
{
	public partial class TextInputForm : Form
	{
		public string Value { get; set; }
		IValidationStrategy validationStrategy;
		public TextInputForm(string defaultText, IValidationStrategy strategy)
		{
			InitializeComponent();
			Value = "";
			textBox1.Text = defaultText;
			validationStrategy =strategy;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Submit();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Abort();
		}


		private bool IsValid()
		{

			bool valid = validationStrategy.Validate(textBox1.Text);
			errorProvider1.SetError(textBox1, valid ? null : validationStrategy.Message);
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
