using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PaytureTestTask.Models
{
    [XmlRoot("Pay")]
    public class PayResponse
    {
        [XmlAttribute("Success")]
        public string Success { get; set; }

        [XmlAttribute("OrderId")]
        public string OrderId { get; set; }

        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Amount")]
        public string Amount { get; set; }

        [XmlAttribute("ACSUrl")]
        public string ACSUrl { get; set; }

        [XmlAttribute("PaReq")]
        public string PaReq { get; set; }

        [XmlAttribute("ThreeDSKey")]
        public string ThreeDSKey { get; set; }

        [XmlAttribute("ThreeDSVersion")]
        public string ThreeDSVersion { get; set; }

        [XmlAttribute("FinalTerminal")]
        public string FinalTerminal { get; set; }

        [XmlElement("AddInfo")]
        public List<AddInfo> AddInfo { get; set; }

        [XmlAttribute("ErrCode")]
        public string ErrCode { get; set; }

        public override string ToString()
        {
            var properties = this.GetType().GetProperties();
            var sb = new StringBuilder();
            foreach (var property in properties.Where(p => p.Name != nameof(AddInfo)))
            {
                var value = property.GetValue(this);
                if (value != null)
                {
                    sb.AppendLine($"{property.Name}: {value}");
                }
            }

            if (AddInfo != null && AddInfo.Count > 0)
            {
                sb.AppendLine($"{nameof(AddInfo)}:");
                foreach (var addInfo in AddInfo)
                {
                    sb.AppendLine($"    {addInfo}");
                }
            }

            return sb.ToString();
        }
    }
}