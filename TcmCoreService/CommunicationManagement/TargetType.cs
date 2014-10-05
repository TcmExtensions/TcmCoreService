#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Target Type
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

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="TargetType" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TargetTypeData" />.
	/// </summary>
	public class TargetType : PublishingTarget
	{
		private TargetTypeData mTargetTypeData;

		private AccessControlList mAccessControlList = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetType"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="targetTypeData"><see cref="T:Tridion.ContentManager.CoreService.Client.TargetTypeData" /></param>
		protected TargetType(Client client, TargetTypeData targetTypeData): base(client, targetTypeData)
		{
			if (targetTypeData == null)
				throw new ArgumentNullException("targetTypeData");

			mTargetTypeData = targetTypeData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetType"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal TargetType(Client client, TcmUri uri): this(client, client.Read<TargetTypeData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="TargetType" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.TargetTypeData" />
		/// </summary>
		/// <param name="targetTypeData"><see cref="T:Tridion.ContentManager.CoreService.Client.TargetTypeData" /></param>
		protected void Reload(TargetTypeData targetTypeData)
		{
			if (targetTypeData == null)
				throw new ArgumentNullException("targetTypeData");

			mTargetTypeData = targetTypeData;
			base.Reload(targetTypeData);

			mAccessControlList = null;
		}

		/// <summary>
		/// Reload the <see cref="TargetType" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<TargetTypeData>(this.Id));			
		}

		/// <summary>
		/// Gets the <see cref="T:TcmCoreService.Info.AccessControlList" /> for this <see cref="TargetType" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.AccessControlList" /> for this <see cref="TargetType" />
		/// </value>
		public AccessControlList AccessControlList
		{
			get
			{
				if (mAccessControlList == null)
					mAccessControlList = new AccessControlList(Client, mTargetTypeData.AccessControlList);

				return mAccessControlList;
			}
		}
	}
}
