using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WebStudioProject.Model
{
    public class ListStatus : ObservableCollection<Статус>
    {
        public ListStatus()
        {
            var queryStatus = from s in pageOrders.DataEntitiesWebStudio.Статус select s;
            foreach (Статус spec in queryStatus)
            {
                this.Add(spec);
            }
        }
    }
}
