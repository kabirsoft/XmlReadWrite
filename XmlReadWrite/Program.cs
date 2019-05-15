using Brady.Emissions;
using Brady.XmlRead;
using Brady.XmlWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Brady
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            ParseXmlReferenceData refData = new ParseXmlReferenceData();
            refData.ReadXml();

            ParseXmlGenerationReport xmlGeneration = new ParseXmlGenerationReport();
            var xReport = xmlGeneration.XmlRead();

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
            outputXml.XmlWrite();
        }
    }
}
