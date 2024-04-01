using Question_App.Models;
using System;
using System.Windows.Forms;

namespace Question_App.Forms
{
    public partial class TestForm : Form
    {
        private Test test;
        private Timer timer;
        private int correctAnswers;
        private int currentQuestion;
        private int timeLeft;

        public TestForm(Test test)
        {
            InitializeComponent();
            this.test = test;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            string answer = answerTextBox.Text.ToLower();
            if (answer == test.questions[currentQuestion].Answer) correctAnswers++;
            if (currentQuestion + 1 == test.questions.Count)
            {
                CheckResult();
                return;
            }

            currentQuestion++;
            DisplayQuestion();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            CheckResult();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            correctAnswers = 0;
            currentQuestion = 0;
            timeLeft = (int)test.Timer * 60;

            timerLabel.Text = timeLeft.ToString();
            timer.Interval = 1000;
            timer.Tick += TimerTick;
            timer.Start();

            DisplayQuestion();
        }

        private void DisplayQuestion()
        {
            questionLabel.Text = test.questions[currentQuestion].Content;
            answerTextBox.Text = string.Empty;
            answerTextBox.Focus();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            timeLeft -= 1;

            if (test.Timer == 0)
            {
                CheckResult();
            }

            timerLabel.Text = timeLeft.ToString();
        }

        private void CheckResult()
        {
            timer.Stop();
            double percent = Math.Round(((double)correctAnswers /test.questions.Count) * 100f, 1);
            MessageBox.Show(
                caption: "Результаты теста",
                text: $"Поздравляю с завершением теста!\n\nПравильных ответов: {correctAnswers} из {test.questions.Count} ({percent}%)",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Information
            );
            test = null;
            timer = null;
            correctAnswers = 0;
            currentQuestion = 0;
            Dispose();
        }
    }
}
