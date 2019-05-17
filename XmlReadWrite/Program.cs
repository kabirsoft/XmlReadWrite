using XmlReadWrite.Emissions;
using XmlReadWrite.XmlRead;
using XmlReadWrite.XmlWrite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace XmlReadWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            
            string file_xml_generation = ConfigurationManager.AppSettings["GenerationReport"];
            string file_xml_reference = ConfigurationManager.AppSettings["ReferenceData"];
            string file_xml_write = ConfigurationManager.AppSettings["outputXml"];
          
            

            ParseXmlReferenceData refData = new ParseXmlReferenceData();
            refData.ReadXml(file_xml_reference);

            ParseXmlGenerationReport xmlGeneration = new ParseXmlGenerationReport();
            var xReport = xmlGeneration.XmlRead(file_xml_generation);

            XmlOffshore offShore = new XmlOffshore(xReport);
            List<WindOffShore> offshorList = offShore.ReadXmlOffshore();

            XmlOnshore onshore = new XmlOnshore(xReport);
            List<WindOnshore> onshorList = onshore.ReadXmlOnshore();

            XmlGas gas = new XmlGas(xReport);
            Dictionary<int, List<Gas>> gasList = gas.ReadXmlGas();
            //gas.GasTotalGeneration();

            XmlCoal coal = new XmlCoal(xReport);
            Dictionary<int, List<Coal>> coalList = coal.ReadXmlCoal();
            //coal.CoalTotalGeneration();

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
