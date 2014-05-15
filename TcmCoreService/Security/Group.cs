#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Group
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
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Security
{
	/// <summary>
	/// <see cref="Group" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.GroupData" />.
	/// </summary>
	public class Group : Trustee
	{
		private GroupData mGroupData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Group"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="groupData"><see cref="T:Tridion.ContentManager.CoreService.Client.GroupData" /></param>
		protected Group(Session session, GroupData groupData): base(session, groupData)
		{
			if (groupData == null)
				throw new ArgumentNullException("groupData");

			mGroupData = groupData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Group"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Group(Session session, TcmUri uri): this(session, session.Read<GroupData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Group" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.GroupData" />
		/// </summary>
		/// <param name="groupData"><see cref="T:Tridion.ContentManager.CoreService.Client.GroupData" /></param>
		protected void Reload(GroupData groupData)
		{
			if (groupData == null)
				throw new ArgumentNullException("groupData");

			mGroupData = groupData;
			base.Reload(groupData);
		}

		/// <summary>
		/// Reload the <see cref="Group" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<GroupData>(this.Id));
		}
	}
}
