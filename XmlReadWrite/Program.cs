using Brady.Emissions;
using Brady.XmlRead;
using Brady.XmlWrite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Brady
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            
            string file_xml_generation = Directory.GetCurrentDirectory() + "\\..\\..\\GenerationReport.xml";
            string file_xml_reference = Directory.GetCurrentDirectory() + "\\..\\..\\ReferenceData.xml";
            string file_xml_write = ConfigurationSettings.AppSettings["outputXml"];
            

            ParseXmlReferenceData refData = new ParseXmlReferenceData();
            refData.ReadXml(file_xml_reference);

            ParseXmlGenerationReport xmlGeneration = new ParseXmlGenerationReport();
            var xReport = xmlGeneration.XmlRead(file_xml_generation);

            XmlOffshore offShore = new XmlOffshore(xReport);
            List<WindOffShore> offshorList = offShore.ReadXmlOffshore();

            XmlOnshore onshore = new XmlOnshore(xReport);
            List<WindOnshore> onshorList = onshore.ReadXmlOnshore();

            XmlGas gas = new XmlGas(xReport);
            List<Gas> gasList = gas.ReadXmlGas();

            XmlCoal coal = new XmlCoal(xReport);
            List<Coal> coalList = coal.ReadXmlCoal();

            Emission emission = new Emission();
            emission.EmissionsGasDaily(gasList);
            emission.EmissionsCoalDaily(coalList);
            //emission.HighestEmissionByDate();

            HeatRate ht = new HeatRate(coalList);
            //ht.ActualHeatRates();

            GenerationOutput outputXml = new GenerationOutput(offShore, onshore, gas, coal, emission, ht);
            outputXml.XmlWrite(file_xml_write);
        }
    }
}
