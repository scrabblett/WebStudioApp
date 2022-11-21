using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace WebStudioProject
{
    /// <summary>
    /// Логика взаимодействия для registrationPage.xaml
    /// </summary>
    public partial class registrationPage : Page
    {
        public registrationPage()
        {
            InitializeComponent();
        }

        public static string GetHash(string input) //кодирование пароля
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            bool existsLogin = false;
            WebStudioEntities DbWebStudio = new WebStudioEntities();
            using (DbWebStudio)
            {
                var users = DbWebStudio.Пользователь;
                foreach (Пользователь u in users)
                {
                    if(tBxLogin.Text == u.login)
                    {
                        MessageBox.Show("Такой логин уже существует!");
                        existsLogin = true;
                    }
                }
                if (existsLogin == false)
                {
                    WebStudioEntities DbWebStudio1 = new WebStudioEntities();
                    using (DbWebStudio1)
                    {
                        int maxIDuser = (from us in DbWebStudio1.Пользователь
                                         select us.IDuser).Max();
                        Пользователь user = new Пользователь();
                        user.login = tBxLogin.Text;
                        user.password = GetHash(passBx.Password);
                        user.type = "администратор";
                        user.IDuser = maxIDuser + 1;
                        DbWebStudio1.Пользователь.Add(user);
                        DbWebStudio1.SaveChanges();
                        Администратор admin = new Администратор();
                        admin.Имя = tBxName.Text;
                        admin.Фамилия = tBxSurname.Text;
                        admin.Дата_рождения = dpAdmin.SelectedDate;
                        admin.IDuser = maxIDuser + 1;
                        DbWebStudio1.Администратор.Add(admin);
                        DbWebStudio1.SaveChanges();
                        MessageBox.Show("Регистрация прошла успешно.");
                        NavigationService.Navigate(new Uri("PageMain.xaml", UriKind.Relative));
                    }
                }
            }
        }
    }
}
