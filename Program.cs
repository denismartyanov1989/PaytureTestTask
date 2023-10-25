using System;
using System.Collections.Generic;
using System.Net;
using PaytureTestTask.Models;
using PaytureTestTask.Services;

namespace PaytureTestTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Security protocols disabled by default on Windows 7,
            // so we need to enable them manually to support https.
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            
            var service = new PaytureService("sandbox3", "Merchant", new XmlDeserializer());
            var payInfo = new PayInfo
            {
                Amount = "12757",
                CardHolder = "Ivan Ivanov",
                EMonth = "12",
                EYear = "25",
                OrderId = "b85bf9ce-6f05-8c90-c5fb-c434c1db7fa",
                PAN = "5218851946955484",
                SecureCode = 123
            };

            var customFields = new Dictionary<string, string>
            {
                { "IP", "148.227.9.233" },
                { "Product", "Ticket" }
            };

            try
            {
                var response = service.PayAsync("b85bf9ce-6f05-8c90-c5fb-ac434c1db7fa", 12757, payInfo,
                    customFields: customFields).Result;
                Console.WriteLine(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling Pay API.");
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
    }
}