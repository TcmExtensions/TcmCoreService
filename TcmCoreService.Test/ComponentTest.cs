using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcmCoreService.ContentManagement;

namespace TcmCoreService.Test
{
    [TestClass]
    public class ComponentTest
    {
        [TestMethod]
        public void ComponentRead()
        {
            using (Client client = new Client(new Uri("http://localhost"),
                "user", "tridion"))
            {
                Component component = client.GetComponent("tcm:5-54");

                Assert.IsNotNull(component, "Component is null");

                Assert.AreEqual(component.Title, "Club sandwich");
            }
        }
    }
}
