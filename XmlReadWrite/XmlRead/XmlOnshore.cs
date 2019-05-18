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
    public class XmlOnshore
    {
        public CultureInfo ci;
        private XDocument xReport; 
        private List<WindOnshore> onshorList = new List<WindOnshore>();
        private double totalGenerationValue;

        public XmlOnshore(XDocument _xReport)
        {
            this.xReport = _xReport;           
        }
        
        public List<WindOnshore>  ReadXmlOnshore()
        {
            var resultWind = xReport.Descendants("WindGenerator")
                    .Select(e => e.Descendants("Day"))
                    .ToList();

            totalGenerationValue = 0;
            foreach (var item in resultWind[1])
            {
                var date = Convert.ToDateTime(item.Element("Date").Value);
                double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                double price = Convert.ToDouble(item.Element("Price").Value, ci);
                totalGenerationValue = totalGenerationValue + (energy * price * ValueFactor.High);
                onshorList.Add(new WindOnshore(date, energy, price));
            }
            return onshorList;
        }
        public double OnshoreTotalGeneration()
        {          
            return totalGenerationValue;
        }
    }
}
