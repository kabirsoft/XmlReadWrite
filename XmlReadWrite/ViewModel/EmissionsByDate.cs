using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReadWrite.ViewModel
{
    public class EmissionsByDate
    {
        public DateTime Date { get; set; }
        public double TotalEmissionsDaily { get; set; }
        public string Name { get; set; }

        public EmissionsByDate(DateTime date, double total, string name)
        {
            this.Date = date;
            this.TotalEmissionsDaily = total;
            this.Name = name;
        }
    }
}
