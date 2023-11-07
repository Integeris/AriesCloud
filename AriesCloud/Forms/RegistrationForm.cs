using AriesCloud.Classes;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AriesCloud.Forms
{
    /// <summary>
    /// Форма регистрации.
    /// </summary>
    public partial class RegistrationForm : Form
    {
        /// <summary>
        /// Создание формы регистрации.
        /// </summary>
        public RegistrationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик кнопки "Регистрация".
        /// </summary>
        /// <param name="sender">Кнопка "Регистрация".</param>
        /// <param name="e">Данные события.</param>
        private void RegistrationButtonOnClick(object sender, EventArgs e)
        {
            string loginPattern = @"^[A-Za-z][A-Za-z0-9]{7,49}$";

            if (!Regex.IsMatch(emailTextBox.Text, @"^[^\s~,\.]{1,}@[^\s~,\.]{1,}\.[^\s~,\.]{1,}(\.[^\s~,\.]{1,})*$"))
            {
                InfoViewer.ShowError("Укажите верную почту.");
                return;
            }
            else if (!Regex.IsMatch(loginTextBox.Text, loginPattern))
            {
                InfoViewer.ShowError("Логин должен состаять из латинских букв и цифр от 8 по 50 символов. Первый символ е может быть цифрой.");
                return;
            }
            else if (!Regex.IsMatch(passwordTextBox.Text, loginPattern))
            {
                InfoViewer.ShowError("Пароль должен состаять из латинских букв и цифр от 8 по 50 символов. Первый символ е может быть цифрой.");
                return;
            }
            else if (loginTextBox.Text == passwordTextBox.Text)
            {
                InfoViewer.ShowError("Логин и пароль не могут быть одинаковыми.");
                return;
            }
            else if (passwordTextBox.Text != confirmTextBox.Text)
            {
                InfoViewer.ShowError("Пароль и подтверждение пароля не совпадают.");
                return;
            }

            try
            {
                Core.Registration(emailTextBox.Text, loginTextBox.Text, passwordTextBox.Text);
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
                return;
            }

            InfoViewer.ShowInformation("Сообщение подтверждения регистрации отправлено на электронную почту.");
            Close();
        }
    }
}
