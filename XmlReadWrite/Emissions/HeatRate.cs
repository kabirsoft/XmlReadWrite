using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReadWrite.Emissions
{
    public class HeatRate
    {
        private Dictionary<int, List<Coal>> coalList = new Dictionary<int, List<Coal>>();
        private Dictionary<int, double> dictHeatRate = new Dictionary<int, double>();

        public HeatRate(Dictionary<int, List<Coal>> _coalList)
        {
            this.coalList = _coalList;
        }

        public Dictionary<int, double> ActualHeatRates()
        {                      
            double heatRate=0;
            double TotalHeatInput=0;
            double ActualNetGeneration = 0;
            int m = 1;
            foreach (var key in coalList)
            {
                TotalHeatInput = key.Value.Select(x => x.TotalHeatInput).First();
                ActualNetGeneration = key.Value.Select(x => x.ActualNetGeneration).First();
                heatRate = (TotalHeatInput / ActualNetGeneration);
                dictHeatRate.Add(m++, heatRate);               
            }
            return dictHeatRate;
        }
    }
}
