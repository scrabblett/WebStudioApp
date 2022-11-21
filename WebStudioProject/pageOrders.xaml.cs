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
    /// Логика взаимодействия для pageOrders.xaml
    /// </summary>
    public partial class pageOrders : Page
    {
        ObservableCollection<Заказ> ListOrders;
        public static WebStudioEntities DataEntitiesWebStudio { get; set; }
        public pageOrders()
        {
            DataEntitiesWebStudio = new WebStudioEntities();
            InitializeComponent();
            ListOrders = new ObservableCollection<Заказ>();

            var queryOrders = from w in DataEntitiesWebStudio.Заказ
                               orderby w.ДатаЗаказа
                               select w;
            foreach (Заказ w in queryOrders)
            {
                ListOrders.Add(w);
            }
            dataGridOrders.ItemsSource = ListOrders;
        }
        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            Заказ order = new Заказ();
            DataEntitiesWebStudio.Заказ.Add(order);
            ListOrders.Add(order);
            dataGridOrders.ScrollIntoView(order);
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
            Заказ order = dataGridOrders.SelectedItem as Заказ;
            if (order != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить выбранный заказ?",
                    "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesWebStudio.Заказ.Remove(order);
                    dataGridOrders.SelectedIndex = dataGridOrders.SelectedIndex == 0 ? 1 : dataGridOrders.SelectedIndex - 1;
                    ListOrders.Remove(order);
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
