﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReadWrite
{
    public class WindOffShore
    {
        public DateTime Date;
        public double Energy;
        public double Price;
        public readonly string Location = "Offshore";
        public readonly string Name = "Wind";

        public WindOffShore(DateTime d, double e, double p)
        {
            this.Date = d;
            this.Energy = e;
            this.Price = p;
        }
    }
}
