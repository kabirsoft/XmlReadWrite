using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Brady.XmlRead
{
    public class ParseXmlGenerationReport
    {
        public XDocument xReport;

        public XDocument XmlRead()
        {

         try{ 
               this.xReport = XDocument.Load("C:\\Asp.Net_project\\XmlReadWrite\\XmlReadWrite\\GenerationReport.xml");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);                
            }
            return xReport;
        }
    }
}
