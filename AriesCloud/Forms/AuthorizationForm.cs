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
    }
}
