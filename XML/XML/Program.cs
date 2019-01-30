using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace XML
{
    class Program
    {
        static void Main(string[] args)
        {
            Library.SavePublisher();

            //var list = Library.LoadPublishers();
            // foreach(Publisher p in list) Console.WriteLine($"{p.PublisherId} - {p.Name}");

            Library.WritePublishersToTXTFile();

            Console.ReadKey();
        }
    }
}

