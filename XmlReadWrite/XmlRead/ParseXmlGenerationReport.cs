using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlReadWrite.XmlRead
{
    public class ParseXmlGenerationReport
    {
        private XDocument xReport;

        public XDocument XmlRead(string file)
        {   
           if (File.Exists(file))
           {
              this.xReport = XDocument.Load(file);
           }
           else
           {
                Console.WriteLine("GenerationReport.xml file not found");
                System.Environment.Exit(1);
            }
            return xReport;
        }
    }
}
