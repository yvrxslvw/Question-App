using DB;
using Question_App.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Question_App.Forms
{
    public partial class EditTestForm : Form
    {
        private readonly List<Question> questions = new List<Question> { };
        private readonly Test editableTest = null;

        public EditTestForm(Test editableTest)
        {
            InitializeComponent();
            this.editableTest = editableTest;
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
                    item = new Question(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Content"]), Convert.ToString(reader["Answer"]));
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
    }
}
