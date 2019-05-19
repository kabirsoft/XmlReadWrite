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
    public class XmlCoal
    {
        public CultureInfo ci;
        private XDocument xReport;        
        private List<Coal> coalList;
        private Dictionary<int, double> dictTotalGenerationValue = new Dictionary<int, double>();
        private Dictionary<int, List<Coal>> dictCoalList = new Dictionary<int, List<Coal>>();

        public XmlCoal(XDocument _xReport)
        {
            this.xReport = _xReport;            
        }

        public Dictionary<int, List<Coal>> Read()
        {
            var resultCoal = xReport.Descendants("CoalGenerator")
                   .Select(e => e.Descendants("Day"))
                   .ToList();

            var coal_TAC = (from el in xReport.Descendants("CoalGenerator")
                            select new
                            {
                                TotalHeatInput = Convert.ToDouble(el.Element("TotalHeatInput").Value, ci),
                                ActualNetGeneration = Convert.ToDouble(el.Element("ActualNetGeneration").Value, ci),
                                EmissionsRating = Convert.ToDouble(el.Element("EmissionsRating").Value, ci)
                            }).ToList();

            int m = 1;
            for (int i = 0; i < resultCoal.Count(); i++)
            {
                coalList = new List<Coal>();
                double totalGenerationValue = 0;
                foreach (var item in resultCoal[i].ToList())
                {
                    DateTime date = Convert.ToDateTime(item.Element("Date").Value);
                    double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                    double price = Convert.ToDouble(item.Element("Price").Value, ci);
                    coalList.Add(new Coal(date, energy, price, coal_TAC[i].TotalHeatInput, coal_TAC[i].ActualNetGeneration, coal_TAC[i].EmissionsRating));                    
                    totalGenerationValue = totalGenerationValue + (energy * price * ValueFactor.Medium);
                }
                dictTotalGenerationValue.Add(m, totalGenerationValue);
                dictCoalList.Add(m, coalList);
                m++;
            }
            return dictCoalList;
        }
        public Dictionary<int, double> CoalTotalGeneration()
        {
            return dictTotalGenerationValue;
        }
    }
}
