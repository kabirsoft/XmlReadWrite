using XmlReadWrite.Models;
using XmlReadWrite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReadWrite.Emissions
{
   public class Emission
    {
        private List<EmissionsByDate> EmissionByDateList = new List<EmissionsByDate>();
        private List<EmissionsByDate> HighestEmissionByDateList = new List<EmissionsByDate>();
        private Dictionary<int, List<Gas>> gasList = new Dictionary<int, List<Gas>>();
        private Dictionary<int, List<Coal>> coalList = new Dictionary<int, List<Coal>>();


        public Emission(Dictionary<int, List<Gas>> g, Dictionary<int, List<Coal>> c)
        {
            this.gasList = g;
            this.coalList = c;
            this.EmissionsGasDaily();
            this.EmissionsCoalDaily();
        }
        public void EmissionsGasDaily()
        {            
            foreach (var key in gasList)
            {
                foreach (var gas in key.Value)
                {                    
                    var EmissionsDaily = gas.Energy * gas.EmissionsRating * EmissionsFactor.Medium;
                    EmissionByDateList.Add(new EmissionsByDate(gas.Date, EmissionsDaily, gas.Name + "[" + key.Key + "]"));
                }
            }
        }
        public void EmissionsCoalDaily()
        {
            foreach (var key in coalList)
            {
                foreach (var coal in key.Value)
                {
                    var EmissionsDaily = coal.Energy * coal.EmissionsRating * EmissionsFactor.High;
                    EmissionByDateList.Add(new EmissionsByDate(coal.Date, EmissionsDaily, (coal.Name + "[" + key.Key + "]")));
                }
            }
        }
        public List<EmissionsByDate> HighestEmissionByDate()
        {
            var query = EmissionByDateList.GroupBy(i=>i.Date)
              .Select(g => new
              {
                  Date = g.Key,
                  MaxEmissionsDaily = g.Max(x => x.TotalEmissionsDaily)                  
              }).OrderBy(x => x.Date);

            string name = "";
            foreach (var q in query)
            {
                name =EmissionByDateList.Where(x => x.TotalEmissionsDaily.Equals(q.MaxEmissionsDaily)).Select(n => n.Name).First().ToString();
                HighestEmissionByDateList.Add(new EmissionsByDate(q.Date, q.MaxEmissionsDaily, name));
            }
            return HighestEmissionByDateList;
        }

    }
}
