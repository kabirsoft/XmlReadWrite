using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlReadWrite.Emissions;
using XmlReadWrite.XmlRead;
using XmlReadWrite.XmlWrite;

namespace XmlReadWrite
{
    public class XmlMain
    {   
        public void Read()
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            string file_xml_generation = ConfigurationManager.AppSettings["GenerationReport"];
            string file_xml_reference = ConfigurationManager.AppSettings["ReferenceData"];
            string file_xml_write = ConfigurationManager.AppSettings["outputXml"];


            ParseXmlReferenceData xmlRef = new ParseXmlReferenceData(file_xml_reference);

            ParseXmlGenerationReport xmlGeneration = new ParseXmlGenerationReport();
            var xReport = xmlGeneration.Read(file_xml_generation);

            XmlOffshore offShore = new XmlOffshore(xReport);
            List<WindOffShore> offshoreList = offShore.Read();

            XmlOnshore onshore = new XmlOnshore(xReport);
            List<WindOnshore> onshoreList = onshore.Read();

            XmlGas gas = new XmlGas(xReport);
            Dictionary<int, List<Gas>> gasList = gas.Read();
            //gas.GasTotalGeneration();

            XmlCoal coal = new XmlCoal(xReport);
            Dictionary<int, List<Coal>> coalList = coal.Read();
            //coal.CoalTotalGeneration();

            Emission emission = new Emission(gasList, coalList);
            //emission.HighestEmissionByDate();            

            HeatRate ht = new HeatRate(coalList);
            //ht.ActualHeatRates();

            GenerationOutput outputXml = new GenerationOutput(offShore, onshore, gas, coal, emission, ht);
            outputXml.XmlWrite(file_xml_write);
        }
    }
}
