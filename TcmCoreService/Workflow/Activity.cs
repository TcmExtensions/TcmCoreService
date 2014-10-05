#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Activity
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

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="Activity" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityData" />.
	/// </summary>
	public abstract class Activity : WorkflowObject
	{
		private ActivityData mActivityData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Activity"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="activityData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityData" /></param>
		protected Activity(Client client, ActivityData activityData): base(client, activityData)
		{
			if (activityData == null)
				throw new ArgumentNullException("activityData");

			mActivityData = activityData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Activity"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Activity(Client client, TcmUri uri): this(client, client.Read<ActivityData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Activity" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityData" />
		/// </summary>
		/// <param name="activityData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityData" /></param>
		protected void Reload(ActivityData activityData)
		{
			if (activityData == null)
				throw new ArgumentNullException("activityData");

			mActivityData = activityData;
			base.Reload(activityData);
		}

		/// <summary>
		/// Reload the <see cref="Activity" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ActivityData>(this.Id));
		}
	}
}
