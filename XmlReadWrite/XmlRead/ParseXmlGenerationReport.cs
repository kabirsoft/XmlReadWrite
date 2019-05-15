using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Brady.XmlRead
{
    public class ParseXmlGenerationReport
    {
        public XDocument xReport;

        public XDocument XmlRead(string file)
        {        
           if (File.Exists(file))
           {
              this.xReport = XDocument.Load(file);
           }
           else
           {
                Console.WriteLine("File not found");                
           }
            return xReport;
        }
    }
}
