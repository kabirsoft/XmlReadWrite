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
    public class XmlGas
    {
        public XDocument xReport;
        public ParseXmlReferenceData RefData;
        public CultureInfo ci;

        public List<Gas> gasList = new List<Gas>();


        public XmlGas(XDocument _xReport)
        {
            this.xReport = _xReport;            
        }
        public List<Gas> ReadXmlGas()
        {
            var resultGas = xReport.Descendants("GasGenerator")
                    .Select(e => e.Descendants("Day"))
                    .ToList();

            var Emission = (string)(from el in xReport.Descendants("GasGenerator")
                                    select el.Element("EmissionsRating")).First();



            Double EmissionRating = Convert.ToDouble(Emission, ci);            

            foreach (var item in resultGas[0])
            {
                DateTime date = Convert.ToDateTime(item.Element("Date").Value);
                double energy = Convert.ToDouble(item.Element("Energy").Value, ci);
                double price = Convert.ToDouble(item.Element("Price").Value, ci);
                gasList.Add(new Gas(date, energy, price, EmissionRating));
                //Console.WriteLine( date + " - "+ energy + "-" + price+ "-" + EmissionRating );
            }

            return gasList;
        }

        public double GasTotalGeneration()
        {
            double totalGenerationValue = 0.0;
            foreach (var item in gasList)
            {
                totalGenerationValue = totalGenerationValue + (item.Energy * item.Price * ValueFactor.Medium);
            }
            //Console.WriteLine("Gas: " + totalGenerationValue);
            return totalGenerationValue;
        }

    }
}
