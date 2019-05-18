using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReadWrite
{
    public class Coal
    {
        public DateTime Date;
        public double Energy;
        public double Price;
        public double TotalHeatInput;
        public double ActualNetGeneration;
        public double EmissionsRating;

        public readonly string Name = "Coal";
       
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
