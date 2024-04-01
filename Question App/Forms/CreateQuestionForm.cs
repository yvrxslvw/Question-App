using System.Windows.Forms;

namespace Question_App.Forms
{
    public partial class CreateQuestionForm : Form
    {
        public CreateQuestionForm()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, System.EventArgs e)
        {
            string content = contentTextBox.Text;
            string answer = answerTextBox.Text;

            MessageBox.Show($"create question feature\n{content} {answer}");
        }
    }
}
