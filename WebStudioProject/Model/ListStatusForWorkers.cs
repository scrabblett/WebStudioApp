using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStudioProject.Model
{
    public class ListStatusForWorkers: ObservableCollection<Статус>
    {
        public ListStatusForWorkers()
        {
            var queryStatus = from s in ordersForWorkers.DataEntitiesWebStudio.Статус select s;
            foreach (Статус spec in queryStatus)
            {
                this.Add(spec);
            }
        }
    }
}
