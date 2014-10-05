#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: VersionedItem
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
using TcmCoreService.Misc;
using TcmCoreService.Security;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="VersionedItem" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.VersionedItemData" />.
	/// </summary>
	public abstract class VersionedItem : RepositoryLocalObject
	{
		private VersionedItemData mVersionedItemData;

		private User mCheckOutUser = null;
		private User mRevisionUser = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="VersionedItem" /> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="versionedItemData"><see cref="T:Tridion.ContentManager.CoreService.Client.VersionedItemData" /></param>
		protected VersionedItem(Client client, VersionedItemData versionedItemData): base(client, versionedItemData)
		{
			if (versionedItemData == null)
				throw new ArgumentNullException("versionedItemData");

			mVersionedItemData = versionedItemData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VersionedItem" /> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal VersionedItem(Session session, TcmUri uri): this(session, session.Read<VersionedItemData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="VersionedItem" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.VersionedItemData" />
		/// </summary>
		/// <param name="versionedItemData"><see cref="T:Tridion.ContentManager.CoreService.Client.VersionedItemData" /></param>
		protected void Reload(VersionedItemData versionedItemData)
		{
			if (versionedItemData == null)
				throw new ArgumentNullException("versionedItemData");

			mVersionedItemData = versionedItemData;
			base.Reload(versionedItemData);

			mCheckOutUser = null;
			mRevisionUser = null;
		}

		/// <summary>
		/// Reload the <see cref="VersionedItem" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<VersionedItemData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="VersionedItem" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<VersionedItemData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="VersionedItem" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<VersionedItemData>(this.Id));
		}

		/// <summary>
		/// Gets the <see cref="T:System.DateTime" /> this <see cref="VersionedItem" /> was checked out
		/// </summary>
		/// <value>
		/// <see cref="T:System.DateTime" /> this <see cref="VersionedItem" /> was checked out or <see cref="P:System.DateTime.MinValue" />
		/// </value>
		public DateTime CheckOutDate
		{
			get
			{
				return (mVersionedItemData.VersionInfo as FullVersionInfo).CheckOutDate.GetValueOrDefault(DateTime.MinValue);
			}
		}

		/// <summary>
		/// Retrieves the <see cref="VersionedItem" /> checkout <see cref="T:TcmCoreService.Security.User" />
		/// </summary>
		/// <value>
		/// <see cref="VersionedItem" /> checkout <see cref="T:TcmCoreService.Security.User" />
		/// </value>
		public User CheckOutUser
		{
			get
			{
				if (mCheckOutUser == null)
					mCheckOutUser = new User(Client, (mVersionedItemData.VersionInfo as FullVersionInfo).CheckOutUser.IdRef);

				return mCheckOutUser;
			}
		}

		/// <summary>
		/// Indicates if this <see cref="VersionedItem" /> is new
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="VersionedItem" /> is new; otherwise <c>false</c>
		/// </value>
		public Boolean IsNew
		{
			get
			{
				return (mVersionedItemData.VersionInfo as FullVersionInfo).IsNew.GetValueOrDefault(false);
			}
		}

		/// <summary>
		/// Returns the current <see cref="T:Tridion.ContentManager.CoreService.Client.LockType" /> for the <see cref="VersionedItem" />
		/// </summary>
		/// <value>
		/// Current <see cref="T:Tridion.ContentManager.CoreService.Client.LockType" /> for the <see cref="VersionedItem" />
		/// </value>
		public LockType LockType
		{
			get
			{
				return (mVersionedItemData.VersionInfo as FullVersionInfo).LockType.GetValueOrDefault(LockType.UnknownByClient);
			}
		}

		/// <summary>
		/// Gets the revision number of this <see cref="VersionedItem" />
		/// </summary>
		/// <value>
		/// Revision number of this <see cref="VersionedItem" />
		/// </value>
		public int Revision
		{
			get
			{
				return (mVersionedItemData.VersionInfo as FullVersionInfo).Revision.GetValueOrDefault(0);
			}
		}

		/// <summary>
		/// Retrieves the <see cref="VersionedItem" /> revisor <see cref="T:TcmCoreService.Security.User" />
		/// </summary>
		/// <value>
		/// <see cref="VersionedItem" /> revisor <see cref="T:TcmCoreService.Security.User" />
		/// </value>
		public User RevisionUser
		{
			get
			{
				if (mRevisionUser == null)
					mRevisionUser = new User(Client, (mVersionedItemData.VersionInfo as FullVersionInfo).Revisor.IdRef);

				return mRevisionUser;
			}
		}

		/// <summary>
		/// Gets the version number of this <see cref="VersionedItem" />
		/// </summary>
		/// <value>
		/// Version number of this <see cref="VersionedItem" />
		/// </value>
		public int Version
		{
			get
			{
				return (mVersionedItemData.VersionInfo as FullVersionInfo).Version.GetValueOrDefault(0);
			}
		}
	}
}
