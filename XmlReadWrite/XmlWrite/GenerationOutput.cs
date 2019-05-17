using XmlReadWrite.Emissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlReadWrite.XmlWrite
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
        public void XmlWrite(string file)
        {

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
                             from el in Gas.GasTotalGeneration()
                             select new XElement("Generator",
                                    new XElement("Name", "Gas["+el.Key+"]"),
                                    new XElement("Total", el.Value)
                                
                          ),
                            from el in Coal.CoalTotalGeneration()
                            select new XElement("Generator",
                              new XElement("Name", "Coal["+ el.Key +"]"),
                              new XElement("Total", el.Value)
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
                     from el in Heatrate.ActualHeatRates()
                     select new XElement("ActualHeatRates",
                        new XElement("Name", "Coal["+ el.Key +"]"),
                        new XElement("HeatRate", el.Value)
                     )

                 )//end GenerationOutput
             );

            //Console.WriteLine(xmlDoc);            
            xmlDoc.Save(file);
        }
    }
}
