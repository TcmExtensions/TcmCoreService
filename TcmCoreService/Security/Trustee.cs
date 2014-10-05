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
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="trustreeData"><see cref="T:Tridion.ContentManager.CoreService.Client.TrusteeData" /></param>
		protected Trustee(Client client, TrusteeData trustreeData): base(client, trustreeData)
		{
			if (trustreeData == null)
				throw new ArgumentNullException("trustreeData");

			mTrustreeData = trustreeData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Trustee"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Trustee(Client client, TcmUri uri): this(client, client.Read<TrusteeData>(uri))
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
			Reload(Client.Read<TrusteeData>(this.Id));
		}
	}
}
