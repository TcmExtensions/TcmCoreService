#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Activity Instance
// ---------------------------------------------------------------------------------
//	Date Created	: March 22, 2014
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

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="ActivityInstance" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" />.
	/// </summary>
	public class ActivityInstance : Activity
	{
		private ActivityInstanceData mActivityInstanceData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityInstance"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="activityInstanceData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" /></param>
		protected ActivityInstance(Client client, ActivityInstanceData activityInstanceData): base(client, activityInstanceData)
		{
			if (activityInstanceData == null)
				throw new ArgumentNullException("activityInstanceData");

			mActivityInstanceData = activityInstanceData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityInstance"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ActivityInstance(Client client, TcmUri uri): this(client, client.Read<ActivityInstanceData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ActivityInstance" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" />
		/// </summary>
		/// <param name="activityInstanceData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" /></param>
		protected void Reload(ActivityInstanceData activityInstanceData)
		{
			if (activityInstanceData == null)
				throw new ArgumentNullException("activityInstanceData");

			mActivityInstanceData = activityInstanceData;
			base.Reload(activityInstanceData);
		}

		/// <summary>
		/// Reload the <see cref="ActivityInstance" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ActivityInstanceData>(this.Id));
		}
	}
}
