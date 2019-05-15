using Brady.Emissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Brady.XmlWrite
{
    public class GenerationOutput
    {
        XmlOffshore OffShore;
        XmlOnshore Onshore;
        XmlGas Gas;
        XmlCoal Coal;
        Emission Emission;
        HeatRate Heatrate;

        public GenerationOutput(XmlOffshore _offShore, XmlOnshore onshore, XmlGas gas, XmlCoal coal, Emission emission, HeatRate heatRate)
        {
            this.OffShore = _offShore;
            this.Onshore = onshore;
            this.Gas = gas;
            this.Coal = coal;
            this.Emission = emission;
            this.Heatrate = heatRate;
        }
        public void XmlWrite()
        {
            //Console.WriteLine("------------ xml total output ---------");
            //Console.WriteLine(OffShore.OffshoreTotalGeneration());
            //Console.WriteLine(Onshore.OnshoreTotalGeneration());
            //Console.WriteLine(Gas.GasTotalGeneration());
            //Console.WriteLine(Coal.CoalTotalGeneration());

            //Console.WriteLine("------------ By date ------------");
            //foreach(var e in Emission.HighestEmissionByDate())
            //{
            //    Console.WriteLine(e.Date +"- " + e.TotalEmissionsDaily + e.Name);
            //}

            //Console.WriteLine("-------------- Heat rate------------");
            //Console.WriteLine(Heatrate.ActualHeatRates());

            /* Start xml writing */

            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "true"),
                new XElement("GenerationOutput",
                    new XElement("Totals",
                         new XElement("Generator",
                              new XElement("Name", "Wind[Offshore]"),
                              new XElement("Total", OffShore.OffshoreTotalGeneration())
                          ),
                          new XElement("Generator",
                              new XElement("Name", "Wind[Onshore]"),
                              new XElement("Total", Onshore.OnshoreTotalGeneration())
                          ),
                          new XElement("Generator",
                              new XElement("Name", "Gas[1]"),
                              new XElement("Total", Gas.GasTotalGeneration())
                          ),
                          new XElement("Generator",
                              new XElement("Name", "Coal[1]"),
                              new XElement("Total", Coal.CoalTotalGeneration())
                          )
                     ),
                     new XElement("MaxEmissionGenerators",
                         from e in Emission.HighestEmissionByDate()
                         select
                         new XElement("Day",
                            new XElement("Name", e.Name),
                            new XElement("Date", e.Date),
                            new XElement("Emission", e.TotalEmissionsDaily)
                        )//end day

                     ),//end MaxEmissionGenerators
                     new XElement("ActualHeatRates",
                        new XElement("Name", "Coal"),
                        new XElement("HeatRate", Heatrate.ActualHeatRates())
                     )

                 )//end GenerationOutput
             );

            //Console.WriteLine(xmlDoc);            
            xmlDoc.Save(@"C:\Asp.Net_project\XmlReadWrite\XmlReadWrite\GenerationOutput.xml");
        }
    }
}
