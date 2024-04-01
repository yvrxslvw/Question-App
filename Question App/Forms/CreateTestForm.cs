using Question_App.Models;
using System;
using System.Windows.Forms;

namespace Question_App.Forms
{
    public partial class CreateTestForm : Form
    {
        public CreateTestForm()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, EventArgs e)
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
                Test test = new Test(name, timer);
                test.InsertDatabase();
                Close();
            }
            catch (Exception exc)
            {
                Utils.ShowError("Произошла непредвиденная ошибка...", exc);
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
            e.Handled = true;
        }
    }
}
