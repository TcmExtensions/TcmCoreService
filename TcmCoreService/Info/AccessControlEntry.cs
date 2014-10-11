#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Access Control Entry
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
using System.Linq;
using System.Text;
using TcmCoreService.Misc;
using TcmCoreService.Security;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="AccessControlEntry" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.AccessControlEntryData" />
	/// </summary>
	public class AccessControlEntry : InfoBase
	{
		private AccessControlEntryData mAccessControlEntryData;
		private Trustee mTrustee;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessControlEntry"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="bluePrintInfo"><see cref="T:Tridion.ContentManager.CoreService.Client.AccessControlEntryData" /></param>
		public AccessControlEntry(Client client, AccessControlEntryData accessControlEntryData): base(client)
		{
			mAccessControlEntryData = accessControlEntryData;
		}

		/// <summary>
		/// Gets the allowed <see cref="T:Tridion.ContentManager.CoreService.Client.Permissions" />
		/// </summary>
		/// <value>
		/// Allowed <see cref="T:Tridion.ContentManager.CoreService.Client.Permissions" />
		/// </value>
		public Permissions AllowedPermissions
		{
			get
			{
				return mAccessControlEntryData.AllowedPermissions;
			}
		}

		/// <summary>
		/// Gets the allowed <see cref="T:Tridion.ContentManager.CoreService.Client.Rights" />
		/// </summary>
		/// <value>
		/// Allowed <see cref="T:Tridion.ContentManager.CoreService.Client.Rights" />
		/// </value>
		public Rights AllowedRights
		{
			get
			{
				return mAccessControlEntryData.AllowedRights;
			}
		}

		/// <summary>
		/// Gets the denied <see cref="T:Tridion.ContentManager.CoreService.Client.Permissions" />
		/// </summary>
		/// <value>
		/// Denied <see cref="T:Tridion.ContentManager.CoreService.Client.Permissions" />
		/// </value>
		public Permissions DeniedPermissions
		{
			get
			{
				return mAccessControlEntryData.DeniedPermissions;
			}
		}

		/// <summary>
		/// Gets the denied <see cref="T:Tridion.ContentManager.CoreService.Client.Rights" />
		/// </summary>
		/// <value>
		/// Denied <see cref="T:Tridion.ContentManager.CoreService.Client.Rights" />
		/// </value>
		public Rights DeniedRights
		{
			get
			{
				return mAccessControlEntryData.DeniedRights;
			}
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Security.Trustee" /> for this <see cref="AccessControlEntry" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Security.Trustee" /> for this <see cref="AccessControlEntry" />
		/// </value>
		public Trustee Trustee
		{
			get
			{
				if (mTrustee == null)
				{
					TcmUri uri = mAccessControlEntryData.Trustee.IdRef;

					switch (uri.ItemType)
					{
						case (int)ItemType.User:
							mTrustee = new User(Client, uri);
							break;
						case (int)ItemType.Group:
							mTrustee = new Group(Client, uri);
							break;
					}
				}

				return mTrustee;
			}
		}

        /// <summary>
        /// Gets <see cref="T:TcmCoreService.Security.Trustee" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="AccessControlEntry" />
        /// </summary>
        /// <value>
        /// <see cref="T:TcmCoreService.Security.Trustee" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="AccessControlEntry" />
        /// </value>
        public TcmUri TrusteeUri
        {
            get
            {
                return mAccessControlEntryData.Trustee.IdRef;
            }
        }
	}
}
