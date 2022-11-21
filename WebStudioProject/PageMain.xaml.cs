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

namespace WebStudioProject
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        static public string userFirstName = "";
        static public string userLastName = "";
        static public int userID;
        public PageMain()
        {
            InitializeComponent();
        }

        private void btnOk_click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            string userType = "";
            WebStudioEntities DbWebStudio = new WebStudioEntities();
            using (DbWebStudio)
            {
                var users = DbWebStudio.Пользователь;
                foreach (Пользователь u in users)
                {
                    var passBxMD5 = registrationPage.GetHash(passBox.Password);
                    if(tBxLogin.Text == u.login && passBxMD5 == u.password)
                    {
                        valid = true;
                        userType = u.type;
                    }
                    if (userType == "исполнитель")
                    {
                        var queryWorker = from w in DbWebStudio.Исполнитель
                                          where w.IDuser == u.IDuser
                                          select w;
                        foreach (Исполнитель w in queryWorker)
                        {
                            userFirstName = w.Имя;
                            userLastName = w.Фамилия;
                            userID = (int)w.IDuser;
                            workersWindow workersWindow = new workersWindow();
                            workersWindow.Show();
                            App.Current.MainWindow.Close();
                        }
                    }
                    if (userType == "администратор")
                    {
                        var queryAdmins = from a in DbWebStudio.Администратор
                                          where a.IDuser == u.IDuser
                                          select a;
                        foreach (Администратор a in queryAdmins)
                        {
                            userFirstName = a.Имя;
                            userLastName = a.Фамилия;
                            userID = (int)a.IDuser;
                            secondWindow secondWindow = new secondWindow();
                            secondWindow.Show();
                            App.Current.MainWindow.Close();
                            
                        }
                    }
                }
                if (valid == true)
                {
                    MessageBox.Show("Верный логин и пароль. Добро пожаловать, " + userType + " " + userFirstName + " " + userLastName);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль. Повторите попытку входа.");
                }
            }
        }

        private void btnRegistration_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("registrationPage.xaml", UriKind.Relative));
        }
    }
}
