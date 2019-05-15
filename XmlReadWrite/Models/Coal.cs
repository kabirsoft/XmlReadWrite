using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brady
{
    public class Coal
    {
        public DateTime Date { get; set; }
        public double Energy { get; set; }
        public double Price { get; set; }        
        public  double TotalHeatInput { get; set; }
        public  double ActualNetGeneration { get; set; }
        public  double EmissionsRating { get; set; }

        public readonly string Name = "Coal";

        public Coal() { }
        public Coal(DateTime date, double energy, double price, double total, double actual, double emi)
        {
            this.Date = date;
            this.Energy = energy;
            this.Price = price;
            this.TotalHeatInput = total;
            this.ActualNetGeneration = actual;
            this.EmissionsRating = emi;
        }
    }
}
