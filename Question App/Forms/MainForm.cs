using DB;
using Question_App.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Question_App
{
    public partial class MainForm : Form
    {
        private readonly List<Test> tests = new List<Test> { };

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadTests()
        {
            testsListBox.Items.Clear();

            SqlDataReader reader = null;

            try
            {
                reader = Database.Select("Tests");
                Test item = null;

                while (reader.Read())
                {
                    item = new Test(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]), Convert.ToInt32(reader["Timer"]));
                    testsListBox.Items.Add($"ID: {item.Id}\t{item.Name}");
                    tests.Add(item);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(
                    text: $"{exc.Message}",
                    caption: "Ошибка",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                );
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
                MessageBox.Show(
                    text: $"Не удалось подключиться к базе данных.\n{exc.Message}",
                    caption: "Ошибка",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                );
                Application.Exit();
            }
        }

        private void TestsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (testsListBox.SelectedIndex == -1) return;

            addNewButton.Enabled = true;
            editButton.Enabled = true;
            deleteButton.Enabled = true;
            startTestButton.Enabled = true;
        }
    }
}
