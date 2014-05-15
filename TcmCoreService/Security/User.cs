#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: User
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
	/// <see cref="User" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.UserData" />.
	/// </summary>
	public class User : Trustee
	{
		private UserData mUserData;

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="userData"><see cref="T:Tridion.ContentManager.CoreService.Client.UserData" /></param>
		protected User(Session session, UserData userData): base(session, userData)
		{
			if (userData == null)
				throw new ArgumentNullException("userData");

			mUserData = userData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal User(Session session, TcmUri uri): this(session, session.Read<UserData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="User" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.UserData" />
		/// </summary>
		/// <param name="userData"><see cref="T:Tridion.ContentManager.CoreService.Client.UserData" /></param>
		protected void Reload(UserData userData)
		{
			if (userData == null)
				throw new ArgumentNullException("userData");

			mUserData = userData;
			base.Reload(userData);
		}

		/// <summary>
		/// Reload the <see cref="User" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<UserData>(this.Id));
		}
	}
}
