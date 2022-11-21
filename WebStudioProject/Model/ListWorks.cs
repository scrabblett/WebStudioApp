using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WebStudioProject.Model
{
    public class ListWorks : ObservableCollection<Работа>
    {
        public ListWorks()
        {
            var queryWorks = from s in pageQuests.DataEntitiesWebStudio.Работа select s;
            foreach (Работа spec in queryWorks)
            {
                this.Add(spec);
            }
        }
    }
}
