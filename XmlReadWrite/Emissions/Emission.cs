using Brady.Models;
using Brady.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brady.Emissions
{
   public class Emission
    {

        public List<EmissionsByDate> emissionByDateList = new List<EmissionsByDate>();
        public Emission(){}

        public void EmissionsGasDaily(List<Gas> gasList)
        {
            foreach (var gas in gasList)
            {
                var EmissionsDaily = gas.Energy * gas.EmissionsRating * EmissionsFactor.Medium;
                emissionByDateList.Add(new EmissionsByDate(gas.Date, EmissionsDaily, gas.Name));
                //Console.WriteLine(gas.Date + "-" + EmissionsDaily + "-" + gas.Name);
            }
        }
        public void EmissionsCoalDaily(List<Coal> coalList)
        {
            foreach (var coal in coalList)
            {
                var EmissionsDaily = coal.Energy * coal.EmissionsRating * EmissionsFactor.High;
                emissionByDateList.Add(new EmissionsByDate(coal.Date, EmissionsDaily, coal.Name));
                //Console.WriteLine(coal.Date + "-" + EmissionsDaily + "-" + coal.Name);
            }
        }
        public List<EmissionsByDate> HighestEmissionByDate()
        {
            var query = (from list1 in emissionByDateList
                         join list2 in emissionByDateList on list1.Date equals list2.Date
                         where (list1.TotalEmissionsDaily > list2.TotalEmissionsDaily)
                         select list1
                     ).OrderBy(x => x.Date);

            //Console.WriteLine("------- Highest by date -------");
            //foreach (var q in query)
            //{
            //    Console.WriteLine(q.Date + " - " + q.TotalEmissionsDaily + " - " + q.Name);
            //}
            return query.ToList();
        }

    }
}
