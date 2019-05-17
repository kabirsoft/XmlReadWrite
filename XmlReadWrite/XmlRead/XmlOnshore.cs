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
        public XDocument xReport;
        public ParseXmlReferenceData RefData;
        public CultureInfo ci;
        public List<WindOnshore> onshorList = new List<WindOnshore>();

        public XmlOnshore(XDocument _xReport)
        {
            this.xReport = _xReport;           
        }
        
        public List<WindOnshore>  ReadXmlOnshore()
        {
            var resultWind = xReport.Descendants("WindGenerator")
                    .Select(e => e.Descendants("Day"))
                    .ToList();
            foreach (var item in resultWind[1])
            {
                var date = Convert.ToDateTime(item.Element("Date").Value);
                double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                double price = Convert.ToDouble(item.Element("Price").Value, ci);
                onshorList.Add(new WindOnshore(date, energy, price));
                //Console.WriteLine(date + "-" + energy + price);
            }
            return onshorList;
        }
        public double OnshoreTotalGeneration()
        {            
            double totalGenerationValue = 0.0;
            foreach (var item in onshorList)
            {
                totalGenerationValue = totalGenerationValue + (item.Energy * item.Price * ValueFactor.High);
            }
            //Console.WriteLine("Onshore: " + totalGenerationValue);
            return totalGenerationValue;
        }
    }
}
