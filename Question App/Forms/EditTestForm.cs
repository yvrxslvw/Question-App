using DB;
using Question_App.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Question_App.Forms
{
    public partial class EditTestForm : Form
    {
        private readonly List<Question> questions = new List<Question> { };
        private readonly Test editableTest = null;
        private Question selectedQuestion = null;
        private bool isDialogShown = false;

        public EditTestForm(Test editableTest)
        {
            InitializeComponent();
            this.editableTest = editableTest;
        }

        private void ClearSelection()
        {
            questionListBox.SelectedIndex = -1;
            selectedQuestion = null;
            ToggleButtons(false);
        }

        private void ToggleButtons(bool state)
        {
            editQuestionButton.Enabled = state;
            deleteQuestionButton.Enabled = state;
        }

        private void LoadTest()
        {
            nameTextBox.Clear();
            timerTextBox.Clear();
            questionListBox.Items.Clear();

            nameTextBox.Text = editableTest.Name;
            timerTextBox.Text = editableTest.Timer.ToString();

            SqlDataReader reader = null;

            try
            {
                reader = Database.Select("Questions", "TestId", editableTest.Id.ToString());
                Question item = null;

                while (reader.Read())
                {
                    item = new Question(Convert.ToInt32(reader["Id"]), editableTest.Id, Convert.ToString(reader["Content"]).Trim(), Convert.ToString(reader["Answer"]).Trim());
                    questionListBox.Items.Add($"{item.Content}");
                    questions.Add(item);
                }
            }
            catch (Exception exc)
            {
                Utils.ShowError("Произошла непредвиденная ошибка...", exc);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        private void EditTestForm_Load(object sender, EventArgs e)
        {
            LoadTest();
        }

        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter) editButton.PerformClick();
                return;
            }
        }

        private void TimerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9') return;
            else if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }
            else if (e.KeyChar == '.')
            {
                if (timerTextBox.Text.IndexOf(".") != -1) e.Handled = true;
                return;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter) editButton.PerformClick();
                return;
            }
            e.Handled = true;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            bool timerCorrect = float.TryParse(timerTextBox.Text, out float timer);

            if (name.Length == 0)
            {
                Utils.ShowError("Вы не заполнили поле с названием теста.");
                return;
            }
            if (name.Length < 3 || name.Length > 24)
            {
                Utils.ShowError("Некорректное название теста.");
                return;
            }
            if (!timerCorrect)
            {
                Utils.ShowError("Некорректное время таймера.");
                return;
            }
            if (timer < 1 || timer > 30)
            {
                Utils.ShowError("Время таймера не меньше 1 и не больше 30.");
                return;
            }

            try
            {
                editableTest.EditName(name);
                editableTest.EditTimer(timer);
                Close();
            }
            catch (Exception exc)
            {
                Utils.ShowError("Произошла непредвиденная ошибка...", exc);
            }
        }

        private void QuestionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = questionListBox.SelectedIndex;
            if (index == -1) return;
            ToggleButtons(true);
            selectedQuestion = questions[index];
        }

        private void AddQuestionButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"add question feature");
            isDialogShown = true;
            ClearSelection();
        }

        private void EditQuestionButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"edit question {selectedQuestion.Id} {selectedQuestion.Content} feature");
            isDialogShown = true;
            ClearSelection();
        }

        private void DeleteQuestionButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"delete question {selectedQuestion.Id} {selectedQuestion.Content} feature");
            isDialogShown = true;
            ClearSelection();
        }

        private void EditTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDialogShown)
            {
                e.Cancel = true;
                isDialogShown = false;
            }
        }
    }
}
