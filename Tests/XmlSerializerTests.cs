using System.IO;
using System.Text;
using NUnit.Framework;
using PaytureTestTask.Models;
using PaytureTestTask.Services;

namespace PaytureTestTask.Tests
{
    public class XmlSerializerTests
    {
        private readonly string responseExample =
            @"<Pay OrderId=""2d436b58-1c49-aa25-8137-ffdc3fb5210f"" Key=""Merchant"" Success=""True"" Amount=""12420"">
<AddInfo Key=""AuthCode"" Value=""122938"" />
<AddInfo Key=""RefNumber"" Value=""637176303771"" />
<AddInfo Key=""CardHolder"" Value=""Ivan Ivanov"" />
<AddInfo Key=""PaymentSystem"" Value=""MasterCard"" />
<AddInfo Key=""PANMask"" Value=""411111xxxxxx0031"" />
<AddInfo Key=""Compensation"" Value=""12360"" />
<AddInfo Key=""BankHumanName"" Value=""TEST BANK"" />
<AddInfo Key=""BankCountryCode"" Value=""US"" />
<AddInfo Key=""BankCity"" Value="""" />
<AddInfo Key=""cardtype"" Value=""V_BUSINESS"" />
<AddInfo Key=""externalmerchantorderid"" Value=""2d436b58-1c49-aa25-8137-ffdc3fb5210f"" />
<AddInfo Key=""externalwallet"" Value=""None"" />
<AddInfo Key=""generalfee"" Value=""60"" />
<AddInfo Key=""is3ds"" Value=""False"" />
<AddInfo Key=""orderdate"" Value=""20200220125920"" />
<AddInfo Key=""ThreeDSType"" Value=""Version1"" />
</Pay>";

        [Test]
        public void TestDeserialization()
        {
            PayResponse response = null;
            var deserializer = new XmlDeserializer();
            using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(responseExample)))
            {
                Assert.DoesNotThrow(() => response = deserializer.Deserialize(stream));
            }

            Assert.AreEqual("2d436b58-1c49-aa25-8137-ffdc3fb5210f", response.OrderId);
            Assert.AreEqual("Merchant", response.Key);
            Assert.AreEqual("True", response.Success);
            Assert.AreEqual("12420", response.Amount);
            Assert.AreEqual(16, response.AddInfo.Count);
        }
    }
}