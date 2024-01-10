using AriesCloud.Classes;
using System;
using System.Security.Cryptography;
using System.Text;
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
        private void RegistrationButtonOnClick(object sender, EventArgs e)
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
        private void EntryButtonOnClick(object sender, EventArgs e)
        {
            string token = $"{loginTextBox.Text}.*.{passwordTextBox.Text}";

            using (MD5 md5 = MD5.Create())
            {
                StringBuilder stringBuilder = new StringBuilder();

                byte[] tokenbytes = Encoding.Default.GetBytes(token);
                tokenbytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, tokenbytes);
                tokenbytes = md5.ComputeHash(tokenbytes);

                foreach (byte b in tokenbytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                token = stringBuilder.ToString();
            }

            try
            {
                if (!Core.Authorization(token))
                {
                    InfoViewer.ShowError("Логин или пароль неверный.");
                    return;
                }
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
                return;
            }

            UserData.Hash = token;
            UserData.Login = loginTextBox.Text;
            Configurator.Load(UserData.Login);

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
