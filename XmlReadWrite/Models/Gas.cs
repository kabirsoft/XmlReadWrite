using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brady
{
    public class Gas
    {
        public DateTime Date { get; set; }
        public double Energy { get; set; }
        public double Price { get; set; }
        public double EmissionsRating { get; set; }
        public readonly string Name = "Gas";

        public Gas() { }
        public Gas(DateTime date, double energy, double price, double emi)
        {
            this.Date = date;
            this.Energy = energy;
            this.Price = price;
            this.EmissionsRating = emi;            
        }
    }
}
