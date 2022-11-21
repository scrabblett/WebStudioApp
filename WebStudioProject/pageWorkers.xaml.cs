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
using System.Collections.ObjectModel;

namespace WebStudioProject
{
    /// <summary>
    /// Логика взаимодействия для pageWorkers.xaml
    /// </summary>
    public partial class pageWorkers : Page
    {
        ObservableCollection<Исполнитель> ListWorkers;
        public static WebStudioEntities DataEntitiesWebStudio { get; set; }
        public pageWorkers()
        {
            DataEntitiesWebStudio = new WebStudioEntities();
            InitializeComponent();
            ListWorkers = new ObservableCollection<Исполнитель>();
            var queryWorkers = from w in DataEntitiesWebStudio.Исполнитель
                               orderby w.Имя
                               select w;
            foreach (Исполнитель w in queryWorkers)
            {
                ListWorkers.Add(w);
            }
            dataGridWorkers.ItemsSource = ListWorkers;
            
        }

        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            BorderEnterLoginPassword.Visibility = Visibility.Visible;
        }

        private void btnEdit_click(object sender, RoutedEventArgs e)
        {
            dataGridWorkers.IsReadOnly = false;
            if (dataGridWorkers.SelectedItem != null)
            {
                var cell = dataGridWorkers.SelectedCells[0];
                dataGridWorkers.CurrentCell = cell;
            }
            else
            {
                dataGridWorkers.CurrentCell = new DataGridCellInfo
                    (
                        dataGridWorkers.Items[0],
                        dataGridWorkers.Columns[0]
                    );
            }
            dataGridWorkers.BeginEdit();
        }

        private void btnSave_click(object sender, RoutedEventArgs e)
        {
            DataEntitiesWebStudio.SaveChanges();
            dataGridWorkers.IsReadOnly = true;
            MessageBox.Show("Данные успешно сохранены.");
        }

        private void btnDelete_click(object sender, RoutedEventArgs e)
        {
            Исполнитель worker = dataGridWorkers.SelectedItem as Исполнитель;
            if (worker != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить исполнителя: " + worker.Имя + " " + worker.Фамилия + " ",
                    "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesWebStudio.Исполнитель.Remove(worker);
                    dataGridWorkers.SelectedIndex = dataGridWorkers.SelectedIndex == 0 ? 1 : dataGridWorkers.SelectedIndex - 1;
                    ListWorkers.Remove(worker);
                    DataEntitiesWebStudio.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления");
                }
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            BorderEnterLoginPassword.Visibility = Visibility.Visible;
            int countLogin = (from us in DataEntitiesWebStudio.Пользователь
                              where tBxLogin.Text== us.login
                              select us).Count();
            if(countLogin != 0)
            {
                MessageBox.Show("Такой логин уже существует.");
            }
            else
            {
                int maxIDuser = (from us in DataEntitiesWebStudio.Пользователь
                                 select us.IDuser).Max();
                Пользователь user = new Пользователь();
                user.login = tBxLogin.Text;
                user.password = registrationPage.GetHash(passBox.Password);
                user.type = "исполнитель";
                user.IDuser = maxIDuser + 1;
                DataEntitiesWebStudio.Пользователь.Add(user);
                DataEntitiesWebStudio.SaveChanges();
                BorderEnterLoginPassword.Visibility = Visibility.Hidden;
                EnterWorker(user.IDuser);
            }
        }
        private void EnterWorker(int IDuser)
        {
            Исполнитель worker = new Исполнитель();
            worker.IDuser = IDuser;
            DataEntitiesWebStudio.Исполнитель.Add(worker);
            ListWorkers.Add(worker);
            dataGridWorkers.ScrollIntoView(worker);
            dataGridWorkers.SelectedIndex = dataGridWorkers.Items.Count - 1;
            dataGridWorkers.Focus();
            dataGridWorkers.IsReadOnly = false;
        }

        private void btnFind_click(object sender, RoutedEventArgs e)
        {
            BorderFind.Visibility = Visibility.Visible;
        }

        private void btnFindWindow_Click(object sender, RoutedEventArgs e)
        {
            BorderFind.Visibility = Visibility.Hidden;
            string result = "";
            if (tBxFind.Text != "")
            {
                var queryFind = from f in DataEntitiesWebStudio.Исполнитель
                                where f.Фамилия == tBxFind.Text
                                join s in DataEntitiesWebStudio.Специальности
                                on f.КодСпециальности equals s.КодСпециальности
                                select f;
                foreach (Исполнитель w in queryFind)
                {
                    result = result + w.Имя + " " + w.Фамилия + " " + w.Специальности.Специальность + " " +  w.Заработная_плата;
                }
                MessageBox.Show(result);
            }
            

        }
    }
}
