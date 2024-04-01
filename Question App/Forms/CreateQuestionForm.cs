using Question_App.Models;
using System;
using System.Windows.Forms;

namespace Question_App.Forms
{
    public partial class CreateQuestionForm : Form
    {
        private readonly int testId;

        public CreateQuestionForm(int testId)
        {
            InitializeComponent();
            this.testId = testId;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string content = contentTextBox.Text;
            string answer = answerTextBox.Text;

            if (content.Length < 3 || content.Length > 256)
            {
                Utils.ShowError("Некорректный вопрос.");
                return;
            }
            else if (answer.Length < 1 || answer.Length > 128)
            {
                Utils.ShowError("Некорректный ответ на вопрос.");
                return;
            }

            try
            {
                Question question = new Question(testId, content, answer);
                question.InsertDatabase();
                Close();
            }
            catch (Exception exc)
            {
                Utils.ShowError("Произошла непредвиденная ошибка...", exc);
            }
        }
    }
}
