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
		/// Initializes a new instance of the <see cref="User" /> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="userData"><see cref="T:Tridion.ContentManager.CoreService.Client.UserData" /></param>
		protected User(Client client, UserData userData): base(client, userData)
		{
			if (userData == null)
				throw new ArgumentNullException("userData");

			mUserData = userData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal User(Client client, TcmUri uri): this(client, client.Read<UserData>(uri))
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
			Reload(Client.Read<UserData>(this.Id));
		}

        /// <summary>
        /// Gets or sets if the <see cref="User" /> is enabled
        /// </summary>
        /// <value>
        /// <c>true</c> if the <see cref="User" /> is enabled, otherwise <c>false</c>
        /// </value>
        public Boolean IsEnabled
        {
            get
            {
                return mUserData.IsEnabled.GetValueOrDefault(false);
            }
            set
            {
                mUserData.IsEnabled = value;
            }
        }

        /// <summary>
        /// Gets if the <see cref="User" /> is enabled editable
        /// </summary>
        /// <value>
        /// <c>true</c> if the <see cref="User" /> is enabled editable, otherwise <c>false</c>
        /// </value>
        public Boolean IsEnabledEditable
        {
            get
            {
                return mUserData.IsEnabledEditable.GetValueOrDefault(false);
            }
        }

        /// <summary>
        /// Gets if the <see cref="User" /> is privileges editable
        /// </summary>
        /// <value>
        /// <c>true</c> if the <see cref="User" /> is privileges editable, otherwise <c>false</c>
        /// </value>
        public Boolean IsPrivilegesEditable
        {
            get
            {
                return mUserData.IsPrivilegesEditable.GetValueOrDefault(false);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="User" /> language id
        /// </summary>
        /// <value>
        /// <see cref="User" /> language id
        /// </value>
        public int LanguageId
        {
            get
            {
                return mUserData.LanguageId.GetValueOrDefault(0);
            }
            set
            {
                mUserData.LanguageId = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="User" /> locale id
        /// </summary>
        /// <value>
        /// <see cref="User" /> locale id
        /// </value>
        public int LocaleId
        {
            get
            {
                return mUserData.LocaleId.GetValueOrDefault(0);
            }
            set
            {
                mUserData.LocaleId = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="User" /> privileges
        /// </summary>
        /// <value>
        /// <see cref="User" /> privileges
        /// </value>
        public int Privileges
        {
            get
            {
                return mUserData.Privileges.GetValueOrDefault(0);
            }
            set
            {
                mUserData.Privileges = value;
            }
        }
	}
}
