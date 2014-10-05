#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Session
// ---------------------------------------------------------------------------------
//	Date Created	: March 19, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using TcmCoreService.AudienceManagement;
using TcmCoreService.CommunicationManagement;
using TcmCoreService.Configuration;
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using TcmCoreService.Publishing;
using TcmCoreService.Security;
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService
{
	/// <summary>
	/// <see cref="Session" /> opens a session with the Tridion Content Manager through <see cref="T:Tridion.ContentManager.CoreService.Client.SessionAwareCoreServiceClient" />
	/// </summary>
	public class Session : Client, IDisposable
	{
		private ClientMode mClientMode;
		private SessionAwareCoreServiceClient mClient;

		/// <summary>
		/// Retrieves the Tridion session identifier
		/// </summary>
		/// <value>
		/// Tridion session identifier.
		/// </value>
		public String SessionId
		{
			get
			{
				return mClient.GetSessionId();
			}
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override String ToString()
		{
			return String.Format("TcmCoreService-{0}@{1}-{{{2}}}", 
				String.IsNullOrEmpty(mCredentials.UserName) ? Thread.CurrentPrincipal.Identity.Name :
				!String.IsNullOrEmpty(mCredentials.Domain) ? mCredentials.Domain + @"\" + mCredentials.UserName : mCredentials.UserName,
				mClient.Endpoint.Address.Uri.GetLeftPart(UriPartial.Authority),
				SessionId);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Session"/> class.
		/// </summary>
		/// <param name="clientMode"><see cref="T:TcmCoreService.Configuration.ClientMode" /></param>
		/// <param name="targetUri">Target <see cref="T:System.Uri" /></param>
		/// <param name="domain">Optional domain</param>
		/// <param name="userName">Optional username</param>
		/// <param name="password">Optional password</param>
		public Session (ClientMode clientMode, Uri targetUri, String domain, String userName, String password) : base (targetUri, userName, password)
		{
			mClientMode = clientMode;
			mTargetUri = targetUri;

			switch (clientMode)
			{
				case ClientMode.HttpClient:
					mClient = new SessionAwareCoreServiceClient(ClientConfiguration.ClientWSHttpBinding,
						new EndpointAddress(ClientConfiguration.ClientWSHttpUri(targetUri)));					
					break;
				case ClientMode.TcpClient:
				default:
					if (ClientConfiguration.IsRunningOnMono)
						throw new NotImplementedException ("NetTcpBinding on Mono cannot be used with Tridion CoreService");

					mClient = new SessionAwareCoreServiceClient(ClientConfiguration.ClientTcpBinding,
						new EndpointAddress(ClientConfiguration.ClientTcpUri(targetUri)));
					break;
			}

			if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
			{
				if (!String.IsNullOrEmpty(domain))
					mCredentials = new NetworkCredential(userName, password, domain);
				else
					mCredentials = new NetworkCredential(userName, password);
			}
			else
				mCredentials = CredentialCache.DefaultNetworkCredentials;

			mClient.ClientCredentials.Windows.ClientCredential = mCredentials;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Session"/> class.
		/// </summary>
		/// <param name="clientMode"><see cref="T:TcmCoreService.Configuration.ClientMode" /></param>
		/// <param name="targetUri">Target <see cref="T:System.Uri" /></param>
		public Session(ClientMode clientMode, Uri targetUri): this(clientMode, targetUri, null, null, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Session"/> class.
		/// </summary>
		/// <param name="targetUri">Target <see cref="T:System.Uri" /></param>
		public Session(Uri targetUri): this(ClientMode.TcpClient, targetUri, null, null, null)
		{
		}

		private StreamUploadClient UploadClient
		{
			get
			{
				StreamUploadClient client;

				switch (mClientMode)
				{
					case ClientMode.HttpClient:
						client = new StreamUploadClient(ClientConfiguration.StreamHttpBinding,
							new EndpointAddress(ClientConfiguration.UploadHttpUri(mTargetUri)));
						break;
					case ClientMode.TcpClient:
					default:
						client = new StreamUploadClient(ClientConfiguration.StreamTcpBinding,
							new EndpointAddress(ClientConfiguration.UploadTcpUri(mTargetUri)));
						break;
				}

				client.ClientCredentials.Windows.ClientCredential = mCredentials;

				return client;
			}
		}

		private StreamDownloadClient DownloadClient
		{
			get
			{
				StreamDownloadClient client;

				switch (mClientMode)
				{
					case ClientMode.HttpClient:
						client = new StreamDownloadClient(ClientConfiguration.StreamHttpBinding,
							new EndpointAddress(ClientConfiguration.DownloadHttpUri(mTargetUri)));
						break;
					case ClientMode.TcpClient:
					default:
						client = new StreamDownloadClient(ClientConfiguration.StreamTcpBinding,
							new EndpointAddress(ClientConfiguration.DownloadTcpUri(mTargetUri)));
						break;
				}

				client.ClientCredentials.Windows.ClientCredential = mCredentials;

				return client;
			}
		}


		/// <summary>
		/// Impersonate the TridionClient configured identity
		/// </summary>
		/// <param name="identity">Tridion user identity.</param>
		public void Impersonate(String identity)
		{
			if (String.IsNullOrEmpty(identity))
				throw new ArgumentException("identity");

			mClient.Impersonate(identity);
		}

		/// <summary>
		/// Drops any impersonation used by the <see cref="TridionClient" />
		/// </summary>
		/// <remarks>
		/// Use this only for performing administration user only tasks!
		/// </remarks>
		public void DropImpersonation()
		{
			if (!String.IsNullOrEmpty(mCredentials.Domain))
				mClient.Impersonate(String.Format(@"{0}\{1}", mCredentials.Domain, mCredentials.UserName));
			else
				mClient.Impersonate(mCredentials.UserName);
		}

		/// <summary>
		/// Upload a file to Tridion from a <see cref="T:System.IO.Stream" />
		/// </summary>
		/// <param name="fileName">Tridion Filename</param>
		/// <param name="binaryContent">Binary Content as <see cref="T:System.IO.Stream" /></param>
		/// <returns>Full tridion file path</returns>
		public String UploadFile(String fileName, Stream binaryContent)
		{
			StreamUploadClient client = null;

			try
			{
				client = UploadClient;
				return client.UploadBinaryContent(fileName, binaryContent);
			}
			finally
			{
				if (client != null)
					if (client.State == CommunicationState.Faulted)
						client.Abort();
					else
						client.Close();					
			}
		}

		/// <summary>
		/// Upload a file to Tridion from a binary array
		/// </summary>
		/// <param name="fileName">Tridion Filename</param>
		/// <param name="binaryContent">Binary Content as byte array</param>
		/// <returns>Full tridion file path</returns>
		public String UploadFile(String fileName, byte[] binaryContent)
		{
			StreamUploadClient client = null;

			try
			{
				client = UploadClient;
				return client.UploadBinaryByteArray(fileName, binaryContent);				
			}
			finally
			{
				if (client != null)
					if (client.State == CommunicationState.Faulted)
						client.Abort();
					else
						client.Close();	
			}
		}

		/// <summary>
		/// Upload a file to Tridion from a Tridion multimedia component or binary TBB
		/// </summary>
		/// <param name="item"><see cref="T:EGIT.CCIT.Tridion.CoreService.TcmUri"/> of the binary Tridion item.</param>
		/// <returns><see cref="T:System.IO.Stream" /> containing the binary data from Tridion</returns>
		public Stream DownloadFile(TcmUri item)
		{
			StreamDownloadClient client = null;

			try
			{
				client = DownloadClient;
				return client.DownloadBinaryContent(item);
			}
			finally
			{
				if (client != null)
					if (client.State == CommunicationState.Faulted)
						client.Abort();
					else
						client.Close();	
			}
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
