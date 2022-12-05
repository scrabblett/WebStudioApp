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
    /// Логика взаимодействия для pageQuests.xaml
    /// </summary>
    public partial class pageQuests : Page
    {
        ObservableCollection<Задание> ListQuests;
        public static WebStudioEntities DataEntitiesWebStudio { get; set; }
        public pageQuests()
        {
            DataEntitiesWebStudio = new WebStudioEntities();
            InitializeComponent();
            ListQuests = new ObservableCollection<Задание>();

            var queryQuests = from w in DataEntitiesWebStudio.Задание
                              orderby w.КодЗаказа
                              select w;
            foreach (Задание w in queryQuests)
            {
                ListQuests.Add(w);
            }
            dataGridOrders.ItemsSource = ListQuests;
        }
        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            Задание quest = new Задание();
            DataEntitiesWebStudio.Задание.Add(quest);
            ListQuests.Add(quest);
            dataGridOrders.ScrollIntoView(quest);
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
            RewriteQuests();
        }

        private void btnDelete_click(object sender, RoutedEventArgs e)
        {
            Задание quest = dataGridOrders.SelectedItem as Задание;
            if (quest != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить выбранное задание?",
                    "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesWebStudio.Задание.Remove(quest);
                    dataGridOrders.SelectedIndex = dataGridOrders.SelectedIndex == 0 ? 1 : dataGridOrders.SelectedIndex - 1;
                    ListQuests.Remove(quest);
                    DataEntitiesWebStudio.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления");
                }
            }
        }
        private void GetQuests()
        {
            var queryQuests = from w in DataEntitiesWebStudio.Задание
                              orderby w.КодЗаказа
                              select w;
            foreach (Задание w in queryQuests)
            {
                ListQuests.Add(w);
            }
            dataGridOrders.ItemsSource = ListQuests;
        }

        private void RewriteQuests()
        {
            DataEntitiesWebStudio = new WebStudioEntities();
            ListQuests.Clear();
            GetQuests();
        }
    }
}
