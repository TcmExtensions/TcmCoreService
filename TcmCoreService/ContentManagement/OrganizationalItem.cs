#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Organizational Item
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
using TcmCoreService.Info;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="OrganizationalItem" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.OrganizationalItemData" />.
	/// </summary>
	public abstract class OrganizationalItem : RepositoryLocalObject
	{
		private OrganizationalItemData mOrganizationalItemData;

		private AccessControlList mAccessControlList = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrganizationalItem" /> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="folderData"><see cref="T:Tridion.ContentManager.CoreService.Client.OrganizationalItemData" /></param>
		protected OrganizationalItem(Client client, OrganizationalItemData organizationalItemData): base(client, organizationalItemData)
		{
			if (organizationalItemData == null)
				throw new ArgumentNullException("organizationalItemData");

			mOrganizationalItemData = organizationalItemData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OrganizationalItem" /> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal OrganizationalItem(Client client, TcmUri uri): this(client, client.Read<OrganizationalItemData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="OrganizationalItem" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.OrganizationalItemData" />
		/// </summary>
		/// <param name="organizationalItemData"><see cref="T:Tridion.ContentManager.CoreService.Client.OrganizationalItemData" /></param>
		protected void Reload(OrganizationalItemData organizationalItemData)
		{
			if (organizationalItemData == null)
				throw new ArgumentNullException("organizationalItemData");

			mOrganizationalItemData = organizationalItemData;
			base.Reload(organizationalItemData);

			mAccessControlList = null;
		}

		/// <summary>
		/// Reload the <see cref="OrganizationalItem" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<OrganizationalItemData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="OrganizationalItem" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<OrganizationalItemData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="OrganizationalItem" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<OrganizationalItemData>(this.Id));
		}

		/// <summary>
		/// Gets the <see cref="T:TcmCoreService.Info.AccessControlList" /> for this <see cref="OrganizationalItem" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.AccessControlList" /> for this <see cref="OrganizationalItem" />
		/// </value>
		public AccessControlList AccessControlList
		{
			get
			{
				if (mAccessControlList == null)
					mAccessControlList = new AccessControlList(Client, mOrganizationalItemData.AccessControlList);

				return mAccessControlList;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="Folder" /> is a permissions inheritance root.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Folder" /> is a permissions inheritance root; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsPermissionsInheritanceRoot
		{
			get
			{
				return mOrganizationalItemData.IsPermissionsInheritanceRoot.GetValueOrDefault(false);
			}
			set
			{
				mOrganizationalItemData.IsPermissionsInheritanceRoot = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="Folder" /> is a root organizational item.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Folder" /> is a root organizational item; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsRootOrganizationalItem
		{
			get
			{
				return mOrganizationalItemData.IsRootOrganizationalItem.GetValueOrDefault(false);
			}
			set
			{
				mOrganizationalItemData.IsRootOrganizationalItem = value;
			}
		}
	}
}
