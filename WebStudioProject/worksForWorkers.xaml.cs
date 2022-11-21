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
    /// Логика взаимодействия для worksForWorkers.xaml
    /// </summary>
    public partial class worksForWorkers : Page
    {
        ObservableCollection<Работа> ListWorks;
        public static WebStudioEntities DataEntitiesWebStudio { get; set; }
        public worksForWorkers()
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
    }
}
