using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcmCoreService.Test
{
    [TestClass]
    public class SessionTest
    {
        [TestMethod]
        public void WSHttpSession()
        {
            using (Session session = new Session(TcmCoreService.Configuration.ClientMode.HttpClient, 
                new Uri("http://localhost"),
                "TRIDION2013", "user", "tridion"))
            {
                Assert.AreEqual("7.1.0", session.ApiVersion);
            }
        }

        [TestMethod]
        public void TcpSession()
        {
            using (Session session = new Session(TcmCoreService.Configuration.ClientMode.TcpClient,
                new Uri("http://localhost"),
                "TRIDION2013", "user", "tridion"))
            {
                Assert.AreEqual("7.1.0", session.ApiVersion);
            }
        }
    }
}
