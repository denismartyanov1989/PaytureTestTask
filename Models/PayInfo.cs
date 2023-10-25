using System.Text;
using PaytureTestTask.Services;

namespace PaytureTestTask.Models
{
    public class PayInfo
    {
        public string PAN { get; set; }

        public string EMonth { get; set; }

        public string EYear { get; set; }

        public string OrderId { get; set; }

        public string Amount { get; set; }

        public int? SecureCode { get; set; }

        public string CardHolder { get; set; }

        public string ToParameter()
        {
            var sb = new StringBuilder();
            sb.Append($"{Parameters.ParameterPan}={this.PAN};");
            sb.Append($"{Parameters.ParameterEMouth}={this.EMonth};");
            sb.Append($"{Parameters.ParameterEYear}={this.EYear};");
            sb.Append($"{Parameters.ParameterOrderId}={this.OrderId};");
            sb.Append($"{Parameters.ParameterAmount}={this.Amount}");
            if (this.SecureCode.HasValue)
            {
                sb.Append($";{Parameters.SecureCodeParameter}={this.SecureCode}");
            }

            if (string.IsNullOrEmpty(this.CardHolder))
            {
                sb.Append($";{Parameters.CardHolderParameter}={this.CardHolder}");
            }

            return sb.ToString();
        }
    }
}