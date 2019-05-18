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
    public class XmlOffshore
    {
        public CultureInfo ci;
        private XDocument xReport; 
        private List<WindOffShore> offshorList = new List<WindOffShore>();
        private double totalGenerationValue;

        public XmlOffshore(XDocument _xReport)
        {
            this.xReport = _xReport;           
        }
        public List<WindOffShore> ReadXmlOffshore()
        {
            var resultWind = xReport.Descendants("WindGenerator")
                    .Select(e => e.Descendants("Day"))
                    .ToList();

            totalGenerationValue = 0;
            foreach (var item in resultWind[0])
            {
                DateTime date = Convert.ToDateTime(item.Element("Date").Value);
                double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                double price = Convert.ToDouble(item.Element("Price").Value, ci);
                totalGenerationValue = totalGenerationValue + (energy * price * ValueFactor.Low);
                offshorList.Add(new WindOffShore(date, energy, price));
            }
            return offshorList;
        }
        public double OffshoreTotalGeneration()
        {  
            return totalGenerationValue;
        }
    }
}
