using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PaytureTestTask.Models;

namespace PaytureTestTask.Services
{
    public class PaytureService
    {
        private const string PayCommand = "Pay";
        private const string Interface = "api";

        private static readonly HttpClient Client = new HttpClient();
        private readonly XmlDeserializer _deserializer;
        private readonly string _environment;
        private readonly string _key;
        private readonly string endpoint = "https://{0}.payture.com/{1}/{2}";

        public PaytureService(string environment, string key, XmlDeserializer deserializer)
        {
            _environment = environment;
            _key = key;
            _deserializer = deserializer;
        }

        public async Task<PayResponse> PayAsync(string orderId, int amount, PayInfo payInfo, string paytureId = null,
            string customerKey = null, Dictionary<string, string> customFields = null)
        {
            var parameters = GetPayParameters(orderId, amount, paytureId, customerKey, payInfo, customFields);
            using (var response = await Client.PostAsync(GetUrl(PayCommand), new FormUrlEncodedContent(parameters)))
            {
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStreamAsync();
                return _deserializer.Deserialize(responseContent);
            }
        }

        private string GetUrl(string payCommand)
        {
            return string.Format(endpoint, _environment, Interface, payCommand);
        }

        private Dictionary<string, string> GetPayParameters(string orderId, int amount, string paytureId,
            string customerKey, PayInfo payInfo, Dictionary<string, string> customFields)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add(Parameters.ParameterKey, _key);
            parameters.Add(Parameters.ParameterOrderId, orderId);
            parameters.Add(Parameters.ParameterAmount, amount.ToString());
            parameters.Add(Parameters.ParameterPayInfo, WebUtility.UrlEncode(payInfo.ToParameter()));
            if (!string.IsNullOrEmpty(paytureId)) parameters.Add(Parameters.ParameterPaytureId, paytureId);

            if (!string.IsNullOrEmpty(customerKey)) parameters.Add(Parameters.ParameterCustomerKey, customerKey);

            if (customFields != null && customFields.Count > 0)
                parameters.Add(Parameters.ParameterCustomFields, string.Join(";", customFields.Select(p => $"{p.Key}={p.Value}")));

            return parameters;
        }
    }
}