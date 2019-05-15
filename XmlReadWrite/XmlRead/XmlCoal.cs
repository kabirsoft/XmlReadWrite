using Brady.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Brady
{
    public class XmlCoal
    {
        public XDocument xReport;
        public CultureInfo ci;
        public ParseXmlReferenceData RefData;

        public List<Coal> coalList = new List<Coal>();

        public XmlCoal(XDocument _xReport)
        {
            this.xReport = _xReport;          
        }

        public List<Coal> ReadXmlCoal()
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
                            }).First(); ;

            //Coal.TotalHeatInput = coal_TAC.TotalHeatInput;
            //Coal.ActualNetGeneration = coal_TAC.ActualNetGeneration;
            //Coal.EmissionsRating = coal_TAC.EmissionsRating;

            foreach (var item in resultCoal[0])
            {
                DateTime date = Convert.ToDateTime(item.Element("Date").Value);
                double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                double price = Convert.ToDouble(item.Element("Price").Value, ci);
                coalList.Add(new Coal(date, energy, price, coal_TAC.TotalHeatInput, coal_TAC.ActualNetGeneration, coal_TAC.EmissionsRating));
                //Console.WriteLine(date + "-" + energy + "-" + price + "-heat :" + coal_TAC.TotalHeatInput + "-net:" + coal_TAC.ActualNetGeneration + "-" + coal_TAC.EmissionsRating);
            }
            return coalList;
        }

        public double CoalTotalGeneration()
        {            
            double totalGenerationValue = 0.0;
            foreach (var item in coalList)
            {
                totalGenerationValue = totalGenerationValue + (item.Energy * item.Price * ValueFactor.Medium);
            }
            //Console.WriteLine("Coal: " + totalGenerationValue);
            return totalGenerationValue;
        }
    }
}
