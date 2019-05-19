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
            XmlMain xml = new XmlMain();
            xml.Read();            
        }
    }
}
