using System;
using System.Collections.Generic;
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
                    caption: "Ошибка",
                    text: $"{message}\n\n{exception.Message}",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                );
        }

        public static void ShuffleList<T>(ref List<T> list)
        {
            int len = list.Count;
            Random rnd = new Random();

            for (int i = 0; i < len; i++)
            {
                int index = rnd.Next(0, len - 1);
                (list[i], list[index]) = (list[index], list[i]);
            }
        }
    }
}
