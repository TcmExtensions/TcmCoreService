#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Client
// ---------------------------------------------------------------------------------
//	Date Created	: October 5, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
using System.Security.Principal;
using System.Net;
using Tridion.ContentManager.CoreService.Client;
using TcmCoreService.Configuration;
using System.ServiceModel;
using TcmCoreService.Misc;
using TcmCoreService.AudienceManagement;
using TcmCoreService.CommunicationManagement;
using TcmCoreService.ContentManagement;
using TcmCoreService.Publishing;
using TcmCoreService.Security;
using TcmCoreService.Workflow;
using System.Threading;


#endregion
using System;

namespace TcmCoreService
{
	/// <summary>
	/// <see cref="Client" /> opens a session with the Tridion Content Manager through <see cref="T:Tridion.ContentManager.CoreService.Client.CoreServiceClient" />
	/// </summary>
	public class Client : IDisposable
	{
		private CoreServiceClient mClient;
		protected Uri mTargetUri;
		protected NetworkCredential mCredentials;

		/// <summary>
		/// Initializes the <see cref="Session" /> class.
		/// </summary>
		static Client()
		{
			AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Client"/> class.
		/// </summary>
		/// <param name="targetUri">Target <see cref="T:System.Uri" /></param>
		/// <param name="domain">Optional domain</param>
		/// <param name="userName">Optional username</param>
		/// <param name="password">Optional password</param>
		public Client(Uri targetUri, String userName, String password)
		{
			mTargetUri = targetUri;

			mClient = new CoreServiceClient (ClientConfiguration.ClientHttpBinding,
				new EndpointAddress (ClientConfiguration.ClientHttpUri (targetUri)));
				
			if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
			{
				mClient.ClientCredentials.UserName.UserName = userName;
				mClient.ClientCredentials.UserName.Password = password;
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

