using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brady.Emissions
{
    public class HeatRate
    {
        public List<Coal> coalList = new List<Coal>();
        public HeatRate(List<Coal> _coalList)
        {
            this.coalList = _coalList;
        }

        public double ActualHeatRates()
        {
            var clList = coalList.First();           
            double actual_heat_rate = clList.TotalHeatInput / clList.ActualNetGeneration;            
            return actual_heat_rate;
        }
    }
}
