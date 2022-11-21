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
    /// Логика взаимодействия для pageWorks.xaml
    /// </summary>
    public partial class pageWorks : Page
    {
        ObservableCollection<Работа> ListWorks;
        public static WebStudioEntities DataEntitiesWebStudio { get; set; }
        public pageWorks()
        {
            DataEntitiesWebStudio = new WebStudioEntities();
            InitializeComponent();
            ListWorks = new ObservableCollection<Работа>();

            var queryWorks = from w in DataEntitiesWebStudio.Работа
                              orderby w.КодРаботы
                              select w;
            foreach (Работа w in queryWorks)
            {
                ListWorks.Add(w);
            }
            dataGridOrders.ItemsSource = ListWorks;
        }
        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            Работа work = new Работа();
            DataEntitiesWebStudio.Работа.Add(work);
            ListWorks.Add(work);
            dataGridOrders.ScrollIntoView(work);
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
            Работа work = dataGridOrders.SelectedItem as Работа;
            if (work != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить выбранную работу?",
                    "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesWebStudio.Работа.Remove(work);
                    dataGridOrders.SelectedIndex = dataGridOrders.SelectedIndex == 0 ? 1 : dataGridOrders.SelectedIndex - 1;
                    ListWorks.Remove(work);
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
