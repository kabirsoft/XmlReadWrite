using XmlReadWrite.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlReadWrite
{
    public class ParseXmlReferenceData
    {
        public XDocument xReference;
        public CultureInfo ci;

        public ParseXmlReferenceData(){}

        public void ReadXml(string file)
        {          
            if (File.Exists(file))
            {
               this.xReference = XDocument.Load(file);
               this.ReadValueFactor();
               this.ReadEmissionsFactor();
            }
            else
            {
                Console.WriteLine("ReferenceData.xml file not found");
                System.Environment.Exit(1);
            }
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
