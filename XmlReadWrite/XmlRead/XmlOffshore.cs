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
        public XDocument xReport;       
        public ParseXmlReferenceData RefData;
        public CultureInfo ci;

        public List<WindOffShore> offshorList = new List<WindOffShore>();

        public XmlOffshore(XDocument _xReport)
        {
            this.xReport = _xReport;           
        }
        public List<WindOffShore> ReadXmlOffshore()
        {
            var resultWind = xReport.Descendants("WindGenerator")
                    .Select(e => e.Descendants("Day"))
                    .ToList();

            foreach (var item in resultWind[0])
            {
                DateTime date = Convert.ToDateTime(item.Element("Date").Value);
                double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                double price = Convert.ToDouble(item.Element("Price").Value, ci);
                offshorList.Add(new WindOffShore(date, energy, price));
            }
            return offshorList;
        }
        public double OffshoreTotalGeneration()
        {            
            double totalGenerationValue = 0.0;
            foreach (var item in offshorList)
            {
                totalGenerationValue = totalGenerationValue + (item.Energy * item.Price * ValueFactor.Low);
            }
            //Console.WriteLine("Offshore: " + totalGenerationValue);
            return totalGenerationValue;
        }
    }
}
