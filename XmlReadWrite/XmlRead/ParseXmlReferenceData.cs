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
    public class ParseXmlReferenceData
    {
        public XDocument xReference;
        public CultureInfo ci;

        public ParseXmlReferenceData(){}

        public void ReadXml()
        {
            try
            {
                this.xReference = XDocument.Load("C:\\Asp.Net_project\\XmlReadWrite\\XmlReadWrite\\ReferenceData.xml");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            this.ReadValueFactor();
            this.ReadEmissionsFactor();
        }
     
        public  void ReadValueFactor()
        {
            //var values = this.xReference.Descendants("ValueFactor").ToList();
            var values = (from el in xReference.Descendants("ValueFactor")
                          select new
                          {
                              High = Convert.ToDouble(el.Element("High").Value, ci),
                              Medium = Convert.ToDouble(el.Element("Medium").Value, ci),
                              Low = Convert.ToDouble(el.Element("Low").Value, ci)

                          }).First();

            ValueFactor.High = values.High;
            ValueFactor.Medium = values.Medium;
            ValueFactor.Low = values.Low;
        }

        public void ReadEmissionsFactor()
        {
            //EmissionsFactor
            //var EmissionsFactor = xReference.Descendants("EmissionsFactor").ToList();
            var values = (from el in xReference.Descendants("EmissionsFactor")
                               select new
                               {
                                   High = Convert.ToDouble(el.Element("High").Value, ci),
                                   Medium = Convert.ToDouble(el.Element("Medium").Value, ci),
                                   Low = Convert.ToDouble(el.Element("Low").Value, ci)
                               }).First();

            EmissionsFactor.High = values.High;
            EmissionsFactor.Medium = values.Medium;
            EmissionsFactor.Low = values.Low;
            //Console.WriteLine(EmissionsFactor.High + "-" + EmissionsFactor.Medium + "-" + EmissionsFactor.Low);
        }
        
    }
}
