using System.IO;
using System.Xml.Serialization;
using PaytureTestTask.Models;

namespace PaytureTestTask.Services
{
    public class XmlDeserializer
    {
        internal PayResponse Deserialize(Stream xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(PayResponse));
            return (PayResponse)xmlSerializer.Deserialize(xml);
        }
    }
}