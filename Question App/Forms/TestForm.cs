using Question_App.Models;
using System.Windows.Forms;

namespace Question_App.Forms
{
    public partial class TestForm : Form
    {
        private readonly Test test = null;

        public TestForm(Test test)
        {
            InitializeComponent();
            this.test = test;
        }
    }
}
