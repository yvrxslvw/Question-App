using DB;
using Question_App.Forms;
using Question_App.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Question_App
{
    public partial class MainForm : Form
    {
        private readonly List<Test> tests = new List<Test> { };
        private Test selectedTest = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ClearSelection()
        {
            testsListBox.SelectedIndex = -1;
            selectedTest = null;
            ToggleButtons(false);
        }

        private void ToggleButtons(bool state)
        {
            editButton.Enabled = state;
            deleteButton.Enabled = state;
            startTestButton.Enabled = state;
        }

        private void LoadTests()
        {
            testsListBox.Items.Clear();
            tests.Clear();

            SqlDataReader reader = null;

            try
            {
                reader = Database.Select("Tests");
                Test item = null;

                while (reader.Read())
                {
                    item = new Test(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]).Trim(), Convert.ToSingle(reader["Timer"]));
                    testsListBox.Items.Add($"{item.Name} ({item.Timer} мин)");
                    tests.Add(item);
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["mssqlDB"].ConnectionString;
                Database.Open(connectionString);
                LoadTests();
            }
            catch (Exception exc)
            {
                Utils.ShowError("Не удалось подключиться к базе данных.", exc);
                Application.Exit();
            }
        }

        private void TestsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = testsListBox.SelectedIndex;
            if (index == -1) return;
            ToggleButtons(true);
            selectedTest = tests[index];
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            CreateTestForm createTestForm = new CreateTestForm();
            createTestForm.ShowDialog();
            LoadTests();
            ClearSelection();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditTestForm editTestForm = new EditTestForm(selectedTest);
            editTestForm.ShowDialog();
            LoadTests();
            ClearSelection();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    caption: "Удаление теста",
                    text: $"Вы уверены, что хотите удалить тест \"{selectedTest.Name}\"?",
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Question
                );
            if (result == DialogResult.Yes)
            {
                selectedTest.RemoveDatabase();
                LoadTests();
            }
            ClearSelection();
        }

        private void StartTestButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = selectedTest.GetQuestions();
                if (!success)
                {
                    Utils.ShowError("В данном тесте нет вопросов.");
                    Show();
                    return;
                }

                Hide();
                TestForm testForm = new TestForm(selectedTest);
                testForm.ShowDialog();
                Show();
                ClearSelection();
            }
            catch (Exception exc)
            {
                Utils.ShowError("Произошла непредвиденная ошибка...", exc);
            }
        }

        private void TestsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = testsListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                EditTestForm editTestForm = new EditTestForm(selectedTest);
                editTestForm.ShowDialog();
                LoadTests();
                ClearSelection();
            }
        }
    }
}
