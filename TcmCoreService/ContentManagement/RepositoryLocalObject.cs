#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: RepositoryLocalObject
// ---------------------------------------------------------------------------------
//	Date Created	: March 21, 2014
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
using System.Linq;
using System.Text;
using TcmCoreService.CommunicationManagement;
using TcmCoreService.Info;
using TcmCoreService.Misc;
using TcmCoreService.Security;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="RepositoryLocalObject" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryLocalObjectData" />.
	/// </summary>
	public abstract class RepositoryLocalObject : IdentifiableObject
	{
		private RepositoryLocalObjectData mRepositoryLocalObjectData;

		private Location mLocation = null;
		private Info.LockInfo mLockInfo = null;
		private BluePrint mBluePrint = null;

		private Schema mMetadataSchema = null;
		private User mCreator = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryLocalObject"/> class.
		/// </summary>
		/// <param name="client"><see cref="TcmCoreService.Client" /></param>
		/// <param name="repositoryLocalObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryLocalObjectData" /></param>
		protected RepositoryLocalObject(Client client, RepositoryLocalObjectData repositoryLocalObjectData): base(client, repositoryLocalObjectData)
		{
			if (repositoryLocalObjectData == null)
				throw new ArgumentNullException("repositoryLocalObjectData");

			mRepositoryLocalObjectData = repositoryLocalObjectData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryLocalObject"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal RepositoryLocalObject(Client client, TcmUri uri): this(client, client.Read<RepositoryLocalObjectData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="RepositoryLocalObject" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryLocalObjectData" />
		/// </summary>
		/// <param name="repositoryLocalObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryLocalObjectData" /></param>
		protected void Reload(RepositoryLocalObjectData repositoryLocalObjectData)
		{
			if (repositoryLocalObjectData == null)
				throw new ArgumentNullException("repositoryLocalObjectData");

			mRepositoryLocalObjectData = repositoryLocalObjectData;
			base.Reload(repositoryLocalObjectData);

			mLocation = null;
			mLockInfo = null;
			mBluePrint = null;

			mMetadataSchema = null;
			mCreator = null;
		}

		/// <summary>
		/// Reload the <see cref="RepositoryLocalObject" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<RepositoryLocalObjectData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="RepositoryLocalObject" />
		/// </summary>
		public virtual void Localize()
		{
			Reload(Client.Localize<RepositoryLocalObjectData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="RepositoryLocalObject" />
		/// </summary>
		public virtual void UnLocalize()
		{
			Reload(Client.UnLocalize<RepositoryLocalObjectData>(this.Id));
		}

		/// <summary>
		/// Gets the <see cref="RepositoryLocalObject" /> <see cref="T:TcmCoreService.Info.BluePrint" />
		/// </summary>
		/// <value>
		/// <see cref="RepositoryLocalObject" /> <see cref="T:TcmCoreService.Info.BluePrint" />
		/// </value>
		public BluePrint BluePrint
		{
			get
			{
				if (mBluePrint == null && mRepositoryLocalObjectData.BluePrintInfo != null)
					mBluePrint = new BluePrint(Client, mRepositoryLocalObjectData.BluePrintInfo);

				return mBluePrint;
			}
		}

		/// <summary>
		/// Retrieves the <see cref="RepositoryLocalObject" /> creator <see cref="T:TcmCoreService.Security.User" />
		/// </summary>
		/// <value>
		/// <see cref="RepositoryLocalObject" /> creator <see cref="T:TcmCoreService.Security.User" />
		/// </value>
		public User Creator
		{
			get
			{
				if (mCreator == null)
					mCreator = new User(Client, (mRepositoryLocalObjectData.VersionInfo as LimitedVersionInfo).Creator.IdRef);

				return mCreator;
			}
		}

		/// <summary>
		/// Gets the <see cref="RepositoryLocalObject" /> <see cref="T:TcmCoreService.Info.Location" />
		/// </summary>
		/// <value>
		/// <see cref="RepositoryLocalObject" /> <see cref="T:TcmCoreService.Info.Location" />
		/// </value>
		public Location Location
		{
			get
			{
				if (mLocation == null && mRepositoryLocalObjectData.LocationInfo != null)
					mLocation = new Location(Client, mRepositoryLocalObjectData.LocationInfo);

				return mLocation;
			}
		}

		/// <summary>
		/// Gets the <see cref="RepositoryLocalObject" /> <see cref="T:TcmCoreService.Info.LockInfo" />
		/// </summary>
		/// <value>
		/// <see cref="RepositoryLocalObject" /> <see cref="T:TcmCoreService.Info.LockInfo" />
		/// </value>
		public Info.LockInfo LockInfo
		{
			get
			{
				if (mLockInfo == null && mRepositoryLocalObjectData.LockInfo != null)
					mLockInfo = new Info.LockInfo(Client, mRepositoryLocalObjectData.LockInfo);

				return mLockInfo;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="RepositoryLocalObject" /> metadata Xml
		/// </summary>
		/// <value>
		/// <see cref="RepositoryLocalObject" /> metadata Xml
		/// </value>
		public String Metadata
		{
			get
			{
				return mRepositoryLocalObjectData.Metadata;
			}
			set
			{
				mRepositoryLocalObjectData.Metadata = value;
			}
		}

		/// <summary>
		/// Gets or sets the metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// Metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema MetadataSchema
		{
			get
			{
				if (mMetadataSchema == null && mRepositoryLocalObjectData.MetadataSchema.IdRef != TcmUri.NullUri)
					mMetadataSchema = new Schema(Client, mRepositoryLocalObjectData.MetadataSchema.IdRef);

				return mMetadataSchema;
			}
			set
			{
				if (value != null)
					mRepositoryLocalObjectData.MetadataSchema.IdRef = value.Id;
				else
					mRepositoryLocalObjectData.MetadataSchema.IdRef = TcmUri.NullUri;

				mMetadataSchema = null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="RepositoryLocalObject" /> is published in context.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="RepositoryLocalObject" /> is published in context; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsPublishedInContext
		{
			get
			{
				return mRepositoryLocalObjectData.IsPublishedInContext.GetValueOrDefault(false);	
			}
		}
	}
}
