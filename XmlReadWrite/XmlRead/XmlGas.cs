using XmlReadWrite.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace XmlReadWrite
{
    public class XmlGas
    {
        public CultureInfo ci;
        private XDocument xReport;
        private List<Gas> gaslist;      
        private Dictionary<int, double> dictTotalGenerationValue = new Dictionary<int, double>();
        private Dictionary<int, List<Gas>> dictGasList = new Dictionary<int, List<Gas>>();

        public XmlGas(XDocument _xReport)
        {
            this.xReport = _xReport;            
        }
        public Dictionary<int, List<Gas>> Read()
        {
            var resultGas = xReport.Descendants("GasGenerator")
                    .Select(e => e.Descendants("Day"))
                    .ToList();

            
            var Emission = xReport.Descendants("GasGenerator")
                           .Select(e=>e.Elements("EmissionsRating")).ToList();
            int m = 1;            
            for (int i = 0; i < resultGas.Count(); i++)
            {
                double totalGenerationValue = 0;

                Double EmissionRating= Convert.ToDouble(Emission.ElementAt(i).First().Value, ci); //Get emission
                gaslist = new List<Gas>();
                foreach (var item in resultGas[i].ToList())
                {
                   DateTime date = Convert.ToDateTime(item.Element("Date").Value);
                   double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                   double price = Convert.ToDouble(item.Element("Price").Value, ci);
                    gaslist.Add(new Gas(date, energy, price, EmissionRating));
                    totalGenerationValue = totalGenerationValue + (energy * price * ValueFactor.Medium);
                }

                dictTotalGenerationValue.Add(m, totalGenerationValue);
                dictGasList.Add(m, gaslist);
                m++;
            }
            return dictGasList;
        }
        public Dictionary<int, double> GasTotalGeneration()
        {   
            return dictTotalGenerationValue;          
        }

    }
}
