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

        public List<EmissionsByDate> EmissionByDateList = new List<EmissionsByDate>();
        public List<EmissionsByDate> HighestEmissionByDateList = new List<EmissionsByDate>();

        public Emission(){}
        public void EmissionsGasDaily(Dictionary<int, List<Gas>> gasList)
        {
            //Console.WriteLine("------------- Gas --------------");
            foreach (var key in gasList)
            {
                //Console.WriteLine("------------"+ key.Key  +"---------------");
                foreach (var gas in key.Value)
                {                    
                    var EmissionsDaily = gas.Energy * gas.EmissionsRating * EmissionsFactor.Medium;
                    EmissionByDateList.Add(new EmissionsByDate(gas.Date, EmissionsDaily, gas.Name + "[" + key.Key + "]"));
                    //Console.WriteLine(gas.Date + "-" + EmissionsDaily + "-" + gas.Name);
                }
            }
        }
        public void EmissionsCoalDaily(Dictionary<int, List<Coal>> coalList)
        {
            //Console.WriteLine("------------------- coal --------------");
            foreach (var key in coalList)
            {
                //Console.WriteLine("------------" + key.Key + "---------------");
                foreach (var coal in key.Value)
                {
                    //Console.WriteLine(coal.Name + "[" + key.Key + "]");
                    var EmissionsDaily = coal.Energy * coal.EmissionsRating * EmissionsFactor.High;
                    EmissionByDateList.Add(new EmissionsByDate(coal.Date, EmissionsDaily, (coal.Name + "[" + key.Key + "]")));
                    //Console.WriteLine(coal.Date + "-" + EmissionsDaily + "-" + coal.Name);
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
            //Console.WriteLine("------- Highest by date -------");
            string name = "";
            foreach (var q in query)
            {
                name =EmissionByDateList.Where(x => x.TotalEmissionsDaily.Equals(q.MaxEmissionsDaily)).Select(n => n.Name).First().ToString();
                //Console.WriteLine(q.Date + " - " + q.MaxEmissionsDaily + "-" + name);
                HighestEmissionByDateList.Add(new EmissionsByDate(q.Date, q.MaxEmissionsDaily, name));
            }
            return HighestEmissionByDateList;
        }

    }
}
