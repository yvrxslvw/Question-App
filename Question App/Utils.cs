﻿using System;
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
    }
}
