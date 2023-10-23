using AriesCloud.Classes;
using System;
using System.Windows.Forms;

namespace AriesCloud.Forms
{
    /// <summary>
    /// Форма авторизации.
    /// </summary>
    public partial class AuthorizationForm : Form
    {
        /// <summary>
        /// Создание формы авторизации.
        /// </summary>
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик кнопки "Регистрация".
        /// </summary>
        /// <param name="sender">Кнопка "Регистрация".</param>
        /// <param name="e">Данные события.</param>
        private void RegistrationButtonOnClick(object sender, System.EventArgs e)
        {
            using (RegistrationForm registrationForm = new RegistrationForm())
            {
                registrationForm.ShowDialog();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Войти".
        /// </summary>
        /// <param name="sender">Кнопка "Войти".</param>
        /// <param name="e">Данные события.</param>
        private void EntryButtonOnClick(object sender, System.EventArgs e)
        {
            // TODO: Сделать проверку авторизации.

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Обработчик ссылки "Забыли пароль?".
        /// </summary>
        /// <param name="sender">Ссылка "Забыли пароль?".</param>
        /// <param name="e">Данные события.</param>
        private void ForgotPasswordLinkLabelOnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (String.IsNullOrEmpty(loginTextBox.Text))
            {
                InfoViewer.ShowError("Введите логин.");
            }

            // TODO: Сделать запрос к серверу на сброс пароля.

            InfoViewer.ShowInformation("На вашу электронную почту отправленна ссылка на сброс паролья. Сбросьте пароль и пропробуйте войти снова.");
        }
    }
}
