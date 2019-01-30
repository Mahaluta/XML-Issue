using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML
{
    public class Publisher
    {
        [XmlElement(ElementName = "FullName")]
        public string Name { get; set; }
        
        [XmlElement(ElementName = "PublisherID")]
        public int PublisherId { get; set; }

    }
}
