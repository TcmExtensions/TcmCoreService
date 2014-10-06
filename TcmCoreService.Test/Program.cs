using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TcmCoreService.CommunicationManagement;
using TcmCoreService.ContentManagement;
using TcmCoreService.Info;

namespace TcmCoreService.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			using (Client client = new Client (new Uri ("http://192.168.10.141"),
				"user", "tridion")) 
			{
				String version = client.ApiVersion;
				Console.WriteLine(version);

				Component component = client.GetComponent("tcm:5-54");

				if (component != null)
				{
					Console.WriteLine(component.Title);
				}

			}

			using (Session session = new Session(TcmCoreService.Configuration.ClientMode.HttpClient,
				new Uri("http://192.168.10.141"),
				"TRIDION2013", "user", "tridion"))
			{
				String version = session.ApiVersion;
				String sessionId = session.SessionId;

				//Folder folder = session.GetFolder("tcm:229-68086-2");

				//Component component = session.GetComponent("tcm:233-193779");

				//ComponentTemplate componentTemplate = session.GetComponentTemplate("tcm:230-539152-32");

				//Page page = session.GetPage("tcm:233-1216852-64");
				//ComponentPresentation componentPresentation = page.ComponentPresentations.First();
			}
		}
	}
}
