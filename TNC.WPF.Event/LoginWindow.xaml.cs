using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TNC.WPF.Event.Data;
using TNC.WPF.Event.Infrastucture;
using TNC.WPF.Event.Models;

namespace TNC.WPF.Event
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string secureCode;

        async void disableButton()
        {
            RefreshBtn.IsEnabled = false;
            CodeBox.IsEnabled = true;
            LoginBtn.IsEnabled = true;
            await Task.Delay(TimeSpan.FromSeconds(10));
            RefreshBtn.IsEnabled = true;
            CodeBox.IsEnabled = false;
            LoginBtn.IsEnabled = false;
        }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                User user = db.Users.Where(u => u.Number == NumberBox.Text).Include(u => u.Role).FirstOrDefault() as User;

                if (user != null)
                {
                    Password.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Неправильный номер сотрудника");
                    return;
                }
                if (Password.Password == "")
                {
                    return;
                }

                if (user.Password == Password.Password)
                {
                    if (CodeBox.Text == "")
                    {
                        // генерация кода доступа
                        secureCode = CodeGeneration.Refresh();//CaptchaBuild.Refresh();
                        // эмуляция СМС
                        MessageBox.Show($"Код: {secureCode}");
                    }

                    CodeBox.Focus();
                    disableButton();
                }
                else
                {
                    MessageBox.Show("Неправильный пароль");
                    return;
                }

                if (CodeBox.Text == secureCode)
                {
                    MessageBox.Show($"Здравствуйте, {user.Name}.\nВы: {user.Role.Name}");
                }
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            secureCode = CodeGeneration.Refresh();//CaptchaBuild.Refresh();
            MessageBox.Show($"Код доступа: {secureCode}");
            disableButton();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            NumberBox.Text = "";
            Password.Password = "";
            CodeBox.Text = "";
        }
    }
}
