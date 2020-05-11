using System;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormInput : Form
    {
        public FormInput()
        {
            InitializeComponent();
            textBox1.Text = Project.Rename;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Project.Rename = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
