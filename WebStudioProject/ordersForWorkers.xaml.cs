using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ordersForWorkers.xaml
    /// </summary>
    public partial class ordersForWorkers : Page
    {
        ObservableCollection<Заказ> ListOrders;
        public static WebStudioEntities DataEntitiesWebStudio { get; set; }
        public ordersForWorkers()
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
    }
}
