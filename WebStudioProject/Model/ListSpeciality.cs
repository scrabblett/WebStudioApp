using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WebStudioProject.Model
{
    public class ListSpeciality : ObservableCollection<Специальности>
    {
        public ListSpeciality()
        {
            var querySpeciality = from s in pageWorkers.DataEntitiesWebStudio.Специальности select s;
            foreach (Специальности spec in querySpeciality)
            {
                this.Add(spec);
            }
        }
    }
}
