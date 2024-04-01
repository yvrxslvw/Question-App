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
                    item = new Test(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]).Trim(), Convert.ToInt32(reader["Timer"]));
                    testsListBox.Items.Add($"ID: {item.Id}\t{item.Name}");
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

            addNewButton.Enabled = true;
            editButton.Enabled = true;
            deleteButton.Enabled = true;
            startTestButton.Enabled = true;

            selectedTest = tests[index];
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            CreateTestForm createTestForm = new CreateTestForm();
            createTestForm.ShowDialog();
            LoadTests();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"edit {selectedTest.Id} {selectedTest.Name} {selectedTest.Timer} feature");
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"delete {selectedTest.Id} {selectedTest.Name} {selectedTest.Timer} feature");
        }

        private void StartTestButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"start {selectedTest.Id} {selectedTest.Name} {selectedTest.Timer} feature");
        }
    }
}
