using System;
using System.Windows.Forms;

namespace Question_App
{
    public static class Utils
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(
                    text: $"{message}",
                    caption: "Ошибка",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                );
        }

        public static void ShowError(string message, Exception exception)
        {
            MessageBox.Show(
                    text: $"{message}\n{exception.Message}\n{exception.InnerException}\n{exception.Source}",
                    caption: "Ошибка",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                );
        }
    }
}
