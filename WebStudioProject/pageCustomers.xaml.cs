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
    /// Логика взаимодействия для pageCustomers.xaml
    /// </summary>
    public partial class pageCustomers : Page
    {
        ObservableCollection<Заказчик> ListCustomers;
        public static WebStudioEntities DataEntitiesWebStudio { get; set; }
        public pageCustomers()
        {
            DataEntitiesWebStudio = new WebStudioEntities();
            InitializeComponent();
            ListCustomers = new ObservableCollection<Заказчик>();

            var queryWorks = from w in DataEntitiesWebStudio.Заказчик
                             orderby w.КодЗаказчика
                             select w;
            foreach (Заказчик w in queryWorks)
            {
                ListCustomers.Add(w);
            }
            dataGridOrders.ItemsSource = ListCustomers;
        }
        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            Заказчик customer = new Заказчик();
            DataEntitiesWebStudio.Заказчик.Add(customer);
            ListCustomers.Add(customer);
            dataGridOrders.ScrollIntoView(customer);
            dataGridOrders.SelectedIndex = dataGridOrders.Items.Count - 1;
            dataGridOrders.Focus();
            dataGridOrders.IsReadOnly = false;
        }

        private void btnEdit_click(object sender, RoutedEventArgs e)
        {
            dataGridOrders.IsReadOnly = false;
            if (dataGridOrders.SelectedItem != null)
            {
                var cell = dataGridOrders.SelectedCells[0];
                dataGridOrders.CurrentCell = cell;
            }
            else
            {
                dataGridOrders.CurrentCell = new DataGridCellInfo
                    (
                        dataGridOrders.Items[0],
                        dataGridOrders.Columns[0]
                    );
            }
            dataGridOrders.BeginEdit();
        }

        private void btnSave_click(object sender, RoutedEventArgs e)
        {
            DataEntitiesWebStudio.SaveChanges();
            dataGridOrders.IsReadOnly = true;
            MessageBox.Show("Данные успешно сохранены.");
        }

        private void btnDelete_click(object sender, RoutedEventArgs e)
        {
            Заказчик customer = dataGridOrders.SelectedItem as Заказчик;
            if (customer != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить выбранного заказчика?",
                    "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesWebStudio.Заказчик.Remove(customer);
                    dataGridOrders.SelectedIndex = dataGridOrders.SelectedIndex == 0 ? 1 : dataGridOrders.SelectedIndex - 1;
                    ListCustomers.Remove(customer);
                    DataEntitiesWebStudio.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления");
                }
            }
        }
    }
}
