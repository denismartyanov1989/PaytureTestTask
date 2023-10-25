using System.Xml.Serialization;

namespace PaytureTestTask.Models
{
    [XmlType("AddInfo")]
    public class AddInfo
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{nameof(Key)}: {Key}; {nameof(Value)}: {Value}";
        }
    }
}