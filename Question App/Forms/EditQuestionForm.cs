using Question_App.Models;
using System;
using System.Windows.Forms;

namespace Question_App.Forms
{
    public partial class EditQuestionForm : Form
    {
        private readonly Question editableQuestion = null;

        public EditQuestionForm(Question question)
        {
            InitializeComponent();
            editableQuestion = question;
        }

        private void EditButton_Click(object sender, System.EventArgs e)
        {
            string content = contentTextBox.Text;
            string answer = answerTextBox.Text;

            if (content.Length < 3 || content.Length > 256)
            {
                Utils.ShowError("Некорректный вопрос.");
            }
            else if (answer.Length < 3 || answer.Length > 128)
            {
                Utils.ShowError("Некорректный ответ на вопрос.");
            }

            try
            {
                editableQuestion.EditContent(content);
                editableQuestion.EditAnswer(answer);
                Close();
            }
            catch (Exception exc)
            {
                Utils.ShowError("Произошла непредвиденная ошибка...", exc);
            }
        }

        private void EditQuestionForm_Load(object sender, System.EventArgs e)
        {
            contentTextBox.Clear();
            answerTextBox.Clear();

            contentTextBox.Text = editableQuestion.Content;
            answerTextBox.Text = editableQuestion.Answer;
        }
    }
}
