using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReadWrite
{
    public class Gas
    {
        public DateTime Date;
        public double Energy;
        public double Price;
        public double EmissionsRating;
        public readonly string Name = "Gas";
        
        public Gas(DateTime date, double energy, double price, double emi)
        {
            this.Date = date;
            this.Energy = energy;
            this.Price = price;
            this.EmissionsRating = emi;            
        }
    }
}
