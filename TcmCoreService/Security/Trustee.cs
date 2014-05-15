#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Trustee
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
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Security
{
	/// <summary>
	/// <see cref="Trustee" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TrusteeData" />.
	/// </summary>
	public abstract class Trustee : SystemWideObject
	{
		private TrusteeData mTrustreeData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Trustee"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="trustreeData"><see cref="T:Tridion.ContentManager.CoreService.Client.TrusteeData" /></param>
		protected Trustee(Session session, TrusteeData trustreeData): base(session, trustreeData)
		{
			if (trustreeData == null)
				throw new ArgumentNullException("trustreeData");

			mTrustreeData = trustreeData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Trustee"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Trustee(Session session, TcmUri uri): this(session, session.Read<TrusteeData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Trustee" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.TrusteeData" />
		/// </summary>
		/// <param name="categoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.TrusteeData" /></param>
		protected void Reload(TrusteeData trustreeData)
		{
			if (trustreeData == null)
				throw new ArgumentNullException("trustreeData");

			mTrustreeData = trustreeData;
			base.Reload(trustreeData);
		}

		/// <summary>
		/// Reload the <see cref="Trustee" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<TrusteeData>(this.Id));
		}
	}
}
