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
	public class Session : IDisposable
	{
		private ClientMode mClientMode;
		private Uri mTargetUri;
		private NetworkCredential mCredentials;
		private SessionAwareCoreServiceClient mClient;

		/// <summary>
		/// Initializes the <see cref="Session"/> class.
		/// </summary>
		static Session()
		{
			AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
		}
	
		/// <summary>
		/// Initializes a new instance of the <see cref="Session"/> class.
		/// </summary>
		/// <param name="clientMode"><see cref="T:TcmCoreService.Configuration.ClientMode" /></param>
		/// <param name="targetUri">Target <see cref="T:System.Uri" /></param>
		/// <param name="domain">Optional domain</param>
		/// <param name="userName">Optional username</param>
		/// <param name="password">Optional password</param>
		public Session(ClientMode clientMode, Uri targetUri, String domain, String userName, String password)
		{
			mClientMode = clientMode;
			mTargetUri = targetUri;

			switch (clientMode)
			{
				case ClientMode.HttpClient:
					mClient = new SessionAwareCoreServiceClient(ClientConfiguration.ClientHttpBinding,
						new EndpointAddress(ClientConfiguration.ClientHttpUri(targetUri)));					
					break;
				case ClientMode.TcpClient:
				default:
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
		/// Retrieves the Tridion API version
		/// </summary>
		/// <value>
		/// Tridion API version.
		/// </value>
		public String ApiVersion
		{
			get
			{
				return mClient.GetApiVersion();
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

		#region Internals
		/// <summary>
		/// Reads the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// from Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Read<T>(TcmUri uri) where T : IdentifiableObjectData
		{
			return Read<T>(uri, new ReadOptions()
			{
				LoadFlags = LoadFlags.Expanded | LoadFlags.KeywordXlinks
			});
		}

		/// <summary>
		/// Reads the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// from Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <param name="readOptions"><see cref="T:Tridion.ContentManager.CoreService.Client.ReadOptions"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Read<T>(TcmUri uri, ReadOptions readOptions) where T : IdentifiableObjectData
		{
			if (uri.IsNull)
				return null;

			return mClient.Read(uri, readOptions) as T;
		}

		/// <summary>
		/// Localize the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// in Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <param name="readOptions"><see cref="T:Tridion.ContentManager.CoreService.Client.ReadOptions"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Localize<T>(TcmUri uri) where T : IdentifiableObjectData
		{
			return Localize<T>(uri, new ReadOptions()
			{
				LoadFlags = LoadFlags.Expanded | LoadFlags.KeywordXlinks
			});
		}

		/// <summary>
		/// Localize the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// in Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <param name="readOptions"><see cref="T:Tridion.ContentManager.CoreService.Client.ReadOptions"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Localize<T>(TcmUri uri, ReadOptions readOptions) where T : IdentifiableObjectData
		{
			return mClient.Localize(uri, readOptions) as T;
		}

		/// <summary>
		/// UnLocalize the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// in Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <param name="readOptions"><see cref="T:Tridion.ContentManager.CoreService.Client.ReadOptions"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T UnLocalize<T>(TcmUri uri) where T : IdentifiableObjectData
		{
			return UnLocalize<T>(uri, new ReadOptions()
			{
				LoadFlags = LoadFlags.Expanded | LoadFlags.KeywordXlinks
			});
		}

		/// <summary>
		/// UnLocalize the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// in Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <param name="readOptions"><see cref="T:Tridion.ContentManager.CoreService.Client.ReadOptions"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T UnLocalize<T>(TcmUri uri, ReadOptions readOptions) where T : IdentifiableObjectData
		{
			return mClient.UnLocalize(uri, readOptions) as T;
		}

		/// <summary>
		/// Creats the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// in Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="objectData"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Create<T>(T objectData) where T : IdentifiableObjectData
		{
			return Create<T>(objectData, new ReadOptions()
			{
				LoadFlags = LoadFlags.Expanded | LoadFlags.KeywordXlinks
			});
		}

		/// <summary>
		/// Creates the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// from Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="objectData"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</param>
		/// <param name="readOptions"><see cref="T:Tridion.ContentManager.CoreService.Client.ReadOptions"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Create<T>(T objectData, ReadOptions readOptions) where T : IdentifiableObjectData
		{
			return mClient.Create(objectData, readOptions) as T;
		}

		/// <summary>
		/// Saves the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// from Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="objectData"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Save<T>(T objectData) where T : IdentifiableObjectData
		{
			return Save<T>(objectData, new ReadOptions()
			{
				LoadFlags = LoadFlags.Expanded | LoadFlags.KeywordXlinks
			});
		}

		/// <summary>
		/// Saves the specified <see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class
		/// from Tridion
		/// </summary>
		/// <typeparam name="T"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</typeparam>
		/// <param name="objectData"><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</param>
		/// <param name="readOptions"><see cref="T:Tridion.ContentManager.CoreService.Client.ReadOptions"/></param>
		/// <returns><see cref="T:Tridion.ContentManager.CoreService.IdentifiableObjectData" /> derived class</returns>
		internal T Save<T>(T objectData, ReadOptions readOptions) where T : IdentifiableObjectData
		{
			return mClient.Save(objectData, readOptions) as T;
		}

		/// <summary>
		/// Deletes the object for the specified <see cref="T:TcmCoreService.Misc.TcmUri" /> from Tridion 
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal void Delete(TcmUri uri)
		{
			mClient.Delete(uri);
		}
		#endregion

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

		#region Audience Management
		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.AudienceManagement.TargetGroup" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.AudienceManagement.TargetGroup" /></returns>
		public TargetGroup GetTargetGroup(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.TargetGroup)
				throw new ArgumentException(String.Format("{0} is not a TargetGroup uri", uri));

			return new TargetGroup(this, uri);
		}
		#endregion

		#region Communication Management
		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /></returns>
		public ComponentTemplate GetComponentTemplate(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ComponentTemplate)
				throw new ArgumentException(String.Format("{0} is not a ComponentTemplate uri", uri));

			return new ComponentTemplate(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.Page" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.Page" /></returns>
		public Page GetPage(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Page)
				throw new ArgumentException(String.Format("{0} is not a Page uri", uri));

			return new Page(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /></returns>
		public PageTemplate GetPageTemplate(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.PageTemplate)
				throw new ArgumentException(String.Format("{0} is not a PageTemplate uri", uri));

			return new PageTemplate(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.Publication" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.Publication" /></returns>
		public Publication GetPublication(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Publication)
				throw new ArgumentException(String.Format("{0} is not a Publication uri", uri));

			return new Publication(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.PublicationTarget" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.PublicationTarget" /></returns>
		public PublicationTarget GetPublicationTarget(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.PublicationTarget)
				throw new ArgumentException(String.Format("{0} is not a PublicationTarget uri", uri));

			return new PublicationTarget(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.StructureGroup" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.StructureGroup" /></returns>
		public StructureGroup GetStructureGroup(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.StructureGroup)
				throw new ArgumentException(String.Format("{0} is not a StructureGroup uri", uri));

			return new StructureGroup(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.TargetType" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.TargetType" /></returns>
		public TargetType GetTargetType(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.TargetType)
				throw new ArgumentException(String.Format("{0} is not a TargetType uri", uri));

			return new TargetType(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.CommunicationManagement.TemplateBuildingBlock" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.CommunicationManagement.TemplateBuildingBlock" /></returns>
		public TemplateBuildingBlock GetTemplateBuildingBlock(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.TemplateBuildingBlock)
				throw new ArgumentException(String.Format("{0} is not a TemplateBuildingBlock uri", uri));

			return new TemplateBuildingBlock(this, uri);
		}
		#endregion

		#region Content Management
		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.ContentManagement.Category" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.ContentManagement.Category" /></returns>
		public Category GetCategory(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Category)
				throw new ArgumentException(String.Format("{0} is not a Category uri", uri));

			return new Category(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.ContentManagement.Component" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.ContentManagement.Component" /></returns>
		public Component GetComponent(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Component)
				throw new ArgumentException(String.Format("{0} is not a Component uri", uri));

			return new Component(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.ContentManagement.Folder" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.ContentManagement.Folder" /></returns>
		public Folder GetFolder(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Folder)
				throw new ArgumentException(String.Format("{0} is not a Folder uri", uri));

			return new Folder(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.ContentManagement.Keyword" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.ContentManagement.Keyword" /></returns>
		public Keyword GetKeyword(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Keyword)
				throw new ArgumentException(String.Format("{0} is not a Keyword uri", uri));

			return new Keyword(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.ContentManagement.MultimediaType" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.ContentManagement.MultimediaType" /></returns>
		public MultimediaType GetMultimediaType(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.MultimediaType)
				throw new ArgumentException(String.Format("{0} is not a MultimediaType uri", uri));

			return new MultimediaType(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.ContentManagement.Schema" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.ContentManagement.Schema" /></returns>
		public Schema GetSchema(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Schema)
				throw new ArgumentException(String.Format("{0} is not a Schema uri", uri));

			return new Schema(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.ContentManagement.VirtualFolder" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.ContentManagement.VirtualFolder" /></returns>
		public VirtualFolder GetVirtualFolder(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.VirtualFolder)
				throw new ArgumentException(String.Format("{0} is not a VirtualFolder uri", uri));

			return new VirtualFolder(this, uri);
		}
		#endregion

		#region Publishing
		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Publishing.PublishTransaction" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Publishing.PublishTransaction" /></returns>
		public PublishTransaction GetPublishTransaction(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.PublishTransaction)
				throw new ArgumentException(String.Format("{0} is not a PublishTransaction uri", uri));

			return new PublishTransaction(this, uri);
		}
		#endregion

		#region Security
		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Security.Group" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Security.Group" /></returns>
		public Group GetGroup(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.Group)
				throw new ArgumentException(String.Format("{0} is not a Group uri", uri));

			return new Group(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Security.User" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Security.User" /></returns>
		public User GetUser(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.User)
				throw new ArgumentException(String.Format("{0} is not a User uri", uri));

			return new User(this, uri);
		}
		#endregion

		#region Workflow
		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.TridionActivityDefinition" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.TridionActivityDefinition" /></returns>
		public TridionActivityDefinition GetActivityDefinition(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ActivityDefinition)
				throw new ArgumentException(String.Format("{0} is not a ActivityDefinition uri", uri));

			return new TridionActivityDefinition(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.ActivityHistory" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.ActivityHistory" /></returns>
		public ActivityHistory GetActivityHistory(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ActivityHistory)
				throw new ArgumentException(String.Format("{0} is not a ActivityHistory uri", uri));

			return new ActivityHistory(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.ActivityInstance" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.ActivityInstance" /></returns>
		public ActivityInstance GetActivityInstance(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ActivityInstance)
				throw new ArgumentException(String.Format("{0} is not a ActivityInstance uri", uri));

			return new ActivityInstance(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.ApprovalStatus" /></returns>
		public ApprovalStatus GetApprovalStatus(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ApprovalStatus)
				throw new ArgumentException(String.Format("{0} is not a ApprovalStatus uri", uri));

			return new ApprovalStatus(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.TridionProcessDefinition" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.TridionProcessDefinition" /></returns>
		public TridionProcessDefinition GetProcessDefinition(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ProcessDefinition)
				throw new ArgumentException(String.Format("{0} is not a ProcessDefinition uri", uri));

			return new TridionProcessDefinition(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.ProcessHistory" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.ProcessHistory" /></returns>
		public ProcessHistory GetProcessHistory(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ProcessHistory)
				throw new ArgumentException(String.Format("{0} is not a ProcessHistory uri", uri));

			return new ProcessHistory(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.ProcessInstance" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.ProcessInstance" /></returns>
		public ProcessInstance GetProcessInstance(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.ProcessInstance)
				throw new ArgumentException(String.Format("{0} is not a ProcessInstance uri", uri));

			return new ProcessInstance(this, uri);
		}

		/// <summary>
		/// Retrieves a <see cref="T:TcmCoreService.Workflow.WorkItem" /> from Tridion
		/// </summary>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		/// <returns><see cref="T:TcmCoreService.Workflow.WorkItem" /></returns>
		public WorkItem GetWorkItem(TcmUri uri)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");

			if (uri.ItemType != (int)ItemType.WorkItem)
				throw new ArgumentException(String.Format("{0} is not a WorkItem uri", uri));

			return new WorkItem(this, uri);
		}
		#endregion

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (mClient != null)
				{
					if (mClient.State == CommunicationState.Faulted)
						mClient.Abort();
					else
						mClient.Close();
				}
			}
		}
	}
}
