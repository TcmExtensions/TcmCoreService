#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Access Control List
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
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="AccessControlList" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.AccessControlListData" />
	/// </summary>
	public class AccessControlList : InfoBase
	{
		private AccessControlListData mAccessControlListData;
		private IEnumerable<AccessControlEntry> mAccessControlEntries = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessControlList"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="bluePrintInfo"><see cref="T:Tridion.ContentManager.CoreService.Client.AccessControlListData" /></param>
		public AccessControlList(Session session, AccessControlListData accessControlListData): base(session)
		{
			mAccessControlListData = accessControlListData;			
		}

		/// <summary>
		/// Gets the <see cref="T:System.Collections.Generic.IEnumerable{AccessControlEntry}" />
		/// </summary>
		/// <value>
		/// <see cref="T:System.Collections.Generic.IEnumerable{AccessControlEntry}" />
		/// </value>
		public IEnumerable<AccessControlEntry> AccessControlEntries
		{
			get
			{
				if (mAccessControlEntries == null)
					mAccessControlEntries = mAccessControlListData.AccessControlEntries.Select(entry => new AccessControlEntry(Session, entry));

				return mAccessControlEntries;
			}
		}
	}
}
