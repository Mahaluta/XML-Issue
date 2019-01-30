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
    public class Library
    {
        public static void SavePublisher()
        {
            //Create Publisher
            Publisher p = new Publisher();
            Console.Write("Read the publisher's name: ");
            p.Name = Console.ReadLine();


            // SQL Server connection
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDatabaseConnection"].ConnectionString
            };

            connection.Open();

            SqlParameter PublisherNameParameter = new SqlParameter("PublisherNameParameter", p.Name);

            SqlCommand InsertPublisher = new SqlCommand(@"INSERT INTO Publisher(Name) VALUES (@PublisherNameParameter);", connection);
            InsertPublisher.Parameters.Add("PublisherNameParameter", SqlDbType.VarChar).Value = p.Name;
            InsertPublisher.ExecuteNonQuery();

            SqlCommand GetID = new SqlCommand(@"SELECT TOP 1 PublisherId FROM Publisher ORDER BY PublisherId DESC;", connection);
            p.PublisherId = (int)GetID.ExecuteScalar();

            connection.Close();

            List<Publisher> Publishers = new List<Publisher>();
            Publishers.Add(p);
            // ------------------------------------------

            //Write to .XML file
            var XMLpath = System.Configuration.ConfigurationManager.AppSettings["xmlPath"];
            using (FileStream file = File.Create(XMLpath))
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(file))
                {
                    var serializer = new XmlSerializer(Publishers.GetType());
                    serializer.Serialize(writer, Publishers);
                    writer.Flush();
                }
            }
            // ------------------------------------------

            Console.WriteLine("The publisher has been added to the DB.");

        }

        //Read from .XML File
        public static List<Publisher> LoadPublishers()
        {
            using (FileStream stream = System.IO.File.OpenRead(System.Configuration.ConfigurationManager.AppSettings["xmlPath"]))
            {
                var serializer = new XmlSerializer(typeof(List<Publisher>));
                return serializer.Deserialize(stream) as List<Publisher>;
            }
            // ------------------------------------------
        }

        //Write to .TXT File
        public static void WritePublishersToTXTFile()
        {
            var list = LoadPublishers();
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.Configuration.ConfigurationManager.AppSettings["txtPath"]))
            {
                foreach (Publisher p in list)
                {
                    writer.WriteLine(p.Name);
                }
            }
        }
    }
}
