using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TcmCoreService.Test
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public void HttpClient()
        {
            using (Client client = new Client(new Uri("http://localhost"),
                "user", "tridion"))
            {
                Assert.AreEqual("7.1.0", client.ApiVersion);
            }
        }
    }
}
