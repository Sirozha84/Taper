using System;
using System.Windows.Forms;

namespace Taper
{
    public partial class FormInput : Form
    {
        public FormInput()
        {
            InitializeComponent();
            Text = Lang.rename;
            labelEnterNewName.Text = Lang.enterNewName;
            textBoxName.Text = Project.rename;
        }

        private void OK(object sender, EventArgs e)
        {
            Project.rename = textBoxName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
