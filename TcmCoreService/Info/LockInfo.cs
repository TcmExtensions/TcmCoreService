#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: LockInfo
// ---------------------------------------------------------------------------------
//	Date Created	: Ocotober 6, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
using TcmCoreService.Security;


#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TcmCoreService.ContentManagement;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="LockInfo" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.LockInfo" />
	/// </summary>
	public class LockInfo : InfoBase
	{
		private Tridion.ContentManager.CoreService.Client.LockInfo mLockInfo;
		private User mLockUser = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="LockInfo"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="lockInfo"><see cref="T:Tridion.ContentManager.CoreService.Client.LockInfo" /></param>
		public LockInfo(Client client, Tridion.ContentManager.CoreService.Client.LockInfo lockInfo): base(client)
		{
			mLockInfo = lockInfo;
		}

		/// <summary>
		/// Gets the lock date
		/// </summary>
		/// <value>
		/// Lock Date.
		/// </value>
		public DateTime LockDate
		{
			get
			{
				return mLockInfo.LockDate.GetValueOrDefault (DateTime.MinValue);
			}
		}

		/// <summary>
		/// Gets the <see cref="T:Tridion.ContentManager.CoreService.Client.LockType" /> for this <see cref="LockInfo" />.
		/// </summary>
		/// <value>
		///   <see cref="T:Tridion.ContentManager.CoreService.Client.LockType" /> for this <see cref="LockInfo" />.
		/// </value>
		public LockType LockType
		{
			get
			{
				return mLockInfo.LockType.GetValueOrDefault(LockType.None);
			}
		}

		/// <summary>
		/// Retrieves the <see cref="LockInfo" /> <see cref="T:TcmCoreService.Security.User" />
		/// </summary>
		/// <value>
		/// <see cref="LockInfo" /> <see cref="T:TcmCoreService.Security.User" />
		/// </value>
		public User LockUser
		{
			get
			{
				if (mLockUser == null)
					mLockUser = new User(Client, mLockInfo.LockUser.IdRef);

				return mLockUser;
			}
		}
			
	}
}
