using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReadWrite
{
    public class WindOnshore
    {
        public DateTime Date;
        public double Energy;
        public double Price;
        public readonly string Location = "Onshore";
        public readonly string Name = "Wind";

        public WindOnshore(DateTime d, double e, double p)
        {
            this.Date = d;
            this.Energy = e;
            this.Price = p;
        }

    }
}
